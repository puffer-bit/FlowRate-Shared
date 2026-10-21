using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization.Metadata;
using Shared.Serialization;

namespace Shared.Validation;

/// <summary>
/// Checks a message against the validation attributes of its own properties and of every nested
/// object and collection item. It walks the source-generated metadata of
/// <see cref="ProtocolJsonContext"/> instead of reflection, so it covers exactly what goes over
/// the wire and stays trim- and NativeAOT-safe.
/// </summary>
public static class MessageValidator
{
    private const int MaxDepth = 8;

    private static readonly ConcurrentDictionary<JsonPropertyInfo, ValidationAttribute[]> AttributeCache = new();

    public static bool TryValidate(object message, out IReadOnlyList<string> errors)
    {
        var found = new List<string>();
        Validate(message, message.GetType().Name, found, 0);
        errors = found;
        return found.Count == 0;
    }

    private static void Validate(object instance, string path, List<string> errors, int depth)
    {
        if (depth > MaxDepth)
        {
            errors.Add($"{path}: nested too deeply.");
            return;
        }

        var typeInfo = ProtocolJsonContext.Default.GetTypeInfo(instance.GetType());
        if (typeInfo is not { Kind: JsonTypeInfoKind.Object })
            return;

        var propertiesValid = true;
        foreach (var property in typeInfo.Properties)
        {
            if (property.Get is not { } getter)
                continue;

            var value = getter(instance);
            if (!ValidateProperty(instance, property, value, path, errors))
                propertiesValid = false;

            if (value != null)
                ValidateNested(value, $"{path}.{property.Name}", errors, depth);
        }

        // Like Validator.TryValidateObject: object-level rules run only on valid properties
        if (propertiesValid && instance is IValidatableObject validatable)
        {
            var context = new ValidationContext(instance, typeInfo.Type.Name, null, null);
            foreach (var result in validatable.Validate(context))
                errors.Add($"{path}: {result.ErrorMessage}");
        }
    }

    private static bool ValidateProperty(object instance, JsonPropertyInfo property, object? value, string path,
        List<string> errors)
    {
        var attributes = AttributeCache.GetOrAdd(property, GetAttributes);
        if (attributes.Length == 0)
            return true;

        var valid = true;
        var context = new ValidationContext(instance, property.Name, null, null) { MemberName = property.Name };
        foreach (var attribute in attributes)
        {
            var result = attribute.GetValidationResult(value, context);
            if (result == ValidationResult.Success)
                continue;

            errors.Add($"{path}: {result?.ErrorMessage}");
            valid = false;
            // Like Validator: a missing required value is not checked any further
            if (attribute is RequiredAttribute)
                break;
        }

        return valid;
    }

    private static void ValidateNested(object value, string path, List<string> errors, int depth)
    {
        switch (value)
        {
            case string:
                return;
            case IDictionary dictionary:
                foreach (DictionaryEntry entry in dictionary)
                {
                    if (entry.Value != null)
                        Validate(entry.Value, $"{path}[{entry.Key}]", errors, depth + 1);
                }
                return;
            case IEnumerable items:
                var index = 0;
                foreach (var item in items)
                {
                    if (item != null)
                        Validate(item, $"{path}[{index}]", errors, depth + 1);
                    index++;
                }
                return;
            default:
                Validate(value, path, errors, depth + 1);
                return;
        }
    }

    // Required goes first so that a missing value yields one error, as with Validator
    private static ValidationAttribute[] GetAttributes(JsonPropertyInfo property) =>
        property.AttributeProvider?
            .GetCustomAttributes(typeof(ValidationAttribute), true)
            .Cast<ValidationAttribute>()
            .OrderBy(attribute => attribute is RequiredAttribute ? 0 : 1)
            .ToArray()
        ?? [];
}

namespace Shared.Models.Media.Transport;

public record TransportData(
    string Host,
    ushort Port,
    DTLSParameters DTLSData,
    ICEParameters ICEData
);

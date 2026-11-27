# Reaction emoji

`emoji.txt` contains the fully qualified, minimally qualified and unqualified emoji sequences
from Unicode Emoji 16.0, including skin tones and flags. Standalone components are excluded.
One line is one accepted reaction. The eight entries in `ChatReactionEmoji.All` remain the
default quick choices; they do not limit the accepted reactions.

Source: https://www.unicode.org/Public/emoji/16.0/emoji-test.txt
These data are covered by the Unicode license in `LICENSE.txt`.

To regenerate, read each non-comment line, skip the `component` status, convert the first
column from hexadecimal code points to Unicode, then sort and deduplicate the sequences.
Do not strip variation selectors or joiners.

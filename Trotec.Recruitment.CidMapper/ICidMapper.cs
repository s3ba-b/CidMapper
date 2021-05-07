namespace Trotec.Recruitment.CidMapper
{
    public interface ICidMapper
    {
        /// <summary>
        /// Initializes this instance with data from Adobe's CMap file
        /// </summary>
        /// <param name="cmapFilePath">Path to the Adobe's CMap file</param>
        /// <returns>True if initialization was successful, false otherwise</returns>
        bool TryInitialize(string cmapFilePath);

        /// <summary>
        /// Maps byte-encoded sequence of CIDs using CID to UTF8 mapping and returns decoded string
        /// </summary>
        /// <param name="cidEncoded">Byte encoded CIDs. Each CID is 2-byte long (Little-Endian byte order)</param>
        /// <param name="decoded">The string decoded from CIDs</param>
        /// <returns>True if decoded successfully, false otherwise</returns>
        bool TryDecode(byte[] cidEncoded, out string decoded);

        /// <summary>
        /// Encodes string as a sequence of bytes representing CIDs
        /// </summary>
        /// <param name="input">String to be encoded</param>
        /// <param name="encoded">Result encoded CIDs. Each CID is 2-byte long (Little-Endian byte order)</param>
        /// <returns>True if encoded successfully, false otherwise.</returns>
        bool TryEncode(string input, out byte[] encoded);
    }
}

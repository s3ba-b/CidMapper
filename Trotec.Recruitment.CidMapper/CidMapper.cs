using System;

namespace Trotec.Recruitment.CidMapper
{
    internal class CidMapper : ICidMapper
    {
        public bool TryInitialize(string cmapFilePath)
        {
            throw new NotImplementedException();
        }

        public bool TryDecode(byte[] cidEncoded, out string decoded)
        {
            throw new NotImplementedException();
        }

        public bool TryEncode(string input, out byte[] encoded)
        {
            throw new NotImplementedException();
        }
    }
}

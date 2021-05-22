using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace Trotec.Recruitment.CidMapper
{
    internal class CidMapper : ICidMapper
    {
        private IEnumerable<CidToUtf8Model> _records;

        public bool TryInitialize(string cmapFilePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t"
            };

            using (var reader = new StreamReader(cmapFilePath))
            using (var csv = new CsvReader(reader, config))
            {
                _records = csv.GetRecords<CidToUtf8Model>().ToList();
            }


            return _records != null;
        }

        public bool TryDecode(byte[] cidEncoded, out string decoded)
        {
            var utf8Encoded = new List<byte>();

            for (var i = 0; i < cidEncoded.Length; i += 2)
            {
                var cidNumber = GetCidNumberFromTwoBytes(cidEncoded[i], cidEncoded[i + 1]);
                var utf8Code = GetUtf8CodeForCidNumber(cidNumber);

                if (utf8Code == null)
                {
                    decoded = null;
                    return false;
                }

                if (utf8Code.Contains(','))
                    // TODO: Can I just choose one of two values, no matter which?
                    utf8Code = GetFirstUtf8ValueForCidNumber(utf8Code);

                for (var j = 0; j < utf8Code.Length; j += 2)
                    utf8Encoded.Add(byte.Parse(utf8Code.Substring(j, 2), NumberStyles.HexNumber));
            }

            try
            {
                decoded = Encoding.UTF8.GetString(utf8Encoded.ToArray());
            }
            catch (Exception)
            {
                decoded = null;
                return false;
            }

            return true;
        }

        private static short GetCidNumberFromTwoBytes(byte firstByte, byte lastByte)
        {
            return (short) ((lastByte << 8) | firstByte);
        }

        private string GetUtf8CodeForCidNumber(short cidNumber)
        {
            return _records.FirstOrDefault(r => r.Id == cidNumber)?.Code;
        }

        private string GetFirstUtf8ValueForCidNumber(string utf8Code)
        {
            return utf8Code.Substring(0, utf8Code.IndexOf(','));
        }

        public bool TryEncode(string input, out byte[] encoded)
        {
            var utf8Encoded = Encoding.UTF8.GetBytes(input);
            var cidEncoded = new List<byte>();

            foreach (var utf8Code in utf8Encoded)
            {
                // TODO: There is no support yet for the rare case where a given CID corresponds to two values in a given character set.
                var cid = _records.FirstOrDefault(r => r.Code.Equals(utf8Code.ToString("X").ToLower()))?.Id;
                if (cid == null)
                {
                    encoded = null;
                    return false;
                }

                cidEncoded.Add((byte) cid);
                cidEncoded.Add(0);
            }

            encoded = cidEncoded.ToArray();
            return true;
        }
    }
}
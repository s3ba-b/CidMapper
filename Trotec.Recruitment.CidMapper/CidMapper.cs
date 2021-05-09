using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
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
                Delimiter = "\t",
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
            
            foreach (var cid in cidEncoded)
            {
                if(cid == 0)
                    continue;
                
                // TODO: There is no support yet for the rare case where a given CID corresponds to two values in a given character set.
                var code = _records.FirstOrDefault(r => r.Id == cid)?.Code;
                
                if (code == null)
                {
                    decoded = null;
                    return false;
                }
                
                utf8Encoded.Add(byte.Parse(code, System.Globalization.NumberStyles.HexNumber));
            }

            decoded = System.Text.Encoding.UTF8.GetString(utf8Encoded.ToArray());
            
            return true;
        }

        public bool TryEncode(string input, out byte[] encoded)
        {
            var utf8Encoded = System.Text.Encoding.UTF8.GetBytes(input);
            var cidEncoded = new List<Byte>();
            
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

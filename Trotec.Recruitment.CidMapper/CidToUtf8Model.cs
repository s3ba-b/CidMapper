using CsvHelper.Configuration.Attributes;

namespace Trotec.Recruitment.CidMapper
{
    public class CidToUtf8Model
    {
        [Name("CID")]
        public int Id { get; set; }
        [Name("UniJIS-UTF8")]
        public string Code { get; set; }
    }
}
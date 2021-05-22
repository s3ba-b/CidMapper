using System.IO;

namespace Trotec.Recruitment.CidMapper.Tests
{
    public abstract class CidMapperTests
    {
        protected ICidMapper Mapper { get; }

        protected CidMapperTests()
        {
            Mapper = new CidMapper();
            Mapper.TryInitialize(Path.GetFullPath(@"../../../TestFiles/cid2code.csv"));
        }
    }
}
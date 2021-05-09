using System.IO;
using System.Linq;
using Xunit;

namespace Trotec.Recruitment.CidMapper.Tests
{
    public class CidMapperTests
    {
        private readonly ICidMapper _mapper;

        public CidMapperTests()
        {
            _mapper = new CidMapper();
            _mapper.TryInitialize(Path.GetFullPath(@"../../../TestFiles/cid2code.csv"));
        }

        [Fact]
        public void CidMapper_EncodesSimpleText()
        {
            var encodedResult = _mapper.TryEncode("Trotec", out var encoded);

            Assert.True(encodedResult);
            Assert.Equal(new byte[] { 0x35, 0, 0x53, 0, 0x50, 0, 0x55, 0, 0x46, 0, 0x44, 0 }, encoded);
        }
        
        [Fact]
        public void CidMapper_EncodesSimpleTextReverse()
        {
            var encodedResult = _mapper.TryEncode("cetorT", out var encoded);

            Assert.True(encodedResult);
            Assert.Equal(new byte[] { 0x44, 0, 0x46, 0, 0x55, 0, 0x50, 0, 0x53, 0, 0x35, 0}, encoded);
            
        }

        [Fact]
        public void CidMapper_DecodesSimpleText()
        {
            var decodedResult = _mapper.TryDecode(new byte[] { 0x35, 0, 0x53, 0, 0x50, 0, 0x55, 0, 0x46, 0, 0x44, 0 },
                out var decoded);

            Assert.True(decodedResult);
            Assert.Equal("Trotec", decoded);
        }
        
        [Fact]
        public void CidMapper_DecodesSimpleTextReverse()
        {
            var decodedResult = _mapper.TryDecode(new byte[] { 0x44, 0, 0x46, 0, 0x55, 0, 0x50, 0, 0x53, 0, 0x35, 0},
                out var decoded);

            Assert.True(decodedResult);
            Assert.Equal("cetorT", decoded);
        }
    }
}

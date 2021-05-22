using Xunit;

namespace Trotec.Recruitment.CidMapper.Tests
{
    public class CidMapperEncodingTests : CidMapperTests
    {
        [Fact]
        public void CidMapper_EncodesSimpleText()
        {
            var encodedResult = Mapper.TryEncode("Trotec", out var encoded);

            Assert.True(encodedResult);
            Assert.Equal(new byte[] {0x35, 0, 0x53, 0, 0x50, 0, 0x55, 0, 0x46, 0, 0x44, 0}, encoded);
        }

        [Fact]
        public void CidMapper_ReturnsFalseWhenEncodingPolishSigns()
        {
            // TODO: This test has wrong assumption. The Polish diacritics are contained in the cid map and should be correctly encoded.
            var encodedResult = Mapper.TryEncode("Śnieżka", out var encoded);

            Assert.False(encodedResult);
        }

        [Fact]
        public void CidMapper_EncodesSimpleTextReverse()
        {
            var encodedResult = Mapper.TryEncode("cetorT", out var encoded);

            Assert.True(encodedResult);
            Assert.Equal(new byte[] {0x44, 0, 0x46, 0, 0x55, 0, 0x50, 0, 0x53, 0, 0x35, 0}, encoded);
        }
    }
}
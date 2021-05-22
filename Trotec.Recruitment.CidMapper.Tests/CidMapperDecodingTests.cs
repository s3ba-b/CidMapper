using Xunit;

namespace Trotec.Recruitment.CidMapper.Tests
{
    public class CidMapperDecodingTests : CidMapperTests
    {
        [Fact]
        public void CidMapper_DecodesWordTrotec()
        {
            var decodedResult = Mapper.TryDecode(new byte[] {0x35, 0, 0x53, 0, 0x50, 0, 0x55, 0, 0x46, 0, 0x44, 0},
                out var decoded);

            Assert.True(decodedResult);
            Assert.Equal("Trotec", decoded);
        }

        [Fact]
        public void CidMapper_DecodesWordTrotecInReverse()
        {
            var decodedResult = Mapper.TryDecode(new byte[] {0x44, 0, 0x46, 0, 0x55, 0, 0x50, 0, 0x53, 0, 0x35, 0},
                out var decoded);

            Assert.True(decodedResult);
            Assert.Equal("cetorT", decoded);
        }

        [Fact]
        public void CidMapper_DecodesLatinCapitalLetterWithHalfwidthKatanaLetter()
        {
            var success = Mapper.TryDecode(new byte[] {0xaf, 0x00, 0x50, 0x01}, out var decoded);
            Assert.True(success);
            Assert.Equal("Ìｪ", decoded); // byte representation of the text in UTF8 is { 195, 140, 239, 189, 170 }
        }

        [Fact]
        public void CidMapper_DecodesHalfwidthKatakanaVoicedSoundMarkWithHalfwidthKatakanaLetterRu()
        {
            var success = Mapper.TryDecode(new byte[] {0x85, 0x01, 0x7f, 0x1}, out var decoded);
            Assert.True(success);
            Assert.Equal("ﾟﾙ",
                decoded); // byte representation of the text in UTF8 is { 0xef, 0xbe, 0x9f, 0xef, 0xbe, 0x99 }
        }

        [Fact]
        public void CidMapper_DecodesTwoDotLeaderAndReferenceMark()
        {
            var success = Mapper.TryDecode(new byte[] {0x9d, 0x02, 0xde, 0x02}, out var decoded);
            Assert.True(success);
            Assert.Equal("‥※",
                decoded); // byte representation of the text in UTF8 is { 0xc2, 0x80, 0xa5, 0xe2, 0x80, 0xbb }
        }
    }
}
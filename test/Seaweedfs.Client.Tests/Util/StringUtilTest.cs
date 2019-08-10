using Seaweedfs.Client.Util;
using Xunit;

namespace Seaweedfs.Client.Tests.Util
{
    public class StringUtilTest
    {
        [Theory]
        [InlineData("3,01637037d6", "3")]
        [InlineData("15868702117", "")]
        [InlineData("2,577,8785632", "2")]
        public void GetVolumeId_Test(string fid, string expected)
        {
            var actual = StringUtil.GetVolumeId(fid);
            Assert.Equal(expected, actual);

        }
    }
}

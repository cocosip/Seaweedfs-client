using Seaweedfs.Client.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Seaweedfs.Client.Tests.Util
{
    public class UrlUtilTest
    {
        [Theory]
        [InlineData("http", "127.0.0.1", 8080, "http://127.0.0.1:8080")]
        [InlineData("https", "192.168.0.129", 9333, "https://192.168.0.129:9333")]
        [InlineData("http", "baidu.com", 80, "http://baidu.com:80")]
        public void ToUrl_Test(string schema, string ipAddress, int port, string expected)
        {
            var actual = UrlUtil.ToUrl(schema, ipAddress, port);
            Assert.Equal(expected, actual);
        }
    }
}

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


        [Theory]
        [InlineData("http://127.0.0.1:8080", "127.0.0.1:8080")]
        [InlineData("http://localhost:80", "localhost")]
        [InlineData("http://192.168.1.117:22122/user?id=3", "192.168.1.117:22122")]
        [InlineData("http://192.168.1.101", "192.168.1.101")]
        public void GetUrlAddress_Test(string url, string expected)
        {
            var actual = UrlUtil.GetUrlAddress(url);
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("http://127.0.0.1:8080", "http://127.0.0.1:8080")]
        [InlineData("http://localhost:81/123/456?name=444#ip=123", "http://localhost:81")]
        [InlineData("http://192.168.1.117:22122/user?id=3", "http://192.168.1.117:22122")]
        public void GetBaseUrl_Test(string url, string expected)
        {
            var actual = UrlUtil.GetBaseUrl(url);
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetQuerys_Test()
        {
            var url1 = "http://192.168.0.196:8080/abc/def?id=1&name=zhangsan&age=4#5546";
            var query1 = UrlUtil.GetQuerys(url1);
            Assert.Equal("1", query1["id"]);
            Assert.Equal("zhangsan", query1["name"]);
            Assert.Equal("4", query1["age"]);
            Assert.Equal(3, query1.Count);

        }
    }
}

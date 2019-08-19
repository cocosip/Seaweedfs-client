using RestSharp;
using Seaweedfs.Client.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Seaweedfs.Client
{
    /// <summary>HttpBuilder扩展
    /// </summary>
    public static class HttpBuilderExtensions
    {
        private static readonly Dictionary<RestSharp.ParameterType, ParameterType> ParameterTypeMappers = new Dictionary<RestSharp.ParameterType, ParameterType>()
        {
            { RestSharp.ParameterType.Cookie,ParameterType.Cookie },
            { RestSharp.ParameterType.GetOrPost,ParameterType.GetOrPost },
            { RestSharp.ParameterType.HttpHeader,ParameterType.HttpHeader },
            { RestSharp.ParameterType.QueryString,ParameterType.QueryString },
            { RestSharp.ParameterType.QueryStringWithoutEncode,ParameterType.QueryStringWithoutEncode },
            { RestSharp.ParameterType.RequestBody,ParameterType.RequestBody },
            { RestSharp.ParameterType.UrlSegment,ParameterType.UrlSegment }
        };

        private static readonly Dictionary<RestSharp.Method, Method> MethodMappers = new Dictionary<RestSharp.Method, Method>()
        {
            { RestSharp.Method.GET,Method.GET },
            { RestSharp.Method.POST,Method.POST },
            { RestSharp.Method.PUT,Method.PUT },
            { RestSharp.Method.DELETE,Method.DELETE },
            { RestSharp.Method.COPY,Method.COPY },
            { RestSharp.Method.HEAD,Method.HEAD },
            { RestSharp.Method.MERGE,Method.MERGE },
            { RestSharp.Method.OPTIONS,Method.OPTIONS },
            { RestSharp.Method.PATCH,Method.PATCH },
        };

        private static readonly Dictionary<RestSharp.DataFormat, DataFormat> DataFormatMappers = new Dictionary<RestSharp.DataFormat, DataFormat>()
        {
            { RestSharp.DataFormat.Json,DataFormat.Json },
            { RestSharp.DataFormat.Xml,DataFormat.Xml },
            { RestSharp.DataFormat.None,DataFormat.None }
        };


        /// <summary>根据HttpBuilder构建RequestRequest请求
        /// </summary>
        public static IRestRequest BuildRequest(this HttpBuilder builder)
        {
            IRestRequest request = new RestRequest
            {
                Method = ParseMethod(builder.Method),
                Resource = builder.Resource.IsNullOrWhiteSpace() ? "" : builder.Resource
            };

            foreach (var parameter in builder.Parameters)
            {
                var p = new RestSharp.Parameter()
                {
                    Name = parameter.Name,
                    Value = parameter.Value,
                    DataFormat = parameter.DataFormat.ParseDataFormat(),
                    ContentType = parameter.ContentType,
                    Type = parameter.Type.ParseParameterType()
                };
                request.AddParameter(p);
            }

            foreach (var file in builder.Files)
            {
                var f = new RestSharp.FileParameter()
                {
                    Name = file.Name,
                    FileName = file.FileName,
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType,
                    Writer = file.Writer
                };
                request.Files.Add(f);
            }

            return request;
        }

        /// <summary>将Method转换为Resharp.Method
        /// </summary>
        private static RestSharp.Method ParseMethod(this Method method)
        {
            var m = MethodMappers.FirstOrDefault(x => x.Value == method);
            return m.Key;
        }

        /// <summary>将DataFormat转换为Resharp.DataFormat
        /// </summary>
        private static RestSharp.DataFormat ParseDataFormat(this DataFormat dataFormat)
        {
            var f = DataFormatMappers.FirstOrDefault(x => x.Value == dataFormat);
            return f.Key;
        }

        /// <summary>将自定义ParameterType转换为RestSharp.ParameterType
        /// </summary>
        private static RestSharp.ParameterType ParseParameterType(this ParameterType parameterType)
        {
            var p = ParameterTypeMappers.FirstOrDefault(x => x.Value == parameterType);
            return p.Key;
        }

        private static ParameterType TransferToParameterType(this RestSharp.ParameterType parameterType)
        {
            var p = ParameterTypeMappers.FirstOrDefault(x => x.Key == parameterType);
            return p.Value;
        }

        private static DataFormat TransferToDataFormat(this RestSharp.DataFormat dataFormat)
        {
            var d = DataFormatMappers.FirstOrDefault(x => x.Key == dataFormat);
            return d.Value;
        }

        /// <summary>从RestSharp Parameter转换参数
        /// </summary>
        public static Parameter TransferToParameter(this RestSharp.Parameter parameter)
        {
            var p = new Parameter()
            {
                Name = parameter.Name,
                Value = parameter.Value,
                ContentType = parameter.ContentType,
                Type = parameter.Type.TransferToParameterType(),
                DataFormat = parameter.DataFormat.TransferToDataFormat()
            };
            return p;
        }


    }
}

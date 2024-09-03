using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUglify.JavaScript.Syntax;
using System.Text;
using Yi.Framework.GoView.Application.Contracts.Dtos;

namespace Yi.Framework.GoView.Application
{
    public class ResultFormattingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResultFormattingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 检查路径是否匹配特定模块或条件
            if (context.Request.Path.StartsWithSegments("/api/app/go-view-system"))
            {
                var originalBodyStream = context.Response.Body;

                using (var newBodyStream = new MemoryStream())
                {
                    context.Response.Body = newBodyStream;

                    try
                    {
                        // 执行下一个中间件
                        await _next(context);

                        //图像不处理
                        if (context.Response.ContentType != null && context.Response.ContentType.Contains("image"))
                        {
                            // 处理图像响应，直接返回
                            newBodyStream.Seek(0, SeekOrigin.Begin);
                            await newBodyStream.CopyToAsync(originalBodyStream);
                            return; // 结束中间件，直接返回图像
                        }

                        // 将 newBodyStream 的位置移动到开始
                        newBodyStream.Seek(0, SeekOrigin.Begin);

                        var responseBody = new StreamReader(newBodyStream).ReadToEnd();
                        newBodyStream.Seek(0, SeekOrigin.Begin);

                        // 检查响应状态码并处理
                        var result = new GoViewResult<object>
                        {
                            Code = StatusCodes.Status200OK,
                            Msg = GetStatusCodeMessage(context.Response.StatusCode),
                            Data = JsonConvert.DeserializeObject(responseBody),
                            Count = null
                        };

                        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                        {
                            result = new GoViewResult<object>
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Msg = GetStatusCodeMessage(StatusCodes.Status403Forbidden),
                                Data = "",
                                Count = null
                            };
                        }

                        context.Response.StatusCode = StatusCodes.Status200OK;

                        // Serialize JSON with custom settings
                        var jsonSettings = new JsonSerializerSettings
                        {
                            Formatting = Formatting.None,
                            ContractResolver = new LowercasePropertyNamesContractResolver()
                        };

                        string resultJson;
                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonTextWriter(stringWriter))
                        {
                            jsonWriter.Formatting = jsonSettings.Formatting;

                            var serializer = JsonSerializer.Create(jsonSettings);
                            serializer.Serialize(jsonWriter, result);
                            resultJson = stringWriter.ToString();
                        }

                        // 设置正确的 Content-Length
                        context.Response.ContentLength = Encoding.UTF8.GetByteCount(resultJson);

                        // 将结果写回原始流
                        await originalBodyStream.WriteAsync(Encoding.UTF8.GetBytes(resultJson));

                    }
                    catch (Exception)
                    {
                        var errorResult = new GoViewResult<object>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Msg = GetStatusCodeMessage(StatusCodes.Status400BadRequest),
                            Data = "",
                            Count = null
                        };

                        context.Response.StatusCode = StatusCodes.Status200OK;

                        var jsonSettings = new JsonSerializerSettings
                        {
                            Formatting = Formatting.None,
                            ContractResolver = new LowercasePropertyNamesContractResolver()
                        };

                        string errorJson;
                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonTextWriter(stringWriter))
                        {
                            jsonWriter.Formatting = jsonSettings.Formatting;

                            var serializer = JsonSerializer.Create(jsonSettings);
                            serializer.Serialize(jsonWriter, errorResult);
                            errorJson = stringWriter.ToString();
                        }

                        // 设置正确的 Content-Length
                        context.Response.ContentLength = Encoding.UTF8.GetByteCount(errorJson);

                        // 将错误结果写回原始流
                        await originalBodyStream.WriteAsync(Encoding.UTF8.GetBytes(errorJson));
                    }
                }
            }
            else
            {
                // 如果路径不匹配，直接调用下一个中间件
                await _next(context);
            }
        }

        private string GetStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "Bad Request",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status403Forbidden => "Forbidden",
                StatusCodes.Status404NotFound => "Not Found",
                StatusCodes.Status500InternalServerError => "Internal Server Error",
                _ => "Unknown Error"
            };
        }
    }

    public class LowercasePropertyNamesContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.PropertyName = property.PropertyName.ToLowerInvariant();
            return property;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Yi.Framework.Module.WeChat
{
    public class WeChatPayHttpHandler : DelegatingHandler
    {
        private readonly string merchantId;
        private readonly string serialNo;

        public WeChatPayHttpHandler(string merchantId, string merchantSerialNo)
        {
            InnerHandler = new HttpClientHandler();

            this.merchantId = merchantId;
            this.serialNo = merchantSerialNo;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var auth = await BuildAuthAsync(request);
            string value = $"WECHATPAY2-SHA256-RSA2048 {auth}";
            request.Headers.Add("Authorization", value);

            return await base.SendAsync(request, cancellationToken);
        }

        protected async  Task<string> BuildAuthAsync(HttpRequestMessage request)
        {
            string method = request.Method.ToString();
            string body = "";
            if (method == "POST" || method == "PUT" || method == "PATCH")
            {
                var content = request.Content;
                body = await content.ReadAsStringAsync();
            }

            string uri = request.RequestUri.PathAndQuery;
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            string nonce = Path.GetRandomFileName();

            string message = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{body}\n";
            string signature = Sign(message);
            return $"mchid=\"{merchantId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{serialNo}\",signature=\"{signature}\"";
        }

        protected string Sign(string message)
        {
            // NOTE： 私钥不包括私钥文件起始的-----BEGIN PRIVATE KEY-----
            //        亦不包括结尾的-----END PRIVATE KEY-----
            string privateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC/7sZqymy7XbgW\r\noZWzgS7Ok4LqPDT05kVrnqSTOeckWNSz8x2o/VHB7UXvQIqOyroNPgOkqXB6Bq59\r\nSWF422uCcwWItZMxdELQChEU9bnd3ia7U4i8gwMGFJOGn2J75CCLa6+IhDwFoC3G\r\nvm7aWH11PSuJd8jLYS4azNsKwJwfAFHbCKqhlHir2qCnFZXabSGnm6obmIUMjCxy\r\ncfnhrPXY8eg/QomyI12wlyO+ZpjWogibI0rleu/zey7z+XhGMcl8qBHjtguN3Sgv\r\nGMGLnm8osjGhY/da1V/IXTsw6/CyG5IP3e0Unwzo9PJwx5nq/zmvQC+uRr1AgcBZ\r\nilKkPwsNAgMBAAECggEAEMltiUGTKQAVbcVMNpsB4Qd918bUSucpAzSo6EeUM9Wh\r\nJOwKmBEv6Wo7R6W5eKu6ghX+c5RuRf33nPWiFNP8Hzi4LzDSYuzsOw3mWJL1YrZf\r\nZNr1hqdeyFVcYdXm4zccsZUFkUcfiM5tsohNYcODlZF4EVnssf0Z7zYjolkeToet\r\nzgU6mQvzQ6zbGeYVOn0A+/+LiCJeZRW+bifQpK4m8JqSD9CSS44VJkWe63r8176o\r\nYXmc5QjxVyUrnJPGgqDZFgc93BGVycJzEDG43QUaCCPdXgXru3Ywsz7pzgVaJHEs\r\nMD4dy9GpZnSKKqD7aAq9G/5/LXCJz5erWd+f20Kn8QKBgQDpLKqtrNxFwIjE8U1+\r\ndQwGlmL3/EjqXNCeQScB6dSAEn8ueWd7JrOyjst2rIx0AD07764K5Uc8xrRBDQg5\r\nYnug5bmGlefIp5CkCtB01Th0wMfgAOTVEtXaaq+6uBfPcSBBEDghTPD5c6oZvPj7\r\nA6ig39fcoUKr9V84VISsYewOAwKBgQDSuJfXhGp5UpJnd49SJSVIZg4FNQVaZfIQ\r\nbhxlRwokAsNaziJryta3Q51afGswr0rj40kEwCogJSlrO9OhktBA74aTuyl3c2s0\r\n4iXzzYjbRBnzN6nAgAHWDMStznAUqyDNzyGvd6uy++erisGq08Ugo0yr10GOKU39\r\nUj4z+a59rwKBgQCiWc5Q9J2+F0tjTNv3I4oHACjSn58pRwyeU6DUTTn/HmHdOvyZ\r\nG55cwd3auFNm5U+9bqmQvok2QOf6rxc91Vtc8PaXRcLHzBwCi+EOp/MSH7RLPHQY\r\nA3BRDp1idZFmh0683o0maosSNL2IBDKbm7WKpbCH1uQ0FLmC4B4sZFXWfwKBgCDx\r\npxuUoijRlf4DHS8Ui52kBvEddvbJFW0oKdxTnOxAWlZp/8umbKc+NO2eogt8fFLg\r\nh9vsRym7ZZxUQCP0lgZw7DNQgY0hSFN+P7y8F3dgUEZMH4fu+1qBqIYbzj4M+xXy\r\nGiwao4daBsA0805HyXvuy9/ZyW/2WTEPmJX7pSIVAoGBAJpZSxjgVnKPDNPNOh0H\r\ndns7IcT8iaFUaoYu/1edWCxW9RWQ9Ml66qFETl7NpMMmilNVmOBUW5hseQ38IgbZ\r\nyt+4DRm9knYCHZCDI46pNugLigQwAkDWsKTxvbK/HwmfQY91cX3tnH2VhPIVsIwf\r\nv5mx7h9s2iAX0f1ThUw3jVfN";
            byte[] keyData = Convert.FromBase64String(privateKey);

            var rsa = RSA.Create();
            //适用该方法的版本https://learn.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.asymmetricalgorithm.importpkcs8privatekey?view=net-7.0
            rsa.ImportPkcs8PrivateKey(keyData, out _);
            rsa.ImportPkcs8PrivateKey(keyData, out _);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }
    }
}

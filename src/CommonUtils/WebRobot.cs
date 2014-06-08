using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cfvbaibai.CommonUtils
{
    public class WebRobot
    {
        public string Accept { get; set; }
        public string AcceptEncoding { get; set; }
        public string AcceptLanguage { get; set; }
        public string UserAgent { get; set; }
        public string Referer { get; set; }
        public string Host { get; set; }
        public string Origin { get; set; }
        public bool IsAjax { get; set; }

        public Encoding Encoding { get; set; }

        private string cookie;

        public WebRobot()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            Initialize();
        }

        private void Initialize()
        {
            this.Encoding = Encoding.UTF8;
            this.Accept = "*/*";
            this.AcceptEncoding = "gzip,deflate,sdch";
            this.AcceptLanguage = "zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4";
            this.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
            this.Referer = null;
            this.Host = null;
            this.Origin = null;
            this.IsAjax = false;
        }

        private WebClient CreateClient(NameValueCollection extraHeaders = null)
        {
            WebClient client = new WebClient();
            client.Encoding = this.Encoding;
            Action <string, string> headerSetter = (header, value) =>
                {
                    if (value != null)
                    {
                        client.Headers.Set(header, value);
                    }
                };
            headerSetter("Accept", this.Accept);
            headerSetter("AcceptEncoding", this.AcceptEncoding);
            headerSetter("AcceptLanguage", this.AcceptLanguage);
            headerSetter("UserAgent", this.UserAgent);
            headerSetter("Referer", this.Referer);
            headerSetter("Host", this.Host);
            headerSetter("Origin", this.Origin);
            if (this.IsAjax)
            {
                headerSetter("X-Requested-With", "XMLHttpRequest");
            }
            if (extraHeaders != null)
            {
                for (int i = 0; i < extraHeaders.Count; ++i)
                {
                    headerSetter(extraHeaders.Keys[i], extraHeaders[i]);
                }
            }
            return client;
        }

        public string Get(string url)
        {
            using (var client = CreateClient())
            {
                return client.DownloadString(url);
            }
        }

        public string Post(string url, string contentType, string data, NameValueCollection extraHeaders = null)
        {
            string result = "";
            using (var client = CreateClient(extraHeaders))
            {
                client.Headers.Set(HttpRequestHeader.ContentType, contentType);
                if (!string.IsNullOrEmpty(cookie))
                {
                    client.Headers.Add(HttpRequestHeader.Cookie, cookie);
                }

                result = client.UploadString(url, data);
                if (string.IsNullOrEmpty(cookie))
                {
                    var setCookie = client.ResponseHeaders["Set-Cookie"];
                    if (!string.IsNullOrEmpty(setCookie))
                    {
                        cookie = setCookie.ToString();
                        cookie = GetCookie(cookie);
                    }
                }

                return result;
            }
        }

        private string GetCookie(string CookieStr)
        {
            string result = "";
            string[] myArray = CookieStr.Split(',');

            if (myArray.Count() > 0)
            {
                result = "Cookie: ";
                foreach (var str in myArray)
                {
                    string[] CookieArray = str.Split(';');
                    result += CookieArray[0].Trim();
                    result += "; ";
                }

                result = result.Substring(0, result.Length - 2);
            }

            return result;
        } 
    }
}

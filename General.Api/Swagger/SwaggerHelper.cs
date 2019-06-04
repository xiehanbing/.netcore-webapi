using System;
using System.IO;
using System.Net.Http;

namespace General.Api
{
    public class SwaggerHelper
    {
        public static void DownSwaggerJson(string url, string version)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                url += "/swagger/" + version + "/swagger.json";
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                //同样的，在此处可通过 ReadAsStreamAsync（）方法，以流的方式下载指定文件（或者将网络流通过 MemoryStream 转换为内存流，再转换为byte进行存储或保存），再通过 Image 对象从流中读取图片文件。
                string retString = response.Content.ReadAsStringAsync().Result;
                var path = AppContext.BaseDirectory + $@"/wwwroot/swagger/{version}";
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                var fileUrl = path + @"/swagger.json";
                File.WriteAllText(fileUrl, retString);
            }

        }
    }
}
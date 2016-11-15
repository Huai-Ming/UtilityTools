using System.IO;
using System.Net;
using System.Text;

namespace HttpUtility
{
    public class HttpRequestHelper
    {
        /// <summary>
        /// 以Get方式 调用WebAPI
        /// </summary>
        /// <param name="url">调用API的URL</param>
        /// <param name="strs">Get请求的参数</param>
        /// <returns>Json串</returns>
        public static string GetValueFromWebApiByGet(string url, string strs = null)
        {
            string requesUrl = string.IsNullOrEmpty(strs) ? url : url + strs;
            // Create the web request
            HttpWebRequest request = WebRequest.Create(requesUrl) as HttpWebRequest;
            if (request != null)
            {
                request.Timeout = 15000;
                // Get response
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream
                    if (response != null)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        return reader.ReadToEnd();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 以Post方式 调用WebAPI
        /// </summary>
        /// <param name="url">调用API的URL</param>
        /// <param name="data">Post请求的参数</param>
        /// <returns>Json串</returns>
        public static string GetValueFromWebApiByPost(string url, string data = null)
        {
            //HttpWebRequest request = WebRequest.Create(Url) as HttpWebRequest;
            string requesUrl = url;
            HttpWebRequest request = WebRequest.Create(requesUrl) as HttpWebRequest;
            // Set type to POST
            if (request != null)
            {
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = 15000;

                // Create a byte array of the data we want to send
                if (data != null)
                {
                    byte[] byteData = Encoding.UTF8.GetBytes(data);

                    // Set the content length in the request headers
                    request.ContentLength = byteData.Length;

                    // Write data
                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                    }
                }

                // Get response
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream
                    if (response != null)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());

                        // output
                        return reader.ReadToEnd();
                    }
                }
            }

            return string.Empty;
        }
    }
}

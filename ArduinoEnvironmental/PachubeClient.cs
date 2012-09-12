using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;

using System.Diagnostics;



namespace com.whatididwas.PachubeClient
{
    public static class PachubeClient
    {
        const string baseUri = "http://api.pachube.com/v2/feeds/";

        public static void Send(string apiKey, string feedId,
            string sample)
        {
            Debug.Print("time: " + DateTime.Now);
            // Debug.Print("memory available: " + Debug.GC(true));
            try
            {
                var request = CreateRequest(apiKey, feedId, sample);
                {
                    request.Timeout = 5000;     // 5 seconds
                    // send request and receive response
                    using (var response =
                        (HttpWebResponse)request.GetResponse())
                    {
                        HandleResponse(response);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }
        static HttpWebRequest CreateRequest(string apiKey, string feedId,
            string sample)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(sample);

            var request = (HttpWebRequest)WebRequest.Create
                (baseUri + feedId + ".csv");

            // request line
            request.Method = "PUT";

            // request headers
            request.ContentLength = buffer.Length;
            request.ContentType = "text/csv";
            request.Headers.Add("X-PachubeApiKey", apiKey);

            // request body
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            return request;
        }

        public static void HandleResponse(HttpWebResponse response)
        {
            Debug.Print("Status code: " + response.StatusCode);
        }

        //static void Main(string[] args)
        //{
        //    Random rand = new Random();
        //    int r = rand.Next();
        //    PachubeClient.Send("4rMcCpxQD_lCJtksfL89f6HwmZiSAKw0YlpIKzFoODJnMD0g", "58314", "1," + rand.Next().ToString());

        //}
    }
}

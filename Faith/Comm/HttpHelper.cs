using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Common
{
   public  class HttpHelper
    {
        public static string HttpPost(string Url, string Data, Encoding Encode)
        {
            string ret = string.Empty;
            try
            {
                byte[] ByteArray = Encode.GetBytes(Data); //转字节组
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(new Uri(Url));
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";

                Request.ContentLength = ByteArray.Length;
                Stream _Stream = Request.GetRequestStream();
                _Stream.Write(ByteArray, 0, ByteArray.Length);//写入参数
                _Stream.Close();
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                Response.Close();
                _Stream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        public static string HttpPost(string Url, string Data)
        {
            return HttpPost(Url, Data, Encoding.UTF8);
        }

        public static string HttpGet(string Url, Encoding Encode)
        {
            try
            {
                
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Url);
                Request.Method = "post";
                Request.Accept = "text/html, application/xhtml+xml, */*";
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                WebResponse Response = Request.GetResponse();
                Stream _Stream = Response.GetResponseStream();
                using (StreamReader Reader = new StreamReader(_Stream, Encode))
                {
                    return Reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string HttpGet(string Url)
        {
            return HttpGet(Url, Encoding.UTF8);
        }
    }
}

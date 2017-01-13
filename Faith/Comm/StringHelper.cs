using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
   public class StringHelper
    {
        public enum StringMidType
        {
            带标记=1,
            去标记=2
        }
       /// <summary>
        /// 截取字符串,返回第一个
       /// </summary>
       /// <param name="source">源字符串</param>
       /// <param name="sTag">开头标识</param>
       /// <param name="eTag">结尾标识</param>
       /// <param name="midType">截取模式. 1不包含标识,2包含标识. 默认1模式</param>
       /// <returns></returns>
       public static string MidString(string source,string sTag,string eTag,int midType=1)
       {
           string ret = string.Empty;
           int s, e;
           if (source.IndexOf(sTag) == -1 || source.IndexOf(eTag) == -1)
           {
               return ret;
           }
           switch (midType)
           {
               case 1:
                   s = source.IndexOf(sTag) + sTag.Length;
                   e = source.IndexOf(eTag, s);
                   ret = source.Substring(s, e - s);
                   break;
               case 2:
                   s = source.IndexOf(sTag);
                   e = source.IndexOf(eTag, s) + eTag.Length;
                   ret = source.Substring(s, e - s);
                   break;
               default:
                   s = source.IndexOf(sTag) + sTag.Length;
                   e = source.IndexOf(eTag, s);
                   ret = source.Substring(s, e - s);
                   break;
           }
           return ret;
       }
       /// <summary>
       /// 截取字符串,返回第一个
       /// </summary>
       /// <param name="source">源字符串</param>
       /// <param name="regexStr">正则表达式</param>
       /// <returns></returns>
       public static string MidString(string source, string regexStr)
       {
           string ret=string.Empty;
           Regex regex = new Regex(regexStr);
           if (regex.IsMatch(source))
           {
               ret = regex.Match(source).Value;
           }
           return ret;
       }
       /// <summary>
       /// 截取字符串，返回集合
       /// </summary>
       /// <param name="source">源字符串</param>
       /// <param name="sTag">开头标识</param>
       /// <param name="eTag">结尾标识</param>
       /// <param name="midType">截取模式. 1不包含标识,2包含标识. 默认1模式</param>
       /// <returns></returns>
       public static List<string> MidStringToList(string source, string sTag, string eTag, int midType = 1)
       {
           List<string> ret = new List<string>();
           int s, e;
           string _source=source;
           while (true)
           {
               if (_source.IndexOf(sTag) == -1 || _source.IndexOf(eTag) == -1)
                   break;
               switch (midType)
               {
                   case 1:
                       s = _source.IndexOf(sTag) + sTag.Length;
                       e = _source.IndexOf(eTag, s);
                       break;
                   case 2:
                       s = _source.IndexOf(sTag);
                       e = _source.IndexOf(eTag, s) + eTag.Length;
                       break;
                   default:
                       s = _source.IndexOf(sTag) + sTag.Length;
                       e = _source.IndexOf(eTag, s);
                       break;
               }
               
               ret.Add(_source.Substring(s, e-s));
               _source = _source.Remove(0, e-s);
           }
           return ret;
       }
       /// <summary>
       /// 截取字符串，返回集合
       /// </summary>
       /// <param name="source">源字符串</param>
       /// <param name="regexStr">正则表达式</param>
       /// <returns></returns>
       public static List<string> MidStringToList(string source, string regexStr)
       {
           List<string> ret =new List<string>();
           Regex regex = new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Multiline);
           if (regex.IsMatch(source))
           {
               MatchCollection matchCollection = regex.Matches(source);
               foreach (Match match in matchCollection)
               {
                   ret.Add(match.Value);
               }
           }
           return ret;
       }

        public static string GetHtmlElement(string source, string sTag, int index, StringMidType smt= StringMidType.去标记)
        {
            var reg = Regex.Matches(source, "<" + sTag);
            if (reg.Count>0)
            {
                int s = 0, e = 0;
                var regE = Regex.Matches(source, "</" + sTag + ">");

                if (smt== StringMidType.带标记)
                {
                    //开始位置
                    s = reg[index].Index;

                    var _html = source.Substring(s, source.Length - s);

                    //结束位置
                    e = regE[regE.Count - index].Index + ("</" + sTag + ">").Length;
                }
                else if (smt == StringMidType.去标记)
                {

                }

                switch (smt)
                {
                    case StringMidType.带标记:
                       
                        break;
                    case StringMidType.去标记:
                        //开始位置
                        s = reg[index].Index;
                        s = source.IndexOf(">", s) + 1;
                        //结束位置
                        e = regE[regE.Count-index].Index;
                        break;
                    default:
                        break;
                }

                //长度
                var len = e - s;
                var ret = source.Substring(s, len);
                return ret;
            }
            return "";
        }
        public static List<string> GetHtmlElementList(string source, string sTag, int index, StringMidType smt = StringMidType.去标记)
        {
            var _source = source;
            var b = true;
            List<string> ret = new List<string>();
            while (b)
            {
                var html = GetHtmlElement(_source, sTag, index, smt);
                if (string.IsNullOrEmpty(html))
                {
                    b = false;
                    continue;
                }
                ret.Add(html);
                _source = _source.Remove(0, html.Length);
            }
            return ret;
        }
    }
}

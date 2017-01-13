using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Faith.Models;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace Faith.Core
{
    /// <summary>
    /// 抓数据核心
    /// </summary>
    public class BookClass
    {
        /// <summary>
        /// 笔趣阁5200
        /// </summary>
        string host = "http://www.biquge.com";
        /// <summary>
        /// 笔趣阁5200百度站内搜索
        /// </summary>
        string BaiduSearchHost = "http://zhannei.baidu.com/cse/search?s=287293036948159515&q={0}";
        public void List(string bookName)
        {
            try
            {
                var html = HttpHelper.HttpGet(string.Format(BaiduSearchHost, bookName));
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var docNode = doc.DocumentNode;
                //result-list
                var nodes = docNode.CssSelect(".result-list");
                foreach (var htmlNode in nodes)
                {
                    Console.WriteLine(htmlNode.InnerText);
                }
                var bookList = StringHelper.GetHtmlElementList(html, "div", 0, StringHelper.StringMidType.带标记);
               // var bookHtmlList = StringHelper.MidStringToList(html,,);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
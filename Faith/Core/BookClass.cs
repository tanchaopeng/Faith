using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

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
        string BaiduSearchHost = "http://zhannei.baidu.com/cse/search?s=287293036948159515&q=";
        public void List(string bookName)
        {
            try
            {
                var html = HttpHelper.HttpGet(string.Format(BaiduSearchHost, bookName));
                html =  StringHelper.GetHtmlElement(html,"div",11);
                var bookList = StringHelper.GetHtmlElementList(html, "div", 0, StringHelper.StringMidType.带标记);
               // var bookHtmlList = StringHelper.MidStringToList(html,,);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
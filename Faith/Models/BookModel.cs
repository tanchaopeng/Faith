using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Faith.Models
{
    public class BookModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string des { get; set; }
        public string collectTime { get; set; }
        public string updataTime { get; set; }
        public string author { get; set; }
        public string newChapter { get; set; }
        public string newChapterLink { get; set; }
    }
}
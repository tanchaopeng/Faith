using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary> 
    /// JSON帮助类 
    /// </summary> 
    public static class JSONHelper
    {
        /// <summary>     
        /// 对象转JSON     
        /// </summary>     
        /// <param name="obj">对象</param>     
        /// < return s>JSON格式的字符串</ return s>     
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string p = @"\\/Date\((\d+)\)\\/";
                var json = jss.Serialize(obj);
                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                Regex reg = new Regex(p);
                json = reg.Replace(json, matchEvaluator);
                return json;
            }
            catch (Exception ex)
            {
                throw new Exception("SONHelper.ObjectToJSON(): " + ex.Message);
            }
        }
        /// <summary>     
        /// 数据表转键值对集合 
        /// 把DataTable转成 List集合, 存每一行     
        /// 集合中放的是键值对字典,存每一列     
        /// </summary>    
        /// <param name="dt">数据表</param>     
        /// < return s>哈希表数组</ return s>     
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                } list.Add(dic);
            }
            return list;
        }
        /// <summary>  
        ///  数据集转键值对数组字典    
        ///  </summary>     
        /// <param name="dataSet">数据集</param>     
        /// < return s>键值对数组字典</ return s>     
        public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
        {
            Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();
            foreach (DataTable dt in ds.Tables)
                result.Add(dt.TableName, DataTableToList(dt));
            return result;
        }
        /// <summary>     
        /// 数据表转JSON     
        /// </summary>     
        /// <param name="dataTable">数据表</param>     
        /// < return s>JSON字符串</ return s>     
        public static string DataTableToJSON(DataTable dt)
        {
            return ObjectToJSON(DataTableToList(dt));
        }
        /// <summary>     
        /// JSON文本转对象,泛型方法     
        /// </summary>     
        /// <typeparam name="T">类型</typeparam>     
        /// <param name="jsonText">JSON文本</param>     
        /// < return s>指定类型的对象</ return s>     
        public static T JSONToObject<T>(string jsonText)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }
        /// <summary>     
        /// 将JSON文本转换为数据表数据    
        /// </summary>     
        /// <param name="jsonText">JSON文本</param>     
        /// < return s>数据表字典</ return s>     
        public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
        }
        /// <summary>     
        /// 将JSON文本转换成数据行     
        /// </summary>     
        /// <param name="jsonText">JSON文本</param>     
        /// < return s>数据行的字典</ return s>     
        public static Dictionary<string, object> DataRowFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, object>>(jsonText);
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }
    }
}
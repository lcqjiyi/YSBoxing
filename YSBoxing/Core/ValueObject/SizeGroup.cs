using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace YSBoxing.Core.ValueObject
{
    public class SizeGroup
    {
        public Dictionary<string, int> sizeCode = new Dictionary<string, int>{
            { "xxs",1},{ "xs",2},{ "s",3},{ "m",4},{ "l",5},{ "xl",6},{ "xxl",7},{ "xxxl",8},{ "xxxxl",9},{ "xxxxxl",10}
        };
        private List<string> sortingSize; 

        public int sizeCount { get; set; }

        public SizeGroup(List<OrderItemQty> OrderItemQty)
        {

            var size = OrderItemQty.GroupBy(o => o.Size).Select(o => o.Key).ToList();
            var sortSize = GetSortsize(size);
            sortingSize = sortSize.OrderBy(o => o.Key).Select(o => o.Value).ToList();
            sizeCount = sortingSize.Count();
        }
        /// <summary>
        /// 得到尺码位置
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public int SizeIndex(string size)
        {
            return sortingSize.IndexOf(size);
        }

        public string GetSize (int index){

            return sortingSize[index];
        }

        /// <summary>
        /// 生成排序列表
        /// </summary>
        /// <param name="sortSize"></param>
        /// <param name="size"></param>
        private Dictionary<int, string> GetSortsize( List<string> size)
        {
            var sortSize = new Dictionary<int, string>();
            foreach (var item in size)
            {
                int codeKey;
                if (sizeCode.ContainsKey(item.ToLower()))
                {
                    sizeCode.TryGetValue(item.ToLower(), out codeKey);
                }
                else
                {
                    string result = System.Text.RegularExpressions.Regex.Replace(item, @"[^0-9]+", "");//取字符串中的数值
                    int.TryParse(result, out codeKey);
                }
                if (codeKey <= 0) throw new FormatException("码数的排序无法确定。");
                sortSize.Add(codeKey, item);
            }
            return sortSize;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace YSBoxing.Core.ValueObject
{
    public class QtyRow
    {
        public QtyRow(string style, string color,int qtyCount)
        {
            Style = style;
            Color = color;
            QtyCount = qtyCount;
            Qtys = new List<int>( new int[qtyCount]);
        }

        public string Style { get; set; }

        public string Color { get; set; }

        private List<int> Qtys { get; set; }

        public int QtyCount { get; set; }

        public static List<QtyRow> GetQtyRow(List<OrderItemQty> OrderItemQty,SizeGroup sizeGroup) {
            List<QtyRow> qtyRows = new List<QtyRow>();
            var Group = OrderItemQty.GroupBy(o => new { o.Style, o.Color }).ToList();
            foreach (var item in Group)
            {
                var tempQtyRow = new QtyRow(item.Key.Style, item.Key.Color,sizeGroup.sizeCount);
                foreach (var itemQty in item)
                {
                    tempQtyRow.Qtys[sizeGroup.SizeIndex(itemQty.Size)] += itemQty.Qty;//加到对应的值上
                }
                qtyRows.Add(tempQtyRow);
               
            }
            return qtyRows;
        }

        public static List<QtyRow> GetQtyRow(List<OrderItemBoxingQty> OrderItemQty, SizeGroup sizeGroup)
        {
            List<QtyRow> qtyRows = new List<QtyRow>();
            var Group = OrderItemQty.GroupBy(o => new { o.Style, o.Color }).ToList();
            foreach (var item in Group)
            {
                var tempQtyRow = new QtyRow(item.Key.Style, item.Key.Color, sizeGroup.sizeCount);
                foreach (var itemQty in item)
                {
                    tempQtyRow.Qtys[sizeGroup.SizeIndex(itemQty.Size)] += itemQty.Qty;//加到对应的值上
                }
                qtyRows.Add(tempQtyRow);

            }
            return qtyRows;
        }

        public int GetQty(int index) {

            return Qtys[index];
        }
    }
}

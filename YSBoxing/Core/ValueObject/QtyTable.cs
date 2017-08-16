using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSBoxing.Core.ValueObject;
namespace YSBoxing.Core.ValueObject
{
    public class QtyTable
    {
        public SizeGroup sizeGroup { get; set; }

        public List<QtyRow> qtyRows { get; set; }

        public QtyTable(List<OrderItemQty> OrderItemQty) {
            sizeGroup = new SizeGroup(OrderItemQty);
            qtyRows = QtyRow.GetQtyRow(OrderItemQty ,sizeGroup);
        }


    }
}

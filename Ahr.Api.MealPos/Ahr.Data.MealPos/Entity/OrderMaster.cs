using System;
using System.Collections.Generic;

namespace Ahr.Data.MealPos
{
    public partial class OrderMaster
    {
        public OrderMaster()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public int? AutoNo { get; set; }
        public string SeatNo { get; set; }
        public string Operator { get; set; }
        public int TaxType { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerTaxNo { get; set; }
        public int OrderAmt { get; set; }
        public int TaxRate { get; set; }
        public int TaxAmt { get; set; }
        public int TotalAmt { get; set; }
        public string Notes { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string WriteUid { get; set; }
        public string WriteIp { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}

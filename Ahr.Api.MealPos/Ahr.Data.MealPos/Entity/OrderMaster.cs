using System;
using System.Collections.Generic;
using System.Linq;

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
        public int OrderNo { get; set; }
        public string SeatNo { get; set; }
        public int TaxType { get; set; }
        public int TaxRate { get; set; }
        public int NetAmt { get; set; }
        public int TaxAmt { get; set; }
        public int TotalAmt { get; set; }
        public string InvoiceNo { get; set; }
        public string TaxId { get; set; }
        public string Notes { get; set; }
        public int CookStatus { get; set; }
        public DateTime? WriteTime { get; set; }
        public string WriteUser { get; set; }
        public string WriteIp { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public void CaculateOrderAmt()
        {
            var orderAmt = this.OrderDetail
                           .Select(a => a.Qty * (a.Price + a.AddPrice))
                           .Sum();
            this.TotalAmt = orderAmt;
            if (this.TaxType == 0)
                this.TaxRate = 0;
            else
                this.TaxRate = 5;

            this.TaxAmt = this.TotalAmt * this.TaxRate / 100;
            this.NetAmt = this.TotalAmt - this.TaxAmt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ahr.Service.MealPos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string SeatNo { get; set; }
        public string OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderNo { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerTaxNo { get; set; }
        public int OrderAmt { get; set; }
        public int TaxType { get; set; }
        public int TaxRate { get; set; }
        public int TaxAmt { get; set; }
        public int TotalAmt { get; set; }
        public string Notes { get; set; }
        public string Operator { get; set; }
        public int CookStatus { get; set; }
        public ICollection<OrderDetailDto> orderDetailDtos { get; set; }
    }

    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int MealId { get; set; }
        public int MealAddOnId { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int AddPrice { get; set; }
        public int TotalAmt { 
            get 
            {
                return Qty * (Price + AddPrice);
            } 
        }
    }
}

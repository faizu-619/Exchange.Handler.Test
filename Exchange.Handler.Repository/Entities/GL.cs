using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Handler.Repository.Entities
{
    public class GL
    {
        public GL()
        {
        }

        public GL(TxType txType, int tradeId, double price, double quantity, double discount)
        {
            TxType = txType;
            TradeId = tradeId;
            Price = price;
            Quantity = quantity;
            Discount = discount;

            switch (txType)
            {
                case TxType.Sell:
                    Credit = (price * quantity);
                    break;
                case TxType.Purchase:
                    Debit = (price * quantity);
                    break;
                case TxType.Balance:
                    Credit = price;
                    break;
                default:
                    break;
            }
        }

        public int Id { get; set; }
        public TxType TxType { get; set; }
        public int TradeId { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public double Discount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Trade Trade { set; get; }
    }

    public enum TxType
    {
        Sell = 1,
        Purchase = 2,
        Balance = 3
    }
}

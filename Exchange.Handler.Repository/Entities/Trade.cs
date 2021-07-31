using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exchange.Handler.Repository.Entities
{
    public class Trade
    {
        public Trade()
        {
        }

        public Trade(string title, int tradeTypeId)
        {
            Title = title;
            TradeTypeId = tradeTypeId;
            IsActive = true;
            IsDelete = false;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TradeTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TradeType TradeType { set; get; }
        public virtual ICollection<GL> Transactions { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Handler.Repository.Entities
{
    public class TradeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Trade> Trades { set; get; }
    }
}

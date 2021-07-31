using Exchange.Handler.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exchange.Handler.Repository
{
    public class DataSeeder
    {
        public static void Seed(DataContext context)
        {
            if (!context.TradeTypes.Any())
            {
                var data = new List<TradeType>
                {
                    new TradeType { Name = "Currency", IsActive = true, IsDelete = false },
                    new TradeType { Name = "CryptoCoin", IsActive = true, IsDelete = false },
                    new TradeType { Name = "Stocks", IsActive = true, IsDelete = false }
                };
                context.TradeTypes.AddRange(data);
                context.SaveChanges();
            }


            if (!context.Trades.Any())
            {
                var data = new List<Trade>
                {
                    new Trade("JPY", 1),
                new Trade("PHP", 1),
                new Trade("BTC", 2),
                new Trade("TSLA ", 3)
                };
                context.Trades.AddRange(data);
                context.SaveChanges();
            }


            if (!context.GL.Any())
            {
                var data = new List<GL>
                {
                    new GL(TxType.Balance, 1, 50000, 1, 0)
                };
                context.GL.AddRange(data);
                context.SaveChanges();
            }
        }
    }
}

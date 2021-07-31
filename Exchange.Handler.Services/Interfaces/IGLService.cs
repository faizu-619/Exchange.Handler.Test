using Exchange.Handler.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Handler.Services
{
    public interface IGLService
    {
        Task<GL> Buy(GL item);
        Task<GL> Sell(GL item);
        Task<bool> Update(GL item);
        Task<double> Balance();
        Task<IEnumerable<GL>> GetTransactions();
        Task Delete(int id);
    }
}

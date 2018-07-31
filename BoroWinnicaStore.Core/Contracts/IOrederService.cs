using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoroWinnicaStore.Core.Contracts
{
    public interface IOrederService
    {
        void CreateOrder(OrderedParallelQuery baseOrder, List<BasketItemViewModel> basketItems);
        List<Order> GetOrderList();
        Order GetOrder(string Id);
        void UpdateOrder(Order updateOrder)

    }
}

using System.Collections.Generic;
using GoodInfo = System.Tuple<string, int>;

namespace ComputerStoreCore
{
    public class ReadOnlyBasket
    {
        protected List<GoodInfo> goods;
        
        public GoodInfo this[int i]
        {
            get { return goods[i]; }
        }

        public ReadOnlyBasket()
        {
            goods = new List<GoodInfo>();
        }

        public ReadOnlyBasket(IEnumerable<GoodInfo> goods)
        {
            goods = new List<GoodInfo>(goods);
        }
    }

    public class Basket : ReadOnlyBasket
    {
        public void Add(GoodInfo good)
        {
            goods.Add(good);
        }

        public ReadOnlyBasket ToReadOnly()
        {
            return new ReadOnlyBasket(goods);
        }
    }

    public class Customer
    {
        private Basket basket;

        public string Name { get; }

        public ReadOnlyBasket GoodsBasketReadOnly
        {
            get { return basket.ToReadOnly(); }
        }

        public Basket GoodsBasket
        {
            get { return basket; }
        }

        public Customer(string name)
        {
            basket = new Basket();
            Name = name;
        }
    }
}

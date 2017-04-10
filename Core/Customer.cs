using System;
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

        public int Count
        {
            get { return goods.Count; }
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

    // Покупатель — реализует паттерн «Одиночка»
    public class Customer
    {
        protected static Customer instance;

        protected Basket basket;

        protected string name, username;

        public string Name
        {
            get { return name; }
        }

        public string Username
        {
            get { return username; }
        }

        public ReadOnlyBasket GoodsBasketReadOnly
        {
            get { return basket.ToReadOnly(); }
        }

        public Basket GoodsBasket
        {
            get { return basket; }
        }

        protected Customer(string name, string username)
        {
            if (name == null || username == null)
                throw new ArgumentNullException();
            if (name == "" || username == "")
                throw new ArgumentException();

            basket = new Basket();
            this.name = name;
            this.username = username;
        }

        public void LogOut()
        {
            instance = null;

            basket = null;
            name = null;
            username = null;
        }

        public static Customer Instance(string name, string username)
        {
            if (instance == null)
                instance = new Customer(name, username);
            return instance;
        }
    }
}

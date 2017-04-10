using System;
using System.Collections.Generic;
using GoodInfo = System.Tuple<string, int>;

namespace ComputerStoreCore
{
    // Корзина только на чтение
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

        protected ReadOnlyBasket()
        {
            goods = new List<GoodInfo>();
        }

        protected ReadOnlyBasket(IEnumerable<GoodInfo> goods)
        {
            goods = new List<GoodInfo>(goods);
        }

        protected static ReadOnlyBasket MakeReadOnlyBasket
                                (IEnumerable<GoodInfo> goods)
        {
            return new ReadOnlyBasket(goods);
        }
    }

    // Корзина — реализует паттерн «Одиночка»
    public class Basket : ReadOnlyBasket
    {
        protected static Basket instance;

        public void Add(GoodInfo good)
        {
            goods.Add(good);
        }

        public ReadOnlyBasket ToReadOnly()
        {
            return ReadOnlyBasket.MakeReadOnlyBasket(goods);
        }

        protected Basket() : base() { }

        public static Basket Instance()
        {
            if (instance == null)
                instance = new Basket();
            return instance;
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

            basket = Basket.Instance();
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

using System;
using System.Collections.Generic;
using AuthArgs = System.Tuple<string, string>;
using GoodInfo = System.Tuple<string, int>;

namespace ComputerStoreCore
{
    public class Store
    {
        private List<Command<object>> commands;
        private DiscountCard card;
        private int acc;
        private string customer;

        public Dictionary<string, int> Goods { get; set; }

        public DiscountCard ActiveCard
        {
            get { return card; }
        }
        public int CurrentAccumulation
        {
            get { return acc; }
        }
        public string AuthorizedCustomer
        {
            get { return customer; }
        }

        public event CommandHandler<AuthArgs> Authorization;
        public event CommandHandler<AuthArgs> Registration;
        public event CommandHandler<Tuple<GoodInfo, Basket>> AddToBasket;
        public event CommandHandler<ReadOnlyBasket> Purchase;
        public event CommandHandler<Void> LogOut;
        public event CommandHandler<Void> Quit;
    }
}

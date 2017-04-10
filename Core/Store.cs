using System;
using System.Collections.Generic;
using AuthArgs = System.Tuple<string, string>;
using GoodInfo = System.Tuple<string, int>;

namespace ComputerStoreCore
{
    public class Store
    {        
        public Dictionary<string, int> Goods { get; set; }

        public SortedList<int, DiscountCard> Cards
        {
            get { return PurchaseCmd.Cards; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                PurchaseCmd.Cards = value;
            }
        }

        public DiscountCard ActiveCard
        {
            get { return PurchaseCmd.ActiveCard; }
        }
        public int CurrentAccumulation
        {
            get { return PurchaseCmd.CurrentAccumulation; }
        }

        public AuthorizationCommand AuthorizationCmd
        {
            get { return AuthorizationCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                AuthorizationCmd = value;
                Authorization = AuthorizationCmd.Handle;
            }
        }
        public RegistrationCommand RegistrationCmd
        {
            get { return RegistrationCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                RegistrationCmd = value;
                Registration = RegistrationCmd.Handle;
            }
        }
        public AddToBasketCommand AddToBasketCmd
        {
            get { return AddToBasketCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                AddToBasketCmd = value;
                AddToBasket = AddToBasketCmd.Handle;
            }
        }
        public PurchaseCommand PurchaseCmd
        {
            get { return PurchaseCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                PurchaseCmd = value;
                Purchase = PurchaseCmd.Handle;
            }
        }
        public LogOutCommand LogOutCmd
        {
            get { return LogOutCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                LogOutCmd = value;
                LogOut = LogOutCmd.Handle;
            }
        }
        public QuitCommand QuitCmd
        {
            get { return QuitCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                QuitCmd = value;
                Quit = QuitCmd.Handle;
            }
        }        

        public event CommandHandler<AuthArgs, Customer> Authorization;
        public event CommandHandler<AuthArgs, Customer> Registration;
        public event CommandHandler<Tuple<GoodInfo, Basket>, Void> AddToBasket;
        public event CommandHandler<ReadOnlyBasket, Void> Purchase;
        public event CommandHandler<Customer, Void> LogOut;
        public event CommandHandler<Void, Void> Quit;
    }    
}

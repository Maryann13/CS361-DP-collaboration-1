﻿using System;
using System.Collections.Generic;
using AuthArgs = System.Tuple<string, string>;
using GoodInfo = System.Tuple<string, int>;
using UserInfo = System.Tuple<string, string>;

namespace ComputerStoreCore
{
    public class Store
    {        
        public Dictionary<string, int> Goods { get; set; }

        public SortedList<int, DiscountCard> Cards { get; set; }
        
        public AuthorizationCommand AuthorizationCmd
        {
            get { return AuthorizationCmd; }
            set
            {
                AuthorizationCmd = value;
                Authorization = AuthorizationCmd.Handle;
            }
        }
        public RegistrationCommand RegistrationCmd
        {
            get { return RegistrationCmd; }
            set
            {
                RegistrationCmd = value;
                Registration = RegistrationCmd.Handle;
            }
        }
        public AddToBasketCommand AddToBasketCmd
        {
            get { return AddToBasketCmd; }
            set
            {
                AddToBasketCmd = value;
                AddToBasket = AddToBasketCmd.Handle;
            }
        }
        public PurchaseCommand PurchaseCmd
        {
            get { return PurchaseCmd; }
            set
            {
                PurchaseCmd = value;
                Purchase = PurchaseCmd.Handle;
            }
        }
        public LogOutCommand LogOutCmd
        {
            get { return LogOutCmd; }
            set
            {
                LogOutCmd = value;
                LogOut = LogOutCmd.Handle;
            }
        }
        public QuitCommand QuitCmd
        {
            get { return QuitCmd; }
            set
            {
                QuitCmd = value;
                Quit = QuitCmd.Handle;
            }
        }        

        public event CommandHandler<AuthArgs> Authorization;
        public event CommandHandler<AuthArgs> Registration;
        public event CommandHandler<Tuple<GoodInfo, Basket>> AddToBasket;
        public event CommandHandler<ReadOnlyBasket> Purchase;
        public event CommandHandler<Void> LogOut;
        public event CommandHandler<Void> Quit;
    }    
}

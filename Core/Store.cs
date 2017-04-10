using System;
using System.Collections.Generic;
using AuthArgs = System.Tuple<string, string>;
using GoodInfo = System.Tuple<string, int>;

namespace ComputerStoreCore
{
    // Магазин
    public class Store
    {
        // Товары
        public Dictionary<string, int> Goods { get; set; }

        // Скидочные карты
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

        // Активная карта
        public DiscountCard ActiveCard
        {
            get { return PurchaseCmd.ActiveCard; }
        }
        // Текущее накопление
        public int CurrentAccumulation
        {
            get { return PurchaseCmd.CurrentAccumulation; }
        }

        // Команда авторизации
        public AuthorizationCommand AuthorizationCmd
        {
            get { return authCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                authCmd = value;
                Authorization = authCmd.Handle;
            }
        }
        // Команда регистрации
        public RegistrationCommand RegistrationCmd
        {
            get { return regCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                regCmd = value;
                Registration = regCmd.Handle;
            }
        }
        // Команда добавления товара в корзину
        public AddToBasketCommand AddToBasketCmd
        {
            get { return addToBasketCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                addToBasketCmd = value;
                AddToBasket = addToBasketCmd.Handle;
            }
        }
        // Команда совершения покупки
        public PurchaseCommand PurchaseCmd
        {
            get { return purchaseCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                purchaseCmd = value;
                Purchase = purchaseCmd.Handle;
            }
        }
        // Команда завершения сеанса работы
        public LogOutCommand LogOutCmd
        {
            get { return logOutCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                logOutCmd = value;
                LogOut = logOutCmd.Handle;
            }
        }
        // Команда выключения системы
        public QuitCommand QuitCmd
        {
            get { return quitCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                quitCmd = value;
                Quit = quitCmd.Handle;
            }
        }

        // Событие авторизации
        public event CommandHandler<AuthArgs, Customer> Authorization;
        // Событие регистрации
        public event CommandHandler<AuthArgs, Customer> Registration;
        // Событие добавления товара в корзину
        public event CommandHandler<Tuple<GoodInfo, Basket>, Void> AddToBasket;
        // Событие совершения покупки
        public event CommandHandler<ReadOnlyBasket, Void> Purchase;
        // Событие завершения сеанса работы
        public event CommandHandler<Customer, Void> LogOut;
        // Событие выключения системы
        public event CommandHandler<Customer, Void> Quit;

        protected AuthorizationCommand authCmd;
        protected RegistrationCommand regCmd;
        protected AddToBasketCommand addToBasketCmd;
        protected PurchaseCommand purchaseCmd;
        protected LogOutCommand logOutCmd;
        protected QuitCommand quitCmd;

        // Обработчик авторизации
        public void OnAuthorization(AuthArgs args, out Customer c)
        {
            c = null;
            Authorization?.Invoke(args, out c);
        }

        // Обработчик регистрации
        public void OnRegistration(AuthArgs args, out Customer c)
        {
            c = null;
            Registration?.Invoke(args, out c);
        }

        // Обработчик добавления товара в корзину
        public void OnAddToBasket(Tuple<GoodInfo, Basket> args, out Void v)
        {
            v = null;
            AddToBasket?.Invoke(args, out v);
        }

        // Обработчик совершения покупки
        public void OnPurchase(ReadOnlyBasket basket, out Void v)
        {
            v = null;
            Purchase?.Invoke(basket, out v);
        }

        // Обработчик завершения сеанса работы
        public void OnLogOut(Customer c, out Void v)
        {
            v = null;
            LogOut?.Invoke(c, out v);
        }

        // Обработчик выключения системы
        public void OnQuit(Customer c, out Void v)
        {
            v = null;
            Quit?.Invoke(c, out v);
        }
    }    
}

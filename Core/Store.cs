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
            get { return AuthorizationCmd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                AuthorizationCmd = value;
                Authorization = AuthorizationCmd.Handle;
            }
        }
        // Команда регистрации
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
        // Команда добавления товара в корзину
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
        // Команда совершения покупки
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
        // Команда завершения сеанса работы
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
        // Команда выключения системы
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
    }    
}

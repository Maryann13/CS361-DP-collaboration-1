using System;
using System.Collections.Generic;
using AuthArgs = System.Tuple<string, string>;
using GoodInfo = System.Tuple<string, int>;

namespace ComputerStoreCore
{
    // Представляет метод, обрабатывающий команду
    public delegate void CommandHandler<CommandArgs, OutArg>
                                      (CommandArgs args, out OutArg o);

    // Поддерживает обработку команды
    public interface IHandleable<T, Out>
    {
        // Обработчик команды
        void Handle(T args, out Out o);
    }

    // Команда
    public abstract class Command<T, Out> : IHandleable<T, Out>
    {
        // Обработчик команды
        public abstract void Handle(T args, out Out o);
    }

    // Команда категории «A»
    public abstract class CommandCategoryA<T, Out> : Command<T, Out> { }

    // Команда категории «B»
    public abstract class CommandCategoryB<T, Out> : Command<T, Out> { }

    // Команда авторизации
    public class AuthorizationCommand : CommandCategoryA<AuthArgs, Customer>
    {
        protected CustomerFactory factory;

        public AuthorizationCommand(CustomerFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException();
            this.factory = factory;
        }

        // Обработчик команды
        public override void Handle(AuthArgs args, out Customer c)
        {
            c = factory.MakeCustomer(args.Item1, args.Item2);
        }
    }

    // Команда регистрации
    public class RegistrationCommand : CommandCategoryA<AuthArgs, Customer>
    {
        protected CustomerFactory factory;

        public RegistrationCommand(CustomerFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException();
            this.factory = factory;
        }

        // Обработчик команды
        public override void Handle(AuthArgs args, out Customer c)
        {
            c = factory.MakeCustomer(args.Item1, args.Item2);
        }
    }

    // Команда добавления товара в корзину
    public class AddToBasketCommand : CommandCategoryB<Tuple<GoodInfo, Basket>, Void>
    {
        // Обработчик команды
        public override void Handle(Tuple<GoodInfo, Basket> args, out Void v)
        {
            args.Item2.Add(args.Item1);
            v = null;
        }
    }

    // Команда совершения покупки
    public class PurchaseCommand : CommandCategoryB<ReadOnlyBasket, Void>
    {
        protected DiscountCard card;
        protected int acc, nextAcc;

        // Скидочные карты
        public SortedList<int, DiscountCard> Cards
        {
            get { return Cards; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                Cards = value;
            }
        }

        // Активная карта
        public DiscountCard ActiveCard
        {
            get { return card; }
        }
        // Текущее накопление
        public int CurrentAccumulation
        {
            get { return acc; }
        }

        public PurchaseCommand()
        {
            card = null;
            acc = 0;
            nextAcc = Cards.Keys[0];
        }

        // Обработчик команды
        public override void Handle(ReadOnlyBasket basket, out Void v)
        {
            for (int i = 0; i < basket.Count; ++i)
                acc += basket[i].Item2;

            if (acc >= nextAcc)
            {
                card = Cards[nextAcc];

                int nextInd = Cards.IndexOfKey(nextAcc) + 1;
                if (nextInd >= Cards.Count)
                    nextAcc = -1;
                else
                    nextAcc = Cards.Keys[nextInd];
            }
            v = null;
        }
    }

    // Команда завершения сеанса работы
    public class LogOutCommand : CommandCategoryB<Customer, Void>
    {
        // Обработчик команды
        public override void Handle(Customer c, out Void v)
        {
            c.LogOut();
            v = null;
        }
    }

    // Команда выключения системы
    public class QuitCommand : Command<Customer, Void>
    {
        // Обработчик команды
        public override void Handle(Customer c, out Void v)
        {
            c.LogOut();
            v = null;
        }
    }

    // Пустой класс
    public class Void { }
}

using System;
using AuthArgs = System.Tuple<string, string>;
using GoodInfo = System.Tuple<string, int>;

namespace ComputerStoreCore
{
    // Представляет метод, обрабатывающий команду
    public delegate void CommandHandler<CommandArgs>(CommandArgs args);

    // Поддерживает обработку команды
    public interface IHandleable<T>
    {
        void Handle(T args);
    }
    
    // Команда
    public abstract class Command<T> : IHandleable<T>
    {
        public abstract void Handle(T args);
    }

    // Команда категории «A»
    public abstract class CommandCategoryA<T> : Command<T> { }

    // Команда категории «B»
    public abstract class CommandCategoryB<T> : Command<T> { }

    // Команда авторизации
    public class AuthorizationCommand : CommandCategoryA<AuthArgs>
    {
        public override void Handle(AuthArgs args)
        {
            // TODO
        }
    }

    // Команда регистрации
    public class RegistrationCommand : CommandCategoryA<AuthArgs>
    {
        public override void Handle(AuthArgs args)
        {
            // TODO
        }
    }

    // Команда добавления товара в корзину
    public class AddToBasketCommand : CommandCategoryB<Tuple<GoodInfo, Basket>>
    {
        public override void Handle(Tuple<GoodInfo, Basket> args)
        {
            // TODO
        }
    }

    // Команда совершения покупки
    public class PurchaseCommand : CommandCategoryB<ReadOnlyBasket>
    {
        public override void Handle(ReadOnlyBasket args)
        {
            // TODO
        }
    }

    // Команда завершения сеанса работы
    public class LogOutCommand : CommandCategoryB<Void>
    {
        public override void Handle(Void args)
        {
            // TODO
        }
    }

    // Команда выключения системы
    public class QuitCommand : Command<Void>
    {
        public override void Handle(Void args)
        {
            // TODO
        }
    }

    // Пустой класс
    public class Void { }
}

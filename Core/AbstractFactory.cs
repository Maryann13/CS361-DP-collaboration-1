﻿
namespace ComputerStoreCore
{
    // Реализует паттерн «Абстрактная фабрика» для скидочных карт
    public class DiscountCardsFactory
    {
        public virtual TubeDiscountCard MakeTubeDiscountCard()
        { return new TubeDiscountCard(); }
        public virtual TransistorDiscountCard MakeTransistorDiscountCard()
        { return new TransistorDiscountCard(); }
        public virtual IntegratedDiscountCard MakeIntegratedDiscountCard()
        { return new IntegratedDiscountCard(); }
    }

    // Реализует паттерн «Абстрактная фабрика» для магазина
    public class StoreFactory
    {
        public virtual Store MakeStore()
            { return new Store(); }
        
        public virtual AuthorizationCommand MakeAuthorizationCommand(CustomerFactory factory)
            { return new AuthorizationCommand(factory); }
        public virtual RegistrationCommand MakeRegistrationCommand(CustomerFactory factory)
            { return new RegistrationCommand(factory); }
        public virtual AddToBasketCommand MakeAddToBasketCommand()
            { return new AddToBasketCommand(); }
        public virtual PurchaseCommand MakePurchaseCommand()
            { return new PurchaseCommand(); }
        public virtual LogOutCommand MakeLogOutCommand()
            { return new LogOutCommand(); }
        public virtual QuitCommand MakeQuitCommand()
            { return new QuitCommand(); }
    }

    // Реализует паттерн «Абстрактная фабрика» для покупателя
    public class CustomerFactory
    {       
        public virtual Customer MakeCustomer(string name, string username)
            { return Customer.Instance(name, username); }
    }
}

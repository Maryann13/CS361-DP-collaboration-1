
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
        
        public virtual AuthorizationCommand MakeAuthorizationCommand()
            { return new AuthorizationCommand(); }
        public virtual RegistrationCommand MakeRegistrationCommand()
            { return new RegistrationCommand(); }
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
        public virtual Basket MakeBasket()
            { return new Basket(); }
        public virtual Customer MakeCustomer(string name)
            { return new Customer(name); }
    }
}

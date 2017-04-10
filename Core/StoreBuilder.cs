
namespace ComputerStoreCore
{
    // Паттерн «Строитель» для магазина
    public interface StoreBuilder
    {
        void BuildStore(StoreFactory factory);
        void BuildCards(DiscountCardsFactory factory);
        void BuildCommands(StoreFactory factory, CustomerFactory custFactory);
        Store GetStore();
    }

    // Реализует паттерн «Строитель» для магазина
    public class StandardStoreBuilder : StoreBuilder
    {
        private Store store;

        public void BuildStore(StoreFactory factory)
        {
            store = factory.MakeStore();
        }

        public void BuildCards(DiscountCardsFactory factory)
        {
            TubeDiscountCard tube = factory.MakeTubeDiscountCard();
            TransistorDiscountCard transistor = factory.MakeTransistorDiscountCard();
            IntegratedDiscountCard integrated = factory.MakeIntegratedDiscountCard();

            store.Cards.Add(tube.Accumulation, tube);
            store.Cards.Add(transistor.Accumulation, transistor);
            store.Cards.Add(integrated.Accumulation, integrated);
        }

        public void BuildCommands(StoreFactory factory, CustomerFactory custFactory)
        {
            store.AuthorizationCmd = factory.MakeAuthorizationCommand(custFactory);
            store.RegistrationCmd = factory.MakeRegistrationCommand(custFactory);
            store.AddToBasketCmd = factory.MakeAddToBasketCommand();
            store.PurchaseCmd = factory.MakePurchaseCommand();
            store.LogOutCmd = factory.MakeLogOutCommand();
            store.QuitCmd = factory.MakeQuitCommand();
        }

        public Store GetStore()
        {
            return store;
        }
    }
}

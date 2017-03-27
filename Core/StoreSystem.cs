
namespace ComputerStoreCore
{
    // Система компьютерного магазина
    public class StoreSystem
    {
        // Создать магазин
        public Store CreateStore(StoreBuilder builder,
            StoreFactory storeFactory, DiscountCardsFactory cardsFactory)
        {
            builder.BuildStore(storeFactory);
            builder.BuildCards(cardsFactory);
            builder.BuildCommands(storeFactory);
            return builder.GetStore();
        }

        // Создать покупателя
        public Customer CreateCustomer(string name, CustomerFactory factory)
        {
            return factory.MakeCustomer(name);
        }
    }
}

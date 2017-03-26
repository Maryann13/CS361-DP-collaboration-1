
namespace ComputerStoreCore
{
    // Скидочная карта
    public abstract class DiscountCard
    {
        // Размер скидки
        public abstract double Discount { get; }
    }

    // Ламповая скидочная карта
    public class TubeDiscountCard : DiscountCard
    {
        public override double Discount
        {
            get { return 0.05; }
        }
    }

    // Транзисторная скидочная карта
    public class TransistorDiscountCard : DiscountCard
    {
        public override double Discount
        {
            get { return 0.1; }
        }
    }

    // Интегральная скидочная карта
    public class IntegratedDiscountCard : DiscountCard
    {
        public override double Discount
        {
            get { return 0.15; }
        }
    }
}

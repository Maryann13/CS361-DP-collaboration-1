
using System;

namespace ComputerStoreCore
{
    // Скидочная карта
    public abstract class DiscountCard
    {
        // Размер накопления для получения
        public abstract int Accumulation { get; }

        // Размер скидки
        public abstract double Discount { get; }
    }

    // Ламповая скидочная карта
    public class TubeDiscountCard : DiscountCard
    {
        public override int Accumulation
        {
            get { return 5000; }
        }

        public override double Discount
        {
            get { return 0.05; }
        }
    }

    // Транзисторная скидочная карта
    public class TransistorDiscountCard : DiscountCard
    {
        public override int Accumulation
        {
            get { return 12500; }
        }

        public override double Discount
        {
            get { return 0.1; }
        }
    }

    // Интегральная скидочная карта
    public class IntegratedDiscountCard : DiscountCard
    {
        public override int Accumulation
        {
            get { return 25000; }
        }

        public override double Discount
        {
            get { return 0.15; }
        }
    }
}

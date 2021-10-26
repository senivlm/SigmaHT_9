using System;

namespace SigmaHT_9
{
    enum MeatCategory { ExtraClass = 50, FisrtCalss = 40, SecondClass = 30 };
    enum KindOfMeat { Mutton, Veal, Pork, Chicken };
    class Meat : Product
    {
        public MeatCategory MeatCategory { get; set; }
        public KindOfMeat KindOfMeat { get; set; }

        public Meat(string name, double price, double weight, int expirationDate, DateTime productionDate,
            MeatCategory meatCategory, KindOfMeat kindOfMeat)
            : base(name, price, weight, expirationDate, productionDate)
        {
            MeatCategory = meatCategory;
            KindOfMeat = kindOfMeat;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            Meat meat = (Meat)obj;
            return (this.MeatCategory == meat.MeatCategory && this.KindOfMeat == meat.KindOfMeat);
        }

        public override string ToString()
        {
            return base.ToString() + $"Category: {this.MeatCategory}\nKind: {this.KindOfMeat}";
        }

        public override void ChangePrice(double percentage)
        {
            base.ChangePrice(percentage);

            if (percentage > 0)
            {
                switch (MeatCategory)
                {
                    case MeatCategory.ExtraClass:
                        Price += (double)MeatCategory.ExtraClass * Price / 100;
                        break;
                    case MeatCategory.FisrtCalss:
                        Price += (double)MeatCategory.FisrtCalss * Price / 100;
                        break;
                    case MeatCategory.SecondClass:
                        Price += (double)MeatCategory.SecondClass * Price / 100;
                        break;
                }
            }
            else if (percentage < 0)
            {
                switch (MeatCategory)
                {
                    case MeatCategory.ExtraClass:
                        Price -= (double)MeatCategory.ExtraClass * Price / 100;
                        break;
                    case MeatCategory.FisrtCalss:
                        Price -= (double)MeatCategory.FisrtCalss * Price / 100;
                        break;
                    case MeatCategory.SecondClass:
                        Price -= (double)MeatCategory.SecondClass * Price / 100;
                        break;
                }
            }
            else
            {
                return;
            }

        }
    }
}

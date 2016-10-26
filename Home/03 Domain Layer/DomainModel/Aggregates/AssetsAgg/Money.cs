using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.AssetsAgg
{
    /// <summary>
    /// 
    /// </summary>
    public class Money : ValueObject<Money>
    {
        public decimal Value { get; set; }

        public CurrencyType CurrencyType { get; set; }


        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var item = obj as Money;
            
            return Equals(this,item);
        }

        public override bool Equals(Money x, Money y)
        {
            if ((x.Value == y.Value) &&
               (x.CurrencyType == y.CurrencyType))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode(Money obj)
        {
            return obj.Value.GetHashCode();
        }

        public static bool operator !=(Money money1, Money money2)
        {
            if ((money1.Value != money2.Value) &&
                (money1.CurrencyType != money2.CurrencyType))
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Money money1, Money money2)
        {
            if ((money1.Value == money2.Value) &&
                (money1.CurrencyType == money1.CurrencyType))
            {
                return true;
            }
            return false;
        }

    }
}

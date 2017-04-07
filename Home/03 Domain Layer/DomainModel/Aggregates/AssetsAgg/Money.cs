using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.Aggregates.AssetsAgg
{
    /// <summary>
    /// 
    /// </summary>
    public class Money : ValueObject<Money>
    {
        [System.ComponentModel.Description("价值"),
            System.ComponentModel.DisplayName("价值")]
        public decimal Value { get; set; }


        [System.ComponentModel.Description("货币类型"),
            System.ComponentModel.DisplayName("货币类型")]
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

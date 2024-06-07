using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umsatzsteuer
{
    public class TaxCalc
    {
        public decimal CalculateTax(decimal amount, decimal taxRate, bool isGross)
        {
            if (isGross)
            {
                return amount - (amount / (1 + taxRate / 100));
            }
            else
            {
                return amount * (taxRate / 100);
            }
        }

        public decimal CalculateNetAmount(decimal grossAmount, decimal taxRate)
        {
            return grossAmount / (1 + taxRate / 100);
        }

        public decimal CalculateGrossAmount(decimal netAmount, decimal taxRate)
        {
            return netAmount * (1 + taxRate / 100);
        }
    }
}


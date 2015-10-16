using System;

namespace Inestors2015.Models
{
    public class FinancialDocLineCM : IEquatable<FinancialDocLineCM>
    {
        public int FinancialDocID
        {
            get;
            set;
        }

        public int FinancialDocLineID
        {
            get;
            set;
        }

        public int CurrencyId
        {
            get;
            set;
        }

        public int DebetId
        {
            get;
            set;
        }

        public int CreditId
        {
            get;
            set;
        }

        public bool MainLine
        {
            get;
            set;
        }

        public string VatRate
        {
            get;
            set;
        }

        bool IEquatable<FinancialDocLineCM>.Equals(FinancialDocLineCM other)
        {
            bool result;
            if (object.ReferenceEquals(other, null))
            {
                result = false;
            }
            else if (object.ReferenceEquals(this, other))
            {
                result = true;
            }
            else
            {
                bool areAllValuesEqual = this.DebetId.Equals(other.DebetId) && this.CreditId.Equals(other.CreditId);
                result = areAllValuesEqual;
            }
            return result;
        }

        public override int GetHashCode()
        {
            int debetId = this.DebetId;
            int hashCode1 = this.DebetId.GetHashCode();
            int creditId = this.CreditId;
            int hashCode2 = this.CreditId.GetHashCode();
            return hashCode1 ^ hashCode2;

        }
    }
}

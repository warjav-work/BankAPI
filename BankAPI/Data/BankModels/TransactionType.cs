using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            BankTransactions = new HashSet<BankTransaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegDate { get; set; }

        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
    }
}

namespace BA.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {
            AccountHistories = new HashSet<AccountHistory>();
        }

        public int TransactionId { get; set; }

        public int? FromAccount { get; set; }

        public int? ToAccount { get; set; }

        public double? Amount { get; set; }

        public int? Currency { get; set; }

        public DateTime? TransactionDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountHistory> AccountHistories { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public virtual BankAccount BankAccount1 { get; set; }

        public virtual Currency Currency1 { get; set; }
    }
}

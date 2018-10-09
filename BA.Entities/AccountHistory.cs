namespace BA.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountHistory")]
    public partial class AccountHistory
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }

        public int? AccountId { get; set; }

        public double? AvailableAmount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}

namespace BA.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FromCurrency { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ToCurrency { get; set; }

        [Key]
        [Column("Rate", Order = 2)]
        public double Rate1 { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Currency Currency1 { get; set; }
    }
}

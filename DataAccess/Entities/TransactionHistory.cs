﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EfStructures.Entities
{
    [Table("TransactionHistory", Schema = "Production")]
    public partial class TransactionHistory
    {
        [Key]
        [Column("TransactionID")]
        public int TransactionId { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column("ReferenceOrderID")]
        public int ReferenceOrderId { get; set; }
        [Column("ReferenceOrderLineID")]
        public int ReferenceOrderLineId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TransactionDate { get; set; }
        [Required]
        [Column(TypeName = "nchar(1)")]
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal ActualCost { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("TransactionHistory")]
        public Product Product { get; set; }
    }
}

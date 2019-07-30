using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EfStructures.Entities
{
    public partial class DatabaseLog
    {
        [Column("DatabaseLogID")]
        public int DatabaseLogId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PostTime { get; set; }
        [Required]
        [Column(TypeName = "sysname")]
        [StringLength(4000)]
        public string DatabaseUser { get; set; }
        [Required]
        [Column(TypeName = "sysname")]
        [StringLength(4000)]
        public string Event { get; set; }
        [Column(TypeName = "sysname")]
        [StringLength(4000)]
        public string Schema { get; set; }
        [Column(TypeName = "sysname")]
        [StringLength(4000)]
        public string Object { get; set; }
        [Required]
        [Column("TSQL")]
        public string Tsql { get; set; }
        [Required]
        [Column(TypeName = "xml")]
        public string XmlEvent { get; set; }
    }
}

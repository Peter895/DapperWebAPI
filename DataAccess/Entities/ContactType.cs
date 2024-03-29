﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EfStructures.Entities
{
    [Table("ContactType", Schema = "Person")]
    public partial class ContactType
    {
        public ContactType()
        {
            BusinessEntityContact = new HashSet<BusinessEntityContact>();
        }

        [Column("ContactTypeID")]
        public int ContactTypeId { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        [StringLength(4000)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("ContactType")]
        public ICollection<BusinessEntityContact> BusinessEntityContact { get; set; }
    }
}



using System.ComponentModel.DataAnnotations.Schema;

namespace DapperWebAPI.Entities
{
    public class ProductDTO
    {



        [Column("ProductID")]
        public int Id { get; set; }
        [Column("Name")]
        public string Description { get; set; }
    }
}

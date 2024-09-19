using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductFinder.Entities
{
    public class Product
    {
        //Primary ve Identity sağlar.
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] //Bu alan benim için gerekli.
        [StringLength(50)] //Nvarchar(50)
        public string Name { get; set; }

        [Required] //Bu alan benim için gerekli.
        [StringLength(50)] //Nvarchar(50)
        public string Category { get; set; }
    }
}

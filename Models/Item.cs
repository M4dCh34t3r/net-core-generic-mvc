using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GenericCrudMVC.Models {
    [Table("Items")]
    public class Item {
		[Column("ID")]
        [Display(Name="ID")]
		public int ID { get; set; }

		[Column("Name")]
		[Display(Name="Name")]
		public string Name { get; set; }

		[Column("Description")]
		[Display(Name="Description")]
		public string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.KitchenManagement.Domain.Entities
{
    [Table("Kitchens", Schema = "KitchenManagement")]
    public class Kitchens : BaseEntity
    {
        [Key]
        public int KitchenId { get; set; }

        public string KitchenName { get; set; } = string.Empty;
        public string KitchenDescription { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int AddressId { get; set; }
    }
}

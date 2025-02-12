using System.ComponentModel.DataAnnotations;

namespace SkiServiceAPI.Models
{
    public class ServiceOrder
    {
        [Key]
        [Display(Name = "Identifier", Description = "Unique identifier for the service order")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Customer Name", Description = "Full name of the customer placing the order")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Email Address", Description = "Email address of the customer")]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone Number", Description = "Contact phone number of the customer")]
        public string Telefon { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Priority", Description = "Priority of the service request")]
        public string Prioritaet { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Service Type", Description = "Type of service requested, such as tuning or waxing")]
        public string Dienstleistung { get; set; }

        [StringLength(20)]
        [Display(Name = "Status", Description = "Current status of the service order")]
        public string Status { get; set; } = "Offen";
    }
}

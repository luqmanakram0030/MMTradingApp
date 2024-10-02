using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMAdmin.Models
{
    [Table("Schedule")]
    public class Schedule
    {

        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Nombre requerido")]
        [StringLength(50, ErrorMessage = "El título debe tener máximo 50 caracteres")]
        public string Title { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "La descripción debe tener máximo 300 caracteres")]
        public string? Description { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime DateStartUtc { get; set; }
        [NotMapped]
        public DateTime DateStart
        {
            get => DateStartUtc.ToLocalTime();
            set => DateStartUtc = value.ToUniversalTime();
        }

        public DateTime DateEndUtc { get; set; }

        public DateTime DateNotify { get; set; } //calculate this at save        

        public int NotifyNumber { get; set; }



        public int? LeadId { get; set; }
        [ForeignKey(nameof(LeadId))]
        public virtual EmployeeModel? Lead { get; set; }

        public int? CreateUserId { get; set; }
        [ForeignKey(nameof(CreateUserId))]
        public virtual User? CreateUser { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        public bool GCRecord { get; set; } = false;
    }
}


using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmiCookBook.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Surname { get; set; }
        
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Login { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        
        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;
        
        public int RoleId { get; set; }
        
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}
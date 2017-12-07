using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngryBirdsDb
{
    [Table("PlayerList")]
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [Column("PlayerName", TypeName = "nvarchar")]
        [StringLength(50)]
        public string PlayerName { get; set; }

    }
}

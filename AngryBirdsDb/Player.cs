using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngryBirdsDb.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [Column("PlayerName", TypeName = "nvarchar")]
        [StringLength(100)]
        public string PlayerName { get; set; }

        public virtual IList<Game> Games { get; set; }
         
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngryBirdsDb.Models
{
    //[Table("GameList")]
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int TrackId { get; set; }

        public int GameScore { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        [ForeignKey("TrackId")]
        public virtual Track Track { get; set; }

    
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngryBirdsDb
{
    [Table("GameList")]
    public class GameList
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

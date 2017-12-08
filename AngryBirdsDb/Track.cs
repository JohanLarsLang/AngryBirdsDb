using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngryBirdsDb.Models
{
    
    public class Track
    {
        [Key]
        public int TrackId { get; set; }

        public int NrBird { get; set; }

        public virtual IList<Game> Games { get; set; }
              
    }
}

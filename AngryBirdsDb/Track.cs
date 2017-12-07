using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngryBirdsDb
{
    [Table("TrackList")]
    public class Track
    {
        [Key]
        public int TrackId { get; set; }

        public int NrBird { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Rengo.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
        public bool IsSent { get; internal set; }
    }
}

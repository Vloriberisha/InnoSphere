using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Models
{
    public class JobSeeker
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string Gender { get; set; }
        public string Photo { get; set; }
        public string Position { get; set; }
        public string DateOfBirth { get; set; }
        public string? Introduction { get; set; }
        public string? Skills { get; set; }
        [JsonIgnore]
        public List<JobApplication> JobApplications { get; set; }

    }
}

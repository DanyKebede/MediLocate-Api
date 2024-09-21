using MediLast.Models;
using System.ComponentModel.DataAnnotations;

namespace MediLast.Dtos.PharmacyReview
{
    public class PharmacyReviewAddDto
    {

        [Required]
        public ReviewDecision Decision { get; set; }

        [Required]
        public string Comments { get; set; }
    }
}

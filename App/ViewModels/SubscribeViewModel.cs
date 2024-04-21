using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class SubscribeViewModel
    {
        
            [Required]
            [EmailAddress]
            [Display(Name = "E-mail address", Prompt = "Your Email")]
            public string Email { get; set; } = null!;
            public bool DailyNewsletter { get; set; }
            public bool AdvertisingUpadtes { get; set; }
            public bool WeekinReview { get; set; }
            public bool EventUpdates { get; set; }
            public bool StartupsWeekly { get; set; }
            public bool Podcasts { get; set; } = true;
        

    }
}

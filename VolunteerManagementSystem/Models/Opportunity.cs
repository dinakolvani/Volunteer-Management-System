namespace VolunteerManagementSystem.Models
{
    public class Opportunity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty; // Matches volunteer interests

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        public string Location { get; set; } = string.Empty; 
    }

    public class VolunteerOpportunityMatch
    {
        public int Id { get; set; }

        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; } = default!;

        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; } = default!;
    }
}

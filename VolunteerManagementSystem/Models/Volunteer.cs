public class Volunteer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // Later, store hashed if serious
    public string PreferredCenters { get; set; }
    public string SkillsInterests { get; set; }
    public string Availability { get; set; }
    public string Address { get; set; }
    public string PhoneHome { get; set; }
    public string PhoneWork { get; set; }
    public string PhoneCell { get; set; }
    public string Email { get; set; }
    public string Education { get; set; }
    public string Licenses { get; set; }

    // Emergency Contact Info
    public string EmergencyName { get; set; }
    public string EmergencyPhoneHome { get; set; }
    public string EmergencyPhoneWork { get; set; }
    public string EmergencyEmail { get; set; }
    public string EmergencyAddress { get; set; }

    // Documents
    public bool HasDriversLicense { get; set; }
    public bool HasSocialSecurityCard { get; set; }

    public string ApprovalStatus { get; set; } // "Approved", "Pending", etc.
}

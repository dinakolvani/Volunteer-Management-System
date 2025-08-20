using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace VolunteerManagementSystem.Models
{
    public enum VolunteerStatus
    {
        ApprovedPending,   // represents "Approved / Pending Approval"
        Approved,
        PendingApproval,
        Disapproved,
        Inactive,
        All
    }

        public class Volunteer
        {
            public int Id { get; set; }

            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            public string CentersPreferred { get; set; } = string.Empty;
            public string SkillsInterests { get; set; } = string.Empty;

            public string Availability { get; set; } = string.Empty;

            public string Address { get; set; } = string.Empty;
            public string HomePhone { get; set; } = string.Empty;
            public string WorkPhone { get; set; } = string.Empty;
            public string CellPhone { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string EducationalBackground { get; set; } = string.Empty;
            public string CurrentLicenses { get; set; } = string.Empty;

            public string EmergencyContactName { get; set; } = string.Empty;
            public string EmergencyContactPhone { get; set; } = string.Empty;
            public string EmergencyContactEmail { get; set; } = string.Empty;
            public string EmergencyContactAddress { get; set; } = string.Empty;

            public bool HasDriversLicenseOnFile { get; set; }
            public bool HasSSCardOnFile { get; set; }

            public VolunteerStatus Status { get; set; }

            public string InterestCategory { get; set; } = string.Empty;
        }
    }

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Constants
{
    public class Enumerations
    {

        public enum JobApplicationStatus
        {
            Submitted,
            Rejected,
            ApprovedForInterview
        }

        public enum Qualification
        {
            HighSchoolDiploma,
            AssociateDegree,
            BachelorDegree,
            MasterDegree,
            Doctorate,
            ProfessionalCertification,
            VocationalTraining,
            WorkExperience
        }

    }
}

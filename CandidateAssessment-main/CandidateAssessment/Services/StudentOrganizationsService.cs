using CandidateAssessment.Models;

namespace CandidateAssessment.Services
{
    public class StudentOrganizationsService
    {
        private CandidateAssessmentContext _dbContext;

        public StudentOrganizationsService(CandidateAssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<StudentOrganization> GetOrganizations()
        {
            return _dbContext.StudentOrganizations;
        }
    }
}

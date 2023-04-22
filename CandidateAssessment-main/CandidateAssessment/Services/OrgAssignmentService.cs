using CandidateAssessment.Models;


namespace CandidateAssessment.Services
{
    public class OrgAssignmentService
    {
        private CandidateAssessmentContext _dbContext;

        public OrgAssignmentService (CandidateAssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrgAssignment> CreateOrgAssignment(OrgAssignment orgAssignment)
        {
            _dbContext.OrgAssignments.Add(orgAssignment);
            await _dbContext.SaveChangesAsync();
            return orgAssignment;
        }
    }
}

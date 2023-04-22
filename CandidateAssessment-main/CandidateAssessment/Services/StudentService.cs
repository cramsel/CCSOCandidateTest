﻿using CandidateAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateAssessment.Services
{
    public class StudentService
    {
        private CandidateAssessmentContext _dbContext;

        public StudentService (CandidateAssessmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students
                .Include(s => s.School)
                .Include(s => s.OrgAssignments)
                    .ThenInclude(oa => oa.StudentOrg);
        }
    }
}

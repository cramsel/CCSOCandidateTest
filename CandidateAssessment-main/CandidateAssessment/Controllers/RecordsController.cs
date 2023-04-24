using CandidateAssessment.Models;
using CandidateAssessment.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CandidateAssessment.Controllers
{
    public class RecordsController : Controller
    {
        private StudentService _studentService;
        private SchoolService _schoolService;
        private StudentOrganizationsService _studentOrganizationsService;
        private OrgAssignmentService _organAssignmentService;
        public RecordsController(StudentService studentService, 
                                 SchoolService schoolService, 
                                 StudentOrganizationsService studentOrganizationsService,
                                 OrgAssignmentService orgAssignmentService)
        {
            _studentService = studentService;
            _schoolService = schoolService;
            _studentOrganizationsService = studentOrganizationsService;
            _organAssignmentService = orgAssignmentService;
        }

        public IActionResult Students()
        {
            ViewBag.AgeList = CreateAgeList();
            ViewBag.SchoolList = CreateSchoolDropdownList();
            ViewBag.OrgList = CreateStudentOrgDropdown();

            var model = _studentService.GetStudents().OrderBy(s => s.LastName);
            return View(model);
        }

        public IActionResult Schools()
        {
            var model = _schoolService.GetSchools().OrderBy(s => s.Name);
            return View(model);
        }

        public IActionResult SchoolStudents()
        {
            string tmpUrl = Request.GetDisplayUrl();
            tmpUrl = tmpUrl.Split('/').Last();
            int schoolIdUrl = Int32.Parse(tmpUrl);
            var model = _studentService.GetStudents().Where(s => s.SchoolId == schoolIdUrl);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> SaveStudent(Student model)
        {
            var newStudent = await _studentService.CreateStudent(model);

            foreach (int org in newStudent.SelectedOrgs)
            {
                await SaveOrgAssignment(org, newStudent.StudentId);
            }

            return RedirectToAction("Students");
        }

        public async Task<ActionResult<OrgAssignment>> SaveOrgAssignment(int studentOrgId, int studentId)
        {
            OrgAssignment tmpOrg = new OrgAssignment { StudentOrgId = studentOrgId, StudentId = studentId};

            var newOrgAssignment = await _organAssignmentService.CreateOrgAssignment(tmpOrg);

            return newOrgAssignment;
        }

        private List<SelectListItem> CreateAgeList()
        {
            var ageList = new List<SelectListItem>();
            for (int i = 18; i < 100; i++)
            {
                ageList.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            return ageList;
        }

        private List<SelectListItem> CreateSchoolDropdownList()
        {
            var schools = _schoolService.GetSchools();
            var schoolList = new List<SelectListItem>();

            foreach (School school in schools)
            {
                schoolList.Add(new SelectListItem { Text = school.Name, Value = school.SchoolId.ToString() });
            }

            return schoolList;
        }

        private MultiSelectList CreateStudentOrgDropdown()
        {
            var options = _studentOrganizationsService.GetOrganizations();
            var optionsList = new List<SelectListItem>();

            foreach (StudentOrganization org in options)
            {
                optionsList.Add(new SelectListItem { Text = org.OrgName, Value = org.Id.ToString() });
            }

            return new MultiSelectList(optionsList, "Value", "Text");
        }
    }
}
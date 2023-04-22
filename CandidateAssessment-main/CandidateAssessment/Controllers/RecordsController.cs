using CandidateAssessment.Models;
using CandidateAssessment.Services;
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
        public RecordsController(StudentService studentService, 
                                 SchoolService schoolService, 
                                 StudentOrganizationsService studentOrganizationsService)
        {
            _studentService = studentService;
            _schoolService = schoolService;
            _studentOrganizationsService = studentOrganizationsService;
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

        [HttpPost]
        public IActionResult SaveStudent(Student model)
        {
            // replace this code with code that actually saves the model

            return RedirectToAction("Students");
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
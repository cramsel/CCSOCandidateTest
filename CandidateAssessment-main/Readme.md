
## Description
The app saved to this repository is a very simple application tracking imaginary students enrolled in a small set of schools. Both the students and schools have basic data fields assigned to them. There is a one-to-many relationship between the students and schools which should be represented in the front end tables.

## Initial Tasks (Completed as of 4/24/23)
If you have any questions about the following tasks, please email christian.whiting@ccsheriff.org.

1. **Add the School column to the Student table** - Display which school each student is attending in the front end student table. 
2. **Complete the Add Student modal form** - Generate the school and student organization dropdown lists from the the database. Add server side code to save the submitted model to the database. You will need to convert the **SelectedOrgs** property of  **Students** to **OrgAssignments** and save to the database. Don't worry about validating the inputs.  
3. **Complete the link in the Students column of the Schools table** - The link should navigate to a page displaying the students that go to the school correlating to the row clicked. A new controller action and view will need to be created to accomplish this. 

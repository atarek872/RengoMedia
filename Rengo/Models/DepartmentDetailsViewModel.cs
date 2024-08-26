namespace Rengo.Models
{
    public class DepartmentDetailsViewModel
    {
        public Department Department { get; set; }
        public List<Department> ParentDepartments { get; set; }
        public List<Department> SubDepartments { get; set; }
    }
}

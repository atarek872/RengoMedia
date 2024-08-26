using System.ComponentModel.DataAnnotations;

namespace Rengo.Models.ModelView
{
    public class Departments
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]

        public string Name { get; set; }

        public string LogoPath { get; set; }

        public int? ParentDepartmentId { get; set; } // Nullable foreign key
    }
}

namespace EfficiencySystem.Models.ViewModels
{
    public class StaffViewModel
    {
        public int DepartmentId {  get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Staff> Staff { get; set; }
    }
}

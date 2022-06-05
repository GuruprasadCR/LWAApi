namespace LWAApi.Models
{
    public class DepartmentStudentsResponse
    {
        public Department department { get; set; }
        public List<Student> students { get; set; }
    }
}

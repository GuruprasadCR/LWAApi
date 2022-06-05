using LWAApi.Models;

namespace LWAApi.Repositories.Contracts
{
    public interface IStudentrepo 
    {

        public Student GetStudent (int StudentID);
        public List<Student> GetStudents();

        bool DoesUserEixsts(int StudentID);
    }
}

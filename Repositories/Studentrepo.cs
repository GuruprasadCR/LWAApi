using LWAApi.Models;
using LWAApi.Repositories.Contracts;
using System.Linq;

namespace LWAApi.Repositories
{
    public class Studentrepo : IStudentrepo
    {

        readonly DatabaseContext _dbContext = new();

        public Studentrepo(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Student> GetStudents()
        {
            try
            {

                //var stud = from S in _dbContext.Student
                //           join D in _dbContext.Department on S.Studentdepartment equals D.DepID
                //           select new
                //           { StudentName = S.Studentname,DepartmentName = D.Depname  };
                              



                //return (List<Student>)stud;

                return _dbContext.Student.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Obsolete("old")]
        public Student GetStudent(int StudentID)
        {
            try
            {
                Student? student = _dbContext.Student.Find(StudentID);
                if (student != null)
                {
                    var stud = from S in _dbContext.Student
                               join D in _dbContext.Department on S.Studentdepartment equals D.DepID
                               select new
                               {
                                   StudentName = S.Studentname,
                                   DepartmentName = D.Depname
                               };

                    return student;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }


        public bool DoesUserEixsts(int StudentID)
        {
            return _dbContext.Student.Any(e => e.StudentID == StudentID);
        }

    }
}

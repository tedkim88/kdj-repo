using RMSSystem.DAL;
using RMSSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSystem.BLL
{
    public class StudentService
    {

        private readonly RMSContext _rentalContext;


        internal StudentService(RMSContext rentalContext)
        {
            
            _rentalContext = rentalContext;
        }

        public StudentView getStudentView(int studentNumber)
        {

            return _rentalContext.Students.Where(x => x.RemoveFromViewFlag == false &&
                                               x.StudentNumber == studentNumber)
                .Select(x => new StudentView
                {

                    StudentNumber = x.StudentNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DisplayName = x.DisplayName,

                    RemoveFromViewFlag = x.RemoveFromViewFlag

                })
            .FirstOrDefault();
        }

        public List<StudentView> getStudents(string lastname, List<RenterView> renters)
        {
            if (string.IsNullOrWhiteSpace(lastname))
            {
                throw new ArgumentNullException("last name is required.");
            }

            var studentNumbers = renters.Select(a => a.StudentNumber).ToList();

            return _rentalContext.Students.Where(x => x.LastName.Contains(lastname)
                                                    && x.RemoveFromViewFlag == false
                                                    && !studentNumbers.Contains(x.StudentNumber))
                                            .Select(x => new StudentView
                                            {
                                                StudentNumber = x.StudentNumber,
                                                FirstName = x.FirstName,
                                                LastName = x.LastName
                                            })
                                            .ToList();
        }


       



    }
}

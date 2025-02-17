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
            //Initialize the _hogWildContext field with the provided HogWildContext instance
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


        //public RenterView getStudentAsRenter (int studentNumber)
        //{
        //    if (studentNumber < 0)
        //    {
        //        throw new ArgumentNullException("Invalid student Number.");
        //    }

        //    var matchingStudent = _rentalContext.Students.Where(x => x.StudentNumber == studentNumber &&
        //                                                            x.RemoveFromViewFlag == false)
        //                                                            .FirstOrDefault();

        //    if (matchingStudent == null)
        //    {
        //        throw new ArgumentNullException("There is no valid matching student.");
        //    }

        ////이부분에서 .Renters에서  파일을가져오려다보니 student정보에는 있는데 renter에는 등록이안되어있는 애들이 있어서 null이 return되서 UI렌더링에서 오류가났음.
        ///모든 스튜던트가 렌터에 등록되어있는게 아님..렌터에 등록하려면 뉴렌탈로 가야하는메커니즘인가?
        //    return _rentalContext.Students.Where(x => x.StudentNumber == studentNumber &&
        //                                             x.RemoveFromViewFlag == false)
        //                                    .Select(x => new RenterView
        //                                    {
        //                                        RenterID = x.RenterID,
        //                                        StudentNumber = x.StudentNumber,
        //                                        StudentName = x.StudentNumberNavigation.FirstName + " " + x.StudentNumberNavigation.LastName,
        //                                        RentalID = x.RentalID,
        //                                        EffectiveDate = x.EffectiveDate,
        //                                        Active = x.Active,
        //                                        EmergencyContactName = x.EmergencyContactName,
        //                                        EmergencyContactNumber = x.EmergencyContactNumber,
        //                                        RemoveFromViewFlag = x.RemoveFromViewFlag
        //                                    })
        //                                    .FirstOrDefault();
        //}





    }
}

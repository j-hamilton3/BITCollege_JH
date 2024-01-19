/*
 * Name: James Hamilton
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: November 10, 2023
 * Updated: November 12, 2023
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.DynamicData;
using BITCollege_JH;
using BITCollege_JH.Data;
using BITCollege_JH.Models;
using Utility;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CollegeRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CollegeRegistration.svc or CollegeRegistration.svc.cs at the Solution Explorer and start debugging.
    
    /// <summary>
    /// A web service to support the college registration functionality.
    /// </summary>
    public class CollegeRegistration : ICollegeRegistration
    {
        // Instance of data context object.
        protected BITCollege_JHContext db = new BITCollege_JHContext();

        /// <summary>
        /// Drops the course based on the selected registration.
        /// </summary>
        /// <param name="registrationId">The registration id for the course to drop.</param>
        /// <returns>True, if the course was dropped. False if an error occurs. </returns>
        public bool DropCourse(int registrationId)
        {
             // Remove the record from the DB and persist the change.
            try
            {
                // Retrieve the record that corresponds to the registrationId argument.
                BITCollege_JH.Models.Registration record = (from Results in db.Registrations
                                                            where Results.RegistrationId == registrationId
                                                            select Results).SingleOrDefault();

                db.Registrations.Remove(record);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + ": Exception Occurred");
                return false;
            }
        }


        /// <summary>
        /// Registers a student for a course.
        /// </summary>
        /// <param name="studentId">The student id of the student to be registered.</param>
        /// <param name="courseId">The course id of the course to be registered.</param>
        /// <param name="notes">Additional notes for the registration.</param>
        /// <returns>A return code that corresponds to multiple registration criteria.</returns>
        public int RegisterCourse(int studentId, int courseId, string notes)
        {
            int returnCode = 0;

            try
            {
               // Retrieve all records from the Registrations table involving the studentId and courseId.
                IQueryable<Registration> allRegistrations = from Results in db.Registrations
                                                            where Results.StudentId == studentId
                                                            where Results.CourseId == courseId
                                                            select Results;

                // Retrieve the corresponding Course record.
                BITCollege_JH.Models.Course course = (from Results in db.Courses
                                                      where Results.CourseId == courseId
                                                      select Results).SingleOrDefault();

                // Retrieve the corresponding Student record.
                BITCollege_JH.Models.Student student = (from Results in db.Students
                                                        where Results.StudentId == studentId
                                                        select Results).SingleOrDefault();

                // Search for registrations that have a null grade.
                IEnumerable<Registration> nullRecords = from Results in allRegistrations
                                                        where Results.Grade == null
                                                        select Results;

                // If there is a null grade -> set the return code to -100.
                if (nullRecords.Count() > 0)
                {
                    returnCode = -100;
                }

                // Check if the course is a MasteryCourse.
                if (course != null && Utility.BusinessRules.CourseTypeLookup(course.CourseType) == CourseType.MASTERY)
                {
                    // Cast the course query to be of MasteryCourse type.
                    BITCollege_JH.Models.MasteryCourse masteryCourse = (MasteryCourse)course;

                    int maximumAttempts = masteryCourse.MaximumAttempts;

                    // Search for registrations that do not have a null grade.
                    IEnumerable<Registration> notNullRecords = from Results in allRegistrations
                                                               where Results.Grade != null
                                                               select Results;

                    // Check if the number of registrations is greater than or equal to MaximumAttempts value.
                    if (notNullRecords.Count() >= maximumAttempts)
                    {
                        returnCode = -200;
                    }
                }

                // If the returnCode is still 0...
                if (returnCode == 0)
                {
                    // Create a new Registration object.
                    BITCollege_JH.Models.Registration newRegistration = new Registration();

                    // Set the Registration's properties.
                    newRegistration.StudentId = studentId;
                    newRegistration.CourseId = courseId;
                    newRegistration.Notes = notes;
                    newRegistration.RegistrationDate = DateTime.Today;

                    // Use predefined functionality to generate the RegistrationNumber.
                    newRegistration.SetNextRegistrationNumber();

                    // Add and persist the new Registration to the database.
                    db.Registrations.Add(newRegistration);
                    db.SaveChanges();
                   
                    // Determine the tuition amount of the course.
                    double tuitionAmount = course.TuitionAmount;

                    // Get the student's Grade Point State.
                    GradePointState state = student.GradePointState;

                    // Set the student's adjusted outstanding fees.
                    student.OutstandingFees += tuitionAmount * state.TuitionRateAdjustment(student);

                    // Persist the change to the Database.
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                returnCode = -300;
                Console.WriteLine(ex + ": Exception Occurred");
            }
                
            return returnCode;
        }

        /// <summary>
        /// Updates the grade for a registration.
        /// </summary>
        /// <param name="grade">The new grade to be updated.</param>
        /// <param name="registrationId">The id of the registration to be updated.</param>
        /// <param name="notes">Additional notes for the registration.</param>
        /// <returns>The student's grade point average.</returns>
        public double? UpdateGrade(double grade, int registrationId, string notes)
        {
            // Retrieve the Registration record.
            BITCollege_JH.Models.Registration registration = (from Results in db.Registrations
                                                              where Results.RegistrationId == registrationId
                                                              select Results).SingleOrDefault();

            // Set the property values.
            registration.Grade = grade;
            registration.Notes = notes;

            // Persist the updated grade to the database.
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + ": Exception Occurred");
            }

            // Call the CalculateGradePointAverage method.
            double? gradePointAverage = CalculateGradePointAverage(registration.StudentId);

            return gradePointAverage;
        }

        /// <summary>
        /// Calculates a student's grade point average.
        /// </summary>
        /// <param name="studentId">The student id of the selected student.</param>
        /// <returns>The calculated grade point average for the student.</returns>
        private double? CalculateGradePointAverage(int studentId)
        {
            // Search for registrations that do not have a null grade and match the Student.
            IQueryable<Registration> notNullRecords = from Results in db.Registrations
                                                      where Results.Grade != null
                                                      where Results.StudentId == studentId
                                                      select Results;

            double totalCreditHours = 0;
            double totalGradePointValue = 0;
            double? calculatedGradePointAverage;


            // Iterate through the result set.
            foreach (Registration record in notNullRecords.ToList())
            {
                // Obtain the grade for the registration.
                double grade = (double)record.Grade;

                // Determine the Course Type for the registration.
                CourseType courseType = Utility.BusinessRules.CourseTypeLookup(record.Course.CourseType);

                // Exclude any Audit Courses from the GPA calculation.
                if (courseType != CourseType.AUDIT)
                {
                    // Use the Course Type and grade to determine the Grade Point Value for the grade.
                    double gradePoint = Utility.BusinessRules.GradeLookup(grade, courseType);

                    // Multiply each registration's GradePointValue by the Course's CreditHours.
                    double gradePointValue = gradePoint * record.Course.CreditHours;

                    // Accumulate all registrations to TotalGradePointValue.
                    totalGradePointValue += gradePointValue;

                    // Accumulate all registrations to TotalCreditHours.
                    totalCreditHours += record.Course.CreditHours;
                }

            }

            // If the Total Credit Hours value is 0 following the above iterations...
            if (totalCreditHours == 0)
            {
                calculatedGradePointAverage = null;
            }
            else
            {
                calculatedGradePointAverage = totalGradePointValue / totalCreditHours;
            }

            // Obtain the Student record to which the calculatedGradePointAverage applies.
            BITCollege_JH.Models.Student student = (from Results in db.Students
                                                    where Results.StudentId == studentId
                                                    select Results).Single();

            // Set the GradePointAverage property to the calculatedGradePointAverage.
            student.GradePointAverage = calculatedGradePointAverage;

            // Ensure that the student is in the appropriate GradePointState***
            student.ChangeState();

            // Persist the change.
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + ": Exception Occurred");
            }

            // Return the calculated Grade Point Average.
            return calculatedGradePointAverage;
        }
    }
}

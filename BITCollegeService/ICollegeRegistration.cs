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
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BITCollege_JH;
using Utility;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollegeRegistration" in both code and config file together.
    
    /// <summary>
    /// A web service to support the college registration functionality.
    /// </summary>
    [ServiceContract]
    public interface ICollegeRegistration
    {
        /// <summary>
        /// Drops the course based on the selected registration.
        /// </summary>
        /// <param name="registrationId">The registration id for the course to drop.</param>
        /// <returns>True, if the course was dropped. False if an error occurs.</returns>
        [OperationContract]
        bool DropCourse(int registrationId);

        /// <summary>
        /// Registers a student for a course.
        /// </summary>
        /// <param name="studentId">The student id of the student to be registered.</param>
        /// <param name="courseId">The course id of the course to be registered.</param>
        /// <param name="notes">Additional notes for the registration.</param>
        /// <returns>A return code that corresponds to multiple registration criteria.</returns>
        [OperationContract]
        int RegisterCourse(int studentId, int courseId, String notes);

        /// <summary>
        /// Updates the grade for a registration.
        /// </summary>
        /// <param name="grade">The new grade to be updated.</param>
        /// <param name="registrationId">The id of the registration to be updated.</param>
        /// <param name="notes">Additional notes for the registration.</param>
        /// <returns>The student's grade point average.</returns>
        [OperationContract]
        double? UpdateGrade(double grade, int registrationId, String notes);
    }
}

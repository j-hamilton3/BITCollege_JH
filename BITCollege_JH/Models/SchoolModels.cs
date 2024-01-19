/*
 * Name: James Hamilton
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: September 05, 2023
 * Updated: October 15th, 2023
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;
using BITCollege_JH.Data;
using Microsoft.Ajax.Utilities;
using System.Security.Principal;

namespace BITCollege_JH.Models
{
    /// <summary>
    /// Student Model - to represent the Students table in the database.
    /// </summary>
    public class Student
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("GradePointState")]
        public int GradePointStateId { get; set; }

        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }

        [Required]
        [Display(Name = "First\nName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last\nName")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK|YT)", ErrorMessage = "Please enter a valid Canadian province code.")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Grade\nPoint\nAverage")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Range(0, 4.5)]
        public double? GradePointAverage { get; set; }

        [Required]
        [Display(Name = "Fees")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double OutstandingFees { get; set; }

        public string Notes { get; set; }

        [Display(Name = "Name")]
        public string FullName 
        {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return String.Format("{0} {1} {2}", this.Address, this.City, this.Province); 
            }
        }

        // Navigational properties
        public virtual GradePointState GradePointState { get; set; }

        public virtual AcademicProgram AcademicProgram { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }

        // Private instance of the Data Context Class.
        private BITCollege_JHContext db = new BITCollege_JHContext();

        /// <summary>
        /// Associates a Student with its correct state.
        /// </summary>
        public void ChangeState()
        {
            // No If Statements!

            GradePointState before = db.GradePointStates.Find(this.GradePointStateId);

            int after = 0;

            while (before.GradePointStateId != after)
            {
                before.StateChangeCheck(this);

                after = before.GradePointStateId;

                before = db.GradePointStates.Find(this.GradePointStateId);
            }
        }

        /// <summary>
        /// Sets the next student number.
        /// </summary>
        public void SetNextStudentNumber()
        {
            this.StudentNumber = (long)Utility.StoredProcedure.NextNumber("NextStudent");
        }
    }

    /// <summary>
    /// AcademicProgram Model - to represent the AcademicPrograms table in the database.
    /// </summary>
    public class AcademicProgram
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        [Required]
        [Display(Name = "Program")]
        public string ProgramAcronym { get; set; }

        [Required]
        [Display(Name = "Program\nName")]
        public string Description { get; set; }

        // Navigational properties
        public virtual ICollection<Student> Student { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }

    /// <summary>
    /// GradePointState Model - to represent the GradePointStates table in the database.
    /// </summary>
    public abstract class GradePointState
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GradePointStateId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Lower\nLimit")]
        public double LowerLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Upper\nLimit")]
        public double UpperLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Tuition\nRate\nFactor")]
        public double TuitionRateFactor { get; set; }

        [Display(Name = "State")]
        public string Description
        {
            get
            {
                return BusinessRules.ParseString(GetType().Name, "State");
            }
        }

        // Navigational Properties
        public virtual ICollection<Student> Student { get; set; }

        // Abstract Methods

        /// <summary>
        /// Adjusts the TuitionRate payable for a student based on their GradePointAverage.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        /// <returns> The adjusted tuition rate.</returns>
        public abstract double TuitionRateAdjustment(Student student);


        /// <summary>
        /// Handles the change from one GradePointState to another.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        public abstract void StateChangeCheck(Student student);

        // Data Context Object
        protected static BITCollege_JHContext db = new BITCollege_JHContext();
    }

    /// <summary>
    /// SuspendedState Model - inherits from GradePointState.
    /// </summary>
    public class SuspendedState : GradePointState
    {
        private static SuspendedState suspendedState;

        /// <summary>
        /// Initializes an instance of SuspendedState, with the LowerLimit, UpperLimit and
        /// TuitionRateFactor set.
        /// </summary>
        private SuspendedState()
        {
            this.LowerLimit = 0.00;
            this.UpperLimit = 1.00;
            this.TuitionRateFactor = 1.1;
        }

        /// <summary>
        /// Returns the one and only object of the SuspendedState class. 
        /// </summary>
        /// <returns> The one and only object of the SuspendedState class. </returns>
        public static SuspendedState GetInstance()
        {
            if (suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();

                if (suspendedState == null)
                {
                    suspendedState = new SuspendedState();

                    db.SuspendedStates.Add(suspendedState);

                    db.SaveChanges();
                }
            }

            return suspendedState;
        }

        /// <summary>
        /// Adjusts the TuitionRate payable for a student based on their GradePointAverage.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        /// <returns> The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double localValue = 1.1;

            if (student.GradePointAverage < .75 && student.GradePointAverage >= .5)
            {
                localValue += .02;
            }
            if (student.GradePointAverage < .5)
            {
                localValue += .05;
            }

            return localValue;
        }

        /// <summary>
        /// Handles the change from one GradePointState to another.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        public override void StateChangeCheck(Student student)
        {
           if (student.GradePointAverage > 1)
           {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
           }
        }
    }

    /// <summary>
    /// ProbationState Model - inherits from GradePointState.
    /// </summary>
    public class ProbationState : GradePointState
    {
        private static ProbationState probationState;

        /// <summary>
        /// Initializes an instance of ProbationState, with the LowerLimit, UpperLimit and
        /// TuitionRateFactor set.
        /// </summary>
        private ProbationState()
        {
            this.LowerLimit = 1.00;
            this.UpperLimit = 2.00;
            this.TuitionRateFactor = 1.075;
        }

        /// <summary>
        /// Returns the one and only object of the ProbationState class. 
        /// </summary>
        /// <returns> The one and only object of the ProbationState class. </returns>
        public static ProbationState GetInstance()
        {
            if (probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();

                if (probationState == null)
                {
                    probationState = new ProbationState();

                    db.ProbationStates.Add(probationState);

                    db.SaveChanges();
                }
            }

            return probationState;
        }

        /// <summary>
        /// Adjusts the TuitionRate payable for a student based on their GradePointAverage.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        /// <returns> The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double localValue = 1.075;

            // To find out if a student has completed more than one course.
            IQueryable<Registration> studentCourses = db.Registrations.Where(x => x.StudentId == student.StudentId
                                                      && x.Grade != null);

            int courseCount = studentCourses.Count();

            if (courseCount >= 5)
            {
                localValue = 1.035;
            }

            return localValue;
        }

        /// <summary>
        /// Handles the change from one GradePointState to another.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        public override void StateChangeCheck(Student student)
        {
           if (student.GradePointAverage < 1)
           {
                student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId;
           }
           else if (student.GradePointAverage > 2)
           {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
           }
        }
    }

    /// <summary>
    /// RegularState Model - inherits from GradePointState.
    /// </summary>
    public class RegularState : GradePointState
    {
        private static RegularState regularState;

        /// <summary>
        /// Initializes an instance of RegularState, with the LowerLimit, UpperLimit and
        /// TuitionRateFactor set.
        /// </summary>
        private RegularState()
        {
            this.LowerLimit = 2.00;
            this.UpperLimit = 3.70;
            this.TuitionRateFactor = 1.0;
        }

        /// <summary>
        /// Returns the one and only object of the RegularState class. 
        /// </summary>
        /// <returns> The one and only object of the RegularState class. </returns>
        public static RegularState GetInstance()
        {
            if (regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();

                if (regularState == null)
                {
                    regularState = new RegularState();

                    db.RegularStates.Add(regularState);

                    db.SaveChanges();
                }
            }

            return regularState;
        }

        /// <summary>
        /// Adjusts the TuitionRate payable for a student based on their GradePointAverage.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        /// <returns> The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double localValue = 1;

            return localValue;
        }

        /// <summary>
        /// Handles the change from one GradePointState to another.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        public override void StateChangeCheck(Student student)
        {
           if (student.GradePointAverage < 2)
           {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
           }
           else if (student.GradePointAverage > 3.7)
           {
                student.GradePointStateId = HonoursState.GetInstance().GradePointStateId;
           }
        }
    }

    /// <summary>
    /// HonoursState Model - inherits from GradePointState.
    /// </summary>
    public class HonoursState : GradePointState
    {
        private static HonoursState honoursState;

        /// <summary>
        /// Initializes an instance of HonoursState, with the LowerLimit, UpperLimit and
        /// TuitionRateFactor set.
        /// </summary>
        private HonoursState()
        {
            this.LowerLimit = 3.70;
            this.UpperLimit = 4.50;
            this.TuitionRateFactor = 0.9;
        }

        /// <summary>
        /// Returns the one and only object of the HonoursState class. 
        /// </summary>
        /// <returns> The one and only object of the HonoursState class. </returns>
        public static HonoursState GetInstance()
        {
            if (honoursState == null)
            {
                honoursState = db.HonoursStates.SingleOrDefault();

                if (honoursState == null)
                {
                    honoursState = new HonoursState();

                    db.HonoursStates.Add(honoursState);

                    db.SaveChanges();
                }
            }

            return honoursState;
        }

        /// <summary>
        /// Adjusts the TuitionRate payable for a student based on their GradePointAverage.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        /// <returns> The adjusted tuition rate.</returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double localValue = 0.90;

            // To find out if a student has completed more than one course.
            IQueryable<Registration> studentCourses = db.Registrations.Where(x => x.StudentId == student.StudentId
                                                      && x.Grade != null);

            int courseCount = studentCourses.Count();

            if (courseCount >= 5)
            {
                localValue -= 0.05;
            }

            if (student.GradePointAverage > 4.25)
            {
                localValue -= 0.02;
            }


            return localValue;
        }

        /// <summary>
        /// Handles the change from one GradePointState to another.
        /// </summary>
        /// <param name="student"> A Student object.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < 3.7)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            }
        }
    }

    /// <summary>
    /// Course Model - to represent the Courses table in the database.
    /// </summary>
    public abstract class Course
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Display(Name = "Course\nNumber")]
        public string CourseNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Credit\nHours")]
        public double CreditHours { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Tuition")]
        public double TuitionAmount { get; set; }

        [Display(Name = "Course\nType")]
        public string CourseType
        {
            get
            {
                return BusinessRules.ParseString(GetType().Name, "Course"); 
            }
        }

        public string Notes { get; set; }

        // Navigational Properties
        public virtual ICollection<Registration> Registration { get; set; }

        public virtual AcademicProgram AcademicProgram { get; set; }

        /// <summary>
        /// Sets the next course number.
        /// </summary>
        public abstract void SetNextCourseNumber();
    }

    /// <summary>
    /// GradedCourse Model - inherits from Course.
    /// </summary>
    public class GradedCourse : Course
    {
        [Required]
        [Display(Name = "Assignments")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double AssignmentWeight { get; set; }

        [Required]
        [Display(Name = "Exams")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double ExamWeight { get; set; }

        /// <summary>
        /// Sets the next course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            this.CourseNumber = "G-" + Utility.StoredProcedure.NextNumber("NextGradedCourse");
        }
    }

    /// <summary>
    /// MasteryCourse Model - inherits from Course.
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }

        /// <summary>
        /// Sets the next course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            this.CourseNumber = "M-" + Utility.StoredProcedure.NextNumber("NextMasteryCourse");
        }
    }
    
    /// <summary>
    /// AuditCourse Model - inherits from Course.
    /// </summary>
    public class AuditCourse : Course
    {
        /// <summary>
        /// Sets the next course number.
        /// </summary>
        public override void SetNextCourseNumber()
        {
            this.CourseNumber = "A-" + Utility.StoredProcedure.NextNumber("NextAuditCourse");
        }
    }

    /// <summary>
    /// Registration Model - to represent the Registrations table in the database.
    /// </summary>
    public class Registration
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Display(Name = "Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }

        [DisplayFormat(NullDisplayText = "Ungraded")]
        [Range(0, 1)]
        public double? Grade { get; set; }

        public string Notes { get; set; }

        // Navigational properties
        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }

        /// <summary>
        /// Sets the next registration number.
        /// </summary>
        public void SetNextRegistrationNumber()
        {
            this.RegistrationNumber = (long)Utility.StoredProcedure.NextNumber("NextRegistration");
        }
    }

    /// <summary>
    /// NextUniqueNumber Model - to represent the NextUniqueNumber table in the database.
    /// </summary>
    public abstract class NextUniqueNumber
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextUniqueNumberId { get; set; }

        [Required]
        public long NextAvailableNumber { get; set; }

        // Instance of data context object.
        protected static BITCollege_JHContext db = new BITCollege_JHContext();
    }

    /// <summary>
    /// NextStudent Model - inherits from NextUniqueNumber.
    /// </summary>
    public class NextStudent : NextUniqueNumber
    {
        private static NextStudent nextStudent;

        /// <summary>
        /// Creates an instance of the NextStudent class.
        /// </summary>
        private NextStudent()
        {
            this.NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// Returns the one and only object of the NextStudent class. 
        /// </summary>
        /// <returns> The one and only object of the NextStudent class.</returns>
        public static NextStudent GetInstance()
        {
            if (nextStudent == null)
            {
                nextStudent = db.NextStudents.SingleOrDefault();

                if (nextStudent == null)
                {
                    nextStudent = new NextStudent();

                    db.NextStudents.Add(nextStudent);

                    db.SaveChanges();
                }
            }

            return nextStudent;
        }
    }

    /// <summary>
    /// NextRegistration Model - inherits from NextUniqueNumber.
    /// </summary>
    public class NextRegistration : NextUniqueNumber
    {
        private static NextRegistration nextRegistration;

        /// <summary>
        /// Creates an instance of the NextRegistration class.
        /// </summary>
        private NextRegistration()
        {
            this.NextAvailableNumber = 700;
        }
        
        /// <summary>
        /// Returns the one and only object of the NextRegistration class. 
        /// </summary>
        /// <returns> The one and only object of the NextRegistration class.</returns>
        public static NextRegistration GetInstance()
        {
            if (nextRegistration == null)
            {
                nextRegistration = db.NextRegistrations.SingleOrDefault();

                if (nextRegistration == null)
                {
                    nextRegistration = new NextRegistration();

                    db.NextRegistrations.Add(nextRegistration);

                    db.SaveChanges();
                }
            }

            return nextRegistration;
        }
    }

    /// <summary>
    /// NextGradedCourse Model - inherits from NextUniqueNumber.
    /// </summary>
    public class NextGradedCourse : NextUniqueNumber
    {
        private static NextGradedCourse nextGradedCourse;

        /// <summary>
        /// Creates an instance of the NextGradedCourse class.
        /// </summary>
        private NextGradedCourse()
        {
            this.NextAvailableNumber = 200000;
        }

        /// <summary>
        /// Returns the one and only object of the NextGradedCourse class. 
        /// </summary>
        /// <returns> The one and only object of the NextGradedCourse class.</returns>
        public static NextGradedCourse GetInstance()
        {
            if (nextGradedCourse == null)
            {
                nextGradedCourse = db.NextGradedCourses.SingleOrDefault();

                if (nextGradedCourse == null)
                {
                    nextGradedCourse = new NextGradedCourse();

                    db.NextGradedCourses.Add(nextGradedCourse);

                    db.SaveChanges();
                }
            }

            return nextGradedCourse;
        }
    }

    /// <summary>
    /// NextAuditCourse Model - inherits from NextUniqueNumber.
    /// </summary>
    public class NextAuditCourse : NextUniqueNumber
    {
        private static NextAuditCourse nextAuditCourse;

        /// <summary>
        /// Creates an instance of the NextAuditCourse class.
        /// </summary>
        private NextAuditCourse()
        {
            this.NextAvailableNumber = 2000;
        }

        /// <summary>
        /// Returns the one and only object of the NextAuditCourse class. 
        /// </summary>
        /// <returns> The one and only object of the NextAuditCourse class.</returns>
        public static NextAuditCourse GetInstance()
        {
            if (nextAuditCourse == null)
            {
                nextAuditCourse = db.NextAuditCourses.SingleOrDefault();

                if (nextAuditCourse == null)
                {
                    nextAuditCourse = new NextAuditCourse();

                    db.NextAuditCourses.Add(nextAuditCourse);

                    db.SaveChanges();
                }
            }

            return nextAuditCourse;
        }
    }

    /// <summary>
    /// NextMasteryCourse Model - inherits from NextUniqueNumber.
    /// </summary>
    public class NextMasteryCourse : NextUniqueNumber
    {
        private static NextMasteryCourse nextMasteryCourse;

        /// <summary>
        /// Creates an instance of the NextMasteryCourse class.
        /// </summary>
        private NextMasteryCourse()
        {
            this.NextAvailableNumber = 20000;
        }

        /// <summary>
        /// Returns the one and only object of the NextMasteryCourse class. 
        /// </summary>
        /// <returns> The one and only object of the NextMasteryCourse class.</returns>
        public static NextMasteryCourse GetInstance()
        {
            if (nextMasteryCourse == null)
            {
                nextMasteryCourse = db.NextMasteryCourses.SingleOrDefault();

                if (nextMasteryCourse == null)
                {
                    nextMasteryCourse = new NextMasteryCourse();

                    db.NextMasteryCourses.Add(nextMasteryCourse);

                    db.SaveChanges();
                }
            }

            return nextMasteryCourse;
        }
    }
}
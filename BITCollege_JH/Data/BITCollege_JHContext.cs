using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_JH.Data
{
    public class BITCollege_JHContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_JHContext() : base("name=BITCollege_JHContext")
        {
        }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.AcademicProgram> AcademicPrograms { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.GradePointState> GradePointStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.ProbationState> ProbationStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.HonoursState> HonoursStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.SuspendedState> SuspendedStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.RegularState> RegularStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.GradedCourse> GradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.AuditCourse> AuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.MasteryCourse> MasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.NextUniqueNumber> NextUniqueNumbers { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.NextAuditCourse> NextAuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.NextGradedCourse> NextGradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.NextMasteryCourse> NextMasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.NextRegistration> NextRegistrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_JH.Models.NextStudent> NextStudents { get; set; }
    }
}

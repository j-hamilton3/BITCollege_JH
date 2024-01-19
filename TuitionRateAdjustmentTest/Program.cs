using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BITCollege_JH.Data;
using BITCollege_JH.Models;
using Utility;


namespace TuitionRateAdjustmentTest
{
    public class Program
    {
        private static BITCollege_JHContext db = new BITCollege_JHContext();

        static void Main(string[] args)
        {
            Suspended_State_Newly_Registered_44();
            Suspended_State_Newly_Registered_60();
            Suspended_State_Newly_Registered_80();

            Probation_State_Newly_Registered_3_Courses();
            Probation_State_Newly_Registered_7_Courses();

            Regular_State_Newly_Registered_250();

            Honours_State_Newly_Registered_3_Courses();
            Honours_State_Newly_Registered_4_Courses();
            Honours_State_Newly_Registered_7_Courses_440();
            Honours_State_Newly_Registered_7_Courses_410();
        }

        static void Suspended_State_Newly_Registered_44()
        {
            // Set up the Student.
            Student student = db.Students.Find(1);
            student.GradePointAverage = .44;
            student.GradePointStateId = 1;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("SuspendedState Tests: \n");

            Console.WriteLine("Expected: 1150");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Suspended_State_Newly_Registered_60()
        {
            // Set up the Student.
            Student student = db.Students.Find(1);
            student.GradePointAverage = .60;
            student.GradePointStateId = 1;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("Expected: 1120");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Suspended_State_Newly_Registered_80()
        {
            // Set up the Student.
            Student student = db.Students.Find(1);
            student.GradePointAverage = .80;
            student.GradePointStateId = 1;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("Expected: 1100");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Probation_State_Newly_Registered_3_Courses()
        {
            // Set up the Student.
            Student student = db.Students.Find(4);
            student.GradePointAverage = 1.15;
            student.GradePointStateId = 2;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("\nProbationState Tests: \n");

            Console.WriteLine("Expected: 1075");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Probation_State_Newly_Registered_7_Courses()
        {
            // Set up the Student.
            Student student = db.Students.Find(5);
            student.GradePointAverage = 1.15;
            student.GradePointStateId = 2;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("Expected: 1035");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Regular_State_Newly_Registered_250()
        {
            // Set up the Student.
            Student student = db.Students.Find(1);
            student.GradePointAverage = 2.50;
            student.GradePointStateId = 3;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("\nRegularState Tests: \n");

            Console.WriteLine("Expected: 1000");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Honours_State_Newly_Registered_3_Courses()
        {
            // Set up the Student.
            Student student = db.Students.Find(4);
            student.GradePointAverage = 3.9;
            student.GradePointStateId = 4;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("\nHonoursState Tests: \n");

            Console.WriteLine("Expected: 900");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Honours_State_Newly_Registered_4_Courses()
        {
            // Set up the Student.
            Student student = db.Students.Find(6);
            student.GradePointAverage = 4.27;
            student.GradePointStateId = 4;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("Expected: 880");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Honours_State_Newly_Registered_7_Courses_440()
        {
            // Set up the Student.
            Student student = db.Students.Find(5);
            student.GradePointAverage = 4.40;
            student.GradePointStateId = 4;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("Expected: 830");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void Honours_State_Newly_Registered_7_Courses_410()
        {
            // Set up the Student.
            Student student = db.Students.Find(5);
            student.GradePointAverage = 4.10;
            student.GradePointStateId = 4;
            db.SaveChanges();

            // Get an instance of the student's state.
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment.
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            Console.WriteLine("Expected: 850");
            Console.WriteLine("Actual: " + tuitionRate);

            Console.ReadLine();
        }
    }
}

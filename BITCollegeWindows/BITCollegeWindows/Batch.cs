/*
 * Name: James Hamilton
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: December 10, 2023
 * Updated: December 12, 2023
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using BITCollege_JH.Models;
using BITCollege_JH.Data;
using System.Globalization;
using System.Security.Cryptography;

namespace BITCollegeWindows
{
    /// <summary>
    /// Batch:  This class provides functionality that will validate
    /// and process incoming XML files.
    /// </summary>
    public class Batch
    {
        protected BITCollege_JHContext db = new BITCollege_JHContext();
        
        private String inputFileName;
        private String logFileName;
        private String logData;

        /// <summary>
        /// Processes all detail errors found within the current file being processed.
        /// </summary>
        /// <param name="beforeQuery">The previous query result set.</param>
        /// <param name="afterQuery">The current query result set.</param>
        /// <param name="message">The error message.</param>
        private void ProcessErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, String message)
        {
            IEnumerable<XElement> errors = beforeQuery.Except(afterQuery);

            foreach (XElement item in errors)
            {
                logData += "\r\n----------ERROR----------";
                logData += "\r\nFile: " + inputFileName;
                logData += "\r\nProgram: " + item.Element("program");
                logData += "\r\nStudent Number: " + item.Element("student_no");
                logData += "\r\nCourse Number: " + item.Element("course_no");
                logData += "\r\nRegistration Number: " + item.Element("registration_no");
                logData += "\r\nType: " + item.Element("type");
                logData += "\r\nGrade: " + item.Element("grade");
                logData += "\r\nNotes: " + item.Element("notes");
                logData += "\r\nNode Count: " + item.Nodes().Count();
                logData += "\r\nError Message: " + message;
                logData += "\r\n_________________________";
            }
        }

        /// <summary>
        /// Used to verify the attributes of the XML file's root element.
        /// </summary>
        private void ProcessHeader()
        {
            // Define an XDocument and populate it with the XML.
            XDocument xDocument = XDocument.Load(inputFileName);

            // Define an XElement object, populate it with data from the root element.
            XElement root = xDocument.Element("student_update");

            // Check for 3 attributes.
            if (root.Attributes().Count() != 3)
            {
                throw new Exception(String.Format("Incorrect number of root attributes for file {0}\n", inputFileName));
            }

            // Check for correct date.
            if (!DateTime.Parse(root.Attribute("date").Value).Equals(DateTime.Today))
            {
                throw new Exception(String.Format("Incorrect date attribute for file {0}\n", inputFileName));
            }

            // Grab the acronym from the header attributes.
            string acronym = root.Attribute("program").Value;

            // Check if the acronym is in the AcademicPrograms table.
            BITCollege_JH.Models.AcademicProgram program = (from Results in db.AcademicPrograms
                                                            where Results.ProgramAcronym == acronym
                                                            select Results).SingleOrDefault();

            // If no matching program is retrieved...
            if (program == null)
            {
                throw new Exception(String.Format("Incorrect program attribute for file {0}\n", inputFileName));
            }

            // Get the checksum value from the header -> parse as an INT.
            int checksum = Int32.Parse(root.Attribute("checksum").Value);

            // Get the sum of all student_no elements in the file.
            IEnumerable<XElement> allTransactions = xDocument.Descendants("transaction");
            IEnumerable<XElement> allStudentNos = allTransactions.Elements("student_no");

            // Loop through all the student_no's and add to a sum.
            int studentNoSum = 0;
            
            foreach (XElement studentNo in allStudentNos)
            {
                studentNoSum += Int32.Parse(studentNo.Value);
            }

            // Check if the checksum does not match the studentNoSum.
            if (checksum != studentNoSum)
            {
                throw new Exception(String.Format("Incorrect checksum attribute for file {0}\n", inputFileName));
            }
        }

        /// <summary>
        /// Processes and validates the contents of a transmission.
        /// </summary>
        private void ProcessDetails()
        {
            // Define an XDocument and populate it with the XML.
            XDocument xDocument = XDocument.Load(inputFileName);

            // Get the transactions.
            IEnumerable<XElement> transactions = xDocument.Descendants().Elements("transaction");

            // Get transactions that have 7 child elements.
            IEnumerable<XElement> sevenChildElements = transactions.Where(x => x.Nodes().Count() == 7);

            ProcessErrors(transactions, sevenChildElements, "Node count is not 7.");

            // Get transactions that match the program attribute of the root element.
            IEnumerable<XElement> programMatches = sevenChildElements.Where(x => x.Element("program").Value == xDocument.Root.Attribute("program").Value);

            ProcessErrors(sevenChildElements, programMatches, "Transaction program does not match root program.");

            // Get transactions where the type element is numeric.
            IEnumerable<XElement> typeIsNumeric = programMatches.Where(x => Utility.Numeric.IsNumeric(x.Element("type").Value, NumberStyles.Number));

            ProcessErrors(programMatches, typeIsNumeric, "Type element is not numeric.");

            // Get transactions where the grade element is numeric or has the value of *.
            IEnumerable<XElement> gradeIsNumeric = typeIsNumeric.Where(x => Utility.Numeric.IsNumeric(x.Element("grade").Value, NumberStyles.Number) || x.Element("grade").Value == "*");

            ProcessErrors(typeIsNumeric, gradeIsNumeric, "Grade element is not numeric or *.");

            // Get transactions where the type element is a 1 or 2.
            IEnumerable<XElement> typeIsOneOrTwo = gradeIsNumeric.Where(x => x.Element("type").Value == "1" || x.Element("type").Value == "2");

            ProcessErrors(gradeIsNumeric, typeIsOneOrTwo, "Type element must be 1 or 2.");

            // Get transactions that have the correct grade associated with the type. (Ex. Type 1 = Grade * and Type 2 = Grade between 0-100 (inclusive).)
            IEnumerable<XElement> correctGradeForType = typeIsOneOrTwo.Where(x =>
                (x.Element("type").Value == "1" && x.Element("grade").Value == "*") ||
                (x.Element("type").Value == "2" && double.TryParse(x.Element("grade").Value, out double gradeValue) && gradeValue >= 0 && gradeValue <= 100));

            ProcessErrors(typeIsOneOrTwo, correctGradeForType, "Grade is not correctly associated with type.");

            // Get transactions that have a student_no that exists in the DB.

            // Retrieve a list of all student numbers.
            IEnumerable<long> allStudentNumbers = (from Results in db.Students
                                                   select Results.StudentNumber).ToList();

            // Get transactions that student_no exists in the DB.
            IEnumerable<XElement> studentNumberExists = correctGradeForType.Where(x => allStudentNumbers.Contains(long.Parse(x.Element("student_no").Value)));

            ProcessErrors(correctGradeForType, studentNumberExists, "The student_no does not exist in the database.");

            // The course_no must be * for type 2 or the course_no must exist in the DB for type 1.

            // Retrieve a list of all course numbers.
            IEnumerable<string> allCourseNumbers = (from Results in db.Courses
                                                    select Results.CourseNumber).ToList();

            // Get transactions that -> type=2 and course_no=* OR type=1 and course exists in the DB.
            IEnumerable<XElement> correctCourseNo = studentNumberExists.Where(x =>
                (x.Element("type").Value == "2" && x.Element("course_no").Value == "*") ||
                (x.Element("type").Value == "1" && allCourseNumbers.Contains(x.Element("course_no").Value)));

            ProcessErrors(studentNumberExists, correctCourseNo, "The course number is not correct.");

            // Get a list of all registration numbers.
            IEnumerable<long> allRegistrationNumbers = (from Results in db.Registrations
                                                        select Results.RegistrationNumber).ToList();

            // Get transactions that -> type=1 and registration_no=* OR type=2 and registration_no exists in the DB.
            IEnumerable<XElement> correctRegistrationNo = correctCourseNo.Where(x =>
                (x.Element("type").Value == "1" && x.Element("registration_no").Value == "*") ||
                (x.Element("type").Value == "2" && allRegistrationNumbers.Contains(long.Parse(x.Element("registration_no").Value))));

            ProcessErrors(correctCourseNo, correctRegistrationNo, "The registration number is not correct");

            // Call ProcessTransactions, sending in the error free result set.
            ProcessTransactions(correctRegistrationNo);

        }

        /// <summary>
        /// Processes all valid transaction records.
        /// </summary>
        /// <param name="transactionRecords">A valid transaction record.</param>
        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {
            // Iterate through the transactions.
            foreach(XElement transaction in transactionRecords)
            {
                // Call the web service.
                RegistrationService.CollegeRegistrationClient service = new RegistrationService.CollegeRegistrationClient();

                // If the type is 1...
                if (transaction.Element("type").Value == "1")
                {
                    // Parse the studentNumber.
                    long studentNumber = long.Parse(transaction.Element("student_no").Value);
                  
                    //Get the student record, for the studentID.
                    BITCollege_JH.Models.Student student = (from Results in db.Students
                                                            where Results.StudentNumber == studentNumber
                                                            select Results).SingleOrDefault();

                    int studentId = student.StudentId;

                    //get the courseNumber
                    string courseNumber = transaction.Element("course_no").Value;

                    //Get the courseId.
                    BITCollege_JH.Models.Course course = (from Results in db.Courses
                                                         where Results.CourseNumber == courseNumber
                                                         select Results).SingleOrDefault();

                    int courseId = course.CourseId;

                    string notes = transaction.Element("notes").Value;

                    int registerReturnCode = service.RegisterCourse(studentId, courseId ,notes);

                    if (registerReturnCode == 0)
                    {
                        logData += String.Format("Student: {0} has successfully registered for course: {1}\n", student.StudentNumber,
                                                                                                             course.CourseNumber);
                    }
                    else
                    {
                        logData += Utility.BusinessRules.RegisterError(registerReturnCode) + "\n";
                    }
                }

                // If the type is 2...
                if (transaction.Element("type").Value == "2")
                {
                    // Get the grade. Should be between 0 and 1.
                    double grade = Math.Round(double.Parse(transaction.Element("grade").Value) / 100, 2);

                    // Get the registrationId for the registration.
                    long registrationNumber = long.Parse(transaction.Element("registration_no").Value);

                    BITCollege_JH.Models.Registration registration = (from Results in db.Registrations
                                                                      where Results.RegistrationNumber == registrationNumber
                                                                      select Results).SingleOrDefault();

                    int registrationId = registration.RegistrationId;

                    string notes = transaction.Element("notes").Value;

                    try
                    {
                        service.UpdateGrade(grade, registrationId, notes);

                        double normalGrade = grade * 100;

                        logData += String.Format("A grade of: {0} has been successfully applied to registration: {1}\n",normalGrade, registrationNumber);

                    }
                    catch (Exception e)
                    {
                        logData += String.Format("Grade update unsuccessful. Message: {0}\n", e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Writes relevant information to a log file.
        /// </summary>
        /// <returns>The information on the log file.</returns>
        public String WriteLogData()
        {
            // Instantiate a streamWriter object.
            StreamWriter writer = new StreamWriter(logFileName);

            // Write the data to the log file.
            writer.Write(logData);

            // Close the StreamWriter.
            writer.Close();

            // Capture in a local variable, the contents of logData.
            String logDataLocal = logData;

            // Set logData to an empty string.
            logData = "";

            // Set logFileName to an empty string.
            logFileName = "";
            
            return logDataLocal;
        }

        /// <summary>
        /// Determines the appropriate filename and proceeds with header and detail processing.
        /// </summary>
        /// <param name="programAcronym">The acronym for the program.</param>
        public void ProcessTransmission(String programAcronym)
        {
            // Formulate the inputFileName.
            this.inputFileName = String.Format("{0}-{1}-{2}.xml", DateTime.Now.Year, DateTime.Now.DayOfYear, programAcronym);

            // Formulate the logFileName.
            logFileName = String.Format("LOG {0}-{1}-{2}.txt", DateTime.Now.Year, DateTime.Now.DayOfYear, programAcronym);

            //Check if file exists.
            if(File.Exists(inputFileName))
            {
                try
                {
                    ProcessHeader();
                    ProcessDetails();
                }
                catch (Exception e)
                {
                    logData += String.Format("Exception has occurred. Message: {0}", e.Message);

                   // WriteLogData();
                }
            }
            else
            {
                // Append a relevant message.
                logData += String.Format("The file: {0} does not exist.\n", inputFileName);
            }

            //WriteLogData();
        }
    }
}

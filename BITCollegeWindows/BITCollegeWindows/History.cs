/*
 * Name: James Hamilton
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: November 21, 2023
 * Updated: November 25, 2023
 */

using BITCollege_JH.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BITCollege_JH.Models;

namespace BITCollegeWindows
{
    public partial class History : Form
    {

        // An instance of the DBContext class.
        protected BITCollege_JHContext db = new BITCollege_JHContext();

        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;

        /// <summary>
        /// given:  This constructor will be used when called from the
        /// Student form.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public History(ConstructorData constructorData)
        {
            InitializeComponent();

            // Populate the constructorData object.
            this.constructorData = constructorData;

            // Populate the upper controls with the constructorData.
            Student student = constructorData.Student;

            Registration registration = constructorData.Registration;

            this.studentNumberMaskedTextBox.Text = student.StudentNumber.ToString();

            this.fullNameLabel1.Text = student.FullName;

            this.descriptionLabel1.Text = student.AcademicProgram.Description;

        }


        /// <summary>
        /// given: This code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Open this form in top right corner of the frame.
        /// further code required:
        /// </summary>
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            try
            {
                // Query selecting data from the Registrations and Courses tables.
                var query =
                    (from registration in db.Registrations
                     join course in db.Courses
                     on registration.CourseId equals course.CourseId
                     where registration.StudentId == constructorData.Student.StudentId
                     select new
                     {
                         registration.RegistrationNumber,
                         registration.RegistrationDate,
                         registration.Course.Title,
                         registration.Grade,
                         course.Notes
                     }).ToList();

                registrationBindingSource.DataSource = query;

            }
            catch (Exception exception)
            {
                MessageBox.Show("The following exception has occurred " + exception.Message,
                                 "An error has occurred.");
            }
            
        }

        private void descriptionLabel1_Click(object sender, EventArgs e)
        {

        }

        private void registrationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void studentNumberMaskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

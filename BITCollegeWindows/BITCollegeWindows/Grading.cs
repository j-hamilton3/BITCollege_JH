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
using Utility;

namespace BITCollegeWindows
{
    public partial class Grading : Form
    {
        // Instance of the DataContext class.
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
        public Grading(ConstructorData constructor)
        {
            InitializeComponent();

            // Populate the constructorData.
            this.constructorData = constructor;

            // Populate the upper controls with the constructorData.
            this.studentBindingSource.DataSource = constructorData.Student;
            

            // Populate the lower controls with the constructorData.
            this.registrationBindingSource.DataSource = constructorData.Registration;
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
        /// given:  Always open in this form in the top right corner of the frame.
        /// further code required:
        /// </summary>
        private void Grading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            // Set the mask for the course number.
            this.courseNumberMaskedTextBox.Mask = Utility.BusinessRules.CourseFormat(constructorData.Registration.Course.CourseType);

            // If a grade has been previously entered.
            if (constructorData.Registration.Grade != null)
            {
                // Disable the grade text box.
                gradeTextBox.Enabled = false;

                // Disable the update link label.
                lnkUpdate.Enabled = false;

                // Make grading not possible label visible.
                lblExisting.Visible = true;
            }
            else
            {
                // Enable the grade text box.
                gradeTextBox.Enabled = true;

                // Enable the update link label.
                lnkUpdate.Enabled = true;

                // Make grading not possible label invisible.
                lblExisting.Visible = false;
            }
        }

        /// <summary>
        /// Handles the logic for updating a student grade
        /// </summary>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // FIX - Provided by Neil!
            if (registrationBindingSource.DataSource != null)
            {
                constructorData.Registration = (Registration)registrationBindingSource.Current;
            }

            // Strip the grade value of the percent formatting. -TODO
            String textBoxValue = Utility.Numeric.ClearFormatting(gradeTextBox.Text, "%");

            // If the value is not numeric...
            if (!Numeric.IsNumeric(textBoxValue, System.Globalization.NumberStyles.Number))
            {
                // Show the message box.
                MessageBox.Show("An error has occurred. The grade must be entered as a decimal value.", "Update error.");
            }
            else
            {
                // Divide the value by 100 and ensure the value is between 0 and 1. 
                double textBoxValueDivided =double.Parse(textBoxValue) / 100;

                // If not within the range, display a message box. 
                if (textBoxValueDivided >= 0 && textBoxValueDivided <= 1)
                {
                    // If the data is within the proper range use WCF Web Service to update grade.
                    RegistrationService.CollegeRegistrationClient service = new RegistrationService.CollegeRegistrationClient();

                    service.UpdateGrade(textBoxValueDivided, constructorData.Registration.RegistrationId, "");

                    // Disable the grade text box.
                    gradeTextBox.Enabled = false;
                }
                else
                {
                    // Show the message box.
                    MessageBox.Show("An error has occurred. The grade must be entered as a decimal value.", "Update error.");
                }
            }
        }

        private void descriptionLabel1_Click(object sender, EventArgs e)
        {

        }

        private void courseTypeLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gradeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void gradeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}

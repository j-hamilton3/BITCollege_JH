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
    public partial class StudentData : Form
    {
        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();

        // Instance of DataContext class.
         protected BITCollege_JHContext db = new BITCollege_JHContext();

        /// <summary>
        /// This constructor will be used when this form is opened from
        /// the MDI Frame.
        /// </summary>
        public StudentData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to StudentData
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form.
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public StudentData (ConstructorData constructor)
        {
            InitializeComponent();
            //Further code to be added.

            // Set the constructorData variable.
            this.constructorData = constructor;

            Console.WriteLine(this.constructorData.Student.StudentNumber.ToString());

            // Set the student number masked text box value.
            this.studentNumberMaskedTextBox.Text = this.constructorData.Student.StudentNumber.ToString();

            // Call the leave event.
            studentNumberMaskedTextBox_Leave(null, null);
            
        }

        /// <summary>
        /// given: Open grading form passing constructor data.
        /// </summary>
        private void lnkUpdateGrade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Grading grading = new Grading(constructorData);
            grading.MdiParent = this.MdiParent;
            grading.Show();
            this.Close();
        }


        /// <summary>
        /// given: Open history form passing constructor data.
        /// </summary>
        private void lnkViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Opens the form in top right corner of the frame.
        /// </summary>
        private void StudentData_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);

            // Ensures the link labels are initially disabled.
            this.lnkUpdateGrade.Visible = false;
            this.lnkViewDetails.Visible = false;
        }

        private void studentNumberLabel_Click(object sender, EventArgs e)
        {

        }

        private void dateCreatedLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gradePointAverageLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gradePointAverageLabel_Click(object sender, EventArgs e)
        {

        }

        private void fullNameLabel1_Click(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// Handles the leave event for the studentNumberMaskedTextBox.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            // Select data from Students table whose StudentNumber matches the value. 
            BITCollege_JH.Models.Student student = (from Results in db.Students
                                                    where Results.StudentNumber.ToString() == studentNumberMaskedTextBox.Text
                                                    select Results).SingleOrDefault();

            Console.WriteLine("The leave event has been triggered.");

            // If no records are retrieved.
            if (student == null)
            {
                // Disable the link labels.
                this.lnkUpdateGrade.Visible = false;
                this.lnkViewDetails.Visible = false;

                // Set focus to the masked box control.
                this.studentNumberMaskedTextBox.Focus();

                // Use the following syntax to clear the form.
                studentBindingSource.DataSource = typeof(Student);
                registrationBindingSource.DataSource = typeof(Registration);

                // Display a message box indicating that the user does not exist.
                MessageBox.Show("Student " + studentNumberMaskedTextBox.Text + " does not exist.",
                                "Invalid Student Number");
            }
            else
            {
                // Set the DataSource property to the result set.
                studentBindingSource.DataSource = student;

                // Select all registrations that correspond to the StudentNumber.
                IQueryable<Registration> registrations = from Results in db.Registrations
                                                         where Results.StudentId == student.StudentId
                                                         select Results;

                // If no registration records were retrieved.
                if (registrations.Count() == 0)
                {
                    // Disable the link labels.
                    this.lnkUpdateGrade.Visible = false;
                    this.lnkViewDetails.Visible = false;

                    // Clear the Registration BindingSource object.
                    registrationBindingSource.DataSource = typeof(Registration);
                }
                else if (registrations.Count() > 0)
                {
                    Console.WriteLine("Registrations were recieved");

                    // Set the binding source.
                    registrationBindingSource.DataSource = registrations.ToList();

                    // If the registration from constructorData is not null.
                    if (this.constructorData.Registration != null)
                    {
                        Console.WriteLine("The if statement was triggered!");

                        registrationNumberComboBox.Text = constructorData.Registration.RegistrationNumber.ToString();
                    }

                    // Disable the link labels.
                    this.lnkUpdateGrade.Visible = true;
                    this.lnkViewDetails.Visible = true;

                    //****//
                    // Get the currently selected registration.
                    Registration registration = (Registration)registrationBindingSource.Current;

                    // Populate constructorData object with current student and registration.
                    this.constructorData.Student = student;
                    this.constructorData.Registration = registration;  
                }
            }
        }

        private void creditHoursLabel1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the registrationNumber Combo Box.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void registrationNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the currently selected registration from the ComboBox.
            Registration selectedRegistration = (Registration)registrationNumberComboBox.SelectedItem;

            // Set the constructorData.Registration to the selected registration.
            this.constructorData.Registration = selectedRegistration;

            Console.WriteLine("The selected registration changed!");
        }
    }
}

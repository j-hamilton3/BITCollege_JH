/*
 * Name: James Hamilton
 * Program: Business Information Technology
 * Course: ADEV-3008 Programming 3
 * Created: December 13, 2023
 * Updated: December 13, 2023
 */

using BITCollege_JH.Data;
using BITCollege_JH.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class BatchUpdate : Form
    {
        public BatchUpdate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Batch processing
        /// Further code to be added.
        /// </summary>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Batch batch = new Batch();

            if (radSelect.Checked)
            {
                batch.ProcessTransmission(descriptionComboBox.SelectedValue.ToString());

                rtxtLog.Text += batch.WriteLogData();
            }

            if (radAll.Checked)
            {
                // Loop through comboBox items.
                foreach (var item in descriptionComboBox.Items)
                {
                    AcademicProgram program = item as AcademicProgram;

                    string acronym = program.ProgramAcronym;

                    batch.ProcessTransmission(acronym);

                    rtxtLog.Text += batch.WriteLogData();
                }
            }
        }

        /// <summary>
        /// given:  Always open this form in top right of frame.
        /// Further code to be added.
        /// </summary>
        private void BatchUpdate_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            // Get data for the binding source.
            BITCollege_JHContext db = new BITCollege_JHContext();

            IQueryable<AcademicProgram> programs = db.AcademicPrograms;

            academicProgramBindingSource.DataSource = programs.ToList();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Batch testBatch = new Batch();

            testBatch.ProcessTransmission("VT");
        }

        private void descriptionLabel_Click(object sender, EventArgs e)
        {

        }

        private void descriptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the CheckedChanged event for the radAll button.
        /// </summary>
        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            descriptionComboBox.Enabled = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event for the radSelect button.
        /// </summary>
        private void radSelect_CheckedChanged(object sender, EventArgs e)
        {
            descriptionComboBox.Enabled = true;
        }
    }
}

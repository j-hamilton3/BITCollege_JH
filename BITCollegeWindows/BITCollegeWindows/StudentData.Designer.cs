namespace BITCollegeWindows
{
    partial class StudentData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label studentNumberLabel;
            System.Windows.Forms.Label dateCreatedLabel;
            System.Windows.Forms.Label outstandingFeesLabel;
            System.Windows.Forms.Label gradePointAverageLabel;
            System.Windows.Forms.Label registrationNumberLabel;
            System.Windows.Forms.Label courseNumberLabel;
            System.Windows.Forms.Label titleLabel;
            System.Windows.Forms.Label creditHoursLabel;
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label fullAddressLabel;
            this.grpStudent = new System.Windows.Forms.GroupBox();
            this.fullAddressLabel1 = new System.Windows.Forms.Label();
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fullNameLabel1 = new System.Windows.Forms.Label();
            this.descriptionLabel1 = new System.Windows.Forms.Label();
            this.gradePointAverageLabel1 = new System.Windows.Forms.Label();
            this.outstandingFeesLabel1 = new System.Windows.Forms.Label();
            this.dateCreatedLabel1 = new System.Windows.Forms.Label();
            this.studentNumberMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.grpRegistration = new System.Windows.Forms.GroupBox();
            this.creditHoursLabel1 = new System.Windows.Forms.Label();
            this.registrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.titleLabel1 = new System.Windows.Forms.Label();
            this.courseNumberLabel1 = new System.Windows.Forms.Label();
            this.registrationNumberComboBox = new System.Windows.Forms.ComboBox();
            this.lnkUpdateGrade = new System.Windows.Forms.LinkLabel();
            this.lnkViewDetails = new System.Windows.Forms.LinkLabel();
            studentNumberLabel = new System.Windows.Forms.Label();
            dateCreatedLabel = new System.Windows.Forms.Label();
            outstandingFeesLabel = new System.Windows.Forms.Label();
            gradePointAverageLabel = new System.Windows.Forms.Label();
            registrationNumberLabel = new System.Windows.Forms.Label();
            courseNumberLabel = new System.Windows.Forms.Label();
            titleLabel = new System.Windows.Forms.Label();
            creditHoursLabel = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            fullAddressLabel = new System.Windows.Forms.Label();
            this.grpStudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            this.grpRegistration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // studentNumberLabel
            // 
            studentNumberLabel.AutoSize = true;
            studentNumberLabel.Location = new System.Drawing.Point(30, 33);
            studentNumberLabel.Name = "studentNumberLabel";
            studentNumberLabel.Size = new System.Drawing.Size(87, 13);
            studentNumberLabel.TabIndex = 0;
            studentNumberLabel.Text = "Student Number:";
            studentNumberLabel.Click += new System.EventHandler(this.studentNumberLabel_Click);
            // 
            // dateCreatedLabel
            // 
            dateCreatedLabel.AutoSize = true;
            dateCreatedLabel.Location = new System.Drawing.Point(30, 141);
            dateCreatedLabel.Name = "dateCreatedLabel";
            dateCreatedLabel.Size = new System.Drawing.Size(73, 13);
            dateCreatedLabel.TabIndex = 2;
            dateCreatedLabel.Text = "Date Created:";
            // 
            // outstandingFeesLabel
            // 
            outstandingFeesLabel.AutoSize = true;
            outstandingFeesLabel.Location = new System.Drawing.Point(296, 142);
            outstandingFeesLabel.Name = "outstandingFeesLabel";
            outstandingFeesLabel.Size = new System.Drawing.Size(93, 13);
            outstandingFeesLabel.TabIndex = 4;
            outstandingFeesLabel.Text = "Outstanding Fees:";
            // 
            // gradePointAverageLabel
            // 
            gradePointAverageLabel.AutoSize = true;
            gradePointAverageLabel.Location = new System.Drawing.Point(30, 183);
            gradePointAverageLabel.Name = "gradePointAverageLabel";
            gradePointAverageLabel.Size = new System.Drawing.Size(109, 13);
            gradePointAverageLabel.TabIndex = 6;
            gradePointAverageLabel.Text = "Grade Point Average:";
            gradePointAverageLabel.Click += new System.EventHandler(this.gradePointAverageLabel_Click);
            // 
            // registrationNumberLabel
            // 
            registrationNumberLabel.AutoSize = true;
            registrationNumberLabel.Location = new System.Drawing.Point(30, 33);
            registrationNumberLabel.Name = "registrationNumberLabel";
            registrationNumberLabel.Size = new System.Drawing.Size(106, 13);
            registrationNumberLabel.TabIndex = 0;
            registrationNumberLabel.Text = "Registration Number:";
            // 
            // courseNumberLabel
            // 
            courseNumberLabel.AutoSize = true;
            courseNumberLabel.Location = new System.Drawing.Point(30, 72);
            courseNumberLabel.Name = "courseNumberLabel";
            courseNumberLabel.Size = new System.Drawing.Size(83, 13);
            courseNumberLabel.TabIndex = 2;
            courseNumberLabel.Text = "Course Number:";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new System.Drawing.Point(296, 72);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(30, 13);
            titleLabel.TabIndex = 4;
            titleLabel.Text = "Title:";
            // 
            // creditHoursLabel
            // 
            creditHoursLabel.AutoSize = true;
            creditHoursLabel.Location = new System.Drawing.Point(30, 114);
            creditHoursLabel.Name = "creditHoursLabel";
            creditHoursLabel.Size = new System.Drawing.Size(68, 13);
            creditHoursLabel.TabIndex = 6;
            creditHoursLabel.Text = "Credit Hours:";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new System.Drawing.Point(31, 69);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(38, 13);
            fullNameLabel.TabIndex = 10;
            fullNameLabel.Text = "Name:";
            // 
            // fullAddressLabel
            // 
            fullAddressLabel.AutoSize = true;
            fullAddressLabel.Location = new System.Drawing.Point(30, 104);
            fullAddressLabel.Name = "fullAddressLabel";
            fullAddressLabel.Size = new System.Drawing.Size(48, 13);
            fullAddressLabel.TabIndex = 12;
            fullAddressLabel.Text = "Address:";
            // 
            // grpStudent
            // 
            this.grpStudent.Controls.Add(fullAddressLabel);
            this.grpStudent.Controls.Add(this.fullAddressLabel1);
            this.grpStudent.Controls.Add(fullNameLabel);
            this.grpStudent.Controls.Add(this.fullNameLabel1);
            this.grpStudent.Controls.Add(this.descriptionLabel1);
            this.grpStudent.Controls.Add(gradePointAverageLabel);
            this.grpStudent.Controls.Add(this.gradePointAverageLabel1);
            this.grpStudent.Controls.Add(outstandingFeesLabel);
            this.grpStudent.Controls.Add(this.outstandingFeesLabel1);
            this.grpStudent.Controls.Add(dateCreatedLabel);
            this.grpStudent.Controls.Add(this.dateCreatedLabel1);
            this.grpStudent.Controls.Add(studentNumberLabel);
            this.grpStudent.Controls.Add(this.studentNumberMaskedTextBox);
            this.grpStudent.Location = new System.Drawing.Point(35, 47);
            this.grpStudent.Name = "grpStudent";
            this.grpStudent.Size = new System.Drawing.Size(600, 232);
            this.grpStudent.TabIndex = 0;
            this.grpStudent.TabStop = false;
            this.grpStudent.Text = "Student Data";
            // 
            // fullAddressLabel1
            // 
            this.fullAddressLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullAddressLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "FullAddress", true));
            this.fullAddressLabel1.Location = new System.Drawing.Point(142, 104);
            this.fullAddressLabel1.Name = "fullAddressLabel1";
            this.fullAddressLabel1.Size = new System.Drawing.Size(372, 23);
            this.fullAddressLabel1.TabIndex = 13;
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataSource = typeof(BITCollege_JH.Models.Student);
            // 
            // fullNameLabel1
            // 
            this.fullNameLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullNameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "FullName", true));
            this.fullNameLabel1.Location = new System.Drawing.Point(142, 69);
            this.fullNameLabel1.Name = "fullNameLabel1";
            this.fullNameLabel1.Size = new System.Drawing.Size(372, 23);
            this.fullNameLabel1.TabIndex = 11;
            this.fullNameLabel1.Click += new System.EventHandler(this.fullNameLabel1_Click);
            // 
            // descriptionLabel1
            // 
            this.descriptionLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "GradePointState.Description", true));
            this.descriptionLabel1.Location = new System.Drawing.Point(267, 183);
            this.descriptionLabel1.Name = "descriptionLabel1";
            this.descriptionLabel1.Size = new System.Drawing.Size(100, 23);
            this.descriptionLabel1.TabIndex = 9;
            // 
            // gradePointAverageLabel1
            // 
            this.gradePointAverageLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradePointAverageLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "GradePointAverage", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.gradePointAverageLabel1.Location = new System.Drawing.Point(142, 183);
            this.gradePointAverageLabel1.Name = "gradePointAverageLabel1";
            this.gradePointAverageLabel1.Size = new System.Drawing.Size(103, 23);
            this.gradePointAverageLabel1.TabIndex = 7;
            this.gradePointAverageLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.gradePointAverageLabel1.Click += new System.EventHandler(this.gradePointAverageLabel1_Click);
            // 
            // outstandingFeesLabel1
            // 
            this.outstandingFeesLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outstandingFeesLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "OutstandingFees", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.outstandingFeesLabel1.Location = new System.Drawing.Point(395, 141);
            this.outstandingFeesLabel1.Name = "outstandingFeesLabel1";
            this.outstandingFeesLabel1.Size = new System.Drawing.Size(119, 23);
            this.outstandingFeesLabel1.TabIndex = 5;
            this.outstandingFeesLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dateCreatedLabel1
            // 
            this.dateCreatedLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateCreatedLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "DateCreated", true));
            this.dateCreatedLabel1.Location = new System.Drawing.Point(142, 141);
            this.dateCreatedLabel1.Name = "dateCreatedLabel1";
            this.dateCreatedLabel1.Size = new System.Drawing.Size(103, 23);
            this.dateCreatedLabel1.TabIndex = 3;
            this.dateCreatedLabel1.Click += new System.EventHandler(this.dateCreatedLabel1_Click);
            // 
            // studentNumberMaskedTextBox
            // 
            this.studentNumberMaskedTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "StudentNumber", true));
            this.studentNumberMaskedTextBox.Location = new System.Drawing.Point(142, 33);
            this.studentNumberMaskedTextBox.Mask = "0000-0000";
            this.studentNumberMaskedTextBox.Name = "studentNumberMaskedTextBox";
            this.studentNumberMaskedTextBox.Size = new System.Drawing.Size(100, 20);
            this.studentNumberMaskedTextBox.TabIndex = 1;
            this.studentNumberMaskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.studentNumberMaskedTextBox.Leave += new System.EventHandler(this.studentNumberMaskedTextBox_Leave);
            // 
            // grpRegistration
            // 
            this.grpRegistration.Controls.Add(creditHoursLabel);
            this.grpRegistration.Controls.Add(this.creditHoursLabel1);
            this.grpRegistration.Controls.Add(titleLabel);
            this.grpRegistration.Controls.Add(this.titleLabel1);
            this.grpRegistration.Controls.Add(courseNumberLabel);
            this.grpRegistration.Controls.Add(this.courseNumberLabel1);
            this.grpRegistration.Controls.Add(registrationNumberLabel);
            this.grpRegistration.Controls.Add(this.registrationNumberComboBox);
            this.grpRegistration.Location = new System.Drawing.Point(35, 269);
            this.grpRegistration.Name = "grpRegistration";
            this.grpRegistration.Size = new System.Drawing.Size(600, 158);
            this.grpRegistration.TabIndex = 1;
            this.grpRegistration.TabStop = false;
            this.grpRegistration.Text = "Registration Data";
            // 
            // creditHoursLabel1
            // 
            this.creditHoursLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.creditHoursLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CreditHours", true));
            this.creditHoursLabel1.Location = new System.Drawing.Point(142, 114);
            this.creditHoursLabel1.Name = "creditHoursLabel1";
            this.creditHoursLabel1.Size = new System.Drawing.Size(121, 23);
            this.creditHoursLabel1.TabIndex = 7;
            this.creditHoursLabel1.Click += new System.EventHandler(this.creditHoursLabel1_Click);
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataSource = typeof(BITCollege_JH.Models.Registration);
            // 
            // titleLabel1
            // 
            this.titleLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.Title", true));
            this.titleLabel1.Location = new System.Drawing.Point(332, 72);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(182, 23);
            this.titleLabel1.TabIndex = 5;
            // 
            // courseNumberLabel1
            // 
            this.courseNumberLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.courseNumberLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CourseNumber", true));
            this.courseNumberLabel1.Location = new System.Drawing.Point(142, 71);
            this.courseNumberLabel1.Name = "courseNumberLabel1";
            this.courseNumberLabel1.Size = new System.Drawing.Size(121, 23);
            this.courseNumberLabel1.TabIndex = 3;
            // 
            // registrationNumberComboBox
            // 
            this.registrationNumberComboBox.DataSource = this.registrationBindingSource;
            this.registrationNumberComboBox.DisplayMember = "RegistrationNumber";
            this.registrationNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.registrationNumberComboBox.FormattingEnabled = true;
            this.registrationNumberComboBox.Location = new System.Drawing.Point(142, 30);
            this.registrationNumberComboBox.Name = "registrationNumberComboBox";
            this.registrationNumberComboBox.Size = new System.Drawing.Size(121, 21);
            this.registrationNumberComboBox.TabIndex = 1;
            this.registrationNumberComboBox.ValueMember = "RegistrationNumber";
            this.registrationNumberComboBox.SelectedIndexChanged += new System.EventHandler(this.registrationNumberComboBox_SelectedIndexChanged);
            // 
            // lnkUpdateGrade
            // 
            this.lnkUpdateGrade.AutoSize = true;
            this.lnkUpdateGrade.Location = new System.Drawing.Point(197, 464);
            this.lnkUpdateGrade.Name = "lnkUpdateGrade";
            this.lnkUpdateGrade.Size = new System.Drawing.Size(74, 13);
            this.lnkUpdateGrade.TabIndex = 2;
            this.lnkUpdateGrade.TabStop = true;
            this.lnkUpdateGrade.Text = "Update Grade";
            this.lnkUpdateGrade.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdateGrade_LinkClicked);
            // 
            // lnkViewDetails
            // 
            this.lnkViewDetails.AutoSize = true;
            this.lnkViewDetails.Location = new System.Drawing.Point(381, 464);
            this.lnkViewDetails.Name = "lnkViewDetails";
            this.lnkViewDetails.Size = new System.Drawing.Size(65, 13);
            this.lnkViewDetails.TabIndex = 3;
            this.lnkViewDetails.TabStop = true;
            this.lnkViewDetails.Text = "View Details";
            this.lnkViewDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewDetails_LinkClicked);
            // 
            // StudentData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 511);
            this.Controls.Add(this.lnkViewDetails);
            this.Controls.Add(this.lnkUpdateGrade);
            this.Controls.Add(this.grpRegistration);
            this.Controls.Add(this.grpStudent);
            this.Name = "StudentData";
            this.Text = "StudentData";
            this.Load += new System.EventHandler(this.StudentData_Load);
            this.grpStudent.ResumeLayout(false);
            this.grpStudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            this.grpRegistration.ResumeLayout(false);
            this.grpRegistration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStudent;
        private System.Windows.Forms.GroupBox grpRegistration;
        private System.Windows.Forms.LinkLabel lnkUpdateGrade;
        private System.Windows.Forms.LinkLabel lnkViewDetails;
        private System.Windows.Forms.MaskedTextBox studentNumberMaskedTextBox;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private System.Windows.Forms.Label dateCreatedLabel1;
        private System.Windows.Forms.Label gradePointAverageLabel1;
        private System.Windows.Forms.Label outstandingFeesLabel1;
        private System.Windows.Forms.ComboBox registrationNumberComboBox;
        private System.Windows.Forms.BindingSource registrationBindingSource;
        private System.Windows.Forms.Label titleLabel1;
        private System.Windows.Forms.Label courseNumberLabel1;
        private System.Windows.Forms.Label fullNameLabel1;
        private System.Windows.Forms.Label descriptionLabel1;
        private System.Windows.Forms.Label creditHoursLabel1;
        private System.Windows.Forms.Label fullAddressLabel1;
    }
}
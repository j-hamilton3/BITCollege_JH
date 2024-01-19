namespace BITCollegeWindows
{
    partial class BatchUpdate
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
            System.Windows.Forms.Label descriptionLabel;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.lnkProcess = new System.Windows.Forms.LinkLabel();
            this.radSelect = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.academicProgramBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.descriptionComboBox = new System.Windows.Forms.ComboBox();
            descriptionLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.academicProgramBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(descriptionLabel);
            this.groupBox1.Controls.Add(this.descriptionComboBox);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.lnkProcess);
            this.groupBox1.Controls.Add(this.radSelect);
            this.groupBox1.Controls.Add(this.radAll);
            this.groupBox1.Location = new System.Drawing.Point(26, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 169);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Batch Selection";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(484, 89);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lnkProcess
            // 
            this.lnkProcess.AutoSize = true;
            this.lnkProcess.Location = new System.Drawing.Point(41, 133);
            this.lnkProcess.Name = "lnkProcess";
            this.lnkProcess.Size = new System.Drawing.Size(76, 13);
            this.lnkProcess.TabIndex = 2;
            this.lnkProcess.TabStop = true;
            this.lnkProcess.Text = "Process Batch";
            this.lnkProcess.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProcess_LinkClicked);
            // 
            // radSelect
            // 
            this.radSelect.AutoSize = true;
            this.radSelect.Location = new System.Drawing.Point(44, 59);
            this.radSelect.Name = "radSelect";
            this.radSelect.Size = new System.Drawing.Size(213, 17);
            this.radSelect.TabIndex = 1;
            this.radSelect.TabStop = true;
            this.radSelect.Text = "Select a Program to Grade and Register";
            this.radSelect.UseVisualStyleBackColor = true;
            this.radSelect.CheckedChanged += new System.EventHandler(this.radSelect_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(44, 36);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(201, 17);
            this.radAll.TabIndex = 0;
            this.radAll.TabStop = true;
            this.radAll.Text = "Grade and Register for ALL Programs";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // rtxtLog
            // 
            this.rtxtLog.Location = new System.Drawing.Point(26, 239);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(714, 169);
            this.rtxtLog.TabIndex = 1;
            this.rtxtLog.Text = "";
            // 
            // academicProgramBindingSource
            // 
            this.academicProgramBindingSource.DataSource = typeof(BITCollege_JH.Models.AcademicProgram);
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(41, 94);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(49, 13);
            descriptionLabel.TabIndex = 4;
            descriptionLabel.Text = "Program:";
            // 
            // descriptionComboBox
            // 
            this.descriptionComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.academicProgramBindingSource, "Description", true));
            this.descriptionComboBox.DataSource = this.academicProgramBindingSource;
            this.descriptionComboBox.DisplayMember = "Description";
            this.descriptionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.descriptionComboBox.FormattingEnabled = true;
            this.descriptionComboBox.Location = new System.Drawing.Point(110, 91);
            this.descriptionComboBox.Name = "descriptionComboBox";
            this.descriptionComboBox.Size = new System.Drawing.Size(147, 21);
            this.descriptionComboBox.TabIndex = 5;
            this.descriptionComboBox.ValueMember = "ProgramAcronym";
            // 
            // BatchUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.groupBox1);
            this.Name = "BatchUpdate";
            this.Text = "Batch Student Update";
            this.Load += new System.EventHandler(this.BatchUpdate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.academicProgramBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkProcess;
        private System.Windows.Forms.RadioButton radSelect;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.BindingSource academicProgramBindingSource;
        private System.Windows.Forms.ComboBox descriptionComboBox;
    }
}
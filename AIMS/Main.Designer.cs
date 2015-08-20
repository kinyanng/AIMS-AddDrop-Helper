namespace AIMS
{
    partial class Main
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
            this.label_copyright = new System.Windows.Forms.Label();
            this.label_eid = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.tb_eid = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.button_login = new System.Windows.Forms.Button();
            this.button_logs = new System.Windows.Forms.Button();
            this.label_log = new System.Windows.Forms.Label();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.label_registeredCourse = new System.Windows.Forms.Label();
            this.listView_registeredCourse = new System.Windows.Forms.ListView();
            this.listView_registeredCourse_CRN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_registeredCourse_courseCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_registeredCourse_section = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_registeredCourse_title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_registeredCourse_credit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_request = new System.Windows.Forms.Label();
            this.listView_request = new System.Windows.Forms.ListView();
            this.listView_request_priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_request_CRN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_request_courseCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_request_section = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_request_title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_request_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_request_availability = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_course = new System.Windows.Forms.Label();
            this.tb_courseCode = new System.Windows.Forms.TextBox();
            this.label_section = new System.Windows.Forms.Label();
            this.label_action = new System.Windows.Forms.Label();
            this.cb_action = new System.Windows.Forms.ComboBox();
            this.label_priority = new System.Windows.Forms.Label();
            this.cb_priority = new System.Windows.Forms.ComboBox();
            this.button_addRequest = new System.Windows.Forms.Button();
            this.button_deleteRequest = new System.Windows.Forms.Button();
            this.group_settings = new System.Windows.Forms.GroupBox();
            this.button_registrationSwitch = new System.Windows.Forms.Button();
            this.tb_delay = new System.Windows.Forms.TextBox();
            this.label_delay = new System.Windows.Forms.Label();
            this.tb_term = new System.Windows.Forms.TextBox();
            this.label_term = new System.Windows.Forms.Label();
            this.registrationWorker = new System.ComponentModel.BackgroundWorker();
            this.group_settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_copyright
            // 
            this.label_copyright.AutoSize = true;
            this.label_copyright.Location = new System.Drawing.Point(20, 528);
            this.label_copyright.Margin = new System.Windows.Forms.Padding(0);
            this.label_copyright.Name = "label_copyright";
            this.label_copyright.Size = new System.Drawing.Size(331, 26);
            this.label_copyright.TabIndex = 0;
            this.label_copyright.Text = "Developed by CS.EN 2015\r\nNot for sale. For academic purpose only, please use at y" +
    "our own risk.";
            this.label_copyright.Click += new System.EventHandler(this.label_copyright_Click);
            // 
            // label_eid
            // 
            this.label_eid.AutoSize = true;
            this.label_eid.Location = new System.Drawing.Point(20, 23);
            this.label_eid.Margin = new System.Windows.Forms.Padding(0);
            this.label_eid.Name = "label_eid";
            this.label_eid.Size = new System.Drawing.Size(68, 13);
            this.label_eid.TabIndex = 0;
            this.label_eid.Text = "Electronic ID";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(20, 53);
            this.label_password.Margin = new System.Windows.Forms.Padding(0);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(53, 13);
            this.label_password.TabIndex = 0;
            this.label_password.Text = "Password";
            // 
            // tb_eid
            // 
            this.tb_eid.Enabled = false;
            this.tb_eid.Location = new System.Drawing.Point(95, 20);
            this.tb_eid.MaxLength = 30;
            this.tb_eid.Name = "tb_eid";
            this.tb_eid.Size = new System.Drawing.Size(165, 20);
            this.tb_eid.TabIndex = 1;
            // 
            // tb_password
            // 
            this.tb_password.Enabled = false;
            this.tb_password.Location = new System.Drawing.Point(95, 50);
            this.tb_password.MaxLength = 30;
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(165, 20);
            this.tb_password.TabIndex = 2;
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // button_login
            // 
            this.button_login.Enabled = false;
            this.button_login.Location = new System.Drawing.Point(67, 80);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(70, 23);
            this.button_login.TabIndex = 3;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // button_logs
            // 
            this.button_logs.Location = new System.Drawing.Point(143, 80);
            this.button_logs.Name = "button_logs";
            this.button_logs.Size = new System.Drawing.Size(70, 23);
            this.button_logs.TabIndex = 4;
            this.button_logs.Text = "Logs";
            this.button_logs.UseVisualStyleBackColor = true;
            this.button_logs.Visible = false;
            this.button_logs.Click += new System.EventHandler(this.button_logs_Click);
            // 
            // label_log
            // 
            this.label_log.AutoSize = true;
            this.label_log.Location = new System.Drawing.Point(20, 130);
            this.label_log.Margin = new System.Windows.Forms.Padding(0);
            this.label_log.Name = "label_log";
            this.label_log.Size = new System.Drawing.Size(0, 13);
            this.label_log.TabIndex = 0;
            // 
            // rtb_log
            // 
            this.rtb_log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_log.Location = new System.Drawing.Point(20, 120);
            this.rtb_log.Margin = new System.Windows.Forms.Padding(0);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.ReadOnly = true;
            this.rtb_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtb_log.Size = new System.Drawing.Size(240, 400);
            this.rtb_log.TabIndex = 0;
            this.rtb_log.TabStop = false;
            this.rtb_log.Text = "";
            // 
            // label_registeredCourse
            // 
            this.label_registeredCourse.AutoSize = true;
            this.label_registeredCourse.Location = new System.Drawing.Point(284, 23);
            this.label_registeredCourse.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label_registeredCourse.Name = "label_registeredCourse";
            this.label_registeredCourse.Size = new System.Drawing.Size(162, 13);
            this.label_registeredCourse.TabIndex = 0;
            this.label_registeredCourse.Text = "Course active registration record:";
            // 
            // listView_registeredCourse
            // 
            this.listView_registeredCourse.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listView_registeredCourse_CRN,
            this.listView_registeredCourse_courseCode,
            this.listView_registeredCourse_section,
            this.listView_registeredCourse_title,
            this.listView_registeredCourse_credit});
            this.listView_registeredCourse.FullRowSelect = true;
            this.listView_registeredCourse.GridLines = true;
            this.listView_registeredCourse.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_registeredCourse.Location = new System.Drawing.Point(284, 41);
            this.listView_registeredCourse.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.listView_registeredCourse.MultiSelect = false;
            this.listView_registeredCourse.Name = "listView_registeredCourse";
            this.listView_registeredCourse.Size = new System.Drawing.Size(480, 140);
            this.listView_registeredCourse.TabIndex = 0;
            this.listView_registeredCourse.TabStop = false;
            this.listView_registeredCourse.UseCompatibleStateImageBehavior = false;
            this.listView_registeredCourse.View = System.Windows.Forms.View.Details;
            this.listView_registeredCourse.SelectedIndexChanged += new System.EventHandler(this.listView_registeredCourse_SelectedIndexChanged);
            // 
            // listView_registeredCourse_CRN
            // 
            this.listView_registeredCourse_CRN.Text = "CRN";
            this.listView_registeredCourse_CRN.Width = 45;
            // 
            // listView_registeredCourse_courseCode
            // 
            this.listView_registeredCourse_courseCode.Text = "Course";
            // 
            // listView_registeredCourse_section
            // 
            this.listView_registeredCourse_section.Text = "Sect.";
            this.listView_registeredCourse_section.Width = 40;
            // 
            // listView_registeredCourse_title
            // 
            this.listView_registeredCourse_title.Text = "Title";
            this.listView_registeredCourse_title.Width = 260;
            // 
            // listView_registeredCourse_credit
            // 
            this.listView_registeredCourse_credit.Text = "Credits";
            this.listView_registeredCourse_credit.Width = 45;
            // 
            // label_request
            // 
            this.label_request.AutoSize = true;
            this.label_request.Location = new System.Drawing.Point(286, 187);
            this.label_request.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label_request.Name = "label_request";
            this.label_request.Size = new System.Drawing.Size(133, 13);
            this.label_request.TabIndex = 0;
            this.label_request.Text = "Course add/drop requests:";
            // 
            // listView_request
            // 
            this.listView_request.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listView_request_priority,
            this.listView_request_CRN,
            this.listView_request_courseCode,
            this.listView_request_section,
            this.listView_request_title,
            this.listView_request_status,
            this.listView_request_availability});
            this.listView_request.FullRowSelect = true;
            this.listView_request.GridLines = true;
            this.listView_request.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_request.HideSelection = false;
            this.listView_request.Location = new System.Drawing.Point(284, 206);
            this.listView_request.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.listView_request.MultiSelect = false;
            this.listView_request.Name = "listView_request";
            this.listView_request.Size = new System.Drawing.Size(480, 240);
            this.listView_request.TabIndex = 0;
            this.listView_request.TabStop = false;
            this.listView_request.UseCompatibleStateImageBehavior = false;
            this.listView_request.View = System.Windows.Forms.View.Details;
            this.listView_request.SelectedIndexChanged += new System.EventHandler(this.listView_request_SelectedIndexChanged);
            // 
            // listView_request_priority
            // 
            this.listView_request_priority.Text = "Prior.";
            this.listView_request_priority.Width = 40;
            // 
            // listView_request_CRN
            // 
            this.listView_request_CRN.Text = "CRN";
            this.listView_request_CRN.Width = 45;
            // 
            // listView_request_courseCode
            // 
            this.listView_request_courseCode.Text = "Course";
            // 
            // listView_request_section
            // 
            this.listView_request_section.Text = "Sect.";
            this.listView_request_section.Width = 40;
            // 
            // listView_request_title
            // 
            this.listView_request_title.Text = "Title";
            this.listView_request_title.Width = 100;
            // 
            // listView_request_status
            // 
            this.listView_request_status.Text = "Status";
            this.listView_request_status.Width = 120;
            // 
            // listView_request_availability
            // 
            this.listView_request_availability.Text = "Avail.";
            this.listView_request_availability.Width = 45;
            // 
            // label_course
            // 
            this.label_course.AutoSize = true;
            this.label_course.Location = new System.Drawing.Point(284, 463);
            this.label_course.Margin = new System.Windows.Forms.Padding(0);
            this.label_course.Name = "label_course";
            this.label_course.Size = new System.Drawing.Size(67, 13);
            this.label_course.TabIndex = 0;
            this.label_course.Text = "Course code";
            // 
            // tb_courseCode
            // 
            this.tb_courseCode.Enabled = false;
            this.tb_courseCode.Location = new System.Drawing.Point(354, 460);
            this.tb_courseCode.MaxLength = 9;
            this.tb_courseCode.Name = "tb_courseCode";
            this.tb_courseCode.Size = new System.Drawing.Size(60, 20);
            this.tb_courseCode.TabIndex = 5;
            this.tb_courseCode.Leave += new System.EventHandler(this.CapitalizeInputText);
            // 
            // label_section
            // 
            this.label_section.AutoSize = true;
            this.label_section.Location = new System.Drawing.Point(417, 463);
            this.label_section.Margin = new System.Windows.Forms.Padding(0);
            this.label_section.Name = "label_section";
            this.label_section.Size = new System.Drawing.Size(43, 13);
            this.label_section.TabIndex = 0;
            this.label_section.Text = "Section";
            // 
            // label_action
            // 
            this.label_action.AutoSize = true;
            this.label_action.Location = new System.Drawing.Point(568, 463);
            this.label_action.Margin = new System.Windows.Forms.Padding(0);
            this.label_action.Name = "label_action";
            this.label_action.Size = new System.Drawing.Size(37, 13);
            this.label_action.TabIndex = 0;
            this.label_action.Text = "Action";
            // 
            // cb_action
            // 
            this.cb_action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_action.Enabled = false;
            this.cb_action.FormattingEnabled = true;
            this.cb_action.Location = new System.Drawing.Point(608, 460);
            this.cb_action.Name = "cb_action";
            this.cb_action.Size = new System.Drawing.Size(72, 21);
            this.cb_action.TabIndex = 9;
            // 
            // label_priority
            // 
            this.label_priority.AutoSize = true;
            this.label_priority.Location = new System.Drawing.Point(683, 463);
            this.label_priority.Margin = new System.Windows.Forms.Padding(0);
            this.label_priority.Name = "label_priority";
            this.label_priority.Size = new System.Drawing.Size(38, 13);
            this.label_priority.TabIndex = 0;
            this.label_priority.Text = "Priority";
            // 
            // cb_priority
            // 
            this.cb_priority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_priority.Enabled = false;
            this.cb_priority.FormattingEnabled = true;
            this.cb_priority.Location = new System.Drawing.Point(724, 460);
            this.cb_priority.Name = "cb_priority";
            this.cb_priority.Size = new System.Drawing.Size(40, 21);
            this.cb_priority.TabIndex = 10;
            // 
            // button_addRequest
            // 
            this.button_addRequest.Enabled = false;
            this.button_addRequest.Location = new System.Drawing.Point(284, 490);
            this.button_addRequest.Name = "button_addRequest";
            this.button_addRequest.Size = new System.Drawing.Size(70, 23);
            this.button_addRequest.TabIndex = 11;
            this.button_addRequest.Text = "Add";
            this.button_addRequest.UseVisualStyleBackColor = true;
            this.button_addRequest.Click += new System.EventHandler(this.button_addRequest_Click);
            // 
            // button_deleteRequest
            // 
            this.button_deleteRequest.Enabled = false;
            this.button_deleteRequest.Location = new System.Drawing.Point(360, 490);
            this.button_deleteRequest.Name = "button_deleteRequest";
            this.button_deleteRequest.Size = new System.Drawing.Size(70, 23);
            this.button_deleteRequest.TabIndex = 12;
            this.button_deleteRequest.Text = "Delete";
            this.button_deleteRequest.UseVisualStyleBackColor = true;
            this.button_deleteRequest.Click += new System.EventHandler(this.button_deleteRequest_Click);
            // 
            // group_settings
            // 
            this.group_settings.Controls.Add(this.button_registrationSwitch);
            this.group_settings.Controls.Add(this.tb_delay);
            this.group_settings.Controls.Add(this.label_delay);
            this.group_settings.Controls.Add(this.tb_term);
            this.group_settings.Controls.Add(this.label_term);
            this.group_settings.Location = new System.Drawing.Point(464, 490);
            this.group_settings.Margin = new System.Windows.Forms.Padding(0);
            this.group_settings.Name = "group_settings";
            this.group_settings.Size = new System.Drawing.Size(300, 51);
            this.group_settings.TabIndex = 0;
            this.group_settings.TabStop = false;
            this.group_settings.Text = "Settings (Press ENTER to confirm)";
            // 
            // button_registrationSwitch
            // 
            this.button_registrationSwitch.Enabled = false;
            this.button_registrationSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_registrationSwitch.Location = new System.Drawing.Point(210, 13);
            this.button_registrationSwitch.Name = "button_registrationSwitch";
            this.button_registrationSwitch.Size = new System.Drawing.Size(80, 30);
            this.button_registrationSwitch.TabIndex = 13;
            this.button_registrationSwitch.Text = "Start";
            this.button_registrationSwitch.UseVisualStyleBackColor = true;
            this.button_registrationSwitch.Click += new System.EventHandler(this.button_registrationSwitch_Click);
            // 
            // tb_delay
            // 
            this.tb_delay.Location = new System.Drawing.Point(134, 20);
            this.tb_delay.MaxLength = 5;
            this.tb_delay.Name = "tb_delay";
            this.tb_delay.Size = new System.Drawing.Size(40, 20);
            this.tb_delay.TabIndex = 0;
            this.tb_delay.TabStop = false;
            this.tb_delay.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UpdateSettings);
            this.tb_delay.Leave += new System.EventHandler(this.tb_delay_Leave);
            // 
            // label_delay
            // 
            this.label_delay.AutoSize = true;
            this.label_delay.Location = new System.Drawing.Point(97, 23);
            this.label_delay.Margin = new System.Windows.Forms.Padding(0);
            this.label_delay.Name = "label_delay";
            this.label_delay.Size = new System.Drawing.Size(34, 13);
            this.label_delay.TabIndex = 0;
            this.label_delay.Text = "Delay";
            // 
            // tb_term
            // 
            this.tb_term.Location = new System.Drawing.Point(44, 20);
            this.tb_term.MaxLength = 6;
            this.tb_term.Name = "tb_term";
            this.tb_term.Size = new System.Drawing.Size(50, 20);
            this.tb_term.TabIndex = 0;
            this.tb_term.TabStop = false;
            this.tb_term.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UpdateSettings);
            this.tb_term.Leave += new System.EventHandler(this.tb_term_Leave);
            // 
            // label_term
            // 
            this.label_term.AutoSize = true;
            this.label_term.Location = new System.Drawing.Point(10, 23);
            this.label_term.Margin = new System.Windows.Forms.Padding(0);
            this.label_term.Name = "label_term";
            this.label_term.Size = new System.Drawing.Size(31, 13);
            this.label_term.TabIndex = 0;
            this.label_term.Text = "Term";
            // 
            // registrationWorker
            // 
            this.registrationWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.registrationWorker_DoWork);
            this.registrationWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.registrationWorker_RunWorkerCompleted);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.group_settings);
            this.Controls.Add(this.button_deleteRequest);
            this.Controls.Add(this.button_addRequest);
            this.Controls.Add(this.cb_priority);
            this.Controls.Add(this.label_priority);
            this.Controls.Add(this.cb_action);
            this.Controls.Add(this.label_action);
            this.Controls.Add(this.label_section);
            this.Controls.Add(this.tb_courseCode);
            this.Controls.Add(this.label_course);
            this.Controls.Add(this.listView_request);
            this.Controls.Add(this.label_request);
            this.Controls.Add(this.listView_registeredCourse);
            this.Controls.Add(this.label_registeredCourse);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.label_log);
            this.Controls.Add(this.button_logs);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_eid);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_eid);
            this.Controls.Add(this.label_copyright);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AIMS Add/Drop Helper - v0.5 R2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.group_settings.ResumeLayout(false);
            this.group_settings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void InitializeSectionComponent()
        {
            this.tb_sections = new System.Windows.Forms.TextBox[3];

            for (int i = 0; i < 3; i++)
            {
                this.tb_sections[i] = new System.Windows.Forms.TextBox();
                this.tb_sections[i].Enabled = false;
                this.tb_sections[i].Location = new System.Drawing.Point(463 + i * 36, 460);
                this.tb_sections[i].MaxLength = 3;
                this.tb_sections[i].Size = new System.Drawing.Size(30, 20);
                this.tb_sections[i].TabIndex = 6 + i;
                this.tb_sections[i].Leave += new System.EventHandler(this.CapitalizeInputText);

                this.Controls.Add(this.tb_sections[i]);
            }
        }

        private System.Windows.Forms.Label label_copyright;
        private System.Windows.Forms.Label label_eid;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox tb_eid;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Button button_logs;
        private System.Windows.Forms.Label label_log;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Label label_registeredCourse;
        private System.Windows.Forms.ListView listView_registeredCourse;
        private System.Windows.Forms.ColumnHeader listView_registeredCourse_CRN;
        private System.Windows.Forms.ColumnHeader listView_registeredCourse_courseCode;
        private System.Windows.Forms.ColumnHeader listView_registeredCourse_section;
        private System.Windows.Forms.ColumnHeader listView_registeredCourse_title;
        private System.Windows.Forms.ColumnHeader listView_registeredCourse_credit;
        private System.Windows.Forms.Label label_request;
        private System.Windows.Forms.ListView listView_request;
        private System.Windows.Forms.ColumnHeader listView_request_priority;
        private System.Windows.Forms.ColumnHeader listView_request_CRN;
        private System.Windows.Forms.ColumnHeader listView_request_courseCode;
        private System.Windows.Forms.ColumnHeader listView_request_section;
        private System.Windows.Forms.ColumnHeader listView_request_title;
        private System.Windows.Forms.ColumnHeader listView_request_status;
        private System.Windows.Forms.ColumnHeader listView_request_availability;
        private System.Windows.Forms.Label label_course;
        private System.Windows.Forms.TextBox tb_courseCode;
        private System.Windows.Forms.Label label_section;
        private System.Windows.Forms.TextBox[] tb_sections;
        private System.Windows.Forms.Label label_action;
        private System.Windows.Forms.ComboBox cb_action;
        private System.Windows.Forms.Label label_priority;
        private System.Windows.Forms.ComboBox cb_priority;
        private System.Windows.Forms.Button button_addRequest;
        private System.Windows.Forms.Button button_deleteRequest;
        private System.Windows.Forms.GroupBox group_settings;
        private System.Windows.Forms.Button button_registrationSwitch;
        private System.Windows.Forms.TextBox tb_delay;
        private System.Windows.Forms.Label label_delay;
        private System.Windows.Forms.TextBox tb_term;
        private System.Windows.Forms.Label label_term;
        private System.ComponentModel.BackgroundWorker registrationWorker;
    }
}


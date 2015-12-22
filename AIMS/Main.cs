using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AIMS
{
    public partial class Main : Form
    {
        private LogManager LogManager;
        private bool IsRegistering = false;

        // User data
        private String Eid;
        private String Password;
        private String SessionID;
        private String IPAddress;

        private List<Course> RegisteredCourseList;
        private List<Course> RequestList;

        // Settings
        private String AddDropTerm;
        private int Delay = 1000;

        public Main()
        {
            InitializeSectionComponent();
            InitializeComponent();

            LogManager = new LogManager();
            RequestList = new List<Course>();

            registrationWorker.WorkerSupportsCancellation = true;
        }

        private void InsertLog(String detail, Color? color = null)
        {
            LogManager.InsertLog(detail);

            if (color != null)
            {
                // Output to UI
                this.Invoke((MethodInvoker)delegate
                {
                    // Run on UI thread
                    String lastLog = LogManager.GetLastLog();
                    rtb_log.AppendText(lastLog + System.Environment.NewLine);
                    rtb_log.Find(lastLog);
                    rtb_log.SelectionColor = (Color)color;

                    rtb_log.SelectionStart = rtb_log.Text.Length;
                    rtb_log.ScrollToCaret();
                });
            }
        }

        private void Start()
        {
            // Get required login parameters from AIMS
            if (PrepareLogin())
            {
                this.Invoke((MethodInvoker)delegate
                {
                    // Run on UI thread
                    SetLoginControls(true);
                    tb_eid.Focus();
                });
            }
        }

        private void RegistrationLoop()
        {
            while (!CheckRegistrationStatus())
            {
                for (int i = 0; i < Math.Ceiling(Delay / 100.0); i++)
                {
                    if (registrationWorker.CancellationPending) return;
                    else Thread.Sleep(i < Math.Ceiling(Delay / 100.0) - 1 ? 100 : Delay - 100 * i);
                }
            }

            InsertLog("Ready for registration.", Color.Blue);

            // Start registration
            List<Course> pendingList = RequestList.Where(course => course.Status == Course.STATUS_PENDING).ToList<Course>();
            while (pendingList.Count > 0)
            {
                if (SendRegistrationRequests(pendingList.Where(course => course.Priority == (pendingList.Min(p => p.Priority))).ToList<Course>()))
                {
                    InsertLog("Registration succeeded.", Color.Blue);
                }
                else
                {
                    InsertLog("Registration failed, refer to status.", Color.Red);
                }

                pendingList = RequestList.Where(course => course.Status == Course.STATUS_PENDING || course.Status == Course.STATUS_SENT).ToList<Course>();

                if (pendingList.Count > 0)
                {
                    for (int i = 0; i < Math.Ceiling(Delay / 100.0); i++)
                    {
                        if (registrationWorker.CancellationPending) return;
                        else Thread.Sleep(i < Math.Ceiling(Delay / 100.0) - 1 ? 100 : Delay - 100 * i);
                    }
                }
            }

            // Finish registration
            InsertLog("Finished all registration.", Color.Blue);

            // Start looping closed section
            List<Course> closedList;
            do
            {
                closedList = RequestList.Where(course => course.Status == Course.STATUS_ERROR_WAITLIST_FULL || course.Status == Course.STATUS_ERROR_CLOSED).ToList<Course>();

                foreach (Course closedCourse in closedList)
                {
                    for (int i = 0; i < Math.Ceiling(Delay / 100.0); i++)
                    {
                        if (registrationWorker.CancellationPending) return;
                        else Thread.Sleep(i < Math.Ceiling(Delay / 100.0) - 1 ? 100 : Delay - 100 * i);
                    }

                    if (CheckCourseAvailabilityFromMasterClassSchedule(closedCourse))
                    {
                        InsertLog(closedCourse.CourseCode + " is available for registration.", Color.Blue);

                        if (SendRegistrationRequests(RequestList.Where(course => course.CourseCode == closedCourse.CourseCode && course.Priority == closedCourse.Priority).ToList<Course>()))
                        {
                            InsertLog("Registration succeeded.", Color.Blue);
                        }
                        else
                        {
                            InsertLog("Registration failed, refer to status.", Color.Red);
                        }
                    }
                }
            } while (closedList.Count > 0);
        }

        // Controls
        private void SetLoginControls(bool isEnabled)
        {
            tb_eid.Enabled = isEnabled;
            tb_password.Enabled = isEnabled;
            button_login.Enabled = isEnabled;

            // Settings
            tb_term.Enabled = isEnabled;
        }

        private void SetRegisteredCourseListView()
        {
            listView_registeredCourse.Items.Clear();

            RegisteredCourseList.Sort();
            foreach (Course course in RegisteredCourseList)
            {
                ListViewItem item = new ListViewItem(new String[]
                {
                    course.CRN,
                    course.CourseCode,
                    course.Section,
                    course.Title,
                    course.Credit.ToString()
                });
                listView_registeredCourse.Items.Add(item);
            }
        }

        private void SetRequestListView()
        {
            listView_request.Items.Clear();

            RequestList.Sort();
            foreach (Course course in RequestList)
            {
                ListViewItem item = new ListViewItem(new String[]
                {
                    course.Priority.ToString(),
                    course.CRN,
                    course.CourseCode,
                    course.Section,
                    course.Title,
                    "[" + Course.ACTION[course.Action].Replace("Web ", "") + "] " + course.Status,
                    course.AvaliableQuota + " /" + course.MaxQuota
                });
                listView_request.Items.Add(item);
            }
        }

        private void SetAddRequestControls(bool isEnabled)
        {
            if (isEnabled) tb_courseCode.Text = "";
            tb_courseCode.Enabled = isEnabled;

            foreach (TextBox tb_section in tb_sections)
            {
                if (isEnabled) tb_section.Text = "";
                tb_section.Enabled = isEnabled;
            }

            if (isEnabled)
            {
                cb_action.Items.Clear();
                cb_action.Items.AddRange(Course.ACTION);
                cb_action.Items.RemoveAt(Course.ACTION_WAIT);
                cb_action.SelectedIndex = 0;
            }
            cb_action.Enabled = isEnabled;

            if (isEnabled)
            {
                // Get the maximum priority
                int maxPriority = (RequestList.Count == 0 ? 0 : RequestList.Max(course => course.Priority)) + 1;
                cb_priority.Items.Clear();
                for (int i = 1; i <= maxPriority; i++)
                {
                    cb_priority.Items.Add(i);
                }
                cb_priority.SelectedIndex = Math.Max(0, cb_priority.Items.Count - 2);
            }
            cb_priority.Enabled = isEnabled;

            button_addRequest.Enabled = isEnabled;

            if (isEnabled) tb_courseCode.Focus();
        }

        // Listeners
        private void CapitalizeInputText(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
        }

        private void UpdateSettings(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;

            // Check term input
            Match match = (new Regex("^[0-9]{6}$", RegexOptions.IgnoreCase)).Match(tb_term.Text);
            if (match.Length == 0)
            {
                tb_term.SelectionStart = 0;
                tb_term.SelectionLength = tb_term.Text.Length;
                tb_term.Focus();
                return;
            }

            // Check delay input
            match = (new Regex("^[0-9]{1,5}$", RegexOptions.IgnoreCase)).Match(tb_delay.Text);
            if (match.Length == 0)
            {
                tb_delay.SelectionStart = 0;
                tb_delay.SelectionLength = tb_delay.Text.Length;
                tb_delay.Focus();
                return;
            }

            AddDropTerm = tb_term.Text;
            Delay = Convert.ToInt32(tb_delay.Text);

            InsertLog("Settings updated.", Color.Purple);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Main_Shown(object sender, System.EventArgs e)
        {
            InsertLog("Warning: For academic purpose only, please use at your own risk.", Color.Red);
            InsertLog("Program started.", Color.Black);

            // Find add drop term
            String log = "Now registering courses for ";
            AddDropTerm += DateTime.Now.Year;
            switch (DateTime.Now.Month)
            {
                case 12:
                case 1:
                case 2:
                case 3:
                case 4:
                    // Special case
                    if (DateTime.Now.Month == 12)
                    {
                        AddDropTerm = DateTime.Now.Year + 1 + "";
                        log += "Semester B " + DateTime.Now.Year + '/' + (DateTime.Now.Year + 1) % 2000;
                    }
                    else
                    {
                        log += "Semester B " + (DateTime.Now.Year - 1) + '/' + DateTime.Now.Year % 2000;
                    }

                    AddDropTerm += "02";
                    break;
                case 5:
                case 6:
                    AddDropTerm += "06";
                    log += "Summer Term " + DateTime.Now.Year;
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    AddDropTerm += "09";
                    log += "Semester A " + DateTime.Now.Year + '/' + (DateTime.Now.Year + 1) % 2000;
                    break;
                default:
                    AddDropTerm = "";
                    log = "Error";
                    break;
            }
            log += ", change it in settings.";
            InsertLog(log, Color.Blue);

            // Show settings
            tb_term.Text = AddDropTerm;
            tb_delay.Text = Delay.ToString();

            // Start
            (new Thread(Start)).Start();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            tb_eid.Text = tb_eid.Text.ToLower();

            Eid = tb_eid.Text;
            Password = tb_password.Text;

            // Check EID and password inputs
            if (Eid == "")
            {
                tb_eid.Focus();
                return;
            }
            if (Password == "")
            {
                tb_password.Focus();
                return;
            }

            SetLoginControls(false);

            // Login to AIMS
            if (Login())
            {
                RegisteredCourseList = GetCourseListFromRegisteredRecords();
                if (RegisteredCourseList != null)
                {
                    SetRegisteredCourseListView();
                    SetAddRequestControls(true);
                    button_registrationSwitch.Enabled = true;
                }
            }
            else
            {
                // Get required login parameters from AIMS
                if (PrepareLogin())
                {
                    SetLoginControls(true);
                    tb_password.Text = "";
                    tb_password.Focus();
                }
            }
        }

        private void button_logs_Click(object sender, EventArgs e)
        {
            LogViewer viewer = new LogViewer(LogManager);
            viewer.Show();
        }

        private void button_addRequest_Click(object sender, EventArgs e)
        {
            // Check course code input
            Match match = (new Regex("^[A-Z]{2,3}[0-9]{4,5}[A-Z]{0,1}$", RegexOptions.IgnoreCase)).Match(tb_courseCode.Text);
            if (match.Length == 0)
            {
                tb_courseCode.Focus();
                return;
            }

            // Check section inputs
            List<String> courseSectionList = new List<String>();
            foreach (TextBox tb_section in tb_sections)
            {
                if (tb_section.Text != "")
                {
                    match = (new Regex("^[A-Z]{1}[0-9]{2}$|^[A-Z]{2}[0-9]{1}$", RegexOptions.IgnoreCase)).Match(tb_section.Text);
                    if (match.Length == 0 || match.Length > 0 && courseSectionList.Contains(tb_section.Text))
                    {
                        tb_section.Focus();
                        return;
                    }
                    courseSectionList.Add(tb_section.Text);
                }
            }
            if (courseSectionList.Count == 0)
            {
                tb_sections[0].Focus();
                return;
            }

            // Check action input
            if (cb_action.SelectedIndex == 0)
            {
                cb_action.Focus();
                return;
            }

            SetAddRequestControls(false);

            // Get course details from Master Class Schedule
            List<Course> newRequestList = GetCourseListFromMasterClassSchedule(tb_courseCode.Text, courseSectionList);
            if (newRequestList != null)
            {
                int requestCount = 0;
                foreach (Course course in newRequestList)
                {
                    course.Priority = Convert.ToInt32(cb_priority.Text);
                    course.Action = cb_action.SelectedIndex;
                    course.Status = Course.STATUS_PENDING;

                    if (!RequestList.Contains(course))
                    {
                        RequestList.Add(course);
                        requestCount++;
                    }
                    else
                    {
                        InsertLog("Cannot add duplicated request for section " + course.Section + ".", Color.Red);
                    }
                }
                SetRequestListView();

                InsertLog("Added " + requestCount + " add/drop request(s).", Color.Blue);
            }

            SetAddRequestControls(true);
        }

        private void button_deleteRequest_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView_request.SelectedItems)
            {
                RequestList.RemoveAt(item.Index);
            }

            SetRequestListView();
            SetAddRequestControls(true);
            button_deleteRequest.Enabled = false;
        }

        private void button_registrationSwitch_Click(object sender, EventArgs e)
        {
            if (IsRegistering)
            {
                // Abort registration worker
                registrationWorker.CancelAsync();
            }
            else
            {
                // Reset course list
                List<Course> closedList = RequestList.Where(course => course.Status == Course.STATUS_ERROR_WAITLIST_FULL || course.Status == Course.STATUS_ERROR_CLOSED).ToList<Course>();
                foreach (Course closedCourse in closedList)
                {
                    closedCourse.Status = Course.STATUS_PENDING;
                }

                listView_request.SelectedItems.Clear();
                SetAddRequestControls(false);
                button_registrationSwitch.Text = "Stop";

                InsertLog("Thread started.", Color.Black);
                IsRegistering = true;

                // Start registration worker
                registrationWorker.RunWorkerAsync();
            }
        }

        private void label_copyright_Click(object sender, EventArgs e)
        {
            button_logs.Visible = !button_logs.Visible;
        }

        private void listView_registeredCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItems.Clear();
        }

        private void listView_request_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsRegistering)
            {
                ((ListView)sender).SelectedItems.Clear();
                return;
            }

            SetAddRequestControls(((ListView)sender).SelectedItems.Count == 0);
            button_deleteRequest.Enabled = (((ListView)sender).SelectedItems.Count > 0);
        }

        private void tb_term_Leave(object sender, EventArgs e)
        {
            tb_term.Text = AddDropTerm;
        }

        private void tb_delay_Leave(object sender, EventArgs e)
        {
            tb_delay.Text = Delay.ToString();
        }

        private void registrationWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            RegistrationLoop();
        }

        private void registrationWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            SetAddRequestControls(true);
            button_registrationSwitch.Text = "Start";

            InsertLog("Thread aborted.", Color.Red);
            IsRegistering = false;
        }
    }
}

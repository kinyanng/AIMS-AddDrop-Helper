using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AIMS
{
    public partial class Main : Form
    {
        // Web interaction methods
        private bool PrepareLogin()
        {
            InsertLog("Waiting response from AIMS server...", Color.Black);

            WebManager loginPage = new WebManager("https://banweb.cityu.edu.hk/pls/PROD/twgkpswd_cityu.P_WWWLogin");
            try
            {
                loginPage.Load();
                InsertLog(loginPage.ToString());

                InsertLog("Server responsed.", Color.Blue);

                // Get required login parameters
                foreach (HtmlAgilityPack.HtmlAttribute attr in loginPage.GetHtmlNode("//input[@name='p_sess_id']").Attributes.AttributesWithName("value"))
                {
                    SessionID = attr.Value;
                }
                foreach (HtmlAgilityPack.HtmlAttribute attr in loginPage.GetHtmlNode("//input[@name='p_ip']").Attributes.AttributesWithName("value"))
                {
                    IPAddress = attr.Value;
                }

                InsertLog("p_ip: " + IPAddress + ", p_sess_id: " + SessionID, Color.Green);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (ex is WebException)
                {
                    InsertLog("Server returned error: " + ex.Message, Color.Red);
                }
                else if (ex is NullReferenceException)
                {
                    InsertLog("Cannot retrieve the login parameters.", Color.Red);
                }
                else InsertLog(ex.ToString(), Color.Red);
            }

            return false;
        }

        private bool Login()
        {
            InsertLog("Logging in...", Color.Black);

            WebManager loginPage = new WebManager("https://banweb.cityu.edu.hk/pls/PROD/twgkpswd_cityu.P_WWWLogin", WebManager.POST);
            try
            {
                loginPage.AddParameter("p_username", Eid)
                        .AddParameter("p_password", Password)
                        .AddParameter("p_sess_id", SessionID)
                        .AddParameter("p_ip", IPAddress)
                        .AddParameter("to_url", "")
                        .Load();
                InsertLog(loginPage.ToString());

                // Check login status
                if (loginPage.GetHtmlNode("//meta[@http-equiv='refresh']") != null)
                {
                    InsertLog("Logged in as " + Eid, Color.Blue);

                    return true;
                }
                else
                {
                    InsertLog("Cannot verify your electronic ID and password.", Color.Red);

                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (ex is WebException)
                {
                    InsertLog("Server returned error: " + ex.Message, Color.Red);
                }
                else InsertLog(ex.ToString(), Color.Red);
            }

            return false;
        }

        private List<Course> GetCourseListFromRegisteredRecords()
        {
            InsertLog("Retrieving your active registered course...", Color.Black);

            WebManager activeCoursePage = new WebManager("https://banweb.cityu.edu.hk/pls/PROD/hwskaddp_cityu.P_ChkReg");
            try
            {
                activeCoursePage.Load();
                InsertLog(activeCoursePage.ToString());

                String title = activeCoursePage.GetHtmlNode("//h2").InnerText;
                label_registeredCourse.Text = label_registeredCourse.Text.Replace(":", " for " + title.Replace("Active Registration (", "").Replace(")", "") + ":");

                List<Course> toReturn = new List<Course>();
                foreach (HtmlAgilityPack.HtmlNode row in activeCoursePage.GetHtmlNodeCollection("//table[2]//tr"))
                {
                    HtmlAgilityPack.HtmlNodeCollection courseDetails = row.SelectNodes("td");

                    // Ignore the first row
                    if (courseDetails != null && courseDetails[0].InnerText != "CRN")
                    {
                        Course course = new Course();

                        course.CRN = courseDetails[0].InnerText;
                        course.CourseCode = courseDetails[1].InnerText;
                        course.Section = courseDetails[2].InnerText;
                        course.Title = courseDetails[3].InnerText;

                        course.Credit = Convert.ToSingle(courseDetails[4].InnerText);

                        toReturn.Add(course);
                    }
                }

                InsertLog("Retrieved registered course.", Color.Blue);

                return toReturn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (ex is WebException)
                {
                    InsertLog("Server returned error: " + ex.Message, Color.Red);

                    if (ex.Message == "Break in attempt" || ex.Message == "Session timeout")
                    {
                        if (PrepareLogin() && Login()) return GetCourseListFromRegisteredRecords();
                    }
                }
                else if (ex is NullReferenceException)
                {
                    InsertLog("Cannot retrieve registered course.", Color.Red);
                }
                else InsertLog(ex.ToString(), Color.Red);
            }

            return null;
        }

        private List<Course> GetCourseListFromMasterClassSchedule(String courseCode, List<String> courseSectionList)
        {
            InsertLog("Retrieving course details of " + courseCode + "...", Color.Black);

            WebManager schedulePage = new WebManager("https://banweb.cityu.edu.hk/pls/PROD/hwscrssh_cityu.P_DispOneSection");
            try
            {
                String[] courseSubjectAndCode = Regex.Split(courseCode, @"(?<=\D)(?=\d)");
                schedulePage.SetReferer("https://banweb.cityu.edu.hk/pls/PROD/hwscrssh_cityu.P_GetCrse")
                        .AddParameter("term", AddDropTerm)
                        .AddParameter("subj", courseSubjectAndCode[0])
                        .AddParameter("crse", courseSubjectAndCode[1])
                        .AddParameter("camp", "")
                        .AddParameter("web_avail", "")
                        .Load();
                InsertLog(schedulePage.ToString());

                // Retrieve the course title
                String courseTitle = "";
                String htmlSource = schedulePage.GetHtmlNode("//div[@class='body']/b[1]").InnerText;
                if (htmlSource.IndexOf("Course : " + courseCode + " ") >= 0)
                {
                    courseTitle = htmlSource.Substring(htmlSource.IndexOf("Course : " + courseCode + " ") + courseCode.Length + 10);

                    InsertLog("title: " + courseTitle, Color.Green);
                }
                else throw new NullReferenceException();

                // Retrive the course details
                List<Course> toReturn = new List<Course>();
                HtmlAgilityPack.HtmlNodeCollection courseTable = schedulePage.GetHtmlNodeCollection("//div[@class='body']//tr[@bgcolor='#ffccff']");
                if (courseTable != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode row in courseTable)
                    {
                        HtmlAgilityPack.HtmlNodeCollection courseDetails = row.SelectNodes("td");

                        // Ignore the duplicate rows
                        if (courseDetails[0].InnerText != "" && courseSectionList.Remove(courseDetails[1].InnerText))
                        {
                            Course course = new Course();

                            course.CRN = courseDetails[0].InnerText;
                            course.CourseCode = courseCode;
                            course.Section = courseDetails[1].InnerText;
                            course.Title = courseTitle;

                            course.Credit = Convert.ToSingle(courseDetails[2].InnerText);
                            course.Date = courseDetails[9].InnerText;
                            course.DayOfWeek = courseDetails[10].InnerText;
                            course.Time = courseDetails[11].InnerText;
                            course.Room = courseDetails[13].InnerText + ", " + courseDetails[12].InnerText;
                            course.Instructor = courseDetails[14].InnerText;

                            courseDetails[6].SelectSingleNode("span[@style='display:none']")?.Remove();
                            int.TryParse(courseDetails[6].InnerText, out course.AvaliableQuota);
                            course.MaxQuota = Convert.ToInt32(courseDetails[7].InnerText);

                            courseDetails[8].SelectSingleNode("span[@style='display:none']")?.Remove();
                            int.TryParse(courseDetails[8].InnerText, out course.WaitlistQuota);

                            toReturn.Add(course);
                        }
                    }
                }

                String notFoundSections = "";
                foreach (String courseSection in courseSectionList)
                {
                    notFoundSections += (notFoundSections == "" ? "" : ", ") + courseSection;
                }
                if (notFoundSections != "")
                {
                    InsertLog("Cannot find section(s) " + notFoundSections + ".", Color.Red);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (ex is WebException)
                {
                    InsertLog("Server returned error: " + ex.Message, Color.Red);

                    if (ex.Message == "Break in attempt" || ex.Message == "Session timeout")
                    {
                        if (PrepareLogin() && Login()) return GetCourseListFromMasterClassSchedule(courseCode, courseSectionList);
                    }
                }
                else if (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                {
                    InsertLog("Cannot retrieve the course details.", Color.Red);
                }
                else InsertLog(ex.ToString(), Color.Red);
            }

            return null;
        }

        private bool CheckCourseAvailabilityFromMasterClassSchedule(Course course)
        {
            InsertLog("Checking availability of " + course.CourseCode + " " + course.Section + "...", Color.Black);

            WebManager schedulePage = new WebManager("https://banweb.cityu.edu.hk/pls/PROD/hwscrssh_cityu.P_DispOneSection");
            try
            {
                String[] courseSubjectAndCode = Regex.Split(course.CourseCode, @"(?<=\D)(?=\d)");
                schedulePage.SetReferer("https://banweb.cityu.edu.hk/pls/PROD/hwscrssh_cityu.P_GetCrse")
                        .AddParameter("term", AddDropTerm)
                        .AddParameter("subj", courseSubjectAndCode[0])
                        .AddParameter("crse", courseSubjectAndCode[1])
                        .AddParameter("camp", "")
                        .AddParameter("web_avail", "")
                        .Load();
                InsertLog(schedulePage.ToString());

                // Retrive the course details
                foreach (HtmlAgilityPack.HtmlNode row in schedulePage.GetHtmlNodeCollection("//div[@class='body']//tr[@bgcolor='#ffccff']"))
                {
                    HtmlAgilityPack.HtmlNodeCollection courseDetails = row.SelectNodes("td");

                    if (courseDetails[1].InnerText == course.Section)
                    {
                        courseDetails[6].SelectSingleNode("span[@style='display:none']")?.Remove();
                        int.TryParse(courseDetails[6].InnerText.Substring(2), out course.AvaliableQuota);

                        courseDetails[8].SelectSingleNode("span[@style='display:none']")?.Remove();
                        int.TryParse(courseDetails[8].InnerText, out course.WaitlistQuota);
                        break;
                    }
                }

                return (course.AvaliableQuota > 0 || course.WaitlistQuota > 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (ex is WebException)
                {
                    InsertLog("Server returned error: " + ex.Message, Color.Red);

                    if (ex.Message == "Break in attempt" || ex.Message == "Session timeout")
                    {
                        if (PrepareLogin() && Login()) return CheckCourseAvailabilityFromMasterClassSchedule(course);
                    }
                }
                else if (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                {
                    InsertLog("Cannot retrieve the course details.", Color.Red);
                }
                else InsertLog(ex.ToString(), Color.Red);
            }

            return false;
        }

        private bool CheckRegistrationStatus()
        {
            InsertLog("Checking registration status...", Color.Black);

            WebManager registrationPage = new WebManager("https://banweb.cityu.edu.hk/pls/PROD/bwskfreg.P_AltPin", WebManager.POST);
            try
            {
                registrationPage.AddParameter("term_in", AddDropTerm).Load();
                InsertLog(registrationPage.ToString());

                HtmlAgilityPack.HtmlNode infoText = registrationPage.GetHtmlNode("//span[@class='infotext']");
                if (infoText != null && infoText.InnerText.Contains("You may register during the following times"))
                {
                    return false;
                }
                else if (infoText != null && infoText.InnerText.Contains("WEB REGISTRATION PERIOD"))
                {
                    return true;
                }

                throw new WebException("Server busy or Unknown error");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (ex is WebException)
                {
                    InsertLog("Server returned error: " + ex.Message, Color.Red);

                    if (ex.Message == "Break in attempt" || ex.Message == "Session timeout")
                    {
                        if (PrepareLogin() && Login()) return CheckRegistrationStatus();
                    }
                }
                else InsertLog(ex.ToString(), Color.Red);
            }

            return false;
        }

        private bool SendRegistrationRequests(List<Course> requestList)
        {
            if (requestList.Count == 0) return true;

            InsertLog("Sending your registration request (priority: " + requestList[0].Priority + ")...", Color.Black);

            WebManager registrationPage = new WebManager("https://banweb.cityu.edu.hk/pls/PROD/bwckcoms.P_Regs", WebManager.POST);
            try
            {
                registrationPage.AddParameter("term_in", AddDropTerm)
                        .AddParameter("RSTS_IN", "DUMMY")
                        .AddParameter("assoc_term_in", "DUMMY")
                        .AddParameter("CRN_IN", "DUMMY")
                        .AddParameter("start_date_in", "DUMMY")
                        .AddParameter("end_date_in", "DUMMY")
                        .AddParameter("SUBJ", "DUMMY")
                        .AddParameter("CRSE", "DUMMY")
                        .AddParameter("SEC", "DUMMY")
                        .AddParameter("LEVL", "DUMMY")
                        .AddParameter("CRED", "DUMMY")
                        .AddParameter("GMOD", "DUMMY")
                        .AddParameter("TITLE", "DUMMY")
                        .AddParameter("MESG", "DUMMY")
                        .AddParameter("REG_BTN", "DUMMY")
                        .AddParameter("MESG", "DUMMY");

                // Drop requests
                int dropCount = 0;
                foreach (Course course in requestList.Where(course => course.Action == Course.ACTION_DROP))
                {
                    course.Status = Course.STATUS_SENT;

                    registrationPage.AddParameter("RSTS_IN", "DW")
                            .AddParameter("CRN_IN", course.CRN)
                            .AddParameter("assoc_term_in", "")
                            .AddParameter("start_date_in", "")
                            .AddParameter("end_date_in", "");
                    dropCount++;
                }

                // Wait requests
                int waitCount = 0;
                foreach (Course course in requestList.Where(course => course.Action == Course.ACTION_WAIT))
                {
                    course.Status = Course.STATUS_SENT;

                    registrationPage.AddParameter("RSTS_IN", "WL")
                            .AddParameter("CRN_IN", course.CRN)
                            .AddParameter("assoc_term_in", "")
                            .AddParameter("start_date_in", "")
                            .AddParameter("end_date_in", "");
                    waitCount++;
                }

                // Add requests
                int addCount = 0;
                foreach (Course course in requestList.Where(course => course.Action == Course.ACTION_ADD))
                {
                    course.Status = Course.STATUS_SENT;

                    registrationPage.AddParameter("RSTS_IN", "RW")
                            .AddParameter("CRN_IN", course.CRN)
                            .AddParameter("assoc_term_in", "")
                            .AddParameter("start_date_in", "")
                            .AddParameter("end_date_in", "");
                    addCount++;
                }

                // Update request list view
                this.Invoke((MethodInvoker)delegate
                {
                    // Run on UI thread
                    SetRequestListView();
                });

                registrationPage.AddParameter("regs_row", dropCount.ToString())
                        .AddParameter("wait_row", waitCount.ToString())
                        .AddParameter("add_row", addCount.ToString())
                        .AddParameter("REG_BTN", "Submit Changes")
                        .Load();
                InsertLog(registrationPage.ToString());

                // Check response content
                HtmlAgilityPack.HtmlNode infoText = registrationPage.GetHtmlNode("//span[@class='infotext']");
                if (infoText == null || !infoText.InnerText.Contains("WEB REGISTRATION PERIOD"))
                {
                    throw new WebException("Server busy or Unknown error");
                }

                // Update registered course list
                RegisteredCourseList.Clear();
                foreach (HtmlAgilityPack.HtmlNode row in registrationPage.GetHtmlNodeCollection("//table[@summary='Current Schedule']//tr"))
                {
                    HtmlAgilityPack.HtmlNodeCollection courseDetails = row.SelectNodes("td");

                    // Ignore the first row with <th>
                    if (courseDetails != null)
                    {
                        Course course = new Course();

                        course.CRN = courseDetails[2].InnerText;
                        course.CourseCode = courseDetails[3].InnerText + courseDetails[4].InnerText;
                        course.Section = courseDetails[5].InnerText;
                        course.Title = courseDetails[9].InnerText;

                        course.Credit = Convert.ToSingle(courseDetails[7].InnerText);

                        RegisteredCourseList.Add(course);
                    }
                }

                // Check registration errors
                bool registrationError = false;

                HtmlAgilityPack.HtmlNodeCollection errorTable = registrationPage.GetHtmlNodeCollection("//table[@summary='This layout table is used to present Drop or Withdrawal Errors.']//tr");
                if (errorTable != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode row in errorTable)
                    {
                        HtmlAgilityPack.HtmlNodeCollection errorDetails = row.SelectNodes("td");

                        // Ignore the first row with <th>
                        if (errorDetails != null)
                        {
                            foreach (Course course in requestList.Where(course => errorDetails[4].InnerText.Contains(course.CRN)))
                            {
                                course.Status = "Error: " + errorDetails[4].InnerText;
                                registrationError = true;
                            }
                        }
                    }
                }

                errorTable = registrationPage.GetHtmlNodeCollection("//table[@summary='This layout table is used to present Registration Errors.']//tr");
                if (errorTable != null)
                {
                    foreach (HtmlAgilityPack.HtmlNode row in errorTable)
                    {
                        HtmlAgilityPack.HtmlNodeCollection errorDetails = row.SelectNodes("td");

                        // Ignore the first row with <th>
                        if (errorDetails != null)
                        {
                            foreach (Course course in requestList.Where(course => course.CRN == errorDetails[1].InnerText || course.CRN == errorDetails[2].InnerText))
                            {
                                // Check waitlist
                                if (errorDetails[0].InnerText.Contains(Course.STATUS_WAITLISTED))
                                {
                                    course.Action = Course.ACTION_WAIT;
                                    course.Status = Course.STATUS_PENDING;
                                }
                                else
                                {
                                    course.Status = "Error: " + errorDetails[0].InnerText;
                                    registrationError = true;
                                }
                            }
                        }
                    }
                }

                // Change the status to completed
                foreach (Course course in requestList.Where(course => course.Status == Course.STATUS_SENT))
                {
                    course.Status = Course.STATUS_COMPLETED;
                }

                // Update list views
                this.Invoke((MethodInvoker)delegate
                {
                    // Run on UI thread
                    SetRegisteredCourseListView();
                    SetRequestListView();
                });

                return (!registrationError);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                if (ex is WebException)
                {
                    InsertLog("Server returned error: " + ex.Message, Color.Red);

                    if (ex.Message == "Break in attempt" || ex.Message == "Session timeout")
                    {
                        if (PrepareLogin() && Login()) return SendRegistrationRequests(requestList);
                    }
                }
                else InsertLog(ex.ToString(), Color.Red);
            }

            return false;
        }
    }
}

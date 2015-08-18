using System;
using System.Text.RegularExpressions;

namespace AIMS
{
    class Course : IComparable
    {
        public static readonly String[] ACTION = new String[] { "", "Web Drop", "Web Add", "Waitlist" };
        public const int ACTION_DROP = 1;
        public const int ACTION_ADD = 2;
        public const int ACTION_WAIT = 3;

        public const String STATUS_PENDING = "Pending";
        public const String STATUS_SENT = "Sent";
        public const String STATUS_COMPLETED = "Completed";
        public const String STATUS_WAITLISTED = "Waitlisted";
        public const String STATUS_ERROR_WAITLIST_FULL = "Error: Closed - Waitlist Full ";
        public const String STATUS_ERROR_CLOSED = "Error: Closed Section ";

        public String CRN = "";
        public String CourseCode = "";
        public String Section = "";
        public String Title = "";

        public float Credit = 0;
        public String Date = "";
        public String DayOfWeek = "";
        public String Time = "";
        public String Room = "";
        public String Instructor = "";

        public int AvaliableQuota = 0;
        public int MaxQuota = 0;
        public int WaitlistQuota = 0;

        public int Priority = 0;
        public int Action = 0;
        public String Status = "";

        public int CompareTo(Object obj)
        {
            Course otherCourse = obj as Course;
            if (otherCourse == null) throw new ArgumentException("Object is not a Course.");

            if (this.Priority != otherCourse.Priority) return this.Priority.CompareTo(otherCourse.Priority);
            if (this.CourseCode != otherCourse.CourseCode) return this.CourseCode.CompareTo(otherCourse.CourseCode);
            else return this.Section[0].CompareTo(otherCourse.Section[0]);
        }

        public override bool Equals(object obj)
        {
            Course otherCourse = obj as Course;
            if (otherCourse == null) throw new ArgumentException("Object is not a Course.");

            return (this.Priority == otherCourse.Priority && this.CourseCode == otherCourse.CourseCode && this.Section[0] == otherCourse.Section[0]);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

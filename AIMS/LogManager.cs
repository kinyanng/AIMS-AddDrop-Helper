using System;
using System.Collections.Generic;

namespace AIMS
{
    public class LogManager
    {
        private List<String> LogList;

        public LogManager()
        {
            LogList = new List<String>();
        }

        public LogManager InsertLog(String detail)
        {
            string log = @"[" + DateTime.Now.ToString("HH:mm:ss") + "] " + detail;
            LogList.Add(log);

            return this;
        }

        public String GetLastLog()
        {
            return (String)LogList[LogList.Count - 1];
        }

        public override String ToString()
        {
            String toReturn = "";

            foreach (String log in LogList){
                toReturn += log + System.Environment.NewLine;
            }
            return toReturn;
        }
    }
}

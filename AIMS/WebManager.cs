using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.XPath;

namespace AIMS
{
    class WebManager
    {
        public const String GET = "GET";
        public const String POST = "POST";
        public const String HOST = "banweb.cityu.edu.hk";
        public const String ORIGIN = "https://banweb.cityu.edu.hk";
        public const String USER_AGENT = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";

        private String RequestUrl;
        private String Referer;
        private String Method;
        private String ParameterString;

        private HttpWebRequest Request;
        private String ResponseHeaders;
        private HtmlAgilityPack.HtmlDocument HtmlDoc;

        private static CookieContainer Cookie = new CookieContainer();

        public WebManager(String requestUrl, String method = GET)
        {
            RequestUrl = requestUrl;
            Referer = requestUrl;
            Method = method;
            ParameterString = "";
        }

        public WebManager SetReferer(String referer)
        {
            Referer = referer;

            return this;
        }

        public WebManager AddParameter(String name, String value)
        {
            if (ParameterString != "") ParameterString += "&";
            ParameterString += HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode(value);

            return this;
        }

        public WebManager AddParametersByList(List<String> nameAndValuePairList)
        {
            if (nameAndValuePairList.Count % 2 != 0) throw new ArgumentOutOfRangeException("Insufficient arguments");

            for (int i = 0; i < nameAndValuePairList.Count / 2; i++)
            {
                if (ParameterString != "") ParameterString += "&";
                ParameterString += HttpUtility.UrlEncode(nameAndValuePairList[i * 2]) + "="
                        + HttpUtility.UrlEncode(nameAndValuePairList[i * 2 + 1]);
            }

            return this;
        }

        public String Load()
        {
            Request = (HttpWebRequest)HttpWebRequest.Create(RequestUrl + (Method == GET ? "?" + ParameterString : ""));

            // Set header
            Request.Host = HOST;
            Request.Headers.Add("Origin", ORIGIN);
            Request.Referer = Referer;
            Request.UserAgent = USER_AGENT;
            Request.CookieContainer = Cookie;

            Request.Method = Method;
            if (Method == POST)
            {
                byte[] bs = Encoding.ASCII.GetBytes(ParameterString);
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = bs.Length;

                using (Stream stream = Request.GetRequestStream())
                {
                    stream.Write(bs, 0, bs.Length);
                }
            }
            Debug.WriteLine("** Request Header **" + System.Environment.NewLine + Request.Headers);
            Debug.WriteLine("Method: " + Method + ", Content: " + ParameterString);

            String htmlSource = "";

            // Send request to server
            using (HttpWebResponse response = (HttpWebResponse)Request.GetResponse())
            {
                ResponseHeaders = response.Headers.ToString();
                Debug.WriteLine("** Response Header **" + System.Environment.NewLine + ResponseHeaders);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Cookie = new CookieContainer();
                    foreach (Cookie cookie in response.Cookies)
                    {
                        Cookie.Add(new Uri(ORIGIN), cookie);
                        Debug.WriteLine("Cookie: " + cookie);
                    }

                    using (StreamReader responseReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8")))
                    {
                        htmlSource = responseReader.ReadToEnd();
                    }

                    HtmlDoc = new HtmlAgilityPack.HtmlDocument();
                    HtmlDoc.LoadHtml(htmlSource);

                    // Check session
                    if (htmlSource.Contains("A break in attempt has been detected!"))
                    {
                        throw new WebException("Break in attempt");
                    }
                    else if (htmlSource.Contains("Your AIMS session has been timeout (15 minutes inactivity)."))
                    {
                        throw new WebException("Session timeout");
                    }
                }
                else
                {
                    throw new WebException("Status " + response.StatusCode);
                }
            }

            return htmlSource;
        }

        public HtmlAgilityPack.HtmlNode GetHtmlNode(String xPath)
        {
            try
            {
                return HtmlDoc.DocumentNode.SelectSingleNode(xPath);
            }
            catch (XPathException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public HtmlAgilityPack.HtmlNodeCollection GetHtmlNodeCollection(String xPath)
        {
            try
            {
                return HtmlDoc.DocumentNode.SelectNodes(xPath);
            }
            catch (XPathException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public override string ToString()
        {
            try
            {
                String toReturn = "** Request Header **" + System.Environment.NewLine + Request.Headers;
                toReturn += System.Environment.NewLine + "Method: " + Method + ", Content: " + ParameterString + System.Environment.NewLine;
                toReturn += System.Environment.NewLine + "** Response Header **" + System.Environment.NewLine + ResponseHeaders;
                //toReturn += System.Environment.NewLine + "** Response Content **" + System.Environment.NewLine + HtmlDoc.DocumentNode.OuterHtml;

                return base.ToString() + System.Environment.NewLine + toReturn;
            }
            catch (Exception) { }

            return base.ToString();
        }
    }
}

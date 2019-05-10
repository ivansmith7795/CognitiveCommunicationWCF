using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace CognitiveCommunicationWCF
{
    public class TextProcessor
    {
        public string Convert2Text(Suggestion suggestion, string html)
        {
            string convertedText = "";

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            //lets replace all the app references
            HtmlNodeCollection apps = doc.DocumentNode.SelectNodes("//span[@class='app_name']");
            if (apps != null)
            {
                foreach (HtmlNode appNode in apps)
                {
                    appNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.application), appNode);
                }
            }
            //lets replace all the start_date references
            HtmlNodeCollection startdates = doc.DocumentNode.SelectNodes("//span[@class='start_date']");
            if (startdates !=null)
            {
                foreach (HtmlNode startdateNode in startdates)
                {
                    startdateNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.release.ToString("h:mm tt dddd, MMMM dd, yyyy") + " (Mountain Time)"), startdateNode);
                }
            }
            //lets replace all the start_date_short references
            HtmlNodeCollection startdateshorts = doc.DocumentNode.SelectNodes("//span[@class='start_date_short']");
            if (startdateshorts != null)
            {
                foreach (HtmlNode startdateshortNode in startdateshorts)
                {
                    startdateshortNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.release.ToString("h:mm tt dddd, MMMM dd")), startdateshortNode);
                }
            }

            //lets replace all the start_date_time_only references
            HtmlNodeCollection startdatestimeonly = doc.DocumentNode.SelectNodes("//span[@class='start_date_time_only']");
            if (startdatestimeonly != null)
            {
                foreach (HtmlNode startdatetimeonlyNode in startdatestimeonly)
                {
                    startdatetimeonlyNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.release.ToString("h:mm tt")), startdatetimeonlyNode);
                }
            }

            //lets replace all the end_date references
            HtmlNodeCollection enddates = doc.DocumentNode.SelectNodes("//span[@class='end_date']");
            if (enddates != null)
            {
                foreach (HtmlNode enddateNode in enddates)
                {
                    enddateNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.archive.ToString("h:mm tt dddd, MMMM dd, yyyy") + " (Mountain Time)"), enddateNode);
                }
            }
            //lets replace all the end_date_short references
            HtmlNodeCollection enddateshorts = doc.DocumentNode.SelectNodes("//span[@class='end_date_short']");
            if (enddateshorts != null)
            {
                foreach (HtmlNode enddateshortNode in enddateshorts)
                {
                    enddateshortNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.archive.ToString("h:mm tt dddd, MMMM dd")), enddateshortNode);
                }
            }

            //lets replace all the end_date_time_only references
            HtmlNodeCollection enddatetimesonly = doc.DocumentNode.SelectNodes("//span[@class='end_date_time_only']");
            if (enddatetimesonly != null)
            {
                foreach (HtmlNode enddatetimeonlyNode in enddatetimesonly)
                {
                    enddatetimeonlyNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.archive.ToString("h:mm tt")), enddatetimeonlyNode);
                }
            }

            //lets replace all the single_date references
            HtmlNodeCollection singledates = doc.DocumentNode.SelectNodes("//span[@class='single_date']");
            if (singledates != null)
            {
                foreach (HtmlNode singledateNode in singledates)
                {
                    singledateNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.release.ToString("h:mm tt")), singledateNode);
                }
            }
            //lets replace all the change_number references
            HtmlNodeCollection changenumbers = doc.DocumentNode.SelectNodes("//span[@class='change_number']");
            if (changenumbers != null)
            {
                foreach (HtmlNode changenumberNode in changenumbers)
                {
                    changenumberNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.changeNumber), changenumberNode);
                }
            }

            //lets replace all the support_group references
            HtmlNodeCollection supportgroups = doc.DocumentNode.SelectNodes("//span[@class='support_group']");
            if (supportgroups != null)
            {
                foreach (HtmlNode supportgroupNode in supportgroups)
                {
                    supportgroupNode.ParentNode.ReplaceChild(HtmlTextNode.CreateNode(suggestion.changeGroup), supportgroupNode);
                }
            }

            convertedText = doc.DocumentNode.InnerHtml;

            return convertedText;
        }

        public  string GenerateDescriptionText(string html)
        {


            string description = StripHTML(html);

            string cleanedText = CleanBodyText(description);

            string newString = "";
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            newString = regex.Replace(cleanedText, " ");


            return newString.Trim();
        }

        public  string StripHTML(string input)
        {
            string strippedTags = Regex.Replace(input, "<.*?>", String.Empty);

            strippedTags = Regex.Replace(strippedTags, "&nbsp;", " ");

            return strippedTags;
        }
        public  string CleanBodyText(string bodyText)
        {
            string result = Regex.Replace(bodyText, @"\r\n?|\n", " ");
            return result;
        }
    }
}
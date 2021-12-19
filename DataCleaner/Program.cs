using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace DataCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            //get Config items
            string path = ConfigurationManager.AppSettings["path"];

            //create lists to store values
            var cleanedList = new List<string>();
            var finalList = new List<Dictionary<string, string>>();

            //regex
            //todo: move to config 12/18/2021
            Regex usernameComment = new Regex("<span class=\"Jv7Aj.*?href=\"https://www\\.instagram\\.com\\/.*?<div");
            Regex removeHref = new Regex("href=.*</");
            Regex getUserName = new Regex("tabindex=.*");
            Regex cleanUsername = new Regex("(?<=tabindex=\"0\">)(.*)(?=<\\/a>)");
            Regex cleanFinalUsername = new Regex("</a(>[\\s\\S]*)$");
            Regex getText = new Regex("(?<=< span class=\"\">)(.*)(?=<\\/)");

            try
            {
                //read the doc
                string htmlText = readFileFromPath(path);

                //get initial matches
                cleanedList = matchesToList(usernameComment, htmlText);

                foreach (var item in cleanedList)
                {
                    // parse the doc
                    // break down to components
                    finalList.Append(getUsernameComments());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }  

            //convert to Excel file 
            convertToExce();

            //save as csv
            convertToCSV();

        }

        private static Dictionary<string, string> getUsernameComments()
        {
            throw new NotImplementedException();
        }

        private static void convertToExce()
        {
            throw new NotImplementedException();
        }

        private static void convertToCSV()
        {
            throw new NotImplementedException();
        }

        private static string readFileFromPath(string path)
        {
            string fullText = "";
            if (File.Exists(path))
            {
                Console.WriteLine($"Reading HTML file at {path}");
                fullText = File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine($"No file found at {path}.");
            }
          
            return fullText;
        }

        private static List<string> matchesToList(Regex regex, string text)
        {
            MatchCollection matches = regex.Matches(text);
            var matchesList = matches.Cast<Match>()
                                        .Select(match => match.Value)
                                        .ToList();
            return matchesList;
        }
    }
}

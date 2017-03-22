using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TechJobsConsole
{
    class JobData
    {
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();
            return AllJobs;
        }

        public static List<string> FindAll(string column)
            
        {
            LoadData();
            List<string> values = new List<string>();
            foreach(Dictionary<string, string> jobs in AllJobs)
            {
                string aValue = jobs[column];
                if (!values.Contains(aValue))
                    {
                    values.Add(aValue);
                }
            }
            return values;
        }
        public static List<Dictionary<string,string>> FindByColumnAndValue(string column, string value)
        {
            LoadData();
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();
            foreach(Dictionary<string, string> rows in AllJobs)
            {
                string aValue = rows[column];
                if (aValue.ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(rows);
                }
            }
            return jobs;
        }
        private static void LoadData()
        {
            if (IsDataLoaded)
            {
                return;
            }
            List<string[]> rows = new List<string[]>();
            using(StreamReader reader = File.OpenText("job_data.csv"))
            {
                while(reader.Peek( ) >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowToArray = CSVRowToStringArray(line);
                    if(rowToArray.Length > 0)
                    {
                        rows.Add(rowToArray);
                    }


                }

                string[] headers = rows[0];
                rows.Remove(headers);

                foreach(string[] row in rows)
                {
                    Dictionary<string, string> rowDict = new Dictionary<string, string>();
                    for(int i=0; i < headers.Length; i++)
                    {
                        rowDict.Add(headers[i], row[i]);
                    }

                    AllJobs.Add(rowDict);
                }
                
            }
            IsDataLoaded = true;

        }

        private static string[] CSVRowToStringArray(string rows, char fieldSep=',', char itemSep = '\"')
        {
            bool IsQuotes = false;
            StringBuilder builder = new StringBuilder();
            List<string> row = new List<string>();
            foreach(char c in rows.ToCharArray())
            {
                if(c== fieldSep && !IsQuotes)
                {
                    row.Add(builder.ToString());
                    builder.Clear();

                }
                else
                {
                    if (c == itemSep)
                    {
                        IsQuotes = !IsQuotes;
                    }
                    else
                    {
                        builder.Append(c);
                    }
                }
            }
            row.Add(builder.ToString());
            return row.ToArray();
        }

           public static List<Dictionary<string,string>> FindByValue(string value)
           {
               LoadData();
               List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

              
               foreach(Dictionary<string, string> jobs in AllJobs)

               {
                foreach(KeyValuePair<string,string> job in jobs)
                {
                    
                    if (job.Value.ToLower().Contains(value.ToLower()))
                    {
                        results.Add(jobs);

                    }

                }
         
                      
                        

                    

                



            }



               return results;


           }
           
  
    }
}

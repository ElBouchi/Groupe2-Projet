using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet.Model
{
    class Work
    {
        private static Object _locker = new Object();

        public static string filePath = @"..\..\..\work.json";
        public string name { get; set; }
        public string repS { get; set; }
        public string repC { get; set; }
        public string type { get; set; }

        public void writeWork(string theName, string theRepS, string theRepC, string theType)
        {
            lock (_locker)
            {
                var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
                var workList = JsonConvert.DeserializeObject<List<Work>>(jsonDataWork) ?? new List<Work>(); //convert a string into an object for JSON
                
                workList.Add(new Work() //parameter that the JSON file will contains
                {
                    name = theName,
                    repS = theRepS,
                    repC = theRepC,
                    type = theType,
                });

                string ResultJsonState = JsonConvert.SerializeObject(workList, Formatting.Indented);  //convert an object into a string for JSON
                File.WriteAllText(filePath, ResultJsonState);
            }
        }
        public List<Work> readOnlyWork()
        {
            lock (_locker)
            {
                var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
                var workList = JsonConvert.DeserializeObject<List<Work>>(jsonDataWork) ?? new List<Work>(); //convert a string into an object for JSON

                return workList;
            }
        }
        public void writeOnlyWork(List<Work> workList)
        {
            lock (_locker)
            {
                string strResultJsonState = JsonConvert.SerializeObject(workList, Formatting.Indented);
                File.WriteAllText(filePath, strResultJsonState);
            }
        }
    }

}

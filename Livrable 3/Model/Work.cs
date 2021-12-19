using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Projet.Model
{
    class Work
    {
        private static Mutex mutex = new Mutex();

        public static string filePath = @"..\..\..\work.json";
        public string name { get; set; }
        public string repS { get; set; }
        public string repC { get; set; }
        public string type { get; set; }

        public void writeWork(string theName, string theRepS, string theRepC, string theType)
        {
            mutex.WaitOne();
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
            mutex.ReleaseMutex();
        }
        public List<Work> readOnlyWork()
        {
            mutex.WaitOne();
            var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonDataWork) ?? new List<Work>(); //convert a string into an object for JSON
            mutex.ReleaseMutex();
            return workList;

        }
        public void writeOnlyWork(List<Work> workList)
        {
            mutex.WaitOne();
            string strResultJsonState = JsonConvert.SerializeObject(workList, Formatting.Indented);
            File.WriteAllText(filePath, strResultJsonState);
            mutex.ReleaseMutex();
        }
    }

}

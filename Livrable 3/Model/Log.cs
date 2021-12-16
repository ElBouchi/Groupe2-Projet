using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet.Model
{
    class Log
    {
        private static Object _locker = new Object();

        public static string filePath = @"..\..\..\log.json";
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string FileSize { get; set; }
        public string FileTransferTime { get; set; }
        public string TimeToCrypt { get; set; }
        public string time { get; set; }


        public void writeLog(string theName, string theRepS, string theRepC, string theSize, string theFileTransferTime, string theTimeToCrypt, string theTime)
        {
            lock (_locker)
            {
                var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
                var logList = JsonConvert.DeserializeObject<List<Log>>(jsonDataWork) ?? new List<Log>(); //convert a string into an object for JSON

                logList.Add(new Log() //parameter that the JSON file will contains
                {
                    Name = theName,
                    FileSource = theRepS,
                    FileTarget = theRepC,
                    FileSize = theSize,
                    FileTransferTime = theFileTransferTime,
                    TimeToCrypt = theTimeToCrypt,
                    time = theTime
                });

                string ResultJsonState = JsonConvert.SerializeObject(logList, Formatting.Indented);  //convert an object into a string for JSON
                File.WriteAllText(filePath, ResultJsonState);
            }
        }
        public List<Log> readOnlyLog()
        {
            lock (_locker)
            {
                var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
                var logList = JsonConvert.DeserializeObject<List<Log>>(jsonDataWork) ?? new List<Log>(); //convert a string into an object for JSON

                return logList;
            }
        }

        public void writeOnlyLog(List<Log> logList)
        {
            lock (_locker)
            {
                string strResultJsonState = JsonConvert.SerializeObject(logList, Formatting.Indented);
                File.WriteAllText(filePath, strResultJsonState);
            }
        }

    }
}
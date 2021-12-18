using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace Projet.Model
{
    public class Log
    {
        private static Mutex mutex = new Mutex();

        public static string filePath = @"..\..\..\log.json";
        public static string filePathXML = @"..\..\..\Log.xml";
        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string FileSize { get; set; }
        public string FileTransferTime { get; set; }
        public string TimeToCrypt { get; set; }
        public string time { get; set; }

        public void writeXML(string theName, string theRepS, string theRepC, string theSize, string theFileTransferTime, string theTimeToCrypt, string theTime)
        {
            mutex.WaitOne();

            string xml = File.ReadAllText(filePathXML);
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Log";
            xRoot.IsNullable = true;

            
            XmlSerializer serializer = new XmlSerializer(typeof(List<Log>), xRoot);

            TextReader textReader = new StringReader(xml);

            List<Log> worklist = (List<Log>)serializer.Deserialize(textReader);



            worklist.Add(new Log()
            {
                Name = theName,
                FileSource = theRepS,
                FileTarget = theRepC,
                FileSize = theSize,
                FileTransferTime = theFileTransferTime,
                TimeToCrypt = theTimeToCrypt,
                time = theTime
            });




            var writer1 = new StringWriter();
            serializer.Serialize(writer1, worklist);
            var xml1 = writer1.ToString();
            File.WriteAllText(filePathXML, xml1);


            mutex.ReleaseMutex();
        }



        public void writeLog(string theName, string theRepS, string theRepC, string theSize, string theFileTransferTime, string theTimeToCrypt, string theTime)
        {
            mutex.WaitOne();
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
            mutex.ReleaseMutex();
        }
        public List<Log> readOnlyLog()
        {
            mutex.WaitOne();
            var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
            var logList = JsonConvert.DeserializeObject<List<Log>>(jsonDataWork) ?? new List<Log>(); //convert a string into an object for JSON
            mutex.ReleaseMutex();
            return logList;
        }

        public void writeOnlyLog(List<Log> logList)
        {//
            mutex.WaitOne();
            string strResultJsonState = JsonConvert.SerializeObject(logList, Formatting.Indented);
            File.WriteAllText(filePath, strResultJsonState);
            mutex.ReleaseMutex();
        }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet.Model
{
    class Etat
    {
        private static Object _locker = new Object();

        public static string filePath = @"..\..\..\state.json";
        public string Name { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string Time { get; set; }
        public string State { get; set; }
        public string TotalFilesToCopy { get; set; }
        public string TotalFilesSize { get; set; }
        public string NbFilesLeftToDo { get; set; }
        public string Progression { get; set; }

        public void writeState(string theName, string theRepS, string theRepC, string theTime, string theState, string theTotalFilesToCopy, string theTotalFilesSize, string theNbFilesLeftToDo, string theProgression)
        {
            lock (_locker)
            {
                var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
                var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataWork) ?? new List<Etat>(); //convert a string into an object for JSON

                stateList.Add(new Etat() //parameter that the JSON file will contains
                {
                    Name = theName,
                    SourceFilePath = theRepS,
                    TargetFilePath = theRepC,
                    Time = theTime, //DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                    State = theState,
                    TotalFilesToCopy = theTotalFilesToCopy,
                    TotalFilesSize = theTotalFilesSize,
                    NbFilesLeftToDo = theNbFilesLeftToDo,
                    Progression = theProgression
                });

                string ResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented);  //convert an object into a string for JSON
                File.WriteAllText(filePath, ResultJsonState);
            }
        }
        public List<Etat> readOnlyState()
        {
            lock (_locker)
            {
                var jsonDataWork = File.ReadAllText(filePath); //Read the JSON file
                var workList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataWork) ?? new List<Etat>(); //convert a string into an object for JSON

                return workList;
            }
        }

        public void writeOnlyState(List<Etat> stateList)
        {
            lock (_locker)
            {
                string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented);
                File.WriteAllText(filePath, strResultJsonState);
            }
        }
    }

}
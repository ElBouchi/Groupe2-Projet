using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet
{
    interface ISauvegarde
    {
        void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, bool createFirstFolder, long fileCount, int getIndex, string getName);
    }
}

using System;

namespace StrongMonkey.Core
{
	static public class Enums
	{
//         public enum MessageType { Info, Warning, Error };

        static public string TEMPSAVEFOLDER { get { return System.IO.Path.GetTempPath(); } }
        static string _logFile = TEMPSAVEFOLDER + @"DrupNET_Log.txt";
        static public int MAXFILESIZEKB = 2048;
        /// <summary>
        /// to set a new log file , just give a file name, the defualt temp folder is added automatically
        /// </summary>
        static public string LOGFILE
        {
            get { return _logFile; }
            set { _logFile = TEMPSAVEFOLDER + value; }
        }
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Wawagruz
{
    public class FileMethods
    {
        /// <summary>
        /// get path to main dll of this project after compiling 
        /// then remove the name of the file and exstention of the file
        /// then add pathadd 
        /// </summary>
        /// <param name="PathAdd"></param>
        /// <returns></returns>
        public static string GetPath(string PathAdd)
        {
            string AppFullname = Assembly.GetEntryAssembly().FullName;
            string Appname = AppFullname.Split(',')[0] + ".dll";
            string Path = Assembly.GetEntryAssembly().Location.Replace(Appname, "");
            return Path + /*@"\" +*/ PathAdd;
        }
    }
}

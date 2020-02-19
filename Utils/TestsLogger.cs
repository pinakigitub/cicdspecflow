using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDBDDRTODOTNETFramework.Utils
{
    public static class TestsLogger
    {
        //TODO set debug mode from AppConfig
        private static bool debug = true;

        //Simple wrapper to write out to console, until we figure out what kind of Log tool we want to use
        public static void Log(string message)
        {
            Console.WriteLine(GetTimestamp(DateTime.Now) + " - " + message);
        }

        internal static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd_HH:mm:ss");
        }


        internal static void Debug(string message)
        {
            if (debug) { Log("[DEBUG] " + message); }
        }
    }
}

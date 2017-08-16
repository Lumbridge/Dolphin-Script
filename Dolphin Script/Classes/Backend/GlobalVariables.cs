using System;
using System.Collections.Generic;

using DolphinScript.Lib.ScriptEventClasses;

namespace DolphinScript.Lib.Backend
{
    class GlobalVariables
    {
        public static string WindowClass { get; set; }
        public static string WindowTitle { get; set; }
        
        public static int                       MouseSpeed       = 15;
        public static List<List<ScriptEvent>>   AllGroups        = new List<List<ScriptEvent>>();
        public static List<ScriptEvent>         AllEvents        = new List<ScriptEvent>();
        public static bool                      IsRegistering    = false, 
                                                IsRunning        = false;

        /// <summary>
        /// shortcut function to write to the console
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            Console.WriteLine("[{0}] {1}", DateTime.Now.ToShortTimeString(), text);
        }

        /// <summary>
        /// swaps position of two elements in a collection
        /// </summary>
        /// <param name="list"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        public static void Swap(IList<ScriptEvent> list, int indexA, int indexB)
        {
            // store temp version of element object
            //
            ScriptEvent tmp = list[indexA];
            
            // move element at index b to the location of the element we stored
            //
            list[indexA] = list[indexB];

            // move the stored element to the location of index b
            //
            list[indexB] = tmp;
        }

        // a list of characters in the alphabet, used for saving and loading config files
        //
        public static List<string> Alphabet = new List<string>()
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
        };

        /// <summary>
        /// Returns a substring between two sub-parts of the string
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetSubstringByString(string a, string b, string c)
        {
            try { return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length)); }
            catch { Console.WriteLine("ERROR CREATING SUBSTRING BETWEEN STRINGS {0} AND {1}", a, b); return "ERROR CREATING SUBSTRING"; }
        }
    }
}

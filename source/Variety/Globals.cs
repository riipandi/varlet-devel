using System;
using System.Collections.Generic;
using System.Text;

namespace Variety
{
    public static class Globals
    {
        public static string Name { get; set; }
        public static int Number { get; set; }
        public static List<string> onlineMembers = new List<string>();

        static Globals()
        {
            Name = "starting name";
            Number = 5;
        }

        public static string ServiceNameHttp()
        {
            return "VarletHttpd";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Screen
    {
        public ScreenSizeType Type { get; set; }

        public string FormatForFile() => $"{(int)Type}";

        public static Screen ParseFromFile(string str)
        {
            string[] attributes = str.Split();

            return new Screen
            {
                Type = (ScreenSizeType)Enum.Parse(typeof(ScreenSizeType), attributes[0])
            };
        }
    }
}

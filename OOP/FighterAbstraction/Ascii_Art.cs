using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Fighters
{
    public class Ascii_Art
    {
        public static int maxheight=default;
        public int width=default;
        public List<string> lines;
        public string symbol;

        public Ascii_Art(string FileLoc,string symbol="")
        {
            var curFileHeight = 0;
            this.symbol = symbol;
            this.lines = new List<string>();
            using (var file =new StreamReader(Path.Join(Directory.GetCurrentDirectory().ToString(), FileLoc)))
            {
                while (file.Peek() >= 0)
                {
                    curFileHeight += 1;
                    var line = file.ReadLine();
                    width=Math.Max(width, line.Length);
                    lines.Add(line);
                }
                maxheight=Math.Max(maxheight, curFileHeight);
            }
        }
    }
}

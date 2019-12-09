using MapleLib.WzLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzStringDumper
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Please select your String.wz");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Wizet files (.wz)|*.wz";
            if (dialog.ShowDialog() == true)
            {
                WzFile file = new WzFile(dialog.FileName, WzMapleVersion.CLASSIC);
                file.ParseWzFile();
                Console.WriteLine("Version: " + file.Version);
                foreach (var wzImages in file.WzDirectory.WzImages)
                {
                    Console.WriteLine("Dumping " + wzImages.Name);
                    foreach (var property in wzImages.WzProperties)
                    {
                        using (StreamWriter sw = new StreamWriter(wzImages.Name+".txt", true))
                        {
                            if (property.WzProperties != null)
                            {
                                foreach (var items in property.WzProperties)
                                {
                                    if (items.Name == "name")
                                    {
                                        sw.WriteLine(items.Parent.Name + " : " + items.WzValue);
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("All files dumped successfully");
            }
            Console.WriteLine("Please press enter to continue");
            Console.ReadKey();
        }
    }
}

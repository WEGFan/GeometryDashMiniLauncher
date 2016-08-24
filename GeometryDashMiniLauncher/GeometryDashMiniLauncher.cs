using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashMiniLauncher
{
    class GeometryDashMiniLauncher
    {
        static void Main(string[] args)
        {
            Console.Title = "Geometry Dash Mini Launcher";

            string input1, input2, dir = AppDomain.CurrentDomain.BaseDirectory;
            string[] TexturePacks;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Welcome to use Geometry Dash Mini Launcher (by WEGFan)！\n");
            if (File.Exists("GeometryDash.exe") && Directory.Exists("Resources"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Install texture packs\n2. Disable current texture pack\n");

                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter: ");
                    input1 = Console.ReadLine();
                }
                while (!IsNumber(input1) || Convert.ToInt32(input1) != 1 && Convert.ToInt32(input1) != 2);


                if (Convert.ToInt32(input1) == 1)
                {
                    do
                    {
                        TexturePacks = Directory.GetDirectories(dir);
                        string[] TexturePacksTemp = new string[TexturePacks.Length];

                        for (int i = 0; i <= TexturePacks.Length - 1; i++)
                        {
                            TexturePacksTemp[i] = TexturePacks[i].Split('\\').Last();
                        }

                        List<string> TexturePacksList = new List<string>(TexturePacksTemp);
                        TexturePacksList.RemoveAt(TexturePacksList.FindIndex(str => str == "Resources"));
                        TexturePacks = TexturePacksList.ToArray<string>();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease disable current texture pack before you install other texture packs (unless you want to merge them).\n\nPlease put the texture pack folder in the game folder, and make sure there aren't any sub-folders in it.\n");

                        if (TexturePacks.Length != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Texture packs available:");

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            for (int i = 1; i <= TexturePacks.Length; i++)
                            {
                                Console.WriteLine(i + ". " + TexturePacks[i - 1]);
                            }

                            do
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("\nPlease enter: ");
                                input2 = Console.ReadLine();
                            }
                            while (!IsNumber(input2) || !(Convert.ToInt32(input2) >= 1 && Convert.ToInt32(input2) <= TexturePacks.Length));
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Can't find texture packs! Press any key to exit...");
                            Console.ReadKey();
                            return;
                        }
                    }
                    while (!Directory.Exists(TexturePacks[Convert.ToInt32(input2) - 1]));

                    var files = Directory.GetFiles(TexturePacks[Convert.ToInt32(input2) - 1], "*.*")
                        .Where(s => s.EndsWith(".fnt") || s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".plist") || s.EndsWith(".png"));
                    foreach (string file in files)
                    {
                        File.Copy(file, Path.GetFileName(file), true);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nDone! Press any key to exit...");
                    Console.ReadKey();
                }

                if (Convert.ToInt32(input1) == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPress any key to disable current texture pack...");
                    Console.ReadKey();

                    var files = Directory.GetFiles(dir, "*.*")
                        .Where(s => s.EndsWith(".fnt") || s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".plist") || s.EndsWith(".png"));
                    foreach (string file in files)
                    {
                        File.Delete(Path.GetFileName(file));
                    }

                    Console.WriteLine("\nDone! Press any key to exit...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can't find GeometryDash.exe and Resources folder! Please put this program in the game folder. Press any key to exit...");
                Console.ReadKey();
            }
        }

        static bool IsNumber(string input)
        {
            try
            {
                Convert.ToInt32(input);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

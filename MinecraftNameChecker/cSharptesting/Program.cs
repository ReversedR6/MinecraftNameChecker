using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftNameChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Info("Minecraft Name Checker by Reversed");

            Log.Critical("1) Check Specific Names");
            Log.Critical("2) Check Names From A Text File");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Log.Critical("What name would you like to check?");
                string nameTochek = Console.ReadLine();
                if (IsAName(nameTochek) == true)
                {
                    Log.Critical(nameTochek + " is available.");
                }

                if (IsAName(nameTochek) == false)
                {
                    Log.Error(nameTochek + " is taken.");
                }
                Log.Critical("Done Checking Name.");
                Console.ReadKey();
            }

            if (choice == "2")
            {

                if (!File.Exists("names.txt"))
                {
                    Log.Error("'names.txt' was not found. Please make a text file with the names that you want to check and name it 'names.txt'.");
                }

                string[] NamesToCheck;

                try
                {
                    NamesToCheck = File.ReadAllLines("names.txt");
                }
                catch
                {
                    Log.Fatal("Could not open the file. Press enter to close the application.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Checking Names in names.txt");
                foreach (var names in NamesToCheck)
                {
                    if (IsAName(names) == true)
                    {
                        Log.Critical(names + " is available.");
                        File.AppendAllLines("AvailableNames.txt", new string[] { names });
                    }

                    if (IsAName(names) == false)
                    {
                        Log.Error(names + " is taken.");
                    }
                    Thread.Sleep(20);
                }
                Log.Critical("Done Checking Names. You Can Check The Names In 'AvailableNames.txt' .");
                Console.ReadKey();
            }

            else
                Log.Fatal("You are a dumbass who can not read.");
        }

        static bool IsAName(string name)
        {

            bool isTaken = false;
            string url = "https://mcnames.net/username/";


            var client = new WebClient();
            string newurl = url + name + "/";
            string content = client.DownloadString(newurl);

            if (content.Contains("Username is not available"))
                isTaken = false;
            else if (content.Contains("Username is available"))
                isTaken = true;



            return isTaken;
        }
    }
}

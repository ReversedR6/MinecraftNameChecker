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

            switch (choice)
            {
                case "1":
                    Log.Critical("What name would you like to check?");
                    string nameToCheck = Console.ReadLine();
                    if (isTaken(nameToCheck) == false)
                    {
                        Log.Valid(nameToCheck + " is available.");
                    }
                    if (isTaken(nameToCheck) == true)
                    {
                        Log.Error(nameToCheck + " is taken.");
                    }
                    Log.Critical("Done Checking Name.");
                    Console.ReadKey();
                    break;
                case "2":
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
                        if (isTaken(names) == false)
                        {
                            Log.Valid(names + " is available.");
                            File.AppendAllLines("AvailableNames.txt", new string[] { names });
                        }
                        if (isTaken(names) == true)
                        {
                            Log.Error(names + " is taken.");
                        }
                        Thread.Sleep(20);
                    }
                    Log.Critical("Done Checking Names. You Can Check The Names In 'AvailableNames.txt' .");
                    Console.ReadKey();
                    break;
                default:
                    Log.Fatal("You made a mistake. Press enter to close the application.");
                    break;
            }
        }


        static bool isTaken(string name)
        {

            bool isTaken = false;
            string url = "https://mcnames.net/username/";


            var client = new WebClient();
            string newurl = url + name + "/";
            string content = client.DownloadString(newurl);

            if (content.Contains("Status: Not available"))
                isTaken = true;
            else if (content.Contains("Status: Available"))
               isTaken = false;

            return isTaken;
        }
    }
}

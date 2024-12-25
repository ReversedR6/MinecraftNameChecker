using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Log
{
    public static void Info(object value)
    {
        Log.Color(ConsoleColor.Yellow);
        Console.WriteLine("[!] " + value.ToString());
        Log.Color();
    }

    public static void Critical(object value)
    {
        Log.Color(ConsoleColor.Cyan);
        Console.WriteLine("[+] " + value.ToString());
    }

    public static void Error(object value)
    {
        Log.Color(ConsoleColor.Red);
        Console.WriteLine("[-] " + value.ToString());
        Log.Color();
    }

    public static void Fatal(object value)
    {
        Error(value);
        Console.ReadLine();
        Environment.Exit(-1);
    }

    public static void Valid(object value)
    {
        Log.Color(ConsoleColor.Green);
        Console.WriteLine("[+] " + value.ToString());
        Log.Color();
    }

    private static void Color(ConsoleColor color = ConsoleColor.White) => Console.ForegroundColor = color;
}

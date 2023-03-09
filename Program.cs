using System;
using System.IO;

namespace BodyCompositionLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if log.txt exist
            string fileName = "log.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Log file created on " + DateTime.Now.ToString());
                }
                Console.WriteLine("Log file created.");
            }
            else
            {
                Console.WriteLine("Log file already exists.");
            }
            // Check if data is already logged for today
            DateTime now = DateTime.Now;
            string path = "log.txt";
            if (IsDateLogged(now, path))
            {
                Console.WriteLine($"Data already logged for {now.ToShortDateString()}. Press any key to exit.");
                Console.ReadKey();

                // Open log file
                System.Diagnostics.Process.Start("notepad.exe", path);
            }
            else
            {
                // Ask for inputs
                Console.WriteLine("Enter weight in kg:");
                double weight = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter waist size in cm:");
                double waistSize = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter neck size in cm:");
                double neckSize = double.Parse(Console.ReadLine());

                // Set height to 185 cm
                double height = 185;

                // Calculate body fat composition using US Navy method
                double bodyFatComposition = 495 / (1.0324 - 0.19077 * Math.Log10(waistSize - neckSize) + 0.15456 * Math.Log10(height)) - 450;

                // Log data
                string log = $"[{now.ToShortDateString()}] weight: {weight} kg, waist size: {waistSize} cm, neck size: {neckSize} cm, body fat composition: {bodyFatComposition}%";

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(log);
                }

                Console.WriteLine("Data logged. Press any key to exit.");
                Console.ReadKey();

                // Open log file
                System.Diagnostics.Process.Start("notepad.exe", path);
            }
        }
        static bool IsDateLogged(DateTime date, string path)
        {
            bool isLogged = false;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(date.ToShortDateString()))
                    {
                        isLogged = true;
                        break;
                    }
                }
            }
            return isLogged;
        }
    }
}

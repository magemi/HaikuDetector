using System;
using System.IO;

namespace HaikuDetectorCS
{
    class Program
    {

        static void Main(string[] args)
        {
            HaikuDetector haikuDetector = new HaikuDetector();

            string[] haikuValidationMessages = haikuDetector.ReadAllFromDirectory(args.Length > 0 ? args[0] : null);

            foreach(string message in haikuValidationMessages)
                Console.WriteLine(message);
        }
    }
}

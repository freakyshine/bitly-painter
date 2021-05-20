using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitlyPainter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("### Welcome to Bitly Painter ###");
            AudibleExit();
        }

        /// <summary>
        /// This method closes the application with a little message, which asks the user to enter any key. Until a key is pressed, the console will stay open.
        /// </summary>
        static void AudibleExit ()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

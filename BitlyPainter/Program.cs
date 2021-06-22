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
            // The main canvas. Also the only canvas, there is no other canvas.
            Canvas BitlyCanvas = new Canvas();

            ConsoleKey input = ConsoleKey.D0;
            while (!(input == ConsoleKey.P))
            {
                Console.Clear();
                Console.WriteLine("Choose what to do");
                Console.WriteLine("<P> Painting\n" +
                                  "<S> Settings\n" +
                                  "<O> Output");
                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.O: // Output
                        Console.WriteLine("You've chosen the output function");
                        break;
                    case ConsoleKey.P: // Paint
                        Console.WriteLine("You've chosen the paint function");
                        break;
                    case ConsoleKey.S: // Settings
                        Console.WriteLine("You've chosen the settings function");
                        // ----- Foreground color -----
                        Console.WriteLine("Please enter your prefered color in this format: RRRGGGBBB, for example black 000000000 or white 255255255");
                        string rawInput = Console.ReadLine();
                        _ = rawInput.Length <= 6 ? rawInput = "---------" : null;
                        byte R, G, B, x, y;
                        Byte.TryParse(rawInput.Substring(0, 3), out R);
                        Byte.TryParse(rawInput.Substring(3, 3), out G);
                        Byte.TryParse(rawInput.Substring(6, 3), out B);
                        BitlyCanvas.Color[0] = R; BitlyCanvas.Color[1] = G; BitlyCanvas.Color[2] = B;
                        // ----- Background color -----
                        Console.WriteLine("Please enter your prefered background color in this format: RRRGGGBBB, for example black 000000000 or white 255255255");
                        rawInput = Console.ReadLine();
                        _ = rawInput.Length <= 6 ? rawInput = "---------" : null;
                        Byte.TryParse(rawInput.Substring(0, 3), out R);
                        Byte.TryParse(rawInput.Substring(3, 3), out G);
                        Byte.TryParse(rawInput.Substring(6, 3), out B);
                        BitlyCanvas.BackgroundColor[0] = R; BitlyCanvas.BackgroundColor[1] = G; BitlyCanvas.BackgroundColor[2] = B;

                        // ------ Canvas size -------
                        Console.WriteLine("Please enter your prefered canvas x value. Valid is a byte.");
                        rawInput = Console.ReadLine();
                        _ = (rawInput.Length <= 0 || rawInput.Length >= 8) ? rawInput = "0" : null;
                        Byte.TryParse(rawInput, out x);
                        BitlyCanvas.Xsize = x;

                        Console.WriteLine("Please enter your prefered canvas y value. Valid is a byte.");
                        rawInput = Console.ReadLine();
                        _ = (rawInput.Length <= 0 || rawInput.Length >= 8) ? rawInput = "0" : null;
                        Byte.TryParse(rawInput, out y);
                        BitlyCanvas.Ysize = y;

                        // ------- Confirm user -------
                        Console.WriteLine("Your settings are set to following parameters:");
                        Console.WriteLine($"You picked the color R:{BitlyCanvas.Color[0]} G:{BitlyCanvas.Color[1]} B:{BitlyCanvas.Color[2]}");
                        //Console.WriteLine($"You picked the background color R:{BitlyCanvas.BackgroundColor[0]} G:{BitlyCanvas.BackgroundColor[1]} B:{BitlyCanvas.BackgroundColor[2]}");
                        Console.WriteLine($"You picked the canvas size: {BitlyCanvas.Xsize}px by {BitlyCanvas.Ysize}px");


                        // ------ Finished setup -------
                        Console.WriteLine("Setup succeeded! Press any key to continue.");
                        Console.ReadKey(true);

                        break;
                    default:
                        break;
                }
            }

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

    /// <summary>
    /// Represents a canvas object, it has typicall properties of a canvas, such as dimensions and color
    /// </summary>
    class Canvas
    {
        /// <summary>
        /// Stores RGB values in a Byte array. Values from 0-255 are valid. Array stores 3 values.
        /// </summary>
        public byte[] Color { get; set; }
        public byte[] BackgroundColor { get; set; }
        /// <summary>
        /// Stores the dimensions of the canvas. Values from 0-255 are valid. Array stores 2 values.
        /// </summary>
        private byte[] Dimensions { get; set; } = new byte[2];
        /// <summary>
        /// Xsize changes the values of the private field Dimensions. Valid values are 0-255. Set to a higher or lower value, the actual value will be 0.
        /// </summary>
        public int Xsize { get { return Dimensions[0]; } set { _ = value < 0 || value > 255 ? Dimensions[0] = 0 : Dimensions[0] = (byte)value; } }
        /// <summary>
        /// Ysize  changes the values of the private field Dimensions. Valid values are 0-255. Set to a higher or lower value, the actual value will be 0.
        /// </summary>
        public int Ysize { get { return Dimensions[1]; } set { _ = value < 0 || value > 255 ? Dimensions[1] = 0 : Dimensions[1] = (byte)value; } }

        /// <summary>
        /// Constructs a Canvas with given X and Y size.
        /// </summary>
        /// <param name="x">Horizontal size</param>
        /// <param name="y">Vertical size. Valid numbers</param>
        public Canvas()
        {
            // Defining a new byte array and setting it's maximum to 3 values. Setting default color to black
            Color = new byte[3] { 0, 0, 0};
            // Defining a new byte array and setting it's maximum to 3 values. Setting default backgroundcolor to white
            BackgroundColor = new byte[3] { 255, 255, 255 };
        }
    }
}

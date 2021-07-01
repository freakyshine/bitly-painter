using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitlyPainter
{
    class Program
    {
        static Canvas BitlyCanvas;
        static void Main(string[] args)
        {
            BitlyCanvas = new Canvas();

            // The main canvas. Also the only canvas, there is no other canvas.
            while (true)
            {
                switch (UserMenu())
                {
                    case 0:
                        Console.WriteLine("You've chosen the paint function");
                        BitlyCanvas.Paint();
                        break;
                    case 1:
                        Console.WriteLine("You've chosen the settings function");
                        BitlyCanvas.SetPreferences();
                        break;
                    case 2:
                        Console.WriteLine("You've chosen the output function");
                        BitlyCanvas.View();
                        break;
                    case 3:
                        // Black foreground color, white background color, canvas size is 6 by 6 pixels and the paint is a smiley
                        BitlyCanvas.SetPreferences(new byte[] { 255, 255, 255 }, new byte[] { 0, 0, 0 }, new int[] { 6, 6 }, "000000010010000000010010001100000000");
                        Console.WriteLine("Developer options have been applied.");
                        break;
                    case 4:
                        Console.WriteLine("You've chosen violence (Peace was never an option)");
                        AudibleExit(0);
                        break;
                    default:
                        break;
                }
                Console.Write("<Enter> OK");
                Console.ReadKey();
            }            
        }

        /// <summary>
        /// The User Menu is used to display the menu and also contains the logic for the menu since it is not that complicated
        /// <para>
        /// Displays the menu and lets the user choose by using the arrow keys. When pressing enter, the method returns where the menu cursor / pointer is currently set.
        /// </para>
        /// </summary>
        /// <returns>
        /// The position where the menu pointer is at the event of key input, enter
        /// </returns>
        static byte UserMenu()
        {
            ConsoleKey userInput;
            byte menuPointer = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("** * * * * * * * * * * * * * * * * *  **\n" +
                                  "**     What would you like to do?     **\n" +
                                  "** * * * * * * * * * * * * * * * * *  **");
                Console.Write($"[{(menuPointer == 0 ? '*' : ' ')}] Painting\n" +
                              $"[{(menuPointer == 1 ? '*' : ' ')}] Settings\n" +
                              $"[{(menuPointer == 2 ? '*' : ' ')}] View Painting\n" +
                              $"[{(menuPointer == 3 ? '*' : ' ')}] Developer Options\n" +
                              $"[{(menuPointer == 4 ? '*' : ' ')}] Exit");
                userInput = Console.ReadKey().Key;
                switch (userInput)
                {
                    case ConsoleKey.UpArrow:
                        menuPointer--;
                        break;
                    case ConsoleKey.DownArrow:
                        menuPointer++;
                        break;
                    default:
                        break;
                }
                byte menuOptions = 5;
                // Following line does: menuOptions-1 is the last option, due to index starting at 0. If the menu pointer overflows, just set to last option of menu
                menuPointer = (byte)(menuPointer > menuOptions ? menuOptions-1 : menuPointer);
                menuPointer %= menuOptions;
            } while (userInput != ConsoleKey.Enter);
            Console.Clear();
            return menuPointer;
        }
        static void Menu ()
        {
            ConsoleKey input = ConsoleKey.D0;
            while (!(input == ConsoleKey.E))
            {
                Console.Clear();
                Console.WriteLine("### Welcome to Bitly Painter ###");
                Console.WriteLine("Choose what to do");
                Console.WriteLine("<P> Painting\n" +
                                  "<S> Settings\n" +
                                  "<O> View painting\n" +
                                  "<.> Developer Option\n" +
                                  "<E> Exit");
                input = Console.ReadKey(true).Key;
                
            }
        }

        /// <summary>
        /// This method closes the application with a little message, which asks the user to enter any key. Until a key is pressed, the console will stay open.
        /// </summary>
        static void AudibleExit (int exitCode)
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(exitCode);
        }
    }

    /// <summary>
    /// Represents a canvas object, it has typicall properties of a canvas, such as dimensions and color
    /// </summary>
    class Canvas
    {
        /// <summary>
        /// A long representing the picture in 0s and 1s.
        /// </summary>
        public string CanvasPaint { get; set; }
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
        /// The paint method is a method essentially just storing a stirng of 1s and 0s into the object. It will then be used in the output option to be showed on screen
        /// </summary>
        public void Paint ()
        {
            CanvasPaint = "";
            if (Xsize < 256 && Xsize > 0)
            {
                do
                {
                    Console.WriteLine("Enter Bit String:");
                    string bitString = Console.ReadLine();
                    if (UInt64.TryParse(bitString, out ulong n) && (Xsize * Ysize == bitString.Length))  // check if input is numeric
                    {
                        CanvasPaint = bitString;
                    }
                    else
                    {
                        if (Xsize * Ysize == bitString.Length)
                        {
                            Console.WriteLine("Please enter valid numeric value");
                        }
                        else
                        {
                            Console.WriteLine("You need to specify exactly one bit for each pixel in the image.");
                            Console.WriteLine($"Your grid has {Xsize*Ysize} px but you specify the value for {bitString.Length} bits");
                        }

                        CanvasPaint = "";
                    }
                } while (CanvasPaint == "");
            } else
            {
                Console.WriteLine("Please define canvas size in settings first. ");
            }
            
        }
        /// <summary>
        /// Set up settings for the canvas
        /// </summary>
        public void SetPreferences ()
        {
            // ----- Foreground color -----
            Console.WriteLine("Please enter your prefered color in this format: RRRGGGBBB, for example black 000000000 or white 255255255");
            string rawInput = Console.ReadLine();
            _ = rawInput.Length <= 6 ? rawInput = "---------" : null;
            byte parsedR, parsedG, parsedB, parsedX = 0, parsedY = 0;
            Byte.TryParse(rawInput.Substring(0, 3), out parsedR);
            Byte.TryParse(rawInput.Substring(3, 3), out parsedG);
            Byte.TryParse(rawInput.Substring(6, 3), out parsedB);
            Color[0] = parsedR; Color[1] = parsedG; Color[2] = parsedB;
            // ----- Background color -----
            Console.WriteLine("Please enter your prefered background color in this format: RRRGGGBBB, for example black 000000000 or white 255255255");
            rawInput = Console.ReadLine();
            _ = rawInput.Length <= 6 ? rawInput = "---------" : null;
            Byte.TryParse(rawInput.Substring(0, 3), out parsedR);
            Byte.TryParse(rawInput.Substring(3, 3), out parsedG);
            Byte.TryParse(rawInput.Substring(6, 3), out parsedB);
            BackgroundColor[0] = parsedR; BackgroundColor[1] = parsedG; BackgroundColor[2] = parsedB;

            // ------ Canvas size -------
            // This not just checks if the product of X and Y is in the range of 1 - 254 but also if x and y independend are in the range as a 1 < product < 255 can only occur if the numbers are in the range
            while (!(Enumerable.Range(1, 254).Contains(parsedX * parsedY)))
            {
                // 254 is the limit of the Console.ReadLine() method
                Console.WriteLine("Please enter your prefered canvas x value.\nValid are values that give a product minimum 1 or a product of maximum 254 (0 < x * y < 255).");
                rawInput = Console.ReadLine();
                _ = (rawInput.Length <= 0 || rawInput.Length >= 8) ? rawInput = "0" : null;
                Byte.TryParse(rawInput, out parsedX);

                Console.WriteLine("Please enter your prefered canvas y value.\nValid are values that give a product minimum 1 or a product of maximum 254 (0 < x * y < 255).");
                rawInput = Console.ReadLine();
                _ = (rawInput.Length <= 0 || rawInput.Length >= 8) ? rawInput = "0" : null;
                Byte.TryParse(rawInput, out parsedY);
            }
            // After the validation period ended, the values can eventually be stored
            Xsize = parsedX;
            Ysize = parsedY;

            // ------- Confirm user -------
            Console.WriteLine("Your settings are set to following parameters:");
            Console.WriteLine($"You picked the color R:{Color[0]} G:{Color[1]} B:{Color[2]}");
            //Console.WriteLine($"You picked the background color R:{BitlyCanvas.BackgroundColor[0]} G:{BitlyCanvas.BackgroundColor[1]} B:{BitlyCanvas.BackgroundColor[2]}");
            Console.WriteLine($"You picked the canvas size: {Xsize}px by {Ysize}px");


            // ------ Finished setup -------
            Console.WriteLine("Setup succeeded! Press any key to continue.");

        }
        public void SetPreferences(byte[] foregroundColor, byte[] backgroundColor, int[] canvasDimensions, string paint)
        {
            // Set foreground color
            Color[0] = foregroundColor[0];
            Color[1] = foregroundColor[1];
            Color[2] = foregroundColor[2];

            // Set background color
            BackgroundColor[0] = backgroundColor[0];
            BackgroundColor[1] = backgroundColor[1];
            BackgroundColor[2] = backgroundColor[2];

            // Set paint
            CanvasPaint = paint;

            // Set Canvas Settings
            Xsize = canvasDimensions[0];
            Ysize = canvasDimensions[1];
        } 

        /// <summary>
        /// This method outputs the canvas with colors
        /// </summary>
        public void View () // Todo: Make a more precice error detection to give the user more information about what happened wrong
        {
            // Check if all necessary values are set
            //Check if color is set and is valid
            Console.WriteLine("Checking parameters...");
            if ((Color[0] < 255 && Color[0] > 0) || (Color[1] < 255 && Color[1] > 0) || (Color[2] < 255 && Color[2] > 0))
            {
                Console.WriteLine("Parameters are set and valid\n\n");

                for (int i = 0; i < Ysize; i++)
                {
                    for (int j = 0; j < Xsize; j++)
                    {
                        if (CanvasPaint.Substring((i*Xsize)+j, 1) == "0")
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("0");
                            Console.Write("0");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("0");
                            Console.Write("0");
                            Console.ResetColor();
                        }
                    }
                    Console.Write("\n");
                }
            }
            else
            {
                Console.WriteLine("Parameters are either not valid or not set. Please check the settings option in the menu and input a paint in the paint option.");
            }
        }

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

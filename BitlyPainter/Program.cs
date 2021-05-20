﻿using System;
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
            while ((!(input == ConsoleKey.P || input == ConsoleKey.S || input == ConsoleKey.O)))
            {
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

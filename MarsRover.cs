using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MarsRover
{
    class Program
    {
        // Default folder  
        static readonly string rootFolder = @"C:\Code\MarsRover\";
        //Default file  
        static readonly string textFile = @"C:\Code\MarsRover\MarsRoverCoordinates.txt";

        /// Class RoverMovement will take in two parameters
        ///	currentCoordinates = current location array contains x-axis, y-axis and direction
        /// roverCommand = command
        public static string[] RoverMovement(string[] currentCoordinates, string roverCommand)
        {
            string[] newLocation = new string[3];                   // new location after roverCommand has executed
            int xAxis = Convert.ToInt32(currentCoordinates[0]);     // new x-axis after roverCommand has executed
            int yAxis = Convert.ToInt32(currentCoordinates[1]);     // new y-axis after roverCommand has executed
            string direction = currentCoordinates[2];               // new direction after roverCommand has executed

            switch (roverCommand)
            {
                case "M":
                    switch (direction)
                    {
                        case "N":
                            yAxis++;
                            break;
                        case "S":
                            yAxis--;
                            break;
                        case "W":
                            xAxis--;
                            break;
                        default:
                            xAxis++;
                            break;
                    }
                    break;
                case "L":
                    switch (direction)
                    {
                        case "N":
                            direction = "W";
                            break;
                        case "S":
                            direction = "E";
                            break;
                        case "W":
                            direction = "S";
                            break;
                        default:
                            direction = "N";
                            break;
                    }
                    break;
                default:
                    switch (direction)
                    {
                        case "N":
                            direction = "E";
                            break;
                        case "S":
                            direction = "W";
                            break;
                        case "W":
                            direction = "N";
                            break;
                        default:
                            direction = "S";
                            break;
                    }
                    break;

            }

            newLocation[0] = Convert.ToString(xAxis);
            newLocation[1] = Convert.ToString(yAxis);
            newLocation[2] = direction;

            return newLocation;
        }
        static void Main(string[] args)
        {

            string patternPosition = @"^[0-9]* [0-9]* [A-Za-z]";	// used to check if input is position of rover
            string patternControl = @"^[A-Za-z]";               	// used to check if input is control of rover
            string splitPattern = @"( )";                       	// used to plit the input text of position
            string[] currentLocation = new string[3];				// current location of rover

            if (File.Exists(textFile))
            {
                // Read a text file line by line.  
                string[] lines = File.ReadAllLines(textFile);

                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, patternPosition);               // match pattern of position and location
                    if (match.Success)                                              // if line is valid then get coordinates
                    {
                        string[] substrings = Regex.Split(line, splitPattern);      // Split control input line on spaces
                        currentLocation[0] = substrings[0];                         // x-axis value
                        currentLocation[1] = substrings[2];                         // y-axis value
                        currentLocation[2] = substrings[4];                         // direction value
                    }
                    else
                    {
                        match = Regex.Match(line, patternControl);                  // match pattern for contraols
                        if (match.Success)                                          // if line valid then execute control command
                        {
                            string[] substrings = Regex.Split(line, "[a-z]");
                            foreach (char c in line)
                            {
                                currentLocation = RoverMovement(currentLocation, c.ToString());
                            }
                            Console.WriteLine(currentLocation[0] + " " + currentLocation[1] + " " + currentLocation[2]);
                        }
                    }
                }
            }
        }
    }
}
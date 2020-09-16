using System;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Room definitions

            Room starterRoom = new Room(
                Room.AsciiArt.Title, "You find yourself infront of two buttons. A BLUE and a RED button.",
                "Press the RED button? (r)      Press the BLUE button? (b)",
                "red[]r[]blue[]b"
            );


            Room blueButton1 = new Room(
                Room.AsciiArt.None, "You find yourself infront of two buttons. A BLUE and a RED button. There is a panel that say 'Yes?'.",
                "Press the RED button? (r)      Press the BLUE button? (b)",
                "red[]r[]blue[]b"
            );

            Room redButton1 = new Room(
                Room.AsciiArt.None, "You find yourself infront of two buttons. A BLUE and a RED button. There is a panel that say 'No.'.",
                "Press the RED button? (r)      Press the BLUE button? (b)",
                "red[]r[]blue[]b"
            );

            Room theOnlyButtonThatMatter = new Room(
                Room.AsciiArt.NothingMattered, "Thanks for playing, none of this mattered, I'am lazy so i did not make anything more complicated then this.",
                "Ok (k)      Ok (k)     Play again? (p)",
                "ok[]k[]ok[]k[]play again[]p"
            );

            #endregion

            #region Paths structures
            starterRoom.toRed = redButton1;
            starterRoom.toBlue = blueButton1;

            redButton1.toRed = theOnlyButtonThatMatter;
            redButton1.toBlue = blueButton1;

            blueButton1.toRed = redButton1;
            blueButton1.toBlue = starterRoom;

            theOnlyButtonThatMatter.toGreen = starterRoom;
            #endregion


            Console.Title = "Buttons The Game!";
            
            //will "start" the game.
            starterRoom.WriteRoom();
        }
    }

    public class Room
    {
        #region Variables
        string text;
        string prompts;
        string[] expectedInput;

        public Room toRed;
        public Room toBlue;
        public Room toGreen;

        public enum AsciiArt
        {
            None,
            Title,
            NothingMattered
        }

        AsciiArt asciiArt;
        #endregion

        //Class constructor
        public Room(AsciiArt _asciiArt, string _text, string _prompts, string _expectedInput)
        {
            //Assign the variables from the constructor to the class.
            asciiArt = _asciiArt;
            text = _text;
            prompts = _prompts;

            //This will take in the string with the supplied "input" using [] as a marker for a new input, this allows me to have an undefined number of inputs.
            //Then it assigns the number of strings to the expectedInput variable
            string[] temp_input = _expectedInput.Split("[]");
            expectedInput = new string[temp_input.Length];

            //This will loop through the number of inputs and assigning them to the corrosponding expectedInput.
            for (int i = 0; i < temp_input.Length; i++)
            {
                expectedInput[i] = temp_input[i];
            }
        }

        #region Methods

        //This method is the main "write" method for the room, taking care of all the neccissary screen stuff
        public void WriteRoom()
        {
            Console.Clear();

            //This will check if the room has a special ascii art! Furthermore, it will also print it!
            switch (asciiArt)
            {
                case AsciiArt.Title:
                    Ascii_Title();
                    break;
                case AsciiArt.NothingMattered:
                    Ascii_NothingMattered();
                    break;
                default:
                    break;
            }

            Console.WriteLine(
                "\n\n" + text + "\n\n" + prompts
            );

            InputHandler();
        }

        public void InputHandler()
        {
            try
            {
                while (true)
                {
                    //This creates a user input variable and converts it to all lowecase, allowing capitilizations by the user to be ignored.
                    string userInput = Console.ReadLine().ToLower();


                    //This will check the length and determain what type of input ye want.
                    //It is also reversed, meaning that if the case is 6 it will still check the previus inputs.
                    switch (expectedInput.Length)
                    {
                        case 4:
                        
                            if (expectedInput[0] == userInput || expectedInput[1] == userInput)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                toRed.WriteRoom();
                            }
                            else if (expectedInput[2] == userInput || expectedInput[3] == userInput)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                toBlue.WriteRoom();
                            }
                            break;
                        case 6:
                            if (expectedInput[4] == userInput || expectedInput[5] == userInput)
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                toGreen.WriteRoom();
                            }


                            goto case 4;
                        default:
                            Console.WriteLine("The dev did an oppsie and did not provide an accepted ammount of answers!");
                            break;
                    }
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("You have reached the end of the game! Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(1); //if i dont do this, the user needs to press ctrl+c the same ammount of times they entered a "room"
            }
        }

        #endregion

        #region AsciiArt

        //As the "@" symbol requieres that the ascii art is at the left of the file, i decided to put all of them here!
        //This has the added benifit of putting all of the art at a dedicated part of the file, instead in the middle of a method!
        public void Ascii_Title()
        {
            Console.WriteLine(@"
 /$$$$$$$              /$$     /$$                                         /$$$$$$$$ /$$                        /$$$$$$   /$$$$$$  /$$      /$$ /$$$$$$$$ /$$
| $$__  $$            | $$    | $$                                        |__  $$__/| $$                       /$$__  $$ /$$__  $$| $$$    /$$$| $$_____/| $$
| $$  \ $$ /$$   /$$ /$$$$$$ /$$$$$$    /$$$$$$  /$$$$$$$   /$$$$$$$         | $$   | $$$$$$$   /$$$$$$       | $$  \__/| $$  \ $$| $$$$  /$$$$| $$      | $$
| $$$$$$$ | $$  | $$|_  $$_/|_  $$_/   /$$__  $$| $$__  $$ /$$_____/         | $$   | $$__  $$ /$$__  $$      | $$ /$$$$| $$$$$$$$| $$ $$/$$ $$| $$$$$   | $$
| $$__  $$| $$  | $$  | $$    | $$    | $$  \ $$| $$  \ $$|  $$$$$$          | $$   | $$  \ $$| $$$$$$$$      | $$|_  $$| $$__  $$| $$  $$$| $$| $$__/   |__/
| $$  \ $$| $$  | $$  | $$ /$$| $$ /$$| $$  | $$| $$  | $$ \____  $$         | $$   | $$  | $$| $$_____/      | $$  \ $$| $$  | $$| $$\  $ | $$| $$          
| $$$$$$$/|  $$$$$$/  |  $$$$/|  $$$$/|  $$$$$$/| $$  | $$ /$$$$$$$/         | $$   | $$  | $$|  $$$$$$$      |  $$$$$$/| $$  | $$| $$ \/  | $$| $$$$$$$$ /$$
|_______/  \______/    \___/   \___/   \______/ |__/  |__/|_______/          |__/   |__/  |__/ \_______/       \______/ |__/  |__/|__/     |__/|________/|__/
                                                                                                                                                             
                                                                                                                                                             

==============================================================================================================================================================");
        }

        public void Ascii_NothingMattered(){
            Console.WriteLine(@"
 ▄▄    ▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄ ▄▄   ▄▄ ▄▄▄ ▄▄    ▄ ▄▄▄▄▄▄▄    ▄▄   ▄▄ ▄▄▄▄▄▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄ ▄▄▄▄▄▄   ▄▄▄▄▄▄▄ ▄▄▄▄▄▄  ▄▄ 
█  █  █ █       █       █  █ █  █   █  █  █ █       █  █  █▄█  █      █       █       █       █   ▄  █ █       █      ██  █
█   █▄█ █   ▄   █▄     ▄█  █▄█  █   █   █▄█ █   ▄▄▄▄█  █       █  ▄   █▄     ▄█▄     ▄█    ▄▄▄█  █ █ █ █    ▄▄▄█  ▄    █  █
█       █  █ █  █ █   █ █       █   █       █  █  ▄▄   █       █ █▄█  █ █   █   █   █ █   █▄▄▄█   █▄▄█▄█   █▄▄▄█ █ █   █  █
█  ▄    █  █▄█  █ █   █ █   ▄   █   █  ▄    █  █ █  █  █       █      █ █   █   █   █ █    ▄▄▄█    ▄▄  █    ▄▄▄█ █▄█   █▄▄█
█ █ █   █       █ █   █ █  █ █  █   █ █ █   █  █▄▄█ █  █ ██▄██ █  ▄   █ █   █   █   █ █   █▄▄▄█   █  █ █   █▄▄▄█       █▄▄ 
█▄█  █▄▄█▄▄▄▄▄▄▄█ █▄▄▄█ █▄▄█ █▄▄█▄▄▄█▄█  █▄▄█▄▄▄▄▄▄▄█  █▄█   █▄█▄█ █▄▄█ █▄▄▄█   █▄▄▄█ █▄▄▄▄▄▄▄█▄▄▄█  █▄█▄▄▄▄▄▄▄█▄▄▄▄▄▄██▄▄█
");
        }

        
        #endregion

    }
}

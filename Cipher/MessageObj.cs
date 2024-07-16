using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cipher
{
    internal class MessageObj
    {
        private string CharacterLibrary { get; set; }
        private string Message { get; set; }
        private string SubstitutionKey { get; set; }
        private string TranspositionKey { get; set; }
        private bool Encode { get; set; }

        public MessageObj()
        {
            while (!SetLibrary()) ;
            Console.WriteLine($"Your library contains {CharacterLibrary.Length} characters. Here is your library:\n{CharacterLibrary}\n");

            while (!SetInput("Message"));
            Console.WriteLine($"Here is your message:\n{Message}\n");

            while (!SetInput("Substitution Key"));
            Console.WriteLine($"Here is your substitution key:\n{SubstitutionKey}\n");

            while (!SetInput("Transposition Key"));
            Console.WriteLine($"Here is your transposition key:\n{TranspositionKey}\n");

            while (!SetEncode());
            string value = Encode ? "encode" : "decode";
            Console.WriteLine($"You have chosen to {value} your message.\n");
        }


        private bool SetLibrary()
        {
            const string defaultLibrary = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ .";

            Console.WriteLine($"Enter your character library as a string, then press enter. If you do not enter any character, the default library will be used. " +
                $"Here is the default library:\n{defaultLibrary}");
            string library = Console.ReadLine();

            try
            {
                if (library != "")
                {
                    for (int i = 0; i < library.Length - 1; i++)
                    {
                        if (library.Substring(i + 1).Contains(library[i]))
                        {
                            throw new ArgumentException("The library must be made from unique characters.");
                        }
                    }

                    CharacterLibrary = library;
                }

                else
                {
                    CharacterLibrary = defaultLibrary;
                }

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        private bool SetInput(string type)
        {
            if (type == "Message")
            {
                Console.WriteLine("Enter your message, then press enter:");
            }

            else if (type == "Substitution Key")
            {
                Console.WriteLine("Enter your substitution key, then press enter:");
            }

            else
            {
                Console.WriteLine("Enter your transposition key, then press enter:");
            }

            string input = Console.ReadLine();

            try
            {
                int inputLength = input.Length;

                if (type == "Message" && (inputLength == 0 || inputLength > CharacterLibrary.Length))
                {
                    throw new ArgumentException($"{type} must be at least one character long, and shorter or equal to the length of the library.");
                }

                else if (type != "Message" && inputLength > CharacterLibrary.Length)
                {
                    throw new ArgumentException($"{type} must be shorter or equal to the length of the library.");
                }

                if (!input.All(CharacterLibrary.Contains))
                {
                    throw new ArgumentException($"Invalid characters in {type}. All characters from {type} must also be present in the library.");
                }

                if (inputLength < CharacterLibrary.Length)
                {
                    for (int i = inputLength; i < CharacterLibrary.Length; i++)
                    {
                        if (type != "Message" && inputLength != 0)
                        {
                            input += input[i % inputLength];
                        }

                        else
                        {
                            int index = RandomNumberGenerator.GetInt32(CharacterLibrary.Length);
                            input += CharacterLibrary[index];
                        }
                    }
                }

                if (type == "Message")
                {
                    Message = input;
                }

                else if (type == "Substitution Key")
                {
                    SubstitutionKey = input;
                }

                else
                {
                    TranspositionKey = input;
                }

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        private bool SetEncode()
        {
            Console.WriteLine("Enter your choice, then press enter. The value 1 is for encoding, and the value 2 is for decoding:");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Encode = true;

                return true;
            }

            else if (choice == "2")
            {
                Encode = false;

                return true;
            }

            Console.WriteLine("Your choice must be 1 or 2. The value 1 is for encoding, and the value 2 is for decoding.");

            return false;
        }

        public void GetOutput()
        {
            Console.WriteLine("Here is the processed message:");
            string encodedMessage = Cipher.EncodeDecode(CharacterLibrary, Message, SubstitutionKey, TranspositionKey, Encode);
            Console.WriteLine(encodedMessage);

            Console.WriteLine("Here is the original message:");
            string decodedMessage = Cipher.EncodeDecode(CharacterLibrary, encodedMessage, SubstitutionKey, TranspositionKey, !Encode);
            Console.WriteLine(decodedMessage);
        }
    }
}

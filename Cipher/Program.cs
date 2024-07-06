namespace Cipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;

            Console.WriteLine("*** Polyalphabetic block cipher ***\n\nMy cipher uses polyalphabetic substitution and transposition to encode and decode messages. " +
                "Now, the character library comes with a default value, but the library is in fact completely modulable. The only restrictions to my cipher are the following:\n\n" +
                "1) The library must be made from unique characters, that is, a specific character cannot appear twice;\n" +
                "2) The message and the keys cannot contain characters that are not in the library;\n" +
                "3) The individual message's and keys' character count cannot be longer than the library's character count;\n" +
                "4) The message cannot be empty, however the keys can be empty. My program will use padding if the message or the keys are not as long as the library. " +
                "That means the program will add characters at the end of the input, so as to make the input become the same length as the library.\n"); 

            while (!quit)
            {
                try
                {
                    MessageObj msg = new MessageObj();

                    msg.GetEncodedOrDecodedMessage();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("\nDo you want to quit the program, or do you want to continue? Type \"q\" to quit, or \"c\" to continue, then press Enter.");
                quit = (Console.ReadLine() == "q" ? true : false);
                Console.Write("\n");
            }
        }
    }
}

using Cipher;

namespace Cipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;

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

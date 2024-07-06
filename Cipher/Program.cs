using Cipher;

namespace Cipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter your character library as a string, then press enter:");
                string library = Console.ReadLine();
                library = Cipher.ValidateLibrary(library);
                Console.WriteLine($"Your library contains {library.Length} characters. Here is your library:\n{library}\n");

                Console.WriteLine("Enter your message, then press enter:");
                string message = Console.ReadLine();
                message = Cipher.ValidateInput(message, "Message");
                Console.WriteLine($"Here is your message:\n{message}\n");

                Console.WriteLine("Enter your substitution key, then press enter:");
                string substitutionKey = Console.ReadLine();
                substitutionKey = Cipher.ValidateInput(substitutionKey, "Substitution Key");
                Console.WriteLine($"Here is your substitution key:\n{substitutionKey}\n");

                Console.WriteLine("Enter your transposition key, then press enter:");
                string transpositionKey = Console.ReadLine();
                transpositionKey = Cipher.ValidateInput(transpositionKey, "Transposition Key");
                Console.WriteLine($"Here is your transposition key:\n{transpositionKey}\n");

                Console.WriteLine("Here is the encoded message:");
                string encodedMessage = Cipher.EncodeDecode(message, substitutionKey, transpositionKey, true);
                Console.WriteLine(encodedMessage);

                Console.WriteLine("Here is the decoded message:");
                string decodedMessage = Cipher.EncodeDecode(encodedMessage, substitutionKey, transpositionKey, false);
                Console.WriteLine(decodedMessage);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cipher
{
    internal class Cipher
    {
        static private string SubstituteCharacters(string library, string message, string key, bool encode)
        {
            var result = new char[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                int msgIndex = library.IndexOf(message[i]);
                int keyIndex = library.IndexOf(key[i]);
                int sign = (encode ? 1 : -1);

                int newIndex = (msgIndex + sign * keyIndex + library.Length) % library.Length;

                result[i] = library[newIndex];
            }

            return new string(result);
        }

        static private string TransposeCharacters(string library, string message, string key, bool encode)
        {
            var charArray = message.ToCharArray();

            int start = (encode ? 0 : message.Length - 1);
            int end = (encode ? message.Length : -1);
            int step = (encode ? 1 : -1);

            for (int i = start; i != end; i += step)
            {
                int swapIndex = (i + library.IndexOf(key[i])) % library.Length;

                (charArray[i], charArray[swapIndex]) = (charArray[swapIndex], charArray[i]);
            }

            return new string(charArray);
        }

        static public string EncodeDecode(string library, string message, string substitutionKey, string transpositionKey, bool encode)
        {
            return encode
                   ? TransposeCharacters(library, SubstituteCharacters(library, message, substitutionKey, encode), transpositionKey, encode)
                   : SubstituteCharacters(library, TransposeCharacters(library, message, transpositionKey, encode), substitutionKey, encode);
        }
    }
}

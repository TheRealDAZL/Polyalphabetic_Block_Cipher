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
        static private string CharacterLibrary = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ .";

        static public string ValidateLibrary(string library)
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

                return library;
            }

            return CharacterLibrary;
        }

        static public string ValidateInput(string input, string type)
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
                    if (type == "Message")
                    {
                        input += CharacterLibrary[RandomNumberGenerator.GetInt32(CharacterLibrary.Length)];
                    }

                    else if (type != "Message" && inputLength != 0)
                    {
                        input += input[i % inputLength];
                    }

                    else
                    {
                        input += CharacterLibrary[RandomNumberGenerator.GetInt32(CharacterLibrary.Length)];
                    }
                }
            }

            return input;
        }

        static private string SubstituteCharacters(string message, string key, bool encode)
        {
            var result = new char[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                int msgIndex = CharacterLibrary.IndexOf(message[i]);
                int keyIndex = CharacterLibrary.IndexOf(key[i]);
                int sign = encode ? 1 : -1;

                int newIndex = (msgIndex + sign * keyIndex + CharacterLibrary.Length) % CharacterLibrary.Length;

                result[i] = CharacterLibrary[newIndex];
            }

            return new string(result);
        }

        static private string TransposeCharacters(string message, string key, bool encode)
        {
            var charArray = message.ToCharArray();

            int start = encode ? 0 : message.Length - 1;
            int end = encode ? message.Length : -1;
            int step = encode ? 1 : -1;

            for (int i = start; i != end; i += step)
            {
                int swapIndex = (i + CharacterLibrary.IndexOf(key[i])) % CharacterLibrary.Length;
                (charArray[i], charArray[swapIndex]) = (charArray[swapIndex], charArray[i]);
            }

            return new string(charArray);
        }

        static public string EncodeDecode(string message, string substitutionKey, string transpositionKey, bool encode)
        {
            return encode
                  ? TransposeCharacters(SubstituteCharacters(message, substitutionKey, encode), transpositionKey, encode)
                  : SubstituteCharacters(TransposeCharacters(message, transpositionKey, encode), substitutionKey, encode);
        }
    }
}

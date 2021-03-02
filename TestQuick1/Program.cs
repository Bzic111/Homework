using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace TestQuick1
{

    class Programm
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine('\u223C'+"Привет");
            ASCIIEncoding ascii = new ASCIIEncoding();

            // A Unicode string with two characters outside the ASCII code range.
            String unicodeString =
                "This Unicode string contains two characters " +
                "with codes outside the ASCII code range, " +
                "Pi (\u03a0) and Sigma (\u03a3).";
            Console.WriteLine("Original string:");
            Console.WriteLine(unicodeString);

            // Save positions of the special characters for later reference.
            int indexOfPi = unicodeString.IndexOf('\u03a0');
            int indexOfSigma = unicodeString.IndexOf('\u03a3');

            // Encode string.
            Byte[] encodedBytes = ascii.GetBytes(unicodeString);
            Console.WriteLine();
            Console.WriteLine("Encoded bytes:");
            foreach (Byte b in encodedBytes)
            {
                Console.Write("[{0}]", b);
            }
            Console.WriteLine();

            // Notice that the special characters have been replaced with
            // the value 63, which is the ASCII character code for '?'.
            Console.WriteLine();
            Console.WriteLine(
                "Value at position of Pi character: {0}",
                encodedBytes[indexOfPi]
            );
            Console.WriteLine(
                "Value at position of Sigma character: {0}",
                encodedBytes[indexOfSigma]
            );

            // Decode bytes back to string.
            // Notice missing Pi and Sigma characters.
            String decodedString = ascii.GetString(encodedBytes);
            Console.WriteLine();
            Console.WriteLine("Decoded bytes:");
            Console.WriteLine(decodedString);


        }

        

    }
}

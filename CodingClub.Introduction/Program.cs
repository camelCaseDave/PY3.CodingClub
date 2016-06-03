 /****************************************************************************************************************************************
 *                                                                                                                                       *
 *                                             London Coding Club - An Introduction                                                      *
 *                                                                                                                                       *
 * 1) C# Is a programming language; a way of communicating with a computer via 0's and 1's.                                              *
 *      * Before, we just had C/C++, used to compile(translate) to the native code of the platform on which the application was written  *
 *             * e.g. Write application on Windows 10 x86 processor -> Linux (won't work) | Windows 8 (won't work)                       *
 *      * C# compiles code to an Intermediate Language(IL).                                                                              *
 *         * IL is independent of platform and can be understood by any hardware architecture/operating system                           *
 *           provided it is running Common Language Run-time(CLR)                                                                        *
 *                                                                                                                                       *
 * (2) Common Language Run-time (CLR)                                                                                                    *
 *     * Interprets Intermediate Language(IL) and translates to Native Code(code readable by platform on which application is being run )*
 *                                                                                                                                       *
 * **************************************************************************************************************************************/

//I've imported the System namespace from the .NET Framework. A namespace houses a collection of classes
//and each class can contain attributes and methods. The System namespace contains classes fundamental
// to C# development by providing us with commonly-used data types and events. 
using System;

// This is my namespace. A namespace contains a collection of classes. Here, my namespace contains classes
// which all relate to my CodingClub.Introduction. In enterprise applications, namespaces might contain
// a collection of classes related to Security or Graphics or CRM Authentication.
// When I compile my application, it will turn into a .dll or an .exe. 
// An .exe is an application that can be run/executed (hence .exe). 
// A .dll cannot be executed, it just links together all of our files into one library ready to be used 
// by other applications (hence .dll: dynamically linked library).
namespace CodingClub.Introduction
{
    // My default class. A class houses data (attributes) and functions (methods; behaviour). 
    // .NET Applications are made up of many classes (.cs files) which talk to each other at run-time
    // to achieve a certain functional goal.
    class Program
    {
        // This is the entry point of a Console Application. When this application is run, the Main
        // method is executed first. A Console Application is the most basic of application because
        // it doesn't have a Graphical User Interface (GUI). Think of cmd.exe, it's just a black box.
        static void Main(string[] args)
        {
            #region (1) Variables and Constants - names we give to a storage location in memory
            int number = 5; // <type> int <identifier> number <assignment operator> = <value> 5
            const decimal Pi = 3.14m;

            // Identifiers can't begin with a number, contain whitespace or use a reserved keyword such as int.

            // Naming Conventions:
            // camelCase - used for variableNames
            // PascalCase - used for Constants, Methods, Classes and NameSpaces.
            #endregion

            #region (2) Data Types
            // Integral Numbers.
            byte myByteName = 1;   // 1 byte  = 8 bits  =                            00000000 (has a range of 1 - 255) :. byte 255 + 1 = overflow
            short myShortName = 1; // 2 bytes = 16 bits =                   00000000 00000000
            int myIntName = 1;     // 4 bytes = 32 bits = 00000000 00000000 00000000 00000000 (has a range of -2.1B - 2.1B)
            long myLongName = 1;   // 8 bytes = 64 bits

            // Real Numbers.
            float myFloatName     = 0.1f;
            double myDoubleName   = 0.1;
            decimal myDecimalName = 0.1m;

            char myCharName = 'a';

            bool myBooleanName = true;

            var myVariableName = "This could be any data type - I'm letting the compiler decide what it is."; // Hover over the variable name.

            string myStringName = "Hi my name's Jaimie Ji and I have a 0% defect rate.";
            string[] myArrayName = { "stringNumberZero", "stringNumeroUno", "stringNumeroDos" };
            #endregion

            #region (3) Type Conversion
            // C# is a statically typed programming language meaning that once you've assigned a data type to 
            // a variable, you cannot change the data type once the application gets up and running. 
            // You can however convert the value of variable from one data type to a different data type
            // and store the value in a new variable.

            // This is implicit type conversion. The compiler knows that we can move a byte to an int because
            // an int is 4 bytes therefore a byte (1 byte) will fit in that memory slot.
            byte myByte = 1;    //                            00000001
            int myInt = myByte; // 00000000 00000000 00000000 00000001

            // Uncomment the second line - this won't compile. How are we going to fit 4 bytes of data (our int)
            // into a memory slot of 1 byte (our byte)?
            int anotherInt = 1;
            //byte anotherByte = anotherInt;

            // The compiler didn't understand our implicit conversion there so we will have to use explicit:
            byte anotherByte = (byte)anotherInt; // This is a cast.

            // Some types are not compatible and can't be converted. Uncomment the second line, how on earth
            // are we going to convert a string to an int?
            string myString = "Jaimie's really rustling my jimmies";
            // int howAboutAnotherInt = (int)myString;

            // Convert is from the System namespace but this really won't work. The string would need to be "1" or similar.
            // The Convert.cs class has lots of conversions like ToByte(), ToInt16() etc.
            //int convertedString = Convert.ToInt32(myString);

            #endregion

            #region (4) Error Handling
            // We prevent crashes in our application by handling errors like so
            try
            {
                // If an error occurs within our try { }, the code within our catch { } is executed.
                //string numberString = "12345";
                //byte b = Convert.ToByte(numberString);
                //Console.WriteLine(b);
            }
            catch (Exception)
            {
                Console.WriteLine("Display a friendly, relevant error message to the user.");
                Console.ReadKey();
            }
            #endregion

            #region (5) Operators

            // Arithmetic operators. First, I've declared 3 variables used for this example: beers, shots and result.
            int numberOfBeers = 5;
            int numberOfShots = 10;
            int result        = 0;

            int add      = numberOfBeers + numberOfShots; //        The result of 5 + 10 = 15
            int subtract = numberOfBeers - numberOfShots; //        The result of 5 - 10 = -10
            int multiply = numberOfBeers * numberOfShots; //        The result of 5 * 10 = 50
            int divide   = numberOfBeers / numberOfShots; //        The result of 5 / 10 = 0.5.. or is it?
            int modulus  = numberOfBeers % numberOfShots; //        The remainder of 5 / 10 = 5

            int increment = numberOfBeers++;              //        The same as numberOfBeers + 1
            int decrement = numberOfBeers--;              //        The same as numberOfBeers - 1

            // Comparison operators, they result in a bool.
            bool equal =                numberOfBeers == numberOfShots;    // ==
            bool notEqual =             numberOfBeers != numberOfShots;    // !=
            bool greaterThan =          numberOfBeers > numberOfShots;     // >
            bool greaterThanOrEqualTo = numberOfBeers >= numberOfShots;    // >=
            bool lessThan =             numberOfBeers < numberOfShots;     // <
            bool lessThanOrEqualTo =    numberOfBeers <= numberOfShots;    // <=

            // Assignment operators
            // =        a = 1
            // +=       a += 7      a = a + 7
            // -=       a -= 8      a = a - 8
            // *=       a *= 2      a = a * 2
            // /=       a /= 5      a = a / 5

            // Logical operators; used in conditional statements and result in bools
            // AND  &&
            // OR   ||
            // NOT   !

            // Bitwise operators; used in low-level programming
            // AND  &
            // OR   |
            #endregion

            #region End of Main method
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            #endregion
        }
    }
}


/*                    ____________         ____________                   ___________        ____________
   |                 |            |       |            |       |         |                  |
   |                 |            |       |            |       |         |                  |
   |                 |            |       |                    |         |                  |
   |                 |            |       |                    |         |                  |____________
   |                 |            |       |         ___        |         |                               |
   |                 |            |       |            |       |         |                               |
   |____________     |____________|       |____________|       |         |___________       _____________|

*/

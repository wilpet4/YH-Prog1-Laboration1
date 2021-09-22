using System;
using System.Collections.Generic;
using System.Numerics;
namespace Laboration_1
{
    class Program
    {
        static void Main()
        {
            string stringToProcess = "29535123p48723487597645723645";

            ProcessString(stringToProcess);
            while (true) // En enkel loop där användaren får mata in en egen sträng till ProcessString().
            {
                Console.Write("\nMata in en egen sträng om du vill: ");
                ProcessString(Console.ReadLine());
            }
        }
        private static void ProcessString(in string stringToProcess) // Detta är själva metoden som gör bearbetar strängen.
        {
            List<string> foundStringList = new List<string>(); // Denna lista ska innehålla alla upphittade strängar.
            List<int[]> posList = new List<int[]>(); // Denna lista ska innehålla start- och slutpositioner för alla upphittade strängar.
            BigInteger sum = 0;
            int startPos;
            int endPos;
            char[] separatedString = stringToProcess.ToCharArray(); // Denna array använder jag för att lätt kontrollera position i strängen.

            for (int i = 0; i < separatedString.Length; i++)
            {
                for (int j = 0; j <= 9; j++) // Här går loopen igenom alla tal 0-9 för att identifiera vilket tal som finns i positionen.
                {
                    if (separatedString[i].ToString() == j.ToString() && stringToProcess.IndexOf(j.ToString(), i + 1) >= 0) // Om tecknet i position "i" är lika med siffran "j" -
                    {                                                                                                       // och det hittas en likadan siffra senare i strängen.
                        startPos = i;
                        endPos = stringToProcess.IndexOf(j.ToString(), i + 1);
                        string finishedString = stringToProcess.Substring(startPos, (endPos - startPos) + 1);// Använder start- och slutpositioner för att lägga in strängen i en variabel.
                        if (BigInteger.TryParse(finishedString, out BigInteger result)) // Rensar bort strängar som innehåller annat än siffror innan de läggs in i listan.
                        {
                            posList.Add(new int[2] { startPos, endPos });
                            foundStringList.Add(finishedString);
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < foundStringList.Count; i++) // Loopen räknar igenom alla färdiga upphittade strängar.
            {
                string beforeFoundString = stringToProcess.Remove(posList[i][0]);
                string afterFoundString = stringToProcess.Remove(0, (posList[i][1] + 1));

                Console.Write(beforeFoundString); // Här skrivs alla tecken innan den upphittade strängen ut.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(foundStringList[i]); // Här skrivs den upphittade strängen ut.
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(afterFoundString + "\n"); // Här skrivs alla tecken efter den upphittade strängen ut.

                sum += BigInteger.Parse(foundStringList[i]);
            }
            Console.WriteLine($"\nSumman av alla identifierade strängar av tal: {sum}");
        }
    }
}
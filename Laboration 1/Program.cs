using System;
using System.Collections.Generic;
using System.Numerics;
namespace Laboration_1
{
    class Program
    {
        static void Main()
        {
            bool isRunning = true;
            string stringToProcess = "29535123p48723487597645723645"; // Textsträngen som ska bearbetas.

            ProcessString(stringToProcess);
            while (isRunning) // En enkel loop där användaren får mata in en egen sträng till ProcessString().
            {
                Console.Write("\nMata in en egen sträng om du vill: ");
                string userString = Console.ReadLine();
                ProcessString(userString);
            }
        }
        private static void ProcessString(in string stringToProcess) // Detta är själva metoden som gör bearbetar strängen.
        {
            List<string> processedStringList = new List<string>(); // Denna lista innehåller alla upphittade strängar.
            List<int[]> posList = new List<int[]>(); // Denna lista innehåller start- och slutpositioner för alla upphittade strängar.
            string finishedString;
            BigInteger sum = 0;
            int startPos;
            int endPos;
            char[] separatedString = stringToProcess.ToCharArray(); // Denna array använder jag för att lätt kontrollera position i strängen.
            for (int i = 0; i < separatedString.Length; i++) // Loopen går igenom alla positioner i textsträngen
            {
                for (int j = 0; j <= 9; j++) // Här går loopen igenom alla tal 0-9 för att identifiera vilket tal som finns i positionen.
                {
                    if (separatedString[i].ToString() == j.ToString()) // Om tecknet i position "i" är lika med siffran "j".
                    {
                        startPos = i; // Här sparar jag första positionen där en siffra uppkommer i en variabel.
                        if (stringToProcess.IndexOf(j.ToString(), i + 1) >= 0) // Eftersom IndexOf() returnerar -1 när den inte hittar ett tecken så
                        {                                                      // använder jag denna if-sats för att filtrera ut minus-värden och förhindra körfel.
                            endPos = stringToProcess.IndexOf(j.ToString(), i + 1); // Här lagrar jag slutpositionen av en upphittad sträng med hjälp av IndexOf().
                            finishedString = stringToProcess.Substring(startPos, (endPos - startPos) + 1);
                            if (BigInteger.TryParse(finishedString, out BigInteger result)) // Här kontrolleras det så att bara strängar utan bokstäver skickas in i listan.
                            {
                                posList.Add(new int[2] {startPos, endPos}); // Här läggar jag in start- och slutpositioner i posList
                                processedStringList.Add(finishedString); // Och här lägger jag in de färdiga strängarna i sin lista.
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < processedStringList.Count; i++)
            {
                string beforeString = stringToProcess.Remove(posList[i][0]);
                string afterString = stringToProcess.Remove(0, (posList[i][1] + 1));

                Console.Write(beforeString); // Här skrivs alla tecken innan den upphittade strängen ut.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(stringToProcess.Substring(posList[i][0], (posList[i][1] - posList[i][0] + 1))); // Här skrivs den upphittade strängen ut.
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(afterString + "\n"); // Här skrivs alla tecken efter den upphittade strängen ut.
            }
            for (int i = 0; i < processedStringList.Count; i++) // Loopen räknar igenom alla färdiga upphittade strängar.
            {
                sum += long.Parse(processedStringList[i]); // Här adderar jag alla färdiga strängar av siffror i listan.
            }
            Console.WriteLine($"\nSumman av alla identifierade strängar av tal: {sum}");
        }
    }
}
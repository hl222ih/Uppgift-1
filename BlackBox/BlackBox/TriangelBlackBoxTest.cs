using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlackBox
{

    /// <summary>
    /// Detta program utför tester på Triangel.exe och skriver ut resultaten.
    /// Skriver även ut en manuellt skapad rapport över felaktigheter som hittats,
    /// efter det att testresultaten analyserats.
    /// </summary>
    class TriangelBlackBoxTest
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observera att Triangel.exe kommer att krascha ett antal gånger under programkörningen. Detta är väntat.");
            Console.ReadLine();

            List<List<string[]>> tests = CreateTestCases();
            PrintTestCases(tests);
            PrintReport();
            
        }

        /// <summary>
        /// Skriver ut en manuellt skriven rapport över de fel som hittats i Triangel.exe.
        /// </summary>
        static void PrintReport()
        {
            Console.WriteLine("Dokumentation och analys av utfallen:");
            Console.WriteLine();
            Console.WriteLine("1. En triangel kan inte bestå av sidor med negativ längd. Detta kontrollerar inte Triangel.exe.");
            Console.WriteLine("2. Om två av triangelns sidor är lika långa tillsammans som den tredje sidan är det ingen triangel, utan ett streck. Detta kontrollerar inte Triangel.exe");
            Console.WriteLine("3. Om två av triangelns sidor är kortare tillsammans än den tredje sidan så går sidorna inte ihop och bildar ingen triangel. Detta kontrollerar inte Triangel.exe");
            Console.WriteLine("4. Om någon av triangelns sidor har längden noll, så är det ingen triangel, utan ett streck (eller en punkt). Detta kontrollerar inte Triangel.exe.");
            Console.WriteLine("5. Triangel.exe kontrollerar inte att rätt antal argument skickas med, utan returnerar i de aktuella testfallen att triangeln är liksidig.");
            Console.WriteLine("6. Om man skickar in argument som som inte kan tolkas till värdet double, exempelvis en textsträng, kraschar Triangel.exe.");
            Console.WriteLine("7. Om heltal skickas in till programmet så hanteras det som om det vore double-värden, vilket väl är rimligt.");
            Console.WriteLine("8. Om värden med små skillnader skickas till Triangel.exe returneras oväntade resultat. 3 av 4 tester som resturneras är felaktiga. Troligen så klipps decimaldelen av talet av genom en explicit cast från double till int eller dylikt. Det skulle förklara de returnerade värdena, åtminstone, i de fyra testfallen.");
            Console.WriteLine("9. Triangel.exe accepterar värdena INF och -INF, vilket inte är rimligt. Om ett för stort värde, som inte avrundas till INF sker en programkrasch.");
            Console.WriteLine("I övrigt verkar Triangel.exe fungera som det är tänkt, men korrekta returvärden.");
            Console.ReadLine();
        }

        static List<List<string[]>> CreateTestCases()
        {
            List<string[]> allEqual = new List<string[]>();
            List<string[]> oneLonger = new List<string[]>();
            List<string[]> oneShorter = new List<string[]>();
            List<string[]> noEqual = new List<string[]>();
            List<string[]> twoEqualToOne = new List<string[]>();
            List<string[]> twoShorterThanOne = new List<string[]>();
            List<string[]> anyOrMoreZero = new List<string[]>();
            List<string[]> anyOrMoreNegative = new List<string[]>();
            List<string[]> tooFewArguments = new List<string[]>();
            List<string[]> tooManyArguments = new List<string[]>();
            List<string[]> badFormat = new List<string[]>();
            List<string[]> integers = new List<string[]>();
            List<string[]> smallDifferences = new List<string[]>();
            List<string[]> outsideValueType = new List<string[]>();

            List<List<string[]>> tests = new List<List<string[]>>() {
                allEqual, oneLonger, oneShorter, noEqual, twoEqualToOne, 
                twoShorterThanOne, anyOrMoreZero, anyOrMoreNegative, 
                tooFewArguments, tooManyArguments, badFormat, integers,
                smallDifferences, outsideValueType};

            const double standardValue = 5.0;
            const double negativeValue = -5.0;
            const double zeroValue = 0.0;

            //tre sidor lika långa
            allEqual.Add(new string[] { (standardValue).ToString(), standardValue.ToString(), (standardValue + 0.001).ToString() });

            //två sidor lika långa
            //en sida längre än de andra två
            oneLonger.Add(new string[] { (standardValue + 1).ToString(), standardValue.ToString(), standardValue.ToString() });
            oneLonger.Add(new string[] { standardValue.ToString(), (standardValue + 1).ToString(), standardValue.ToString() });
            oneLonger.Add(new string[] { standardValue.ToString(), standardValue.ToString(), (standardValue + 1).ToString() });
            //en sida kortare än de andra två
            oneShorter.Add(new string[] { (standardValue - 1).ToString(), standardValue.ToString(), standardValue.ToString() });
            oneShorter.Add(new string[] { standardValue.ToString(), (standardValue - 1).ToString(), standardValue.ToString() });
            oneShorter.Add(new string[] { standardValue.ToString(), standardValue.ToString(), (standardValue - 1).ToString() });

            //inga sidor lika långa
            noEqual.Add(new string[] { (standardValue + 1).ToString(), (standardValue - 1).ToString(), standardValue.ToString() });
            noEqual.Add(new string[] { (standardValue + 1).ToString(), standardValue.ToString(), (standardValue - 1).ToString() });
            noEqual.Add(new string[] { standardValue.ToString(), (standardValue + 1).ToString(), (standardValue - 1).ToString() });
            noEqual.Add(new string[] { (standardValue - 1).ToString(), (standardValue + 1).ToString(), standardValue.ToString() });
            noEqual.Add(new string[] { (standardValue - 1).ToString(), standardValue.ToString(), (standardValue + 1).ToString() });
            noEqual.Add(new string[] { standardValue.ToString(), (standardValue - 1).ToString(), (standardValue + 1).ToString() });

            //två sidor lika lång tillsammans som den tredje sidan
            twoEqualToOne.Add(new string[] { standardValue.ToString(), standardValue.ToString(), (standardValue * 2).ToString() });
            twoEqualToOne.Add(new string[] { standardValue.ToString(), (standardValue * 2).ToString(), standardValue.ToString() });
            twoEqualToOne.Add(new string[] { (standardValue * 2).ToString(), standardValue.ToString(), standardValue.ToString() });

            //två sidor kortare än den tredje sidan
            twoShorterThanOne.Add(new string[] { standardValue.ToString(), standardValue.ToString(), (standardValue * 2 + 1).ToString()});
            twoShorterThanOne.Add(new string[] { standardValue.ToString(), (standardValue * 2 + 1).ToString(), standardValue.ToString() });
            twoShorterThanOne.Add(new string[] { (standardValue * 2 + 1).ToString(), standardValue.ToString(), standardValue.ToString() });

            //någon eller fler av sidorna 0
            anyOrMoreZero.Add(new string[] { zeroValue.ToString(), standardValue.ToString(), standardValue.ToString() });
            anyOrMoreZero.Add(new string[] { standardValue.ToString(), zeroValue.ToString(), standardValue.ToString() });
            anyOrMoreZero.Add(new string[] { standardValue.ToString(), standardValue.ToString(), zeroValue.ToString() });
            anyOrMoreZero.Add(new string[] { zeroValue.ToString(), zeroValue.ToString(), standardValue.ToString() });
            anyOrMoreZero.Add(new string[] { zeroValue.ToString(), standardValue.ToString(), zeroValue.ToString() });
            anyOrMoreZero.Add(new string[] { standardValue.ToString(), zeroValue.ToString(), zeroValue.ToString() });
            anyOrMoreZero.Add(new string[] { zeroValue.ToString(), zeroValue.ToString(), zeroValue.ToString() });

            //någon eller fler av sidorna negativ
            anyOrMoreNegative.Add(new string[] { negativeValue.ToString(), standardValue.ToString(), standardValue.ToString() });
            anyOrMoreNegative.Add(new string[] { standardValue.ToString(), negativeValue.ToString(), standardValue.ToString() });
            anyOrMoreNegative.Add(new string[] { standardValue.ToString(), standardValue.ToString(), negativeValue.ToString() });
            anyOrMoreNegative.Add(new string[] { negativeValue.ToString(), negativeValue.ToString(), standardValue.ToString() });
            anyOrMoreNegative.Add(new string[] { negativeValue.ToString(), standardValue.ToString(), negativeValue.ToString() });
            anyOrMoreNegative.Add(new string[] { standardValue.ToString(), negativeValue.ToString(), negativeValue.ToString() });
            anyOrMoreNegative.Add(new string[] { negativeValue.ToString(), negativeValue.ToString(), negativeValue.ToString() });

            //för få argument
            tooFewArguments.Add(new string[] { standardValue.ToString(), standardValue.ToString() });
            tooFewArguments.Add(new string[] { standardValue.ToString() });

            //för många argument
            tooManyArguments.Add(new string[] { standardValue.ToString(), standardValue.ToString(), standardValue.ToString(), standardValue.ToString() });
            tooManyArguments.Add(new string[] { standardValue.ToString(), standardValue.ToString(), standardValue.ToString(), standardValue.ToString(), standardValue.ToString() });

            //felaktigt format
            badFormat.Add(new string[] { "abc", "abc", "abc" });
            
            //heltal
            integers.Add(new string[] { Convert.ToInt32(standardValue).ToString(), Convert.ToInt32(standardValue + 1).ToString(), Convert.ToInt32(standardValue - 1).ToString() });
            
            //argument med små skillnader sinsemellan
            smallDifferences.Add (new string[] { standardValue.ToString(), (standardValue + 0.01).ToString(), (standardValue + 0.01).ToString() });
            smallDifferences.Add (new string[] { standardValue.ToString(), (standardValue + 0.01).ToString(), (standardValue + 0.02).ToString() });
            smallDifferences.Add (new string[] { standardValue.ToString(), (standardValue - 0.01).ToString(), (standardValue - 0.01).ToString() });
            smallDifferences.Add (new string[] { standardValue.ToString(), (standardValue - 0.01).ToString(), (standardValue - 0.02).ToString() });

            //tal som går utanför omfånget för double-taltypen.
            outsideValueType.Add (new string[] { (Double.MaxValue + 1).ToString(), (Double.MaxValue + 1).ToString(), (Double.MaxValue + 1).ToString() });
            outsideValueType.Add (new string[] { (Double.MaxValue * 3).ToString(), (Double.MaxValue * 3).ToString(), (Double.MaxValue * 3).ToString() });
            outsideValueType.Add (new string[] { (Double.MinValue - 1).ToString(), (Double.MinValue - 1).ToString(), (Double.MinValue - 1).ToString() });
            outsideValueType.Add (new string[] { (Double.MinValue * 3).ToString(), (Double.MinValue * 3).ToString(), (Double.MinValue * 3).ToString() });

            return tests;
        }

        /// <summary>
        /// Skriver ut testfallen, förväntade resultat och verkliga resultat och jämför dessa.
        /// </summary>
        /// <param name="tests">Testerna.</param>
        static void PrintTestCases(List<List<string[]>> tests)
        {
            Console.WriteLine("╔════════╦════════╦════════╦════════════════════╦═══════════════════╦════════╗");
            Console.WriteLine("║ sida 1 ║ sida 2 ║ sida 3 ║ förväntat resultat ║ verkligt resultat ║ utfall ║");
            Console.WriteLine("╠════════╩════════╩════════╩════════════════════╩═══════════════════╩════════╣");

            //rubriker
            string[] testGroupHeadings = new string[]{
                "Tre sidor lika långa",
                "En sida längre än de andra",
                "En sida kortare än de andra",
                "Inga sidor lika långa",
                "Två sidor tillsammans lika långa som den tredje",
                "Två sidor tillsammans kortare än den tredje",
                "Minst en sida med värdet 0.0",
                "Minst en sida med negativt värde",
                "För få argument", 
                "För många argument",
                "Felaktigt format",
                "Heltal",
                "Små skillnader",
                "Utanför värdetypens min- och maxvärde (ursäkta tabellens utseende)"};

            for (int i = 0; i < tests.Count; i++)
            {
                if (i != 0)
                {
                    Console.WriteLine("╠════════╩════════╩════════╩════════════════════╩═══════════════════╩════════╣");
                }
                Console.WriteLine("║  {0,2}. {1,-70}║", (i + 1), testGroupHeadings[i]);
                Console.WriteLine("╠════════╦════════╦════════╦════════════════════╦═══════════════════╦════════╣");

                foreach (string[] test in tests[i])
                {
                    string expected = "";

                    double side1 = 0.0;
                    double side2 = 0.0;
                    double side3 = 0.0;
                    string side1AsString = null;
                    string side2AsString = null;
                    string side3AsString = null;

                    string arguments = String.Empty;
                    int temp = 0;
                    if (test.Length < 3 || test.Length > 3)
                    {
                        expected = "ogiltig";


                        if (test.Length > 0)
                        {
                            side1AsString = test[0];
                        }
                        else
                        {
                            side1AsString = String.Empty;
                        }

                        if (test.Length > 1)
                        {
                            side2AsString = test[1];
                        }
                        else
                        {
                            side2AsString = String.Empty;
                        }

                        if (test.Length > 2)
                        {
                            side3AsString = test[2];
                        }
                        else
                        {
                            side3AsString = String.Empty;
                        }
                        
                    }
                    //för heltalstestet, testar om inmatning är konverterbar till heltal, men samtidigt att inmatningen inte innehåller decimaler
                    else if (Int32.TryParse(test[0], out temp) && Int32.TryParse(test[1], out temp) && Int32.TryParse(test[2], out temp) &&
                        System.Text.RegularExpressions.Regex.IsMatch(test[0], @"(\.|,)") &&
                        System.Text.RegularExpressions.Regex.IsMatch(test[1], @"(\.|,)") &&
                        System.Text.RegularExpressions.Regex.IsMatch(test[2], @"(\.|,)"))
                    {
                        //har bara ett test, 5 6 4 som ska ge resultatet oliksidig.
                        //det bör rimligtvis vara ok att skicka med ett heltal istället
                        //för en double, tycker jag.
                        
                        expected = "oliksidig";
                        side1AsString = test[0];
                        side2AsString = test[1];
                        side3AsString = test[2];
                    }
                    else if (!Double.TryParse(test[0], out side1) || !Double.TryParse(test[1], out side2) || !Double.TryParse(test[2], out side3))
                    {
                        expected = "ogiltig";
                        side1AsString = test[0];
                        side2AsString = test[1];
                        side3AsString = test[2];
                    }
                    else if (side1 <= 0 || side2 <= 0 || side3 <= 0 ||
                        (side1 + side2) <= side3 ||
                        (side1 + side3) <= side2 ||
                        (side2 + side3) <= side1)
                    {
                        expected = "ogiltig";
                    }
                    else if (side1 == side2 && side2 == side3)
                    {
                        expected = "liksidig";
                    }
                    else if (side1 == side2 || side2 == side3 || side1 == side3)
                    {
                        expected = "likbent";
                    }
                    else
                    {
                        expected = "oliksidig";
                    }


                    string testResult = String.Empty;

                    //ta argumenten från side1(double) eller side1AsString(string) beroende på om parsning lyckades längre upp i koden
                    if ((side1AsString != null) && (side2AsString != null) && (side3AsString != null))
                    {
                        testResult = RunTestCase(String.Format("{0} {1} {2}", side1AsString, side2AsString, side3AsString));
                    }
                    else
                    {
                        testResult = RunTestCase(String.Format("{0} {1} {2}", side1.ToString(), side2.ToString(), side3.ToString()));
                    }
            
                    //Förkortar ner programmets svar för att passa in i tabellen.
                    string formattedTestResult = String.Empty;
                    if (testResult.Equals("Triangeln har inga lika sidor\r\n"))
                    {
                        formattedTestResult = "oliksidig";
                    }
                    else if (testResult.Equals("Triangeln är likbent\r\n"))
                    {
                        formattedTestResult = "likbent";
                    }
                    else if (testResult.Equals("Triangeln är liksidig\r\n"))
                    {
                        formattedTestResult = "liksidig";
                    }
                    else
                    {
                        if (testResult.Length >= 2)
                        {
                            formattedTestResult = testResult.Substring(0, testResult.Length - 2);
                        }
                        else
                        {
                            formattedTestResult = testResult;
                        }
                    }


                    Console.WriteLine("║ {0,-6} ║ {1,-6} ║ {2,-6} ║ {3,-18} ║ {4,-17} ║ {5,-6} ║",
                        ((side1AsString != null) ? side1AsString : side1.ToString("0.00")), 
                        ((side2AsString != null) ? side2AsString : side2.ToString("0.00")), 
                        ((side3AsString != null) ? side3AsString : side3.ToString("0.00")),
                        expected, formattedTestResult, (expected.Equals(formattedTestResult) ? "true" : "false"));
                }
            }
            Console.WriteLine("╚════════╩════════╩════════╩════════════════════╩═══════════════════╩════════╝");                
        }

        /// <summary>
        /// Kör programmet triangel.exe och returnerar programmets output.
        /// </summary>
        /// <param name="arg">längden på sidorna</param>
        /// <returns>triangel.exe:s output för de givna argumenten  </returns>
        static string RunTestCase(string arg)
        {
            Process process = new Process();
            process.StartInfo.FileName = "triangel.exe";
            process.StartInfo.Arguments = arg;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                output = "<programkrasch>\r\n";
            }

            return output;
        }
    }   
}

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

            List<List<double[]>> tests = CreateTestCases();
            PrintTestCases(tests);
            PrintReport();
            
        }

        /// <summary>
        /// Skriver ut en manuellt skriven rapport över de fel som hittats i Triangel.exe.
        /// </summary>
        static void PrintReport()
        {
            Console.WriteLine("Dokumentation och analys av utfallen:");
            Console.WriteLine("En triangel kan inte bestå av sidor med negativ längd. Detta kontrollerar inte Triangel.exe.");
            Console.WriteLine("Om två av triangelns sidor är lika långa tillsammans som den tredje sidan är det ingen triangel, utan ett streck. Detta kontrollerar inte Triangel.exe");
            Console.WriteLine("Om två av triangelns sidor är kortare tillsammans än den tredje sidan så går sidorna inte ihop och bildar ingen triangel. Detta kontrollerar inte Triangel.exe");
            Console.WriteLine("Om någon av triangelns sidor har längden noll, så är det ingen triangel, utan ett streck. Detta kontrollerar inte Triangel.exe.");
            Console.WriteLine("Av uppgiften framgår att Triangel tar tre värden av värdetypen double. Jag utgår från att uppgiften inte handlar om input-validering. Exempelvis kraschar programmet om man skriver in text istället för nummer.");
            Console.ReadLine();
        }

        static List<List<double[]>> CreateTestCases()
        {
            List<double[]> allEqual = new List<double[]>();
            List<double[]> oneLonger = new List<double[]>();
            List<double[]> oneShorter = new List<double[]>();
            List<double[]> noEqual = new List<double[]>();
            List<double[]> twoEqualToOne = new List<double[]>();
            List<double[]> twoShorterThanOne = new List<double[]>();
            List<double[]> anyOrMoreZero = new List<double[]>();
            List<double[]> anyOrMoreNegative = new List<double[]>();
            List<List<double[]>> tests = new List<List<double[]>>() {
                allEqual, oneLonger, oneShorter, noEqual, twoEqualToOne, 
                twoShorterThanOne, anyOrMoreZero, anyOrMoreNegative };

            const double standardValue = 5.0;
            const double negativeValue = -5.0;
            const double zeroValue = 0.0;

            //tre sidor lika långa
            allEqual.Add(new double[]{standardValue, standardValue, standardValue});

            //två sidor lika långa
            //en sida längre än de andra två
            oneLonger.Add(new double[] { standardValue + 1, standardValue, standardValue });
            oneLonger.Add(new double[] { standardValue, standardValue + 1, standardValue });
            oneLonger.Add(new double[] { standardValue, standardValue, standardValue + 1 });
            //en sida kortare än de andra två
            oneShorter.Add(new double[] { standardValue - 1, standardValue, standardValue });
            oneShorter.Add(new double[] { standardValue, standardValue - 1, standardValue });
            oneShorter.Add(new double[] { standardValue, standardValue, standardValue - 1 });

            //inga sidor lika långa
            noEqual.Add(new double[] { standardValue + 1, standardValue - 1, standardValue });
            noEqual.Add(new double[] { standardValue + 1, standardValue, standardValue - 1 });
            noEqual.Add(new double[] { standardValue, standardValue + 1, standardValue - 1 });
            noEqual.Add(new double[] { standardValue - 1, standardValue + 1, standardValue });
            noEqual.Add(new double[] { standardValue - 1, standardValue, standardValue + 1 });
            noEqual.Add(new double[] { standardValue, standardValue - 1, standardValue + 1 });

            //två sidor lika lång tillsammans som den tredje sidan
            twoEqualToOne.Add(new double[] { standardValue, standardValue, standardValue * 2 });
            twoEqualToOne.Add(new double[] { standardValue, standardValue * 2, standardValue });
            twoEqualToOne.Add(new double[] { standardValue * 2, standardValue, standardValue });

            //två sidor kortare än den tredje sidan
            twoShorterThanOne.Add(new double[] { standardValue, standardValue, standardValue * 2 + 1});
            twoShorterThanOne.Add(new double[] { standardValue, standardValue * 2 + 1, standardValue });
            twoShorterThanOne.Add(new double[] { standardValue * 2 + 1, standardValue, standardValue });

            //någon eller fler av sidorna 0
            anyOrMoreZero.Add(new double[] { zeroValue, standardValue, standardValue });
            anyOrMoreZero.Add(new double[] { standardValue, zeroValue, standardValue });
            anyOrMoreZero.Add(new double[] { standardValue, standardValue, zeroValue });
            anyOrMoreZero.Add(new double[] { zeroValue, zeroValue, standardValue });
            anyOrMoreZero.Add(new double[] { zeroValue, standardValue, zeroValue });
            anyOrMoreZero.Add(new double[] { standardValue, zeroValue, zeroValue });
            anyOrMoreZero.Add(new double[] { zeroValue, zeroValue, zeroValue });

            //någon eller fler av sidorna negativ
            anyOrMoreNegative.Add(new double[] { negativeValue, standardValue, standardValue });
            anyOrMoreNegative.Add(new double[] { standardValue, negativeValue, standardValue });
            anyOrMoreNegative.Add(new double[] { standardValue, standardValue, negativeValue });
            anyOrMoreNegative.Add(new double[] { negativeValue, negativeValue, standardValue });
            anyOrMoreNegative.Add(new double[] { negativeValue, standardValue, negativeValue });
            anyOrMoreNegative.Add(new double[] { standardValue, negativeValue, negativeValue });
            anyOrMoreNegative.Add(new double[] { negativeValue, negativeValue, negativeValue });

            //jag utgår ifrån att validering görs för att inputvärden verkligen är av värdetypen 
            //double och inom de gränser som värdetypen tillåter.

            return tests;
        }

        /// <summary>
        /// Skriver ut testfallen, förväntade resultat och verkliga resultat och jämför dessa.
        /// </summary>
        /// <param name="tests">Testerna.</param>
        static void PrintTestCases(List<List<double[]>> tests)
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
                "Minst en sida med negativt värde"};

            for (int i = 0; i < tests.Count; i++)
            {
                if (i != 0)
                {
                    Console.WriteLine("╠════════╩════════╩════════╩════════════════════╩═══════════════════╩════════╣");
                }
                Console.WriteLine("║  " + (i + 1) + ". " + String.Format("{0,-71}", testGroupHeadings[i]) + "║");
                Console.WriteLine("╠════════╦════════╦════════╦════════════════════╦═══════════════════╦════════╣");

                foreach (double[] test in tests[i])
                {
                    string expected = "";
                    if (test[0] <= 0 || test[1] <= 0 || test[2] <= 0 ||
                        (test[0] + test[1]) <= test[2] ||
                        (test[0] + test[2]) <= test[1] ||
                        (test[1] + test[2]) <= test[0])
                    {
                        expected = "ogiltig";
                    }
                    else if (test[0] == test[1] && test[1] == test[2])
                    {
                        expected = "liksidig";
                    }
                    else if (test[0] == test[1] || test[1] == test[2] || test[0] == test[2])
                    {
                        expected = "likbent";
                    }
                    else
                    {
                        expected = "oliksidig";
                    }

                    string testResult = RunTestCase(test[0], test[1], test[2]);
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

                    Console.WriteLine("║ {0,-6} ║ {1,-6} ║ {2,-6} ║ {3,-18} ║ {4,-17} ║ {5,-6} ║",
                        test[0].ToString("0.0"), test[1].ToString("0.0"), test[2].ToString("0.0"),
                        expected, formattedTestResult, (expected.Equals(formattedTestResult) ? "true" : "false"));
                }
            }
            Console.WriteLine("╚════════╩════════╩════════╩════════════════════╩═══════════════════╩════════╝");                
        }

        /// <summary>
        /// Kör programmet triangel.exe och returnerar programmets output.
        /// </summary>
        /// <param name="arg1">längden på sida 1 i triangeln</param>
        /// <param name="arg2">längden på sida 2 i triangeln</param>
        /// <param name="arg2">längden på sida 3 i triangeln</param>
        /// <returns>triangel.exe:s output för de givna argumenten  </returns>
        static string RunTestCase(double arg1, double arg2, double arg3)
        {
            Process process = new Process();
            process.StartInfo.FileName = "triangel.exe";
            process.StartInfo.Arguments = String.Format("{0} {1} {2}", arg1.ToString(), arg2.ToString(), arg3.ToString());
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }
    }   
}

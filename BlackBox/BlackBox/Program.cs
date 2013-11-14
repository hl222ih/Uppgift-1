using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlackBox
{
    class Program
    {
        static void Main(string[] args)
        {

            List<List<double[]>> tests = CreateTestCases();
            PrintTestCases(tests);
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

        static void PrintTestCases(List<List<double[]>> tests)
        {
            Console.WriteLine("╔════════╦════════╦════════╦════════════════════╦═══════════════════╦════════╗");
            Console.WriteLine("║ sida 1 ║ sida 2 ║ sida 3 ║ förväntat resultat ║ verkligt resultat ║ utfall ║");
            Console.WriteLine("╠════════╩════════╩════════╩════════════════════╩═══════════════════╩════════╣");

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
                Console.WriteLine("║  " + (i + 1) + ". " + String.Format("{0,-40}", testGroupHeadings[i]) + "                               ║");
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
                    Console.WriteLine("║ {0,-6} ║ {1,-6} ║ {2,-6} ║ {3,-18} ║ {4,-17} ║ {5,-6} ║",
                        test[0].ToString("0.0"), test[1].ToString("0.0"), test[2].ToString("0.0"),
                        String.Format("{0,-15}", expected), "-", "-");
                }
            }
            Console.WriteLine("╚════════╩════════╩════════╩════════════════════╩═══════════════════╩════════╝");                
        }
    }   
}

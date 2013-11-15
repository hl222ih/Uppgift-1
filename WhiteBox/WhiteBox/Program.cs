using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteBox
{
    class Program
    {
        static void Main(string[] args)
        {
            //temporärt testvärde
            args = new string[] { "4.0", "4.0", "4.0s" };

            string message = String.Empty;
            string errorMessage = String.Empty;

            if (ValidateInput(args, out errorMessage))
            {
            }
            else
            {
                message = errorMessage;
            }

            Console.WriteLine(message);
            Console.ReadLine();

        }

        private static bool ValidateInput(string[] args, out string errorMessage)
        {
            errorMessage = String.Empty;
            if (args.Length != 3 && args.Length != 6)
            {
                errorMessage = "Felaktigt antal argument. Ange sidans längder (tre argument) eller de tre punkternas koordinater (sex argument)";
                return false;
            }

            double[] argsAsDouble = new double[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (!Double.TryParse(args[i], out argsAsDouble[i]))
                {
                    errorMessage = "Ett eller flera argument kan inte tolkas som ett flyttal (double).";
                    return false;
                }
            }

            if (argsAsDouble.Length == 3)
            {
                foreach (double argAsDouble in argsAsDouble)
                {
                    if (argAsDouble <= 0 || argAsDouble == Double.PositiveInfinity)
                    {
                        errorMessage = "Ett eller flera argument representerar för stora tal eller för små tal.";
                        return false;
                    }
                }
            }

            if (argsAsDouble.Length == 6)
            {
                foreach (double argAsDouble in argsAsDouble)
                {
                    if (Double.IsInfinity(argAsDouble))
                    {
                        errorMessage = "Ett eller flera argument representerar för stora eller för små tal.";
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

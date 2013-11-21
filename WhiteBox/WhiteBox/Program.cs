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

            //Egentligen för Triangle-klassens konstruktor att kolla, men
            //eftersom Triangelklassen har en konstruktor med parametrar
            //för både hörnens punkter och sidornas längder måste
            //man på något sätt bestämma om de medskickade värdena
            //är avsedda för att skapa punkter eller sidlängder.
            //Att räkna antalet argument är ett sätt, och det sättet
            //jag valt att använda i denna lösning.
            if (args.Length != 3 && args.Length != 6)
            {
                Console.WriteLine("Felaktigt antal argument.");
                return;
            }
            
            bool canAllArgumentsBeParsedToDouble = false;
            bool canAllArgumentsBeParsedToInt32 = false;

            if (args.Length == 3)
            {
                Predicate<string> doublePredicate = delegate(string arg){ double notUsed; return (Double.TryParse(arg, out notUsed)); };

                if (Array.TrueForAll<string>(args, doublePredicate))
                {
                    canAllArgumentsBeParsedToDouble = true;
                }

            }
            else if (args.Length == 6)
            {
                Predicate<string> intPredicate = delegate(string arg){ int notUsed; return (Int32.TryParse(arg, out notUsed)); };

                if (Array.TrueForAll<string>(args, intPredicate))
                {
                    canAllArgumentsBeParsedToInt32 = true;
                }
            }

            if (!canAllArgumentsBeParsedToDouble && !canAllArgumentsBeParsedToInt32)
            {
                Console.WriteLine("De angivna värdena kan inte tolkas.");
                return;
            }

            Triangle triangle = null;
            try
            {
                if (args.Length == 3)
                {
                    triangle = new Triangle(
                        Double.Parse(args[0]), Double.Parse(args[1]), Double.Parse(args[2])
                        );
                }
                else if (args.Length == 6)
                {
                    Point point1 = new Point(Int32.Parse(args[0]), Int32.Parse(args[1]));
                    Point point2 = new Point(Int32.Parse(args[2]), Int32.Parse(args[3]));
                    Point point3 = new Point(Int32.Parse(args[4]), Int32.Parse(args[5]));

                    triangle = new Triangle(point1, point2, point3);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("De angivna värdena är inte giltiga för en triangel.");
                return;
            }

            if (triangle.isEquilateral())
            {
                Console.WriteLine("Triangeln är liksidig.");
            }
            else if (triangle.isIsosceles())
            {
                Console.WriteLine("Triangeln är likbent.");
            }
            else if (triangle.isEquilateral())
            {
                Console.WriteLine("Triangeln är oliksidig.");
            }
        }
    }
}

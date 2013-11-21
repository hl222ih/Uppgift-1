using System;
using System.Linq;
using System.Collections.Generic;

public struct Point { 
  public int x, y;
  
  public Point(int a, int b) {
    x = a;
    y = b;
  }
}

public class Triangle {
  double[] sides;

  public Triangle(double a, double b, double c) {
    sides = new double[] { a, b, c };
    if (!CanSidesMakeALegalTriangle())
      throw new ArgumentException("Ogiltiga värden har angivits");
  } 

  public Triangle(double[] s) {
    if (s.Length != 3)
      throw new ArgumentException("Ogiltigt antal värden har angivits");
    sides = new double[s.Length];
    for(int i=0;i<s.Length;i++)
      sides[i]=s[i];
    if (!CanSidesMakeALegalTriangle())
        throw new ArgumentException("Ogiltiga värden har angivits");
  } 
  
  public Triangle(Point a, Point b, Point c) {
    sides = new double[3];
    sides[0] = Math.Sqrt(Math.Pow((double)(b.x - a.x), 2.0) + Math.Pow((double)(b.y - a.y), 2.0));
    sides[1] = Math.Sqrt(Math.Pow((double)(b.x - c.x), 2.0) + Math.Pow((double)(b.y - c.y), 2.0));
    sides[2] = Math.Sqrt(Math.Pow((double)(c.x - a.x), 2.0) + Math.Pow((double)(c.y - a.y), 2.0));
    if (!CanSidesMakeALegalTriangle())
      throw new ArgumentException("Ogiltiga värden har angivits");
  }

  public Triangle(Point[] s) {
    if (s.Length != 3)
      throw new ArgumentException("Ogiltigt antal värden har angivits");
    sides = new double[s.Length];
    sides[0] = Math.Sqrt(Math.Pow((double)(s[1].x - s[0].x), 2.0) + Math.Pow((double)(s[1].y - s[0].y), 2.0));
    sides[1] = Math.Sqrt(Math.Pow((double)(s[1].x - s[2].x), 2.0) + Math.Pow((double)(s[1].y - s[2].y), 2.0));
    sides[2] = Math.Sqrt(Math.Pow((double)(s[2].x - s[0].x), 2.0) + Math.Pow((double)(s[2].y - s[0].y), 2.0));
    if (!CanSidesMakeALegalTriangle())
      throw new ArgumentException("Ogiltiga värden har angivits");
  }

  private bool CanSidesMakeALegalTriangle()
  {
      if (sides[0] > 0 && sides[1] > 0 && sides[2] > 0 &&
          sides[0] < Double.PositiveInfinity && sides[1] < Double.PositiveInfinity && sides[2] < Double.PositiveInfinity &&
          (sides[0] + sides[1] > sides[2]) && (sides[0] + sides[2] > sides[1]) && (sides[0] < sides[1] + sides[2]) &&
          sides[0] > 0)
          return true;
      return false;
  }

  private int uniqueSides()
  {
      var temp = sides.Distinct<double>().Count();
      return temp;
  }

  public bool isScalene() {
    if(uniqueSides()==3) //En oliksidig triangel har 3 unika sidor, inte 1.
      return true;
    return false;
  }

  public bool isEquilateral() {
    if(uniqueSides()==1) //En liksidig triangel har 1 unik sida, inte 3.
      return true;
    return false;
  }

  public bool isIsosceles() {
      if (uniqueSides() == 2 || uniqueSides() == 1) //En likbent triangel har 1 eller 2 unika sidor, eftersom en liksidig triangel också är en likbent triangel.
      return true;
    return false;
  }
}

/* Exempel på användning: */
/* class Program { */
/*   static void Main(string[] args) { */
/*     double[] input = new double[3]; */
/*     for(int i=0;i<3;i++) */
/*       input[i]=double.Parse(args[i]); */
    
/*     Triangle t = new Triangle(input); */

/*     if(t.isScalene()) Console.WriteLine("Triangeln har inga lika sidor"); */
/*     if(t.isEquilateral()) Console.WriteLine("Triangeln är liksidig"); */
/*     if(t.isIsosceles()) Console.WriteLine("Triangeln är likbent"); */
/*  } */
/* } */

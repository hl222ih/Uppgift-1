using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WhiteBoxTest
{
    [TestClass]
    public class WhiteBoxTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooFewPointArguments()
        {
            Triangle triangle = new Triangle(new Point[] { new Point(1, 1) });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooManyPointArguments()
        {
            Triangle triangle = new Triangle(new Point[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4) });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SamePointArgumentsArrayConstructor()
        {
            Triangle triangle = new Triangle(new Point[] { new Point(1, 1), new Point(1, 1), new Point(2, 2) });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SamePointArgumentsPointConstructor()
        {
            Triangle triangle = new Triangle(new Point(1, 1), new Point(1, 1), new Point(2, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooFewSideArguments()
        {
            Triangle triangle = new Triangle(new double[] { 1.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooManySideArguments()
        {
            Triangle triangle = new Triangle(new double[] { 1.0, 1.0, 1.0, 1.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments1ArrayConstructor()
        {
            //resulterar i en linje, inte en triangel
            Triangle triangle = new Triangle(new double[] { 1.0, 2.0, 3.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments1DoubleConstructor()
        {
            Triangle triangle = new Triangle(1.0, 2.0, 3.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments2ArrayConstructor()
        {
            //summan av de två kortaste sidorna kan inte vara kortare än den längsta sidan.
            Triangle triangle = new Triangle(new double[] { 4.0, 2.0, 1.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments2DoubleConstructor()
        {
            Triangle triangle = new Triangle(4.0, 2.0, 1.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments3ArrayConstructor()
        {
            //ingen sida kan vara noll.
            Triangle triangle = new Triangle(new double[] { 0.0, 2.0, 2.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments3DoubleConstructor()
        {
            Triangle triangle = new Triangle(0.0, 2.0, 2.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments4ArrayConstructor()
        {
            //ingen sida kan vara mindre än noll.
            Triangle triangle = new Triangle(new double[] { 4.0, 5.0, -1.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments4DoubleConstructor()
        {
            Triangle triangle = new Triangle( 4.0, 5.0, -1.0 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments5ArrayConstructor()
        {
            //ingen sida får vara längre än vad som kan hanteras av datatypen
            Triangle triangle = new Triangle(new double[] { 4.0, Double.MaxValue + 1, 4.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments5DoubleConstructor()
        {
            //ingen sida får vara längre än vad som kan hanteras av datatypen
            Triangle triangle = new Triangle(4.0, Double.MaxValue + 1, 4.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments6ArrayConstructor()
        {
            //ingen sida får vara PositiveInfinity
            Triangle triangle = new Triangle(new double[] { Double.PositiveInfinity, 3.0, 4.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments6DoubleConstructor()
        {
            //ingen sida får vara PositiveInfinity
            Triangle triangle = new Triangle(Double.PositiveInfinity, 3.0, 4.0);
        }

        [TestMethod]
        public void isEquilateralWithThreeEqualSides()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 5.0);
            Assert.IsTrue(triangle.isEquilateral());
        }

        [TestMethod]
        public void isNotEquilateralWithOneDeviantLongerSide()
        {
            Triangle triangle = new Triangle(5.0, 6.0, 5.0);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isNotEquilateralWithOneDeviantSlightlyLongerSide()
        {
            Triangle triangle = new Triangle(5.0, 5.01, 5.0);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isNotEquilateralWithOneDeviantShorterSide()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 4.0);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isNotEquilateralWithOneDeviantSlightlyShorterSide()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 4.99);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isNotEquilateralWithAllSidesDeviant()
        {
            Triangle triangle = new Triangle(4.0, 5.0, 3.0);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isNotEquilateralWithAllSidesSlightlyDeviantFromIntegerSide1()
        {
            //båda de avvikande sidlängderna kortare än "heltalslängden"
            Triangle triangle = new Triangle(4.99, 5.0, 4.98);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isNotEquilateralWithAllSidesSlightlyDeviantFromIntegerSide2()
        {
            //de avvikande sidlängderna på vardera sidan av "heltalslängden"
            Triangle triangle = new Triangle(4.99, 5.0, 5.01);
            Assert.IsFalse(triangle.isEquilateral());
        }
        
        [TestMethod]
        public void isNotEquilateralWithAllSidesSlightlyDeviantFromIntegerSide3()
        {
            //båda de avvikande sidlängderna längre än "heltalslängden"
            Triangle triangle = new Triangle(5.01, 5.0, 5.02);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isIsoscelesWithThreeEqualSides()
        {
            //En liksidig triangel är ett specialfall av en likbent triangel.
            Triangle triangle = new Triangle(5.0, 5.0, 5.0);
            Assert.IsTrue(triangle.isIsosceles());
        }

        [TestMethod]
        public void isIsoscelesWithOneDeviantLongerSide()
        {
            Triangle triangle = new Triangle(5.0, 6.0, 5.0);
            Assert.IsTrue(triangle.isIsosceles());
        }

        [TestMethod]
        public void isIsoscelesWithOneDeviantSlightlyLongerSide()
        {
            Triangle triangle = new Triangle(5.0, 5.01, 5.0);
            Assert.IsTrue(triangle.isIsosceles());
        }

        [TestMethod]
        public void isIsoscelesWithOneDeviantShorterSide()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 4.0);
            Assert.IsTrue(triangle.isIsosceles());
        }

        [TestMethod]
        public void isIsoscelesWithOneDeviantSlightlyShorterSide()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 4.99);
            Assert.IsTrue(triangle.isIsosceles());
        }

        [TestMethod]
        public void isNotIsoscelesWithAllSidesDeviant()
        {
            Triangle triangle = new Triangle(4.0, 5.0, 3.0);
            Assert.IsFalse(triangle.isIsosceles());
        }

        [TestMethod]
        public void isNotIsoscelesWithAllSidesSlightlyDeviantFromIntegerSide1()
        {
            //båda de avvikande sidlängderna kortare än "heltalslängden"
            Triangle triangle = new Triangle(4.99, 5.0, 4.98);
            Assert.IsFalse(triangle.isIsosceles());
        }

        [TestMethod]
        public void isNotIsoscelesWithAllSidesSlightlyDeviantFromIntegerSide2()
        {
            //de avvikande sidlängderna på vardera sidan av "heltalslängden"
            Triangle triangle = new Triangle(4.99, 5.0, 5.01);
            Assert.IsFalse(triangle.isIsosceles());
        }
        
        [TestMethod]
        public void isNotIsoscelesWithAllSidesSlightlyDeviantFromIntegerSide3()
        {
            //båda de avvikande sidlängderna längre än "heltalslängden"
            Triangle triangle = new Triangle(5.01, 5.0, 5.02);
            Assert.IsFalse(triangle.isIsosceles());
        }

        [TestMethod]
        public void isNotScaleneWithThreeEqualSides()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 5.0);
            Assert.IsFalse(triangle.isScalene());
        }

        [TestMethod]
        public void isNotScaleneWithOneDeviantLongerSide()
        {
            Triangle triangle = new Triangle(5.0, 6.0, 5.0);
            Assert.IsFalse(triangle.isScalene());
        }

        [TestMethod]
        public void isNotScaleneWithOneDeviantSlightlyLongerSide()
        {
            Triangle triangle = new Triangle(5.0, 5.01, 5.0);
            Assert.IsFalse(triangle.isScalene());
        }

        [TestMethod]
        public void isNotScaleneWithOneDeviantShorterSide()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 4.0);
            Assert.IsFalse(triangle.isScalene());
        }

        [TestMethod]
        public void isNotScaleneWithOneDeviantSlightlyShorterSide()
        {
            Triangle triangle = new Triangle(5.0, 5.0, 4.99);
            Assert.IsFalse(triangle.isScalene());
        }

        [TestMethod]
        public void isScaleneWithAllSidesDeviant()
        {
            Triangle triangle = new Triangle(4.0, 5.0, 3.0);
            Assert.IsTrue(triangle.isScalene());
        }

        [TestMethod]
        public void isScaleneWithAllSidesSlightlyDeviantFromIntegerSide1()
        {
            //båda de avvikande sidlängderna kortare än "heltalslängden"
            Triangle triangle = new Triangle(4.99, 5.0, 4.98);
            Assert.IsTrue(triangle.isScalene());
        }

        [TestMethod]
        public void isScaleneWithAllSidesSlightlyDeviantFromIntegerSide2()
        {
            //de avvikande sidlängderna på vardera sidan av "heltalslängden"
            Triangle triangle = new Triangle(4.99, 5.0, 5.01);
            Assert.IsTrue(triangle.isScalene());
        }
        
        [TestMethod]
        public void isScaleneWithAllSidesSlightlyDeviantFromIntegerSide3()
        {
            //båda de avvikande sidlängderna längre än "heltalslängden"
            Triangle triangle = new Triangle(5.01, 5.0, 5.02);
            Assert.IsTrue(triangle.isScalene());
        }
    }
}

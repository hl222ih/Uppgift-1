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
        public void SamePointArguments()
        {
            Triangle triangle = new Triangle(new Point[] { new Point(1, 1), new Point(1, 1), new Point(2, 2) });
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
        public void InvalidSideArguments1()
        {
            //resulterar i en linje, inte en triangel
            Triangle triangle = new Triangle(new double[] { 1.0, 2.0, 3.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments2()
        {
            //summan av de två kortaste sidorna kan inte vara kortare än den längsta sidan.
            Triangle triangle = new Triangle(new double[] { 4.0, 2.0, 1.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments3()
        {
            //ingen sida kan vara noll.
            Triangle triangle = new Triangle(new double[] { 0.0, 2.0, 2.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments4()
        {
            //ingen sida kan vara mindre än noll.
            Triangle triangle = new Triangle(new double[] { 4.0, 5.0, -1.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments5()
        {
            //ingen sida får vara längre än vad som kan hanteras av datatypen
            Triangle triangle = new Triangle(new double[] { 4.0, Double.MaxValue + 1, 4.0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSideArguments6()
        {
            //ingen sida får vara PositiveInfinity
            Triangle triangle = new Triangle(new double[] { Double.PositiveInfinity, 3.0, 4.0 });
        }

        [TestMethod]
        public void isScalene()
        {
            //liksidig
            Triangle triangle = new Triangle(5.0, 5.0, 5.0);
            Assert.IsFalse(triangle.isScalene());

            ////likbent
            triangle = new Triangle(5.0, 4.0, 5.0);
            Assert.IsFalse(triangle.isScalene());
            triangle = new Triangle(5.0, 4.99, 5.0);
            Assert.IsFalse(triangle.isScalene());
            triangle = new Triangle(5.0, 5.01, 5.0);
            Assert.IsFalse(triangle.isScalene());

            //oliksidig
            triangle = new Triangle(3.0, 4.0, 5.0);
            Assert.IsTrue(triangle.isScalene());
            triangle = new Triangle(4.99, 5.0, 5.01);
            Assert.IsTrue(triangle.isScalene());
            triangle = new Triangle(4.0, 4.01, 4.02);
            Assert.IsTrue(triangle.isScalene());
        }

        [TestMethod]
        public void isEquilateral()
        {
            //liksidig
            Triangle triangle = new Triangle(5.0, 5.0, 5.0);
            Assert.IsTrue(triangle.isEquilateral());

            //likbent
            triangle = new Triangle(5.0, 4.0, 5.0);
            Assert.IsFalse(triangle.isEquilateral());
            triangle = new Triangle(5.0, 4.99, 5.0);
            Assert.IsFalse(triangle.isEquilateral());
            triangle = new Triangle(5.0, 5.01, 5.0);
            Assert.IsFalse(triangle.isEquilateral());

            //oliksidig
            triangle = new Triangle(3.0, 4.0, 5.0);
            Assert.IsFalse(triangle.isEquilateral());
            triangle = new Triangle(4.99, 5.0, 5.01);
            Assert.IsFalse(triangle.isEquilateral());
            triangle = new Triangle(4.0, 4.01, 4.02);
            Assert.IsFalse(triangle.isEquilateral());
        }

        [TestMethod]
        public void isIsosceles()
        {
            //en liksidig triangel är ett specialfall av en likbent triangel.
            //en liksidig triangel är alltså alltid likbent.
            Triangle triangle = new Triangle(5.0, 5.0, 5.0);
            Assert.IsTrue(triangle.isIsosceles());

            //likbent
            triangle = new Triangle(5.0, 4.0, 5.0);
            Assert.IsTrue(triangle.isIsosceles());
            triangle = new Triangle(5.0, 4.99, 5.0);
            Assert.IsTrue(triangle.isIsosceles());
            triangle = new Triangle(5.0, 5.01, 5.0);
            Assert.IsTrue(triangle.isIsosceles());

            //oliksidig
            triangle = new Triangle(3.0, 4.0, 5.0);
            Assert.IsFalse(triangle.isIsosceles());
            triangle = new Triangle(4.99, 5.0, 5.01);
            Assert.IsFalse(triangle.isIsosceles());
            triangle = new Triangle(4.0, 4.01, 4.02);
            Assert.IsFalse(triangle.isIsosceles());
        }
    }
}

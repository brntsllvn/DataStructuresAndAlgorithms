﻿using NUnit.Framework;
using Shouldly;

namespace BeefyCases
{
    [TestFixture]
    class BeefyTestCases
    {
        [TestCase("A", 0, new[] { 0 })]
        [TestCase("B", 1, new[] { 0, 1 })]
        [TestCase("C", 3, new[] { 0, 1, 2 })]
        public void Simple_Cases(string caseName, int expected, int[] input)
        {
            var spectacularClass = new SpectacularClass();
            spectacularClass.SimpleAdd(input).ShouldBe(expected);
        }

        #region
        // http://stackoverflow.com/questions/25859094/an-attribute-argument-must-be-a-constant-expression-create-a-attribute-of
        //[TestCase("A decimal", 0.0m, new[] { 0.0m })]
        //[TestCase("B decimal", 0.1m, new[] { 0.0m, 0.1m })]
        //[TestCase("C decimal", 0.3m, new[] { 0.0m, 0.1m, 0.2m })]
        //public void Broken_Decimal_Cases(string caseName, int expected, decimal[] input)
        //{
        //    var spectacularClass = new SpectacularClass();
        //    spectacularClass.DecimalAdd(input).ShouldBe(expected);
        //}
        #endregion

        #region
        // http://www.nunit.org/index.php?p=testCaseSource&r=2.5
        [Test, TestCaseSource(nameof(DecimalCases))]
        public void Working_Decimal_Cases(string caseName, decimal expected, decimal[] input)
        {
            var spectacularClass = new SpectacularClass();
            spectacularClass.DecimalAdd(input).ShouldBe(expected);
        }

        private static readonly object[] DecimalCases =
        {
            new object[] {"A decimal", 0.0m, new[] { 0.0m }},
            new object[] {"B decimal", 0.1m, new[] { 0.0m, 0.1m }},
            new object[] {"C decimal", 0.3m, new[] { 0.0m, 0.1m, 0.2m} }
        };
        #endregion

        #region
        // http://stackoverflow.com/questions/25859094/an-attribute-argument-must-be-a-constant-expression-create-a-attribute-of
        //[TestCase("A Custom Class", 0, new[] { new WeatherBalloon(0) })]
        //[TestCase("B Custom Class", 1, new[] { new WeatherBalloon(0), new WeatherBalloon(1) })]
        //[TestCase("C Custom Class", 3, new[] { new WeatherBalloon(0), new WeatherBalloon(1), new WeatherBalloon(2) })]
        //public void Broken_Complicated_TestCases(string caseName, int expected, WeatherBalloon[] input)
        //{
        //    var spectacularClass = new SpectacularClass();
        //    spectacularClass.ComplicatedAdd(input).ShouldBe(expected);
        //}
        #endregion

        #region
        // http://www.nunit.org/index.php?p=testCaseSource&r=2.5
        [Test, TestCaseSource(nameof(FancyCases))]
        public void Working_Complicated_TestCases(string caseName, int expected, WeatherBalloon[] input)
        {
            var spectacularClass = new SpectacularClass();
            spectacularClass.ComplicatedAdd(input).ShouldBe(expected);
        }

        private static readonly object[] FancyCases =
        {
            new object[] {"A Custom Class", 0, new[] {new WeatherBalloon(0)}},
            new object[] {"B Custom Class", 1, new[] {new WeatherBalloon(0), new WeatherBalloon(1)}},
            new object[] {"C Custom Class", 3, new[] {new WeatherBalloon(0), new WeatherBalloon(1), new WeatherBalloon(2)}}
        };
        #endregion
    }
}

using Morpho25.Management;
using Morpho25.Settings;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MorphoTests.Simx
{
    internal class SimpleForcingTest
    {
        private SimpleForcing _simpleForcing;

        [SetUp]
        public void Setup()
        {
            var temperatures = new List<double>() 
            {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0,
                15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0,
                22.0, 23.0, 24.0
            };
            var humidty = new List<double>()
            {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0,
                15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0,
                22.0, 23.0, 24.0
            };

            _simpleForcing = new SimpleForcing(temperatures, humidty);
        }

        [Test]
        public void InitTest()
        {
            Assert.IsNotNull(_simpleForcing);

            Assert.IsTrue(_simpleForcing.Temperature.ToList().Count == 24);
            Assert.IsTrue(_simpleForcing.RelativeHumidity.ToList().Count == 24);

            Assert.IsTrue(_simpleForcing.Temperature.ToList()[0] == 274.15);
        }

        [Test]
        public void ErrorTest()
        {
            var temperatures = new List<double>()
            {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0,
                15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0,
                22.0, 23.0, 24.0
            };
            var humidty = new List<double>()
            {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0
            };

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var simpleForcing = new SimpleForcing(temperatures, humidty);
            });
            Assert.IsTrue("Temperature List size = Relative Humidity List size." == ex.Message);


            temperatures = new List<double>()
            {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0,
                15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0,
                22.0, 23.0
            };
            humidty = new List<double>()
            {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0,
                15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0,
                22.0, 23.0
            };

            ex = Assert.Throws<ArgumentException>(() =>
            {
                var simpleForcing = new SimpleForcing(temperatures, humidty);
            });
            Assert.IsTrue("Please, provide 24 values for each variable. Settings of a typical day to use for forcing." == ex.Message);


            temperatures = new List<double>()
            {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0,
                15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0,
                22.0, 23.0, 24.0
            };
            humidty = new List<double>()
            {
                1.0, 2.0, -13.0, 4.0, 5.0, 6.0, 7.0,
                8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0,
                15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0,
                22.0, 23.0, 24.0
            };

            ex = Assert.Throws<ArgumentException>(() =>
            {
                var simpleForcing = new SimpleForcing(temperatures, humidty);
            });
            Assert.IsTrue("Relative humidity go from 0 to 100." == ex.Message);
        }

        [Test]
        public void XMLMethod()
        {
            var values = _simpleForcing.Values;

            Assert.IsTrue(values.Length == 2);

            var tags = _simpleForcing.Tags;

            Assert.IsTrue(tags.Length == 2);

            Assert.IsTrue(_simpleForcing.Title == "SimpleForcing");
        }
    }
}

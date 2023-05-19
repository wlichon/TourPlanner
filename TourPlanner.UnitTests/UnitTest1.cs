using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner.Models;
using TourPlanner;
using TourPlanner.Core;
using System.Configuration;

namespace TourPlanner.UnitTests
{
    // UNIT TESTS

    // NAMING CONVENTION: Function_Scenario_ExpectedBehaviour
    // Arrange
    // Act
    // Assert
    


    [TestFixture]
    public class TourAttributesTests
    {
        public class TourPopularityTests
        {

            [Test]

            public void TourPopularity_NoLogs_ReturnsZero()
            {
                // Arrange

                var tour = new Tour();

                // Act

                var result = tour.TourPopularity();

                Assert.AreEqual(result, 0);
            }

            [Test]
            public void TourPopularity_OneLog_ReturnsOne()
            {
                // Arrange

                var tour = new Tour();

                // Act
                tour.TourLogs.Add(new TourLog());

                var result = tour.TourPopularity();

                Assert.AreEqual(result, 1);
            }

            [Test]

            public void TourPopularity_TenLogs_ReturnsTen()
            {
                // Arrange

                var tour = new Tour();

                // Act

                for(int i = 0; i < 10; i++)
                    tour.TourLogs.Add(new TourLog());

                var result = tour.TourPopularity();

                Assert.AreEqual(result, 10);
            }
        }

        

        public class TourChildFriendlinessTests
        {

            [Test]
            public void ChildFriendliness_NoLogs_ReturnsFalse()
            {
                // Arrange
                var tour = new Tour();

                // Act
                var result = tour.ChildFriendly();

                // Assert
                Assert.AreEqual(result, false);
            }

            [Test]

            public void ChildFriendliness_DifficultyTooHigh_ReturnsFalse()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 7000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(0.5);
                log1.Difficulty = 4;

                tour.TourLogs.Add(log1);

                // Act

                var result = tour.ChildFriendly();

                // Assert

                Assert.AreEqual(result, false);
            }

            [Test]

            public void ChildFriendliness_DistanceTooHigh_ReturnsFalse()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 11000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(0.5);
                log1.Difficulty = 2;

                tour.TourLogs.Add(log1);

                // Act

                var result = tour.ChildFriendly();

                // Assert

                Assert.AreEqual(result, false);
            }

            [Test]

            public void ChildFriendliness_DurationTooLong_ReturnsFalse()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 7000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(2);
                log1.Difficulty = 2;

                tour.TourLogs.Add(log1);

                // Act

                var result = tour.ChildFriendly();

                // Assert

                Assert.AreEqual(result, false);
            }

            [Test]

            public void ChildFriendliness_GoodAttributes_ReturnsTrue()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 7000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(0.5);
                log1.Difficulty = 2;

                tour.TourLogs.Add(log1);

                // Act

                var result = tour.ChildFriendly();

                // Assert

                Assert.AreEqual(result, true);
            }

            [Test]

            public void ChildFriendliness_NoDifficultyLogOtherwiseChildfriendly_ReturnsTrue()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 7000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(0.5);

                tour.TourLogs.Add(log1);

                // Act

                var result = tour.ChildFriendly();

                // Assert

                Assert.AreEqual(result, true);
            }

            [Test]

            public void ChildFriendliness_MultipleLogsAverageDurationTooHigh_ReturnsFalse()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 7000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(0.9);
                log1.Difficulty = 2;

                tour.TourLogs.Add(log1);

                var log2 = new TourLog();

                log2.Duration = TimeSpan.FromHours(1);
                log2.Difficulty = 1;

                tour.TourLogs.Add(log2);

                var log3 = new TourLog();

                log3.Duration = TimeSpan.FromHours(1.5);
                log3.Difficulty = 3;

                tour.TourLogs.Add(log3);

                // Act

                var result = tour.ChildFriendly();

                // Assert

                Assert.AreEqual(result, false);
            }

            [Test]

            public void ChildFriendliness_MultipleLogsAverageDifficultyTooHigh_ReturnsFalse()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 7000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(0.9);
                log1.Difficulty = 3;

                tour.TourLogs.Add(log1);

                var log2 = new TourLog();

                log2.Duration = TimeSpan.FromHours(1);
                log2.Difficulty = 5;

                tour.TourLogs.Add(log2);

                var log3 = new TourLog();

                log3.Duration = TimeSpan.FromHours(0.5);
                log3.Difficulty = 4;

                tour.TourLogs.Add(log3);

                // Act

                var result = tour.ChildFriendly(); 

                // Assert

                Assert.AreEqual(result, false);
            }

            [Test]

            public void ChildFriendliness_MultipleLogsAttributesFine_ReturnsTrue()
            {
                // Arrange
                var tour = new Tour();
                tour.TourInfo.Distance = 7000;

                var log1 = new TourLog();

                log1.Duration = TimeSpan.FromHours(0.9);
                log1.Difficulty = 2;

                tour.TourLogs.Add(log1);

                var log2 = new TourLog();

                log2.Duration = TimeSpan.FromHours(1);
                log2.Difficulty = 2;

                tour.TourLogs.Add(log2);

                var log3 = new TourLog();

                log3.Duration = TimeSpan.FromHours(0.5);
                log3.Difficulty = 4;

                tour.TourLogs.Add(log3);

                // Act

                var result = tour.ChildFriendly();

                // Assert

                Assert.AreEqual(result, true);
            }
        }



    }

    [TestFixture]

    public class StaticMapApiTests
    {
        [Test]

        public void IsJpeg_OneCheckSumCorrect_ReturnsFalse()
        {
            //Arrange

            var dp = new DirectionsProcessor();
            byte[] NotJpeg = { 0xFF, 0xFF, 0xCD, 0x00 };

            //Act

            bool result = dp.IsJpeg(NotJpeg);

            //Assert

            Assert.IsFalse(result);
        }

        [Test]

        public void IsJpeg_CorrectChecksumShortJpeg_ReturnsTrue()
        {
            var dp = new DirectionsProcessor();
            byte[] IsJpeg = { 0xFF, 0xD8, 0x00, 0x01, 0x02, 0xFF, 0xD9 };

            bool result = dp.IsJpeg(IsJpeg);

            Assert.IsTrue(result);
        }

        [Test]

        public void IsJpeg_OneCheckSumWrong_ReturnsFalse()
        {
            var dp = new DirectionsProcessor();
            byte[] IsJpeg = { 0xFF, 0xD8, 0x00, 0x01, 0x02, 0xFF, 0xD8 };

            bool result = dp.IsJpeg(IsJpeg);

            Assert.False(result);
        }

        [Test]

        public void IsJpeg_TwoCheckSumsWrong_ReturnsTrue()
        {
            var dp = new DirectionsProcessor();
            byte[] IsJpeg = { 0xFF, 0xD9, 0x00, 0x01, 0x02, 0xFF, 0xD8 };

            bool result = dp.IsJpeg(IsJpeg);

            Assert.IsFalse(result);
        }

        [Test]

        public void IsJpeg_CorrectChecksumLongJpeg_ReturnsTrue()
        {
            var dp = new DirectionsProcessor();
            byte[] IsJpeg = { 0xFF, 0xD9, 0x00, 0x01, 0x02, 0x00, 0x01, 0x02, 0x00, 0x01,
                0x02, 0x00, 0x01, 0x02, 0x00, 0x01, 0x02, 0x02, 0x02, 0x00, 0x01, 0x02, 0x00,
                0x00, 0x01, 0xFF, 0xD8 };

            bool result = dp.IsJpeg(IsJpeg);

            Assert.IsFalse(result);
        }

        [Test]

        public void IsJpeg_NullArray_ReturnsFalse()
        {
            var dp = new DirectionsProcessor();
            byte[] NotJpeg = null;

            bool result = dp.IsJpeg(NotJpeg);

            Assert.IsFalse(result);
        }
    }


    [TestFixture]

    public class ConstantsTests
    {
        [Test]

        public void MapQuestDirectionsSuffix_ValidParameters_ReturnsCorrectString()
        {
            var generatedSuffix = Constants.MapQuestDirectionsSuffix("Wien", "Paris", "fastest");
            var trueSuffix = $"/directions/v2/route?key={ConfigurationManager.AppSettings["ApiKey"]}&from=Wien&to=Paris&routeType=fastest";

            Assert.AreEqual(generatedSuffix, trueSuffix);

        }


        [Test]

        public void MapQuestMapSuffix_ValidParameters_ReturnsCorrectString()
        {
            var generatedSuffix = Constants.MapQuestMapSuffix("Wien", "Paris");
            var trueSuffix = $"/staticmap/v5/map?start=Wien&end=Paris&size={ConfigurationManager.AppSettings["ImageWidth"]}," +
                $"{ConfigurationManager.AppSettings["ImageHeight"]}@2x&key={ConfigurationManager.AppSettings["ApiKey"]}";

            Assert.AreEqual(generatedSuffix, trueSuffix);
        }
    }
}
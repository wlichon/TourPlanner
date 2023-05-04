using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner.Models;

namespace TourPlanner.UnitTests
{
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
}
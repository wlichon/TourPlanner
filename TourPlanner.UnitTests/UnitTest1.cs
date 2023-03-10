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
    public class TourTests
    {
        [SetUp]
        public void TestInitialize()
        {
            ObservableCollection<TourLog> logs1 = new ObservableCollection<TourLog>();
            ObservableCollection<TourLog> logs2 = new ObservableCollection<TourLog>();

            for (int i = 0; i < 5; i++)
            {
                logs1.Add(new TourLog
                {
                    TourLogId = 1,
                    Date = DateTime.Now,
                    Distance = 2500,
                    Duration = TimeSpan.FromSeconds(600)
                });

            }

            for (int i = 0; i < 5; i++)
            {
                logs2.Add(new TourLog
                {
                    TourLogId = 0,
                    Date = DateTime.Now,
                    Distance = 1500,
                    Duration = TimeSpan.FromSeconds(900)
                });

            }

            var Tours = new List<Tour>()
            {
                new Tour(){TourId = 0, TourInfo = new TourInfo(null, "Wien", "Berlin", 400, "fun little tour", "Car", 6), TourLogs = logs1, TourName = "WienToBerlin"},
                new Tour(){TourId = 1, TourInfo = new TourInfo(null, "Denver", "Chicago", 600, "example tour", "Bicycle", 50), TourLogs = logs2, TourName = "LongBicycleTour"}
            };


            

        }

        [Test]
        public void Equality_SameTours_ReturnsTrue()
        {
            var tour1 = new Tour { TourName = "Andreaspark", TourId = 0, TourInfo = new TourInfo { Distance = 10 }, TourLogs = new ObservableCollection<TourLog>{ new TourLog { Distance = 5 } }};
            var tour2 = new Tour { TourName = "Andreaspark", TourId = 0, TourInfo = new TourInfo { Distance = 10 }, TourLogs = new ObservableCollection<TourLog>{ new TourLog { Distance = 5 } }};


            Assert.True(tour1 == tour2);
        }

        [Test]
        public void Equality_DifferentTourLogs_ReturnsFalse()
        {
            var tour1 = new Tour { TourName = "Andreaspark", TourId = 0, TourInfo = new TourInfo { Distance = 10 }, TourLogs = new ObservableCollection<TourLog> { new TourLog { Distance = 5, Duration = TimeSpan.FromSeconds(600) }, new TourLog { Distance = 5, Duration = TimeSpan.FromSeconds(600) } } };
            var tour2 = new Tour { TourName = "Andreaspark", TourId = 0, TourInfo = new TourInfo { Distance = 10 }, TourLogs = new ObservableCollection<TourLog> { new TourLog { Distance = 5, Duration = TimeSpan.FromSeconds(600) }, new TourLog { Distance = 5, Duration = TimeSpan.FromSeconds(599) } } };

            

            Assert.True(tour1 != tour2);
        }

        [Test]
        public void Equality_SameTourReference_ReturnsTrue()
        {
            var tour1 = new Tour { TourName = "Andreaspark", TourId = 0, TourInfo = new TourInfo { Distance = 10 }, TourLogs = new ObservableCollection<TourLog> { new TourLog { Distance = 5, Duration = TimeSpan.FromSeconds(600) }, new TourLog { Distance = 5, Duration = TimeSpan.FromSeconds(600) } } };
            var tour2 = tour1;


            Assert.True(tour1 == tour2);
        }


    }
}
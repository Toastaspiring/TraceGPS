using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraceGPS;

namespace UnitTestTraceGPS
{
    /// <summary>
    ///Classe de test pour PointTest, destinée à contenir tous
    ///les tests unitaires PointTest
    ///</summary>
    [TestClass]
    public class UnitTestPoint
    {
        private Point point1;
        private Point point2;

        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            point1 = new Point();
            point2 = new Point(48.5, -1.6, 100.5);
        }

        /// <summary>
        ///Test pour getAltitude
        ///</summary>
        [TestMethod()]
        public void getAltitudeTest()
        {
            Assert.AreEqual(0, point1.getAltitude(), 0.001);
            Assert.AreEqual(100.5, point2.getAltitude(), 0.001);
        }

        /// <summary>
        ///Test pour getDistance
        ///</summary>
        [TestMethod()]
        public void getDistanceTest()
        {
            double dist = point1.getDistance(point2);
            Assert.AreEqual(5395, dist, 5);
        }

        /// <summary>
        ///Test pour getDistance
        ///</summary>
        [TestMethod()]
        public void getDistanceTest1()
        {
            double dist = Point.getDistance(point1, point2);
            Assert.AreEqual(5395, dist, 5);
        }

        /// <summary>
        ///Test pour getLatitude
        ///</summary>
        [TestMethod()]
        public void getLatitudeTest()
        {
            Assert.AreEqual(0, point1.getLatitude(), 0.001);
            Assert.AreEqual(48.5, point2.getLatitude(), 0.001);
        }

        /// <summary>
        ///Test pour getLongitude
        ///</summary>
        [TestMethod()]
        public void getLongitudeTest()
        {
            Assert.AreEqual(0, point1.getLongitude(), 0.001);
            Assert.AreEqual(-1.6, point2.getLongitude(), 0.001);
        }

        /// <summary>
        ///Test pour setAltitude
        ///</summary>
        [TestMethod()]
        public void setAltitudeTest()
        {
            point1.setAltitude(200.5);
            Assert.AreEqual(200.5, point1.getAltitude(), 0.001);
        }

        /// <summary>
        ///Test pour setLatitude
        ///</summary>
        [TestMethod()]
        public void setLatitudeTest()
        {
            point1.setLatitude(10.5);
            Assert.AreEqual(10.5, point1.getLatitude(), 0.001);
        }

        /// <summary>
        ///Test pour setLongitude
        ///</summary>
        [TestMethod()]
        public void setLongitudeTest()
        {
            point1.setLongitude(12.5);
            Assert.AreEqual(12.5, point1.getLongitude(), 0.001);
        }

        /// <summary>
        ///Test pour toString
        ///</summary>
        [TestMethod()]
        public void toStringTest()
        {
            String msg;
            msg = "Latitude :\t" + "000,000" + "\n";
            msg += "Longitude :\t" + "000,000" + "\n";
            msg += "Altitude :\t" + "000,000" + "\n";
            Assert.AreEqual(msg, point1.toString());

            msg = "Latitude :\t" + "048,500" + "\n";
            msg += "Longitude :\t" + "-001,600" + "\n";
            msg += "Altitude :\t" + "100,500" + "\n";
            Assert.AreEqual(msg, point2.toString());
        }
    }
}

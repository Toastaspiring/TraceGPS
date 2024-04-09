using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraceGPS;

namespace UnitTestTraceGPS
{
    /// <summary>
    ///Classe de test pour PointDeTraceTest, destinée à contenir tous
    ///les tests unitaires PointDeTraceTest
    ///</summary>
    [TestClass]
    public class UnitTestPointDeTrace
    {
        private PointDeTrace point1, point2, point3, point4, point5;

        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // on utilise ici les 5 constructeurs
            point1 = new PointDeTrace();
            DateTime uneDate = Convert.ToDateTime("21/06/2016 14:30:20");
            point2 = new PointDeTrace(48.5, -1.6, 100.5, uneDate);
            point3 = new PointDeTrace(48.5, -1.6, 100.5, uneDate, 140);
            point4 = new PointDeTrace(48.5, -1.6, 100.5, uneDate, 140, 3600, 21.5, 23.5);
            point5 = new PointDeTrace(point4);
        }

        /// <summary>
        ///Test pour getDateHeure
        ///</summary>
        [TestMethod()]
        public void getDateHeureTest()
        {
            Assert.AreEqual("01/01/0001 00:00:00", point1.getDateHeure().ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.AreEqual("21/06/2016 14:30:20", point2.getDateHeure().ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.AreEqual("21/06/2016 14:30:20", point3.getDateHeure().ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.AreEqual("21/06/2016 14:30:20", point4.getDateHeure().ToString("dd/MM/yyyy HH:mm:ss"));
            Assert.AreEqual("21/06/2016 14:30:20", point5.getDateHeure().ToString("dd/MM/yyyy HH:mm:ss"));
        }

        /// <summary>
        ///Test pour getDistanceCumulee
        ///</summary>
        [TestMethod()]
        public void getDistanceCumuleeTest()
        {
            Assert.AreEqual(0, point1.getDistanceCumulee());
            Assert.AreEqual(0, point2.getDistanceCumulee());
            Assert.AreEqual(0, point3.getDistanceCumulee());
            Assert.AreEqual(21.5, point4.getDistanceCumulee());
            Assert.AreEqual(21.5, point5.getDistanceCumulee());
        }

        /// <summary>
        ///Test pour getRythmeCardio
        ///</summary>
        [TestMethod()]
        public void getRythmeCardioTest()
        {
            Assert.AreEqual(0, point1.getRythmeCardio());
            Assert.AreEqual(0, point2.getRythmeCardio());
            Assert.AreEqual(140, point3.getRythmeCardio());
            Assert.AreEqual(140, point4.getRythmeCardio());
            Assert.AreEqual(140, point5.getRythmeCardio());
        }

        /// <summary>
        ///Test pour getTempsCumule
        ///</summary>
        [TestMethod()]
        public void getTempsCumuleTest()
        {
            Assert.AreEqual(0, point1.getTempsCumule());
            Assert.AreEqual(0, point2.getTempsCumule());
            Assert.AreEqual(0, point3.getTempsCumule());
            Assert.AreEqual(3600, point4.getTempsCumule());
            Assert.AreEqual(3600, point5.getTempsCumule());
        }

        /// <summary>
        ///Test pour getTempsCumuleEnChaine
        ///</summary>
        [TestMethod()]
        public void getTempsCumuleEnChaineTest()
        {
            Assert.AreEqual("00:00:00", point1.getTempsCumuleEnChaine());
            Assert.AreEqual("00:00:00", point2.getTempsCumuleEnChaine());
            Assert.AreEqual("00:00:00", point3.getTempsCumuleEnChaine());
            Assert.AreEqual("01:00:00", point4.getTempsCumuleEnChaine());
            Assert.AreEqual("01:00:00", point5.getTempsCumuleEnChaine());
        }

        /// <summary>
        ///Test pour getVitesse
        ///</summary>
        [TestMethod()]
        public void getVitesseTest()
        {
            Assert.AreEqual(0, point1.getVitesse());
            Assert.AreEqual(0, point2.getVitesse());
            Assert.AreEqual(0, point3.getVitesse());
            Assert.AreEqual(23.5, point4.getVitesse());
            Assert.AreEqual(23.5, point5.getVitesse());
        }

        /// <summary>
        ///Test pour setDateHeure
        ///</summary>
        [TestMethod()]
        public void setDateHeureTest()
        {
            DateTime uneDate = Convert.ToDateTime("22/07/2017 15:31:21");
            point1.setDateHeure(uneDate);
            Assert.AreEqual("22/07/2017 15:31:21", point1.getDateHeure().ToString("dd/MM/yyyy HH:mm:ss"));
        }

        /// <summary>
        ///Test pour setDistanceCumulee
        ///</summary>
        [TestMethod()]
        public void setDistanceCumuleeTest()
        {
            point1.setDistanceCumulee(12.75);
            Assert.AreEqual(12.75, point1.getDistanceCumulee(), 0.001);
        }

        /// <summary>
        ///Test pour setRythmeCardio
        ///</summary>
        [TestMethod()]
        public void setRythmeCardioTest()
        {
            point1.setRythmeCardio(120);
            Assert.AreEqual(120, point1.getRythmeCardio());
        }

        /// <summary>
        ///Test pour setTempsCumule
        ///</summary>
        [TestMethod()]
        public void setTempsCumuleTest()
        {
            point1.setTempsCumule(4000);
            Assert.AreEqual(4000, point1.getTempsCumule());
        }

        /// <summary>
        ///Test pour setVitesse
        ///</summary>
        [TestMethod()]
        public void setVitesseTest()
        {
            point1.setVitesse(22.75);
            Assert.AreEqual(22.75, point1.getVitesse(), 0.001);
        }

        /// <summary>
        ///Test pour toString
        ///</summary>
        [TestMethod()]
        public void toStringTest()
        {
            String msg;
            msg = "Id trace :\t" + "0" + "\n";
            msg += "Id point :\t" + "0" + "\n";	
            msg += "Latitude :\t" + "000,000" + "\n";
            msg += "Longitude :\t" + "000,000" + "\n";
            msg += "Altitude :\t" + "000,000" + "\n";
            msg += "Heure de passage :\t" + "01/01/0001 00:00:00" + "\n";
            msg += "Rythme cardiaque :\t" + "0" + "\n";
            msg += "Temps cumule (s) :\t" + "0" + "\n";
            msg += "Temps cumule (hh:mm:ss) :\t" + "00:00:00" + "\n";
            msg += "Distance cumulée (Km) :\t" + "000,000" + "\n";
            msg += "Vitesse (Km/h) :\t" + "000,000" + "\n";
            Assert.AreEqual(msg, point1.toString());

            msg = "Id trace :\t" + "0" + "\n";
            msg += "Id point :\t" + "0" + "\n";	
            msg += "Latitude :\t" + "048,500" + "\n";
            msg += "Longitude :\t" + "-001,600" + "\n";
            msg += "Altitude :\t" + "100,500" + "\n";
            msg += "Heure de passage :\t" + "21/06/2016 14:30:20" + "\n";
            msg += "Rythme cardiaque :\t" + "140" + "\n";
            msg += "Temps cumule (s) :\t" + "3600" + "\n";
            msg += "Temps cumule (hh:mm:ss) :\t" + "01:00:00" + "\n";
            msg += "Distance cumulée (Km) :\t" + "021,500" + "\n";
            msg += "Vitesse (Km/h) :\t" + "023,500" + "\n";
            Assert.AreEqual(msg, point5.toString());
        }
    }
}

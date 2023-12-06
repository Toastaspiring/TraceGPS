using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraceGPS;
using System.Collections;

namespace UnitTestTraceGPS
{
    /// <summary>
    ///Classe de test pour TraceTest, destinée à contenir tous
    ///les tests unitaires TraceTest
    ///</summary>
    [TestClass]
    public class UnitTestTrace
    {
        private static Trace trace1;		// cette trace restera vide
        private static Trace trace2;		// cette trace va comporter des points

        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            trace1 = new Trace();		// cette trace restera vide
            trace2 = new Trace();		// cette trace va comporter des points

            DateTime uneDate1 = Convert.ToDateTime("21/06/2016 14:00:00");
            PointDeTrace point1 = new PointDeTrace(48.500, -1.600, 50, uneDate1);
            trace2.ajouterPoint(point1);

            DateTime uneDate2 = Convert.ToDateTime("21/06/2016 14:03:40");
            PointDeTrace point2 = new PointDeTrace(48.510, -1.600, 200, uneDate2);
            trace2.ajouterPoint(point2);

            DateTime uneDate3 = Convert.ToDateTime("21/06/2016 14:05:20");
            PointDeTrace point3 = new PointDeTrace(48.510, -1.610, 150, uneDate3);
            trace2.ajouterPoint(point3);

            DateTime uneDate4 = Convert.ToDateTime("21/06/2016 14:08:40");
            PointDeTrace point4 = new PointDeTrace(48.500, -1.610, 200, uneDate4);
            trace2.ajouterPoint(point4);

            DateTime uneDate5 = Convert.ToDateTime("21/06/2016 14:10:00");
            PointDeTrace point5 = new PointDeTrace(48.500, -1.600, 50, uneDate5);
            trace2.ajouterPoint(point5);
        }

        /// <summary>
        ///Test pour getCentre
        ///</summary>
        [TestMethod()]
        public void getCentreTest()
        {
            Assert.AreEqual(null, trace1.getCentre());

            Assert.AreEqual(48.505, trace2.getCentre().getLatitude(), 0.001);
            Assert.AreEqual(-1.605, trace2.getCentre().getLongitude(), 0.001);
            Assert.AreEqual(0, trace2.getCentre().getAltitude(), 0);
        }

        /// <summary>
        ///Test pour getDateHeureDebut
        ///</summary>
        [TestMethod()]
        public void getDateHeureDebutTest()
        {
            String debut1 = trace1.getDateHeureDebut().ToString("dd/MM/yyyy HH:mm:ss");
            Assert.AreEqual("01/01/0001 00:00:00", debut1);

            String debut2 = trace2.getDateHeureDebut().ToString("dd/MM/yyyy HH:mm:ss");
            Assert.AreEqual("21/06/2016 14:00:00", debut2);
        }

        /// <summary>
        ///Test pour getDateHeureFin
        ///</summary>
        [TestMethod()]
        public void getDateHeureFinTest()
        {
            String fin1 = trace1.getDateHeureFin().ToString("dd/MM/yyyy HH:mm:ss");
            Assert.AreEqual("01/01/0001 00:00:00", fin1);

            String fin2 = trace2.getDateHeureFin().ToString("dd/MM/yyyy HH:mm:ss");
            Assert.AreEqual("21/06/2016 14:10:00", fin2);
        }

        /// <summary>
        ///Test pour getDeniveleNegatif
        ///</summary>
        [TestMethod()]
        public void getDeniveleNegatifTest()
        {
            double denivele1 = trace1.getDeniveleNegatif();
            Assert.AreEqual(0, denivele1, 0);

            double denivele2 = trace2.getDeniveleNegatif();
            Assert.AreEqual(200, denivele2, 0.1);
        }

        /// <summary>
        ///Test pour getDenivelePositif
        ///</summary>
        [TestMethod()]
        public void getDenivelePositifTest()
        {
            double denivele1 = trace1.getDenivelePositif();
            Assert.AreEqual(0, denivele1, 0);

            double denivele2 = trace2.getDenivelePositif();
            Assert.AreEqual(200, denivele2, 0.1);
        }

        /// <summary>
        ///Test pour getDenivelle
        ///</summary>
        [TestMethod()]
        public void getDenivelleTest()
        {
            double denivele1 = trace1.getDenivele();
            Assert.AreEqual(0, denivele1, 0);

            double denivele2 = trace2.getDenivele();
            Assert.AreEqual(150, denivele2, 0.1);
        }

        /// <summary>
        ///Test pour getDistanceTotale
        ///</summary>
        [TestMethod()]
        public void getDistanceTotaleTest()
        {
            double dist1 = trace1.getDistanceTotale();
            Assert.AreEqual(0, dist1, 0);

            double dist2 = trace2.getDistanceTotale();
            Assert.AreEqual(3.698, dist2, 0.004);
        }

        /// <summary>
        ///Test pour getDureeEnSecondes
        ///</summary>
        [TestMethod()]
        public void getDureeEnSecondesTest()
        {
            long duree1 = trace1.getDureeEnSecondes();
            Assert.AreEqual(0, duree1);

            long duree2 = trace2.getDureeEnSecondes();
            Assert.AreEqual(600, duree2);
        }

        /// <summary>
        ///Test pour getDureeTotale
        ///</summary>
        [TestMethod()]
        public void getDureeTotaleTest()
        {
            String duree1 = trace1.getDureeTotale();
            Assert.AreEqual("00:00:00", duree1);

            String duree2 = trace2.getDureeTotale();
            Assert.AreEqual("00:10:00", duree2);
        }

        /// <summary>
        ///Test pour getLesPointsDeTrace
        ///</summary>
        [TestMethod()]
        public void getLesPointsDeTraceTest()
        {
            ArrayList lesPoints1 = trace1.getLesPointsDeTrace();
            Assert.AreEqual(0, lesPoints1.Count);

            ArrayList lesPoints2 = trace2.getLesPointsDeTrace();
            Assert.AreEqual(5, lesPoints2.Count);
            PointDeTrace premierPoint = (PointDeTrace)lesPoints2[0];
            Assert.AreEqual(48.5, premierPoint.getLatitude(), 0.001);
            PointDeTrace dernierPoint = (PointDeTrace)lesPoints2[4];
            Assert.AreEqual(48.5, dernierPoint.getLatitude(), 0.001);
        }

        /// <summary>
        ///Test pour getNombrePoints
        ///</summary>
        [TestMethod()]
        public void getNombrePointsTest()
        {
            int nb1 = trace1.getNombrePoints();
            Assert.AreEqual(0, nb1);

            int nb2 = trace2.getNombrePoints();
            Assert.AreEqual(5, nb2);
        }

        /// <summary>
        ///Test pour getVitesseMoyenne
        ///</summary>
        [TestMethod()]
        public void getVitesseMoyenneTest()
        {
            double vitesse1 = trace1.getVitesseMoyenne();
            Assert.AreEqual(0, vitesse1, 0);

            double vitesse2 = trace2.getVitesseMoyenne();
            Assert.AreEqual(22.188, vitesse2, 0.02);
        }

        /// <summary>
        ///Test pour toString
        ///</summary>
        [TestMethod()]
        public void toStringTest()
        {
            String msg1 = "";
            msg1 += "Nombre de points :\t\t" + "00000" + "\n";
            Assert.AreEqual(msg1, trace1.toString());

            String msg2 = "";
            msg2 += "Nombre de points :\t\t" + "00005" + "\n";
            msg2 += "Heure de début :\t\t" + "21/06/2016 14:00:00" + "\n";
            msg2 += "Heure de fin :\t\t" + "21/06/2016 14:10:00" + "\n";
            msg2 += "Durée totale :\t\t" + "00:10:00" + "\n";
            msg2 += "Distance totale en Km :\t" + "003,69" + "\n";
            msg2 += "Dénivelé en m :\t\t" + "0150,00" + "\n";
            msg2 += "Dénivelé positif en m :\t" + "0200,00" + "\n";
            msg2 += "Dénivelé négatif en m :\t" + "0200,00" + "\n";
            msg2 += "Vitesse moyenne en Km/h :\t" + "22,17" + "\n";
            msg2 += "Centre du parcours :\n";
            msg2 += "   - Latitude :\t\t" + "048,505" + "\n";
            msg2 += "   - Longitude :\t\t" + "-001,605" + "\n";
            msg2 += "   - Altitude :\t\t" + "000,000" + "\n";
            Assert.AreEqual(msg2, trace2.toString());
        }

        /// <summary>
        ///Test pour ajouterPoint
        ///</summary>
        [TestMethod()]
        public void ajouterPointTest()
        {
            PointDeTrace point1 = (PointDeTrace)trace2.getLesPointsDeTrace()[0];
            Assert.AreEqual(0, point1.getTempsCumule());
            Assert.AreEqual(0, point1.getDistanceCumulee());
            Assert.AreEqual(0, point1.getVitesse());

            PointDeTrace point2 = (PointDeTrace)trace2.getLesPointsDeTrace()[1];
            Assert.AreEqual(220, point2.getTempsCumule());
            Assert.AreEqual(1.112, point2.getDistanceCumulee(), 0.002);
            Assert.AreEqual(18.196, point2.getVitesse(), 0.04);

            PointDeTrace point3 = (PointDeTrace)trace2.getLesPointsDeTrace()[2];
            Assert.AreEqual(320, point3.getTempsCumule());
            Assert.AreEqual(1.849, point3.getDistanceCumulee(), 0.003);
            Assert.AreEqual(26.532, point3.getVitesse(), 0.04);

            PointDeTrace point4 = (PointDeTrace)trace2.getLesPointsDeTrace()[3];
            Assert.AreEqual(520, point4.getTempsCumule());
            Assert.AreEqual(2.961, point4.getDistanceCumulee(), 0.004);
            Assert.AreEqual(20.016, point4.getVitesse(), 0.04);

            PointDeTrace point5 = (PointDeTrace)trace2.getLesPointsDeTrace()[4];
            Assert.AreEqual(600, point5.getTempsCumule());
            Assert.AreEqual(3.698, point5.getDistanceCumulee(), 0.004);
            Assert.AreEqual(33.165, point5.getVitesse(), 0.04);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraceGPS;

namespace UnitTestTraceGPS
{
    [TestClass]
    public class UnitTestUtilisateur
    {
        private Utilisateur utilisateur1;
        private Utilisateur utilisateur2;

        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //Utilisateur 1 est vide
            utilisateur1 = new Utilisateur();

            //Utilisateur 2 est remplie
            int unId = 111;
            String unPseudo = "toto";
            String unMdpSha1 = "abcdef";
            String uneAdrMail = "toto@free.fr";
            String unNumTel = "1122334455";
            int unNiveau = 1;
            DateTime uneDateCreation = Convert.ToDateTime("21/06/2016 14:00:00");
            int unNbTraces = 3;
            DateTime uneDateDerniereTrace = Convert.ToDateTime("28/06/2016 14:00:00");
            utilisateur2 = new Utilisateur(unId, unPseudo, unMdpSha1, uneAdrMail, unNumTel, unNiveau, uneDateCreation, unNbTraces, uneDateDerniereTrace);
        }

        /// <summary>
        ///Test pour getId
        ///</summary>
        [TestMethod()]
        public void getIdTest()
        {
            Assert.AreEqual(0, utilisateur1.getId());
            Assert.AreEqual(111, utilisateur2.getId());
        }

        /// <summary>
        ///Test pour setId
        ///</summary>
        [TestMethod()]
        public void setIdTest()
        {
            utilisateur1.setId(2);
            Assert.AreEqual(2, utilisateur1.getId());
        }

        /// <summary>
        ///Test pour getPseudo
        ///</summary>
        [TestMethod()]
        public void getPseudoTest()
        {
            Assert.AreEqual("", utilisateur1.getPseudo());
            Assert.AreEqual("toto", utilisateur2.getPseudo());
        }

        /// <summary>
        ///Test pour setPseudo
        ///</summary>
        [TestMethod()]
        public void setPseudoTest()
        {
            utilisateur1.setPseudo("wow");
            Assert.AreEqual("wow", utilisateur1.getPseudo());
        }

        /// <summary>
        ///Test pour getMdpSha1
        ///</summary>
        [TestMethod()]
        public void getMdpSha1Test()
        {
            Assert.AreEqual("", utilisateur1.getMdpSha1());
            Assert.AreEqual("abcdef", utilisateur2.getMdpSha1());
        }

        /// <summary>
        ///Test pour setMdpSha1
        ///</summary>
        [TestMethod()]
        public void setMdpSha1Test()
        {
            utilisateur1.setMdpSha1("abcdef");
            Assert.AreEqual("abcdef", utilisateur1.getMdpSha1());
        }

        /// <summary>
        ///Test pour getAdrMail
        ///</summary>
        [TestMethod()]
        public void getAdrMailTest()
        {
            Assert.AreEqual("", utilisateur1.getAdrMail());
            Assert.AreEqual("toto@free.fr", utilisateur2.getAdrMail());
        }

        /// <summary>
        ///Test pour setAdrMail
        ///</summary>
        [TestMethod()]
        public void setAdrMailTest()
        {
            utilisateur1.setAdrMail("toto@free.fr");
            Assert.AreEqual("toto@free.fr", utilisateur1.getAdrMail());
        }

        /// <summary>
        ///Test pour getNumTel
        ///</summary>
        [TestMethod()]
        public void getNumTelTest()
        {
            Assert.AreEqual("", utilisateur1.getNumTel());
            Assert.AreEqual("11.22.33.44.55", utilisateur2.getNumTel());
        }

        /// <summary>
        ///Test pour setNumTel
        ///</summary>
        [TestMethod()]
        public void setNumTelTest()
        {
            utilisateur1.setNumTel("1122334455");
            Assert.AreEqual("11.22.33.44.55", utilisateur1.getNumTel());
        }

        /// <summary>
        ///Test pour getNiveau
        ///</summary>
        [TestMethod()]
        public void getNiveauTest()
        {
            Assert.AreEqual(0, utilisateur1.getNiveau());
            Assert.AreEqual(1, utilisateur2.getNiveau());
        }

        /// <summary>
        ///Test pour setNiveau
        ///</summary>
        [TestMethod()]
        public void setNiveauTest()
        {
            utilisateur1.setNiveau(1);
            Assert.AreEqual(1, utilisateur1.getNiveau());
        }

        /// <summary>
        ///Test pour getDateCreation
        ///</summary>
        [TestMethod()]
        public void getDateCreationTest()
        {
            Assert.AreEqual(DateTime.MinValue, utilisateur1.getDateCreation());
            Assert.AreEqual(Convert.ToDateTime("21/06/2016 14:00:00"), utilisateur2.getDateCreation());
        }

        /// <summary>
        ///Test pour setDateCreation
        ///</summary>
        [TestMethod()]
        public void setDateCreationTest()
        {
            utilisateur1.setDateCreation(Convert.ToDateTime("21/06/2016 14:00:00"));
            Assert.AreEqual(Convert.ToDateTime("21/06/2016 14:00:00"), utilisateur1.getDateCreation());
        }

        /// <summary>
        ///Test pour getNbTraces
        ///</summary>
        [TestMethod()]
        public void getNbTracesTest()
        {
            Assert.AreEqual(0, utilisateur1.getNbTraces());
            Assert.AreEqual(3, utilisateur2.getNbTraces());
        }

        /// <summary>
        ///Test pour setNbTraces
        ///</summary>
        [TestMethod()]
        public void setNbTracesTest()
        {
            utilisateur1.setNbTraces(3);
            Assert.AreEqual(3, utilisateur1.getNbTraces());
        }

        /// <summary>
        ///Test pour getDateDerniereTrace
        ///</summary>
        [TestMethod()]
        public void getDateDerniereTraceTest()
        {
            Assert.AreEqual(DateTime.MinValue, utilisateur1.getDateDerniereTrace());
            Assert.AreEqual(Convert.ToDateTime("28/06/2016 14:00:00"), utilisateur2.getDateDerniereTrace());
        }

        /// <summary>
        ///Test pour setDateDerniereTrace
        ///</summary>
        [TestMethod()]
        public void setDateDerniereTraceTest()
        {
            utilisateur1.setDateDerniereTrace(Convert.ToDateTime("28/06/2016 14:00:00"));
            Assert.AreEqual(Convert.ToDateTime("28/06/2016 14:00:00"), utilisateur1.getDateDerniereTrace());
        }

        /// <summary>
        ///Test pour toString
        ///</summary>
        [TestMethod()]
        public void toStringTest()
        {
            String msg = "";
            msg += "id : " + "0" + "\n";
            msg += "pseudo : " + "" + "\n";
            msg += "mdpSha1 : " + "" + "\n";
            msg += "adrMail : " + "" + "\n";
            msg += "numTel : " + "" + "\n";
            msg += "niveau : " + "0" + "\n";
            msg += "dateCreation : " + "01/01/0001 00:00:00" + "\n";
            msg += "nbTraces : " + "0" + "\n";
            Assert.AreEqual(msg, utilisateur1.toString());

            msg = "";
            msg += "id : " + "111" + "\n";
            msg += "pseudo : " + "toto" + "\n";
            msg += "mdpSha1 : " + "abcdef" + "\n";
            msg += "adrMail : " + "toto@free.fr" + "\n";
            msg += "numTel : " + "11.22.33.44.55" + "\n";
            msg += "niveau : " + "1" + "\n";
            msg += "dateCreation : " + "21/06/2016 14:00:00" + "\n";
            msg += "nbTraces : " + "3" + "\n";
            msg += "dateDerniereTrace : " + "28/06/2016 14:00:00" + "\n";
            Assert.AreEqual(msg, utilisateur2.toString());
        }

    }
}

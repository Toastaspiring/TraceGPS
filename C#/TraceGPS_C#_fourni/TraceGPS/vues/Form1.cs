// TP C# réalisé sous Visual Studio 2013
// Thème : affichage de trace GPS - Formulaire principal
// Auteur : dp
// Dernière mise à jour : 1/11/2021

using System;

using System.Drawing;                                       // pour la classe Bitmap (utilisée pour le tracé)
using System.Windows.Forms;

using GMap.NET;                                             // affichage de cartes
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using System.Windows.Forms.DataVisualization.Charting;      // pour la classe Chart (affichage de graphiques)

namespace TraceGPS
{
    public partial class Form1 : Form
    {
        // déclaration des variables globales

        // pour gérer la trace
        Trace laTrace;
        PasserelleFichierXML laPasserelle;		    // on déclare avec la classe abstraite

        // pour gérer la carte et ses marqueurs
        PointLatLng leCentre = new PointLatLng(48.114208, -1.665977);       // le centre de Rennes
        GMapOverlay leCalqueDesPoints; 	    // le calque contenant les marqueurs des points de parcours
        GMarkerGoogle unMarqueur; 		    // un marqueur de point de parcours
        Bitmap imageMarqueur;               // pour afficher chaque point de la trace sur la carte

        // constructeur du formulaire
        public Form1()
        {
            InitializeComponent();

            // titre de l'application
            if (Global.pseudo == "")
                this.Text = Global.NOM_APPLICATION + " - accès anonyme";
            else
                this.Text = Global.NOM_APPLICATION + " - " + Global.pseudo;

            // création de l'objet Trace
            laTrace = new Trace();

            // effacement des données affichées
            this.lblNbPoints.Text = "";
            this.lblHeureDebut.Text = "";
            this.lblHeureFin.Text = "";
            this.lblDuree.Text = "";
            this.lblDistance.Text = "";
            this.lblVitesse.Text = "";
            this.lblDenivelePositif.Text = "";
            this.lblDeniveleNegatif.Text = "";

            // réglage de la taille de la carte en fonction de la taille du formulaire
            laCarte.Width = this.ClientSize.Width - laCarte.Left;
            laCarte.Height = this.ClientSize.Height - laCarte.Top;

            // positionnement du graphique en bas de la fenetre en fonction de la taille du formulaire
            chart1.Top = this.ClientSize.Height - chart1.Height - 10;

            // Chargement et initialisation de la carte
            laCarte.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            laCarte.DragButton = MouseButtons.Left;

            // Ajoute un calque sur la carte pour afficher les marqueurs des points de parcours
            leCalqueDesPoints = new GMapOverlay("marqueurs des points de parcours");
            laCarte.Overlays.Add(leCalqueDesPoints);

            // centrage de la carte sur le centre de Rennes et réglage du zoom
            laCarte.Position = leCentre;
            laCarte.Zoom = 13;

            // préparation de l'image du marqueur, à partir des images chargées dans le contrôle imageList1
            imageMarqueur = (Bitmap)imageList1.Images["carre-vert.png"];
        }

        // gestion du redimensionnement du formulaire
        private void Form1_Resize(object sender, EventArgs e)
        {
            // réglage de la taille de la carte en fonction de la taille du formulaire
            laCarte.Width = this.ClientSize.Width - laCarte.Left - 10;
            laCarte.Height = this.ClientSize.Height - laCarte.Top - 10;

            // positionnement du graphique en bas de la fenetre en fonction de la taille du formulaire
            chart1.Height = this.ClientSize.Height - chart1.Top - 10;
        }

        // gestion du bouton Quitter
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // gestion du bouton Ouvrir...
        private void btnOuvrir_Click(object sender, EventArgs e)
        {
            // On filtre pour ouvrir uniquement les fichiers gpx, pwx et tcx (la barre verticale set de séparateur)
			this.openFileDialog1.Filter = "Fichiers de trace (*.gpx, *.pwx, *.tcx)|*.gpx;*.pwx;*.tcx";

            // On modifie le titre de la boîte :
            this.openFileDialog1.Title = "Rechercher un fichier de trace (gpx, pwx, tcx)";

            // On affiche la boîte et on teste si l'utilisateur a terminé avec le bouton Ouvrir 
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // récupération du nom du fichier
                String nomFichier = this.openFileDialog1.FileName;

				// création de la passerelle en fonction du type de fichier
				if (nomFichier.ToLower().EndsWith(".gpx")) laPasserelle = new PasserelleGPX();
				if (nomFichier.ToLower().EndsWith(".pwx")) laPasserelle = new PasserellePWX();
				if (nomFichier.ToLower().EndsWith(".tcx")) laPasserelle = new PasserelleTCX();

                // analyse du fichier choisi et mise à jour de l'objet laTrace créé par le constructeur du formulaire
                String msg = laPasserelle.creerTrace(nomFichier, laTrace);

                if (msg != "")
                {   // si erreur retournée par la passerelle :
                    MessageBox.Show(msg, "Problème", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {   // si pas d'erreur

                    // affichage du nom de fichier (sans le chemin d'accès) dans le bandeau du formulaire
                    int position = nomFichier.LastIndexOf("\\");
                    String nomCourt = nomFichier.Substring(position + 1);
                    this.Text = Global.NOM_APPLICATION + " - " + nomCourt;

                    // mise à jour de l'objet laTrace
                    laTrace.setTerminee(true);

                    // affichage du parcours choisi
                    afficherParcoursChoisi();
                }
            }
        }

        // affichage du parcours choisi
        private void afficherParcoursChoisi()
        {
            // affichage des données du parcours
            this.lblNbPoints.Text = laTrace.getNombrePoints().ToString();
            this.lblHeureDebut.Text = laTrace.getDateHeureDebut().ToString("dd/MM/yyyy HH:mm:ss");
            this.lblHeureFin.Text = laTrace.getDateHeureFin().ToString("dd/MM/yyyy HH:mm:ss");
            this.lblDuree.Text = laTrace.getDureeTotale();
            this.lblDistance.Text = laTrace.getDistanceTotale().ToString("0.00");
            this.lblVitesse.Text = laTrace.getVitesseMoyenne().ToString("0.00");
            this.lblDenivelePositif.Text = laTrace.getDenivelePositif().ToString("0.00");
            this.lblDeniveleNegatif.Text = laTrace.getDeniveleNegatif().ToString("0.00");

            if (laTrace.getNombrePoints() > 0)
            {
                // création d'un objet PointLatLng correspondant au centre du parcours
                leCentre = new PointLatLng(laTrace.getCentre().getLatitude(), laTrace.getCentre().getLongitude());

                // centrage de la carte sur le centre du parcours et réglage du zoom
                laCarte.Position = leCentre;
                laCarte.Zoom = 13;

                // affichage des marqueurs
                afficherMarqueurs();

                // affichage des graphiques
                afficherGraphiques();
            }
        }

        // affichage des marqueurs
        private void afficherMarqueurs()
        {
            // efface les marqueurs précédents
            leCalqueDesPoints.Clear();

            // affichage d'un marqueur pour chaque point de la trace
            for (int i = 0; i < laTrace.getNombrePoints() ; i++)
            {
                PointDeTrace unPointDeTrace = (PointDeTrace)laTrace.getLesPointsDeTrace()[i];
                PointLatLng unPoint = new PointLatLng(unPointDeTrace.getLatitude(), unPointDeTrace.getLongitude());
                String texteMarqueur = "";

                // choix du type de marqueur et préparation d'une infobulle (toolTipText) associée au marqueur
                if (i == 0)
                {   // premier point du parcours
                    unMarqueur = new GMarkerGoogle(unPoint, GMarkerGoogleType.green_big_go);
                    texteMarqueur = "Départ\n";
                }
                else
                {
                    if (i == laTrace.getLesPointsDeTrace().Count - 1)
                    {   // dernier point du parcours
                        unMarqueur = new GMarkerGoogle(unPoint, GMarkerGoogleType.red_big_stop);
                        texteMarqueur = "Arrivée\n";
                    }
                    else
                    {   // autres points du parcours
                        unMarqueur = new GMarkerGoogle(unPoint, imageMarqueur);     // crée un marqueur avec l'image préparée dans le constructeur
                    }
                }
                // complète l'infobulle (toolTipText) avec l'horaire, le kilométrage, la vitesse, l'altitude et le rythme cardiaque
                texteMarqueur += "Temps : " + unPointDeTrace.getTempsCumuleEnChaine();
                texteMarqueur += "\nDistance : " + unPointDeTrace.getDistanceCumulee().ToString("0.000") + " Km";
                texteMarqueur += "\nVitesse : " + unPointDeTrace.getVitesse().ToString("0.0") + " Km/h";
                texteMarqueur += "\nAltitude : " + unPointDeTrace.getAltitude().ToString("0.0") + " m";
                if (unPointDeTrace.getRythmeCardio() > 0)
                    texteMarqueur += "\nCardio : " + unPointDeTrace.getRythmeCardio().ToString("0");
                unMarqueur.ToolTipText = texteMarqueur;

                // ajoute un tag contenant le numéro de point, ce qui permettra à la fonction  
                // laCarte_OnMarkerEnter de récupérer le numéro du point survolé par la souris
                unMarqueur.Tag = i;

                // ajoute le marqueur au calque
                leCalqueDesPoints.Markers.Add(unMarqueur);
            }
        }

        // gestion du choix des couleurs du tracé
        private void btnRouge_Click(object sender, EventArgs e)
        {
            // modification de l'image des marqueurs
            imageMarqueur = (Bitmap) imageList1.Images["carre-rouge.png"];
            // affichage des marqueurs
            afficherMarqueurs();
        }

        private void btnVert_Click(object sender, EventArgs e)
        {
            // modification de l'image des marqueurs
            imageMarqueur = (Bitmap)imageList1.Images["carre-vert.png"];
            // affichage des marqueurs
            afficherMarqueurs();
        }

        private void btnJaune_Click(object sender, EventArgs e)
        {
            // modification de l'image des marqueurs
            imageMarqueur = (Bitmap)imageList1.Images["carre-jaune.png"];
            // affichage des marqueurs
            afficherMarqueurs();
        }

        private void btnBleu_Click(object sender, EventArgs e)
        {
            // modification de l'image des marqueurs
            imageMarqueur = (Bitmap)imageList1.Images["carre-bleu.png"];
            // affichage des marqueurs
            afficherMarqueurs();
        }

        // gestion du choix du type de carte
        // Attention : Google n'est pas toujours le meilleur provider ; n'hésitez pas à tester d'autres providers
        private void cbxTypeCarte_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cbxTypeCarte.Text)
            {
                case "Carte":
                {   laCarte.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                    break;
                }
                case "Terrain":
                {
                    laCarte.MapProvider = GMap.NET.MapProviders.GoogleTerrainMapProvider.Instance;
                    break;
                }
                case "Hybride":
                {
                    laCarte.MapProvider = GMap.NET.MapProviders.BingHybridMapProvider.Instance;
                    break;
                }
                case "Satellite":
                {
                    laCarte.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
                    break;
                }
            }
        }

        // centrage de la carte sur le centre du parcours et réglage du zoom
        private void btnRecentrer_Click(object sender, EventArgs e)
        {
            laCarte.Position = leCentre;
            laCarte.Zoom = 13;
        }

        // augmenter le zoom
        private void btnZoomAvant_Click(object sender, EventArgs e)
        {
            if (laCarte.Zoom < laCarte.MaxZoom) laCarte.Zoom++;
        }

        // diminuer le zoom
        private void btnZoomArriere_Click(object sender, EventArgs e)
        {
            if (laCarte.Zoom > laCarte.MinZoom) laCarte.Zoom--;
        }

        // afficher un formulaire d'information
        private void btnApropos_Click(object sender, EventArgs e)
        {
            Form uneFeuille = new FormAPropos();
            uneFeuille.ShowDialog();
            uneFeuille = null;
        }

        // imprimer la carte
        private void btnImprimer_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK )
            {   printDialog1.Document = printDocument1;
                printDocument1.Print();
            }
        }

        // centrage de l'image dans la page
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.PageSettings.Landscape = true;
            Image uneImage = laCarte.ToImage();
            float X = (float)(e.PageBounds.Width - uneImage.Width) / 2;
            float Y = (float)(e.PageBounds.Height - uneImage.Height) / 2;
            e.Graphics.DrawImage(uneImage, X, Y);
        }

        // affichage des graphiques
        private void afficherGraphiques()
        {
            // efface les séries précédentes
            DataPointCollection lesAltitudes = chart1.Series["serieAltitude"].Points;
            lesAltitudes.Clear();
            DataPointCollection lesRythmes = chart1.Series["serieCardio"].Points;
            lesRythmes.Clear();

            for (int i = 0; i < laTrace.getLesPointsDeTrace().Count; i++)
            {   PointDeTrace unPointDeTrace = (PointDeTrace)laTrace.getLesPointsDeTrace()[i];

                // ajoute un point au graphique de l'altitude
                DataPoint uneAltitude = new DataPoint();
                uneAltitude.XValue = i + 1;
                uneAltitude.YValues[0] = unPointDeTrace.getAltitude();
                lesAltitudes.Add(uneAltitude);

                // ajoute un point au graphique du rythme cardiaque
                DataPoint unRythme = new DataPoint();
                unRythme.XValue = i + 1;
                unRythme.YValues[0] = unPointDeTrace.getRythmeCardio();
                lesRythmes.Add(unRythme);
            }
            // efface les annotations sur le graphique
            chart1.Annotations["flecheAltitude"].AnchorDataPoint = null;
            chart1.Annotations["flecheCardio"].AnchorDataPoint = null;
        }

        // déplace les annotations sur les graphiques quant un marqueur du parcours est survolé
        private void laCarte_OnMarkerEnter(GMapMarker item)
        {
            // récupération du numéro du point survolé à l'aide du Tag associé au point
            int i = (int) item.Tag;

            // déplace l'annotation du graphique de l'altitude
            DataPointCollection lesAltitudes = chart1.Series[0].Points;
            chart1.Annotations["flecheAltitude"].AnchorDataPoint = lesAltitudes[i];

            // déplace l'annotation du graphique du rythme cardiaque
            DataPointCollection lesRythmes = chart1.Series[1].Points;
            chart1.Annotations["flecheCardio"].AnchorDataPoint = lesRythmes[i];
        }

    } // fin de la classe
} // fin du namespace

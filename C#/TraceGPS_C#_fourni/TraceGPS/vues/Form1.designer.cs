namespace TraceGPS
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ArrowAnnotation arrowAnnotation1 = new System.Windows.Forms.DataVisualization.Charting.ArrowAnnotation();
            System.Windows.Forms.DataVisualization.Charting.ArrowAnnotation arrowAnnotation2 = new System.Windows.Forms.DataVisualization.Charting.ArrowAnnotation();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.laCarte = new GMap.NET.WindowsForms.GMapControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label13 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOuvrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRouge = new System.Windows.Forms.ToolStripButton();
            this.btnVert = new System.Windows.Forms.ToolStripButton();
            this.btnJaune = new System.Windows.Forms.ToolStripButton();
            this.btnBleu = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRecentrer = new System.Windows.Forms.ToolStripButton();
            this.btnZoomAvant = new System.Windows.Forms.ToolStripButton();
            this.btnZoomArriere = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cbxTypeCarte = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimer = new System.Windows.Forms.ToolStripButton();
            this.btnApropos = new System.Windows.Forms.ToolStripButton();
            this.btnQuitter = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDeniveleNegatif = new System.Windows.Forms.Label();
            this.lblDenivelePositif = new System.Windows.Forms.Label();
            this.lblVitesse = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblDuree = new System.Windows.Forms.Label();
            this.lblHeureFin = new System.Windows.Forms.Label();
            this.lblHeureDebut = new System.Windows.Forms.Label();
            this.lblNbPoints = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // laCarte
            // 
            this.laCarte.Bearing = 0F;
            this.laCarte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.laCarte.CanDragMap = true;
            this.laCarte.EmptyTileColor = System.Drawing.Color.Navy;
            this.laCarte.GrayScaleMode = false;
            this.laCarte.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.laCarte.LevelsKeepInMemmory = 5;
            this.laCarte.Location = new System.Drawing.Point(500, 35);
            this.laCarte.MarkersEnabled = true;
            this.laCarte.MaxZoom = 20;
            this.laCarte.MinZoom = 2;
            this.laCarte.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.laCarte.Name = "laCarte";
            this.laCarte.NegativeMode = false;
            this.laCarte.PolygonsEnabled = true;
            this.laCarte.RetryLoadTile = 0;
            this.laCarte.RoutesEnabled = true;
            this.laCarte.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.laCarte.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.laCarte.ShowTileGridLines = false;
            this.laCarte.Size = new System.Drawing.Size(768, 688);
            this.laCarte.TabIndex = 0;
            this.laCarte.Zoom = 13D;
            this.laCarte.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.laCarte_OnMarkerEnter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(60, 172);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(348, 17);
            this.label13.TabIndex = 32;
            this.label13.Text = "Visionnez vos tracés aux formats gpx, pwx, tcx.";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOuvrir,
            this.toolStripSeparator1,
            this.btnRouge,
            this.btnVert,
            this.btnJaune,
            this.btnBleu,
            this.toolStripSeparator2,
            this.btnRecentrer,
            this.btnZoomAvant,
            this.btnZoomArriere,
            this.toolStripSeparator3,
            this.cbxTypeCarte,
            this.toolStripSeparator4,
            this.btnImprimer,
            this.btnApropos,
            this.btnQuitter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1194, 25);
            this.toolStrip1.TabIndex = 33;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOuvrir
            // 
            this.btnOuvrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOuvrir.Image = ((System.Drawing.Image)(resources.GetObject("btnOuvrir.Image")));
            this.btnOuvrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOuvrir.Name = "btnOuvrir";
            this.btnOuvrir.Size = new System.Drawing.Size(23, 22);
            this.btnOuvrir.Text = "toolStripButton1";
            this.btnOuvrir.ToolTipText = "Ouvrir un fichier...";
            this.btnOuvrir.Click += new System.EventHandler(this.btnOuvrir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRouge
            // 
            this.btnRouge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRouge.Image = ((System.Drawing.Image)(resources.GetObject("btnRouge.Image")));
            this.btnRouge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRouge.Name = "btnRouge";
            this.btnRouge.Size = new System.Drawing.Size(23, 22);
            this.btnRouge.Text = "Tracé en rouge";
            this.btnRouge.Click += new System.EventHandler(this.btnRouge_Click);
            // 
            // btnVert
            // 
            this.btnVert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVert.Image = ((System.Drawing.Image)(resources.GetObject("btnVert.Image")));
            this.btnVert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVert.Name = "btnVert";
            this.btnVert.Size = new System.Drawing.Size(23, 22);
            this.btnVert.Text = "Tracé en vert";
            this.btnVert.Click += new System.EventHandler(this.btnVert_Click);
            // 
            // btnJaune
            // 
            this.btnJaune.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnJaune.Image = ((System.Drawing.Image)(resources.GetObject("btnJaune.Image")));
            this.btnJaune.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnJaune.Name = "btnJaune";
            this.btnJaune.Size = new System.Drawing.Size(23, 22);
            this.btnJaune.Text = "Tracé en jaune";
            this.btnJaune.Click += new System.EventHandler(this.btnJaune_Click);
            // 
            // btnBleu
            // 
            this.btnBleu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBleu.Image = ((System.Drawing.Image)(resources.GetObject("btnBleu.Image")));
            this.btnBleu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBleu.Name = "btnBleu";
            this.btnBleu.Size = new System.Drawing.Size(23, 22);
            this.btnBleu.Text = "Tracé en bleu";
            this.btnBleu.Click += new System.EventHandler(this.btnBleu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRecentrer
            // 
            this.btnRecentrer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecentrer.Image = ((System.Drawing.Image)(resources.GetObject("btnRecentrer.Image")));
            this.btnRecentrer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecentrer.Name = "btnRecentrer";
            this.btnRecentrer.Size = new System.Drawing.Size(23, 22);
            this.btnRecentrer.Text = "Recentrer le tracé";
            this.btnRecentrer.Click += new System.EventHandler(this.btnRecentrer_Click);
            // 
            // btnZoomAvant
            // 
            this.btnZoomAvant.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomAvant.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomAvant.Image")));
            this.btnZoomAvant.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomAvant.Name = "btnZoomAvant";
            this.btnZoomAvant.Size = new System.Drawing.Size(23, 22);
            this.btnZoomAvant.Text = "Zoom avant";
            this.btnZoomAvant.Click += new System.EventHandler(this.btnZoomAvant_Click);
            // 
            // btnZoomArriere
            // 
            this.btnZoomArriere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomArriere.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomArriere.Image")));
            this.btnZoomArriere.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomArriere.Name = "btnZoomArriere";
            this.btnZoomArriere.Size = new System.Drawing.Size(23, 22);
            this.btnZoomArriere.Text = "Zom arrière";
            this.btnZoomArriere.Click += new System.EventHandler(this.btnZoomArriere_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cbxTypeCarte
            // 
            this.cbxTypeCarte.Items.AddRange(new object[] {
            "Carte",
            "Terrain",
            "Hybride",
            "Satellite"});
            this.cbxTypeCarte.Name = "cbxTypeCarte";
            this.cbxTypeCarte.Size = new System.Drawing.Size(80, 25);
            this.cbxTypeCarte.Text = "Carte";
            this.cbxTypeCarte.ToolTipText = "Type de carte";
            this.cbxTypeCarte.SelectedIndexChanged += new System.EventHandler(this.cbxTypeCarte_SelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimer
            // 
            this.btnImprimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimer.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimer.Image")));
            this.btnImprimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimer.Name = "btnImprimer";
            this.btnImprimer.Size = new System.Drawing.Size(23, 22);
            this.btnImprimer.Text = "Imprimer";
            this.btnImprimer.Click += new System.EventHandler(this.btnImprimer_Click);
            // 
            // btnApropos
            // 
            this.btnApropos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnApropos.Image = ((System.Drawing.Image)(resources.GetObject("btnApropos.Image")));
            this.btnApropos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApropos.Name = "btnApropos";
            this.btnApropos.Size = new System.Drawing.Size(23, 22);
            this.btnApropos.Text = "A propos de...";
            this.btnApropos.Click += new System.EventHandler(this.btnApropos_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuitter.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitter.Image")));
            this.btnQuitter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(23, 22);
            this.btnQuitter.Text = "Quiter";
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(77, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(312, 124);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "carre-rouge.png");
            this.imageList1.Images.SetKeyName(1, "carre-vert.png");
            this.imageList1.Images.SetKeyName(2, "carre-bleu.png");
            this.imageList1.Images.SetKeyName(3, "carre-jaune.png");
            // 
            // chart1
            // 
            arrowAnnotation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            arrowAnnotation1.Height = -10D;
            arrowAnnotation1.Name = "flecheAltitude";
            arrowAnnotation1.Width = 0D;
            arrowAnnotation2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            arrowAnnotation2.Height = -10D;
            arrowAnnotation2.Name = "flecheCardio";
            arrowAnnotation2.Width = 0D;
            this.chart1.Annotations.Add(arrowAnnotation1);
            this.chart1.Annotations.Add(arrowAnnotation2);
            this.chart1.BackColor = System.Drawing.SystemColors.Control;
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineWidth = 3;
            chartArea1.Name = "grapheAltitude";
            chartArea2.Name = "grapheCardio";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(0, 354);
            this.chart1.Name = "chart1";
            series1.ChartArea = "grapheAltitude";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "serieAltitude";
            series2.ChartArea = "grapheCardio";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "serieCardio";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(494, 369);
            this.chart1.TabIndex = 38;
            title1.DockedToChartArea = "grapheAltitude";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            title1.IsDockedInsideChartArea = false;
            title1.Name = "titreAltitude";
            title1.Text = "Altitude (m)";
            title2.DockedToChartArea = "grapheCardio";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            title2.IsDockedInsideChartArea = false;
            title2.Name = "titreCardio";
            title2.Text = "Rythme cardiaque";
            this.chart1.Titles.Add(title1);
            this.chart1.Titles.Add(title2);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDeniveleNegatif);
            this.groupBox1.Controls.Add(this.lblDenivelePositif);
            this.groupBox1.Controls.Add(this.lblVitesse);
            this.groupBox1.Controls.Add(this.lblDistance);
            this.groupBox1.Controls.Add(this.lblDuree);
            this.groupBox1.Controls.Add(this.lblHeureFin);
            this.groupBox1.Controls.Add(this.lblHeureDebut);
            this.groupBox1.Controls.Add(this.lblNbPoints);
            this.groupBox1.Location = new System.Drawing.Point(12, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 139);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Données du parcours";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(258, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Dénivelé négatif (m) :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Dénivelé positif (m) :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(258, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Vitesse moyenne (Km/h) :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Distance (Km) :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Durée :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Heure de fin :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Heure de début :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Nombre de points :";
            // 
            // lblDeniveleNegatif
            // 
            this.lblDeniveleNegatif.AutoSize = true;
            this.lblDeniveleNegatif.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeniveleNegatif.Location = new System.Drawing.Point(392, 110);
            this.lblDeniveleNegatif.Name = "lblDeniveleNegatif";
            this.lblDeniveleNegatif.Size = new System.Drawing.Size(14, 13);
            this.lblDeniveleNegatif.TabIndex = 28;
            this.lblDeniveleNegatif.Text = "0";
            // 
            // lblDenivelePositif
            // 
            this.lblDenivelePositif.AutoSize = true;
            this.lblDenivelePositif.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenivelePositif.Location = new System.Drawing.Point(128, 110);
            this.lblDenivelePositif.Name = "lblDenivelePositif";
            this.lblDenivelePositif.Size = new System.Drawing.Size(14, 13);
            this.lblDenivelePositif.TabIndex = 27;
            this.lblDenivelePositif.Text = "0";
            // 
            // lblVitesse
            // 
            this.lblVitesse.AutoSize = true;
            this.lblVitesse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVitesse.Location = new System.Drawing.Point(392, 88);
            this.lblVitesse.Name = "lblVitesse";
            this.lblVitesse.Size = new System.Drawing.Size(14, 13);
            this.lblVitesse.TabIndex = 26;
            this.lblVitesse.Text = "0";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistance.Location = new System.Drawing.Point(392, 25);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(14, 13);
            this.lblDistance.TabIndex = 25;
            this.lblDistance.Text = "0";
            // 
            // lblDuree
            // 
            this.lblDuree.AutoSize = true;
            this.lblDuree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuree.Location = new System.Drawing.Point(128, 88);
            this.lblDuree.Name = "lblDuree";
            this.lblDuree.Size = new System.Drawing.Size(14, 13);
            this.lblDuree.TabIndex = 24;
            this.lblDuree.Text = "0";
            // 
            // lblHeureFin
            // 
            this.lblHeureFin.AutoSize = true;
            this.lblHeureFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeureFin.Location = new System.Drawing.Point(128, 67);
            this.lblHeureFin.Name = "lblHeureFin";
            this.lblHeureFin.Size = new System.Drawing.Size(14, 13);
            this.lblHeureFin.TabIndex = 23;
            this.lblHeureFin.Text = "0";
            // 
            // lblHeureDebut
            // 
            this.lblHeureDebut.AutoSize = true;
            this.lblHeureDebut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeureDebut.Location = new System.Drawing.Point(128, 46);
            this.lblHeureDebut.Name = "lblHeureDebut";
            this.lblHeureDebut.Size = new System.Drawing.Size(14, 13);
            this.lblHeureDebut.TabIndex = 22;
            this.lblHeureDebut.Text = "0";
            // 
            // lblNbPoints
            // 
            this.lblNbPoints.AutoSize = true;
            this.lblNbPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNbPoints.Location = new System.Drawing.Point(128, 25);
            this.lblNbPoints.Name = "lblNbPoints";
            this.lblNbPoints.Size = new System.Drawing.Size(14, 13);
            this.lblNbPoints.TabIndex = 21;
            this.lblNbPoints.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 741);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.laCarte);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trace GPS 1.1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl laCarte;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOuvrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnRouge;
        private System.Windows.Forms.ToolStripButton btnVert;
        private System.Windows.Forms.ToolStripButton btnJaune;
        private System.Windows.Forms.ToolStripButton btnBleu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnRecentrer;
        private System.Windows.Forms.ToolStripButton btnZoomAvant;
        private System.Windows.Forms.ToolStripButton btnZoomArriere;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox cbxTypeCarte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnImprimer;
        private System.Windows.Forms.ToolStripButton btnApropos;
		private System.Windows.Forms.ToolStripButton btnQuitter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDeniveleNegatif;
        private System.Windows.Forms.Label lblDenivelePositif;
        private System.Windows.Forms.Label lblVitesse;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblDuree;
        private System.Windows.Forms.Label lblHeureFin;
        private System.Windows.Forms.Label lblHeureDebut;
        private System.Windows.Forms.Label lblNbPoints;
    }
}


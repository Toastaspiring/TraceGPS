<?php
namespace modele;
// Projet TraceGPS
// fichier : modele/Trace.php
// Rôle : la classe Trace représente une trace ou un parcours
// Dernière mise à jour : 9/7/2021 par dP
include_once ('PointDeTrace.php');
class Trace
{
    // ------------------------------------------------------------------------------------------------------
    // ---------------------------------- Attributs privés de la classe -------------------------------------
    // ------------------------------------------------------------------------------------------------------
    
    private $id; // identifiant de la trace
    private $dateHeureDebut; // date et heure de début
    private $dateHeureFin; // date et heure de fin
    private $terminee; // true si la trace est terminée, false sinon
    private $idUtilisateur; // identifiant de l'utilisateur ayant créé la trace
    private $lesPointsDeTrace; // la collection (array) des objets PointDeTrace formant la trace
    
    // ------------------------------------------------------------------------------------------------------
    // ----------------------------------------- Constructeur -----------------------------------------------
    // ------------------------------------------------------------------------------------------------------
    
    public function __construct($unId, $uneDateHeureDebut, $uneDateHeureFin, $terminee, $unIdUtilisateur) {
        $this->id = $unId;
        $this->dateHeureDebut = $uneDateHeureDebut;
        $this->dateHeureFin = $uneDateHeureFin;
        $this->terminee = $terminee;
        $this->idUtilisateur = $unIdUtilisateur;
        $this->lesPointsDeTrace = array();
    }
    
    // ------------------------------------------------------------------------------------------------------
    // ---------------------------------------- Getters et Setters ------------------------------------------
    // ------------------------------------------------------------------------------------------------------
    
    public function getId() {return $this->id;}
    public function setId($unId) {$this->id = $unId;}
    
    public function getDateHeureDebut() {return $this->dateHeureDebut;}
    public function setDateHeureDebut($uneDateHeureDebut) {$this->dateHeureDebut = $uneDateHeureDebut;}
    public function getDateHeureFin() {return $this->dateHeureFin;}
    public function setDateHeureFin($uneDateHeureFin) {$this->dateHeureFin= $uneDateHeureFin;}
    
    public function getTerminee() {return $this->terminee;}
    public function setTerminee($terminee) {$this->terminee = $terminee;}
    
    public function getIdUtilisateur() {return $this->idUtilisateur;}
    
    public function setIdUtilisateur($unIdUtilisateur) {$this->idUtilisateur = $unIdUtilisateur;}
    public function getLesPointsDeTrace() {return $this->lesPointsDeTrace;}
    public function setLesPointsDeTrace($lesPointsDeTrace) {$this->lesPointsDeTrace = $lesPointsDeTrace;}
    
    // Fournit une chaine contenant toutes les données de l'objet
    public function toString() {
        $msg = "Id : " . $this->getId() . "<br>";
        $msg .= "Utilisateur : " . $this->getIdUtilisateur() . "<br>";
        if ($this->getDateHeureDebut() != null) {
            $msg .= "Heure de début : " . $this->getDateHeureDebut() . "<br>";
        }
        if ($this->getTerminee()) {
            $msg .= "Terminée : Oui <br>";
        }
        else {
            $msg .= "Terminée : Non <br>";
        }
        $msg .= "Nombre de points : " . $this->getNombrePoints() . "<br>";
        if ($this->getNombrePoints() > 0) {
            if ($this->getDateHeureFin() != null) {
                $msg .= "Heure de fin : " . $this->getDateHeureFin() . "<br>";
            }
            $msg .= "Durée en secondes : " . $this->getDureeEnSecondes() . "<br>";
            $msg .= "Durée totale : " . $this->getDureeTotale() . "<br>";
            $msg .= "Distance totale en Km : " . $this->getDistanceTotale() . "<br>";
            $msg .= "Dénivelé en m : " . $this->getDenivele() . "<br>";
            $msg .= "Dénivelé positif en m : " . $this->getDenivelePositif() . "<br>";
            $msg .= "Dénivelé négatif en m : " . $this->getDeniveleNegatif() . "<br>";
            $msg .= "Vitesse moyenne en Km/h : " . $this->getVitesseMoyenne() . "<br>";
            $msg .= "Centre du parcours : " . "<br>";
            $msg .= " - Latitude : " . $this->getCentre()->getLatitude() . "<br>";
            $msg .= " - Longitude : " . $this->getCentre()->getLongitude() . "<br>";
            $msg .= " - Altitude : " . $this->getCentre()->getAltitude() . "<br>";
        }
        return $msg;
    }
    
    public function getNombrePoints(){
        return sizeof($this->lesPointsDeTrace);
    }
    
    public function getCentre(){
        if($this->getNombrePoints() == 0){return null;}
        
        $longitudeMini = INF;
        $latitudeMini = INF;
        $longitudeMaxi = -INF;
        $latitudeMaxi = -INF;
        
        foreach ($this->lesPointsDeTrace as $point){
            $longitude = $point->getLongitude();
            $latitude = $point->getLatitude();
            
            if($longitude > $longitudeMaxi){$longitudeMaxi = $longitude;}
            if($longitude < $longitudeMini){$longitudeMini = $longitude;}
            
            if($latitude > $latitudeMaxi){$latitudeMaxi = $latitude;}
            if($latitude < $latitudeMini){$latitudeMini = $latitude;}
        }
        
        $longitudeMoyenne = ($longitudeMaxi+$longitudeMini)/2;
        $latitudeMoyenne = ($latitudeMaxi+$latitudeMini)/2;
        $pointCentre = new Point($latitudeMoyenne,$longitudeMoyenne,0);
        
        return $pointCentre;
    }
    
    public function getDenivele(){
        if($this->getNombrePoints() == 0){return 0;}
        
        $altitudeMax = -INF;
        $altitudeMini = INF;
        
        foreach($this->lesPointsDeTrace as $point){
            $altitude = $point->getAltitude();
            
            if($altitude > $altitudeMax){$altitudeMax = $altitude;}
            if($altitude < $altitudeMini){$altitudeMini = $altitude;}
        }
        
        return $altitudeMax-$altitudeMini;
    }
    
    public function getDureeEnSecondes(){
        if($this->getNombrePoints() == 0){return 0;}
        
        $dateDebut = strtotime($this->lesPointsDeTrace[0]->getDateHeure());
        $dateFin = strtotime($this->lesPointsDeTrace[$this->getNombrePoints()-1]->getDateHeure());
        
        return $dateFin-$dateDebut;
    }
    
    public function getDureeTotale(){       
        $tempsTotal = $this->getDureeEnSecondes();
        
        $heures = floor($tempsTotal / 3600);
        $tempsTotal -= $heures*3600;
        $minutes = floor($tempsTotal / 60);
        $tempsTotal -= $minutes*60;
        $secondes = $tempsTotal;
        
        return sprintf("%02d",$heures) . ":" . sprintf("%02d",$minutes) . ":" . sprintf("%02d",$secondes);
    }
    
    public function getDistanceTotale(){
        if($this->getNombrePoints() == 0) {return 0;}
        
        return $this->lesPointsDeTrace[$this->getNombrePoints()-1]->getDistanceCumulee();
    }
    
    public function getDenivelePositif(){
        $totalDenivele = 0;

        for ($i = 1; $i < $this->getNombrePoints()-1; $i++) {
            $altitude1 = $this->lesPointsDeTrace[$i-1]->getAltitude();
            $altitude2 = $this->lesPointsDeTrace[$i]->getAltitude();
            
            if($altitude1 < $altitude2){
                $totalDenivele += $altitude2 - $altitude1;
            }
        }
        return $totalDenivele;
    }
    
    public function getDeniveleNegatif(){
        $totalDenivele = 0;
        
        for ($i = 1; $i < $this->getNombrePoints()-1; $i++) {
            $altitude1 = $this->lesPointsDeTrace[$i-1]->getAltitude();
            $altitude2 = $this->lesPointsDeTrace[$i]->getAltitude();
            
            if($altitude1 < $altitude2){
                $totalDenivele += $altitude1 - $altitude2;
            }
        }
        return -$totalDenivele;
    }
    
    public function getVitesseMoyenne(){
        if($this->getNombrePoints() == 0){return 0;}
        
        $distancetotale = $this->getDistanceTotale();
        $dureeTotale = $this->getDureeEnSecondes();
        
        return $distancetotale/($dureeTotale/3600);
    }
    
    public function ajouterPoint($nvPoint){
        $distanceCumulee = 0;
        $tempsCumule = 0;
        $vitesse = 0;
        
        if($this->getNombrePoints() != 0){
            $dernierPoint = $this->lesPointsDeTrace[$this->getNombrePoints()-1];
            $distanceEntrePoints = Point::getDistance($dernierPoint, $nvPoint);
            $tempsEntrePoints = strtotime($nvPoint->getDateHeure())-strtotime($dernierPoint->getDateHeure());
            
            $distanceCumulee = $dernierPoint->getDistanceCumulee() + $distanceEntrePoints;
            $tempsCumule = $dernierPoint->getTempsCumule() + $tempsEntrePoints;
            $vitesse = $distanceEntrePoints / ($tempsEntrePoints/3600);
        }
        
        $nvPoint->setDistanceCumulee($distanceCumulee);
        $nvPoint->setTempsCumule($tempsCumule);
        $nvPoint->setVitesse($vitesse);
        
        array_push($this->lesPointsDeTrace,$nvPoint);
    }
    
    public function viderListePoints(){
        $this->lesPointsDeTrace = array();
    }
    
} // fin de la classe Trace


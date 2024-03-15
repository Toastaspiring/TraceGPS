<?php
//
// Projet TraceGPS - version web mobile
// fichier : controleurs/CtrlMenu.php
// Rôle : traiter la demande d'accès au menu
// Dernière mise à jour : 01/11/2021 par dP
include_once ('modele/DAO.php');
$EXPRESSION = "#^(?=.*[a-z].*$)(?=.*[A-Z].*$)(?=.*[0-9].*$)(?=.{8,}$)#";
// on vérifie si le demandeur de cette action est bien authentifié
if ( $_SESSION['niveauConnexion'] == 0) {
    // si le demandeur n'est pas authentifié, il s'agit d'une tentative d'accès frauduleux
    // dans ce cas, on provoque une redirection vers la page de connexion
    header ("Location: index.php?action=Deconnecter");
}
elseif (!preg_match($EXPRESSION, $_SESSION['mdp'])) {
    // connexion du serveur web à la base MySQL
    $dao = new DAO();

    include_once ('controleurs/CtrlChangerDeMdp.php');
}else{
    // connexion du serveur web à la base MySQL
    $dao = new DAO();
    
    // affiche la vue
    include_once ('vues/VueMenu.php');
}
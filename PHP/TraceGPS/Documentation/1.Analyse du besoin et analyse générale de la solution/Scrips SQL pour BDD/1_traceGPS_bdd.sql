--
-- MySQL : création de la base tracegps et de son login associé
--
-- Notes:
-- (1) Le nom de base, le login et mot de passe doivent être identiques aux paramètres 
--     precisés dans le fichier parametres.localhost.php :
--           $db_database = "tracegps";
--           $db_login = "tracegps";
--           $db_password = "spgecart";

-- création de la base de données
DROP DATABASE IF EXISTS tracegps;
CREATE DATABASE IF NOT EXISTS tracegps
  CHARACTER SET utf8 COLLATE utf8_general_ci;

-- création d'un login tracegps ayant tous les droits sur la base tracegps et
-- affectation à ce login de tous les droits sur la base de donneés tracegps

CREATE USER tracegps@'localhost' IDENTIFIED BY 'spgecart';
GRANT ALL ON tracegps.* TO tracegps@'localhost' ;

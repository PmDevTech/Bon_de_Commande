---------------22/07/2021------------
ALTER TABLE `t_fournisseur` CHANGE `TelFournis` `TelFournis` VARCHAR(15) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL;
ALTER TABLE `t_fournisseur` CHANGE `CelFournis` `CelFournis` VARCHAR(15) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL;
ALTER TABLE `t_fournisseur` CHANGE `FaxFournis` `FaxFournis` VARCHAR(15) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL;
---------------26/07/2021-------------
ALTER TABLE `t_soumissionfournisseur` CHANGE `AttRegFiscale` `AttRegFiscale` DATETIME NULL DEFAULT NULL;
ALTER TABLE `t_soumissionfournisseur` CHANGE `AttCNPS` `AttCNPS` DATETIME NULL DEFAULT NULL;
--------------28/07/2021------------
ALTER TABLE `t_soumissionfournisseur` CHANGE `MontantPropose` `MontantPropose` DOUBLE NULL DEFAULT NULL;
ALTER TABLE `t_soumissionfournisseur` CHANGE `CautionBancaire` `CautionBancaire` DOUBLE NULL DEFAULT NULL;
ALTER TABLE `t_soumissionfournisseur` ADD `CodeSousLot` VARCHAR(10) NOT NULL AFTER `CodeLot`;
------06/08/2021-----------
CREATE TABLE `bdpdmold`.`t_spectechcaractpropose` ( `RefSpecCaractPro` BIGINT(19) NOT NULL AUTO_INCREMENT , `RefSpecFournit` INT(11) NOT NULL , `LibelleCaract` VARCHAR(300) NOT NULL , PRIMARY KEY (`RefSpecCaractPro`)) ENGINE = InnoDB;
CREATE TABLE `bdpdmold`.`t_soumiscaractfournitsupl` ( `RefSpecCaract` INT(11) NOT NULL , `RefSoumis` INT(11) NOT NULL , `ValeurOfferte` VARCHAR(100) NOT NULL , `Commentaire` VARCHAR(200) NOT NULL ) ENGINE = InnoDB;
ALTER TABLE `t_soumiscaractfournitsupl` ADD INDEX( `RefSpecCaract`, `ValeurOfferte`,`RefSoumis`);
ALTER TABLE `t_soumiscaractfournitsupl` CHANGE `Commentaire` `Commentaire` VARCHAR(200) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL;

--10/08/2021
ALTER TABLE `t_soumiscaractfournitsupl` ADD `MentionValeur` VARCHAR(12) NULL DEFAULT NULL AFTER `ValeurOfferte`;

--09/11/2021
ALTER TABLE `t_dao` ADD `DossValider` BOOLEAN NOT NULL DEFAULT FALSE AFTER `Attribution`;
ALTER TABLE `t_dao` ADD `Attribution` VARCHAR(160) NULL DEFAULT NULL AFTER `DateFinJugement`;
ALTER TABLE `t_soumisfournispostqualif` ADD `CodeLot` VARCHAR(11) NULL DEFAULT NULL AFTER `CodeFournis`;
DROP TABLE IF EXISTS `t_spectechcaractpropose`;
CREATE TABLE IF NOT EXISTS `t_spectechcaractpropose` (
  `RefSpecCaractPro` bigint(19) NOT NULL AUTO_INCREMENT,
  `RefSpecFournit` int(11) NOT NULL,
  `LibelleCaract` varchar(300) NOT NULL,
  PRIMARY KEY (`RefSpecCaractPro`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
DROP TABLE IF EXISTS `t_soumiscaractfournitsupl`;
CREATE TABLE IF NOT EXISTS `t_soumiscaractfournitsupl` (
  `RefSpecCaract` int(11) NOT NULL,
  `RefSoumis` int(11) NOT NULL,
  `ValeurOfferte` varchar(100) NOT NULL,
  `MentionValeur` varchar(12) DEFAULT NULL,
  `Commentaire` varchar(200) DEFAULT NULL,
  `ID_COJO` int(11) NOT NULL,
  KEY `RefSpecCaract` (`RefSpecCaract`,`ValeurOfferte`),
  KEY `RefSoumis` (`RefSoumis`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
DROP TABLE IF EXISTS `t_soumissionfournisseurexamdetail`;
CREATE TABLE IF NOT EXISTS `t_soumissionfournisseurexamdetail` (
  `ID` bigint(19) NOT NULL AUTO_INCREMENT,
  `CodeFournis` bigint(19) NOT NULL,
  `NumeroDAO` varchar(19) NOT NULL,
  `CodeLot` varchar(6) NOT NULL,
  `Monnaie` varchar(10) NOT NULL,
  `MontantPropose` double DEFAULT NULL,
  `MontantAvecMonnaie` double DEFAULT NULL,
  `SigneErreur` varchar(1) DEFAULT NULL,
  `ErreurCalcul` double DEFAULT NULL,
  `SomProvision` double DEFAULT NULL,
  `PrctRabais` double(19,2) DEFAULT NULL,
  `MontantRabais` double DEFAULT NULL,
  `AjoutOmission` double DEFAULT NULL,
  `Ajustements` double DEFAULT NULL,
  `VariationMineure` double DEFAULT NULL,
  `PrixCorrigeOffre` double DEFAULT NULL,
  `RangExamDetaille` int(11) DEFAULT NULL,
  `ExamPQValide` varchar(3) DEFAULT NULL,
  `JustifPQValide` varchar(500) DEFAULT NULL,
  `RangPostQualif` int(11) DEFAULT NULL,
  `Selectionne` varchar(3) DEFAULT NULL,
  `MotifSelect` varchar(500) DEFAULT NULL,
  `Attribue` varchar(3) DEFAULT NULL,
  `DateModif` datetime NOT NULL,
  `Operateur` varchar(10) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--11/11/2021
ALTER TABLE `t_dao` CHANGE `Operateur` `Operateur` INT(11) NULL DEFAULT NULL;

--07/12/2021
ALTER TABLE `t_soumiscaractfournit` CHANGE `ValeurOfferte` `ValeurOfferte` VARCHAR(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL;

--08/12/2021
ALTER TABLE `t_dao` ADD `DateReport` DATETIME NULL AFTER `DossValider`, ADD `DatePublicationReport` DATETIME NULL AFTER `DateReport`, ADD `JournalPublicationReport` VARCHAR(150) NULL AFTER `DatePublicationReport`, ADD `DateSaisiReport` DATETIME NULL AFTER `JournalPublicationReport`;
ALTER TABLE `t_grh_employe` ADD `Emp_Cordonnateur` BOOLEAN NOT NULL DEFAULT FALSE AFTER `EMP_NB_ENF_CHARGE`;

----17/01/2022
ALTER TABLE `t_paramtechprojet` ADD `ModePlanMarche` VARCHAR(150) NULL DEFAULT NULL AFTER `MethodeMarcheAuto`;
ALTER TABLE `t_paramtechprojet` ADD `ElaboPPM` VARCHAR(150) NULL DEFAULT NULL AFTER `ModePlanMarche`;

CREATE TABLE `t_ppm_responsableetape` ( `ID` INT(19) NOT NULL AUTO_INCREMENT , `Nom` VARCHAR(150) NOT NULL , `Prenoms` VARCHAR(250) NOT NULL , `Service` VARCHAR(150) NOT NULL , `Fonction` VARCHAR(150) NOT NULL , `Téléphone` VARCHAR(150) NOT NULL , `Portable` VARCHAR(150) NOT NULL , `Fax` VARCHAR(150) NOT NULL , `Email` VARCHAR(250) NOT NULL , PRIMARY KEY (`ID`)) ENGINE = InnoDB;
ALTER TABLE `t_ppm_responsableetape` ADD `CodeProjet` VARCHAR(150) NOT NULL AFTER `Email`;

RENAME TABLE `t_ppm_ppsd_repartition` TO `bdpdm`.`t_ppm_repartitionbailleur`;

---18/01/2022
ALTER TABLE `t_marchesigne` ADD `NumeroDAO` VARCHAR(75) NULL AFTER `RefMarche`;
ALTER TABLE `t_ppm_responsableetape` CHANGE `Téléphone` `Telephone` VARCHAR(150) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL;
ALTER TABLE `t_planmarche` ADD `StatutRespoEtape` VARCHAR(150) NULL DEFAULT NULL AFTER `ResponsableEtape`;

--21/01/2022
ALTER TABLE `t_ppm_marche` ADD `ModePlanMarche` VARCHAR(150) NOT NULL AFTER `CodeConvention`;

--24/01/2022
ALTER TABLE `t_ppm_marche` ADD `NumeroPlan` VARCHAR(100) NULL DEFAULT NULL AFTER `CodeUtils`, ADD UNIQUE (`NumeroPlan`);

--25/01/2022
ALTER TABLE `t_marche` ADD `NiveauActu` INT(19) NULL DEFAULT NULL AFTER `RefPPM`;
ALTER TABLE `t_marche` CHANGE `MontantEstimatif` `MontantEstimatif` BIGINT(19) NULL DEFAULT NULL;

CREATE TABLE `t_ppm_historiquemarche` ( `IDhist` INT(19) NOT NULL AUTO_INCREMENT , `RefMarche` INT(11) NOT NULL , `NumeroMarche` VARCHAR(20) NULL DEFAULT NULL , `CodeProjet` VARCHAR(20) NOT NULL , `NumeroComptable` VARCHAR(10) NULL DEFAULT NULL , `TypeMarche` VARCHAR(160) NOT NULL , `DescriptionMarche` VARCHAR(500) NOT NULL , `NumeroDAO` VARCHAR(50) NULL DEFAULT NULL , `CodeLot` VARCHAR(10) NULL DEFAULT NULL , `Forfait_TpsPasse` VARCHAR(12) NULL DEFAULT NULL , `MontantEstimatif` BIGINT(19) NULL DEFAULT NULL , `MethodeMarche` VARCHAR(10) NULL DEFAULT NULL , `QualifPrePost` VARCHAR(20) NULL DEFAULT NULL , `RevuePrioPost` VARCHAR(20) NULL DEFAULT NULL , `PeriodeMarche` VARCHAR(23) NOT NULL , `InitialeBailleur` VARCHAR(250) NOT NULL , `CodeConvention` VARCHAR(250) NOT NULL , `Convention_ChefFile` VARCHAR(160) NOT NULL , `ModePPM` VARCHAR(100) NOT NULL , `CodeProcAO` INT(11) NULL DEFAULT NULL , `JoursCompte` VARCHAR(27) NULL DEFAULT NULL , `RefPPM` BIGINT(20) NOT NULL , `NiveauActu` INT(19) NOT NULL , `DateActualisation` DATETIME NOT NULL , PRIMARY KEY (`IDhist`)) ENGINE = InnoDB;
ALTER TABLE `t_dao` ADD `Statut_DAO` VARCHAR(150) NULL DEFAULT NULL AFTER `DossValider`;

--27/01/2022
ALTER TABLE `t_ppm_marche` ADD `ElaboPPM` VARCHAR(150) NULL DEFAULT NULL AFTER `NumeroPlan`;

--01/02/2022
ALTER TABLE `t_ppm_historiquemarche` CHANGE `NiveauActu` `NiveauActu` INT(19) NULL DEFAULT NULL;
ALTER TABLE `t_ppm_historiquemarche` CHANGE `DateActualisation` `DateActualisation` DATETIME NULL DEFAULT NULL;
ALTER TABLE `t_marche` CHANGE `Convention_ChefFile` `ChefFile` VARCHAR(160) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL;
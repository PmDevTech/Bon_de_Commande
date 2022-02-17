
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

------------------ 03/08/2021 ------------------------
ALTER TABLE `t_ami` ADD `RefMarche` BIGINT(19) NOT NULL AFTER `NumeroDAMI`;

------------------ 04/08/2021 -----------------------
ALTER TABLE `t_ami` CHANGE `DateEdition` `DateEdition` DATETIME NOT NULL, CHANGE `DatePub` `DatePub` DATETIME NOT NULL, CHANGE `DateOuverture` `DateOuverture` DATETIME NOT NULL, CHANGE `DateFinOuverture` `DateFinOuverture` DATETIME NOT NULL, CHANGE `DateSaisie` `DateSaisie` DATETIME NOT NULL, CHANGE `DateModif` `DateModif` DATETIME NULL;

--------------- 05/08/2021 --------------------------
ALTER TABLE `t_consultant` CHANGE `NomDepot` `NomDepot` VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL, CHANGE `TitreDepot` `TitreDepot` VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL, CHANGE `ContactDepot` `ContactDepot` VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL, CHANGE `MailDepot` `MailDepot` VARCHAR(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL, CHANGE `DateDepot` `DateDepot` VARCHAR(19) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL, CHANGE `PT` `PT` SMALLINT(6) NULL, CHANGE `PF` `PF` SMALLINT(6) NULL;
----------- 06/08/2021 ----------------------
ALTER TABLE `t_ami` ADD `CheminDocAMI` VARCHAR(500) NOT NULL AFTER `TexteGeneralites`, ADD `DescriptionAMI` VARCHAR(500) NOT NULL AFTER `CheminDocAMI`;


------06/08/2021-----------
CREATE TABLE `bdpdmold`.`t_spectechcaractpropose` ( `RefSpecCaractPro` BIGINT(19) NOT NULL AUTO_INCREMENT , `RefSpecFournit` INT(11) NOT NULL , `LibelleCaract` VARCHAR(300) NOT NULL , PRIMARY KEY (`RefSpecCaractPro`)) ENGINE = InnoDB;
CREATE TABLE `bdpdmold`.`t_soumiscaractfournitsupl` ( `RefSpecCaract` INT(11) NOT NULL , `RefSoumis` INT(11) NOT NULL , `ValeurOfferte` VARCHAR(100) NOT NULL , `Commentaire` VARCHAR(200) NOT NULL ) ENGINE = InnoDB;
ALTER TABLE `t_soumiscaractfournitsupl` ADD INDEX( `RefSpecCaract`, `ValeurOfferte`,`RefSoumis`);
ALTER TABLE `t_soumiscaractfournitsupl` CHANGE `Commentaire` `Commentaire` VARCHAR(200) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL;

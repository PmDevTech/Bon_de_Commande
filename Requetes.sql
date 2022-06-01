------------------25/05/2022-----------------------
CREATE TABLE `bdpdmtest2`.`t_bc_listebesoins` ( `Id_listebesoins` BIGINT(19) NOT NULL AUTO_INCREMENT , `RefBonCommande` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `RefListeBesoins` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `Designation` VARCHAR(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `Quantite` VARCHAR(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `PrixUnitaire` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `PrixTotal` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , PRIMARY KEY (`Id_listebesoins`)) ENGINE = MyISAM;
ALTER TABLE `t_boncommande` CHANGE `RefBon` `RefBonCommande` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;
ALTER TABLE `t_boncommande` CHANGE `RefMarche` `CodeFournisseur` BIGINT(19) NOT NULL;
ALTER TABLE `t_boncommande` CHANGE `RefLot` `TypeElabBC` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;
ALTER TABLE `t_boncommande` ADD `NumDAO` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL AFTER `TypeElabBC`;
ALTER TABLE `t_boncommande` ADD `CodeLot` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL AFTER `NumDAO`;
ALTER TABLE `t_boncommande` DROP `CodeFournis`;
ALTER TABLE `t_boncommande` DROP `NumContrat`;
ALTER TABLE `t_boncommande` DROP `MontantContrat`;
ALTER TABLE `t_boncommande` ADD `ConditionsPaiement` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `DateCommande`;
ALTER TABLE `t_boncommande` ADD `DelaiLivraison` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `ConditionsPaiement`, ADD `LieuLivraison` VARCHAR(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `DelaiLivraison`;
ALTER TABLE `t_boncommande` CHANGE `DateLivraison` `InstructionSpeciale` VARCHAR(510) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;
ALTER TABLE `t_boncommande` ADD `TVA` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `InstructionSpeciale`, ADD `Remise` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `TVA`, ADD `AutreTaxe` VARCHAR(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `Remise`, ADD `PcrtAutreTaxe` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `AutreTaxe`;
ALTER TABLE `t_boncommande` ADD `EMP_ID` BIGINT(19) NOT NULL AFTER `PcrtAutreTaxe`;
ALTER TABLE `t_boncommande` ADD `IntituleMarche` VARCHAR(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL AFTER `CodeLot`;
ALTER TABLE `t_bc_listebesoins` CHANGE `RefListeBesoins` `RefListeBesoins` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;
ALTER TABLE `t_boncommande` CHANGE `DelaiLivraison` `DelaiLivraison` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL, CHANGE `LieuLivraison` `LieuLivraison` VARCHAR(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;
ALTER TABLE `t_boncommande` CHANGE `InstructionSpeciale` `InstructionSpeciale` VARCHAR(510) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;
ALTER TABLE `t_boncommande` CHANGE `AutreTaxe` `AutreTaxe` VARCHAR(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL, CHANGE `PcrtAutreTaxe` `PcrtAutreTaxe` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

----26/05/2022------
ALTER TABLE `t_boncommande` CHANGE `CodeLot` `RefLot` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL;
ALTER TABLE `t_boncommande` CHANGE `NumDAO` `NumeroDAO` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL;
ALTER TABLE `t_boncommande` CHANGE `TVA` `PcrtTVA` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL, CHANGE `Remise` `PcrtRemise` VARCHAR(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;
ALTER TABLE `t_boncommande` ADD `MontantTVA` DOUBLE(19,2) NOT NULL AFTER `PcrtTVA`;
ALTER TABLE `t_boncommande` ADD `MontantRemise` DOUBLE(19,2) NOT NULL AFTER `PcrtRemise`;
ALTER TABLE `t_boncommande` ADD `MontantAutreTaxe` DOUBLE(19,2) NOT NULL AFTER `PcrtAutreTaxe`;
ALTER TABLE `t_boncommande` ADD `MontantNetHT` DOUBLE(19,2) NOT NULL AFTER `MontantAutreTaxe`, ADD `MontantTotal` DOUBLE(19,2) NOT NULL AFTER `MontantNetHT`, ADD `MontantTotalTTC` DOUBLE(19,2) NOT NULL AFTER `MontantTotal`;
ALTER TABLE `t_boncommande` CHANGE `CodeFournisseur` `CodeFournisseur` SMALLINT(6) NOT NULL;
ALTER TABLE `t_boncommande` CHANGE `DateCommande` `DateCommande` DATETIME NOT NULL;

--27/05/2022---
ALTER TABLE `t_boncommande` ADD `MontantBCHT` VARCHAR(20) NOT NULL AFTER `InstructionSpeciale`;
ALTER TABLE `t_boncommande` CHANGE `MontantTVA` `MontantTVA` DOUBLE(19,5) NOT NULL, CHANGE `MontantRemise` `MontantRemise` DOUBLE(19,5) NOT NULL, CHANGE `MontantAutreTaxe` `MontantAutreTaxe` DOUBLE(19,5) NOT NULL, CHANGE `MontantNetHT` `MontantNetHT` DOUBLE(19,5) NOT NULL, CHANGE `MontantTotal` `MontantTotal` DOUBLE(19,5) NOT NULL, CHANGE `MontantTotalTTC` `MontantTotalTTC` DOUBLE(19,5) NOT NULL;
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixTotal` `PrixTotal` DOUBLE(19,5) NOT NULL;
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixUnitaire` `PrixUnitaire` DOUBLE(19,5) NOT NULL;

----28/05/2022---
ALTER TABLE `t_bc_listebesoins` CHANGE `Quantite` `Quantite` VARCHAR(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL, CHANGE `PrixUnitaire` `PrixUnitaire` DOUBLE(19,5) NULL;
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixUnitaire` `PrixUnitaire` VARCHAR(20) NULL DEFAULT NULL;

---30/05/2022----
ALTER TABLE `t_boncommande` CHANGE `MontantBCHT` `MontantBCHT` DOUBLE(19,2) NOT NULL;
ALTER TABLE `t_boncommande` CHANGE `MontantTVA` `MontantTVA` DOUBLE(19,2) NOT NULL;
ALTER TABLE `t_boncommande` CHANGE `MontantRemise` `MontantRemise` DOUBLE(19,2) NOT NULL;
ALTER TABLE `t_boncommande` CHANGE `MontantAutreTaxe` `MontantAutreTaxe` DOUBLE(19,2) NOT NULL, CHANGE `MontantNetHT` `MontantNetHT` DOUBLE(19,2) NOT NULL, CHANGE `MontantTotal` `MontantTotal` DOUBLE(19,2) NOT NULL, CHANGE `MontantTotalTTC` `MontantTotalTTC` DOUBLE(19,2) NOT NULL;
ALTER TABLE `t_boncommande` ADD `ID_BC` BIGINT(19) NOT NULL AUTO_INCREMENT FIRST, ADD PRIMARY KEY (`ID_BC`);
ALTER TABLE `t_boncommande` ADD `Annee` VARCHAR(10) NOT NULL AFTER `RefBonCommande`;

---01/06/2022----
ALTER TABLE `t_boncommande` ADD `BonValider` VARCHAR(3) NOT NULL AFTER `MontantTotalTTC`;
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixTotal` `PrixTotal` DOUBLE(19,2) NOT NULL;


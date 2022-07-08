-----28/06/2022----
ALTER TABLE `t_boncommande` CHANGE `BonValider` `Statut` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

--08/07/2022--
ALTER TABLE `t_boncommande` DROP `PrixUnitaire`
ALTER TABLE `t_boncommande` ADD `MontantOffre` DOUBLE NOT NULL AFTER `MontantRabais`;
ALTER TABLE `t_boncommande` DROP `Quantite`
DROP TABLE IF EXISTS `worlds`;
CREATE TABLE `worlds` (
  `id` int(10) unsigned NOT NULL,
  `realm` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `dungeons`;
CREATE TABLE `dungeons` (
  `id` int(10) unsigned NOT NULL,
  `world` int(10) unsigned DEFAULT NULL,
  `name` varchar(45) NOT NULL,
  `type` tinyint(3) unsigned NOT NULL COMMENT '0 - classic\n1 - elite\n2 - event',
  `difficulty` smallint(5) unsigned NOT NULL,
  `synergy` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `world_INDEX` (`world`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `battles`;
CREATE TABLE `battles` (
  `id` int(10) unsigned NOT NULL,
  `dungeon` int(10) unsigned NOT NULL,
  `name` varchar(45) NOT NULL,
  `stamina` smallint(5) unsigned NOT NULL,
  `times_run` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `FK_Battle_Dungeon_idx` (`dungeon`),
  CONSTRAINT `FK_battle_dungeon` FOREIGN KEY (`dungeon`) REFERENCES `dungeons` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `items`;
CREATE TABLE `items` (
  `id` int(10) unsigned NOT NULL,
  `name` varchar(45) NOT NULL,
  `rarity` tinyint(3) unsigned NOT NULL,
  `realm` tinyint(3) unsigned DEFAULT NULL,
  `type` tinyint(3) unsigned NOT NULL COMMENT 'equipment,item,orb',
  `subtype` tinyint(4) unsigned DEFAULT NULL COMMENT 'type: 1 (equipment), 2 (item), 3 (orb)\ntype 1, subtype:\n1 Dagger\n2 Sword\n3 Spear\n4 Bow\n5 Ball\n6 Axe\n7 Thrown\n8 Katana\n9 Book\n10 Whip\n11 Fist\n12 Staff\n13 Rod\n14 Shield\n15 Helm\n16 Hat\n17 Robe\n18 Light Armor\n19 Armor\n20 Bracer\n21 Accessory\n22 Instrument\n23 Hammer\n\ntype 3, subtype\n1 power\n2 white\n3 black\n4 non-elem\n5 fire\n6 ice\n7 lightning\n8 earth\n9 wind\n10 holy\n11 dark\n12 blue\n13 summon',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `realm_INDEX` (`realm`),
  KEY `rarity_INDEX` (`rarity`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `drops`;
CREATE TABLE `drops` (
  `battleid` int(10) unsigned NOT NULL,
  `itemid` int(10) unsigned NOT NULL,
  `count` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`battleid`,`itemid`),
  CONSTRAINT `FK_drop_battle` FOREIGN KEY (`battleid`) REFERENCES `battles` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `dungeon_drops`;
DROP VIEW IF EXISTS `dungeon_drops`;
CREATE VIEW `dungeon_drops` AS SELECT 
 1 AS `battleid`,
 1 AS `itemid`,
 1 AS `dungeon_name`,
 1 AS `dungeon_type`,
 1 AS `battle_name`,
 1 AS `times_run`,
 1 AS `item_name`,
 1 AS `item_rarity`,
 1 AS `item_realm`,
 1 AS `item_type`,
 1 AS `item_subtype`,
 1 AS `drop_count`;

DROP PROCEDURE IF EXISTS `record_battle_encounter`;
DELIMITER ;;
CREATE PROCEDURE `record_battle_encounter`(
	IN battle_id INT)
BEGIN
	UPDATE battles SET times_run=times_run+1 WHERE id=battle_id;
END ;;
DELIMITER ;

DROP PROCEDURE IF EXISTS `record_drop_event`;
DELIMITER ;;
CREATE PROCEDURE `record_drop_event`(
	IN battle_id INT,
    IN item_id INT,
    IN item_count INT)
BEGIN
	# If it fails, it's probably due to a foreign key violation (There is no record
    # of this battle).  Ignore the error as well as this entire operation.
	INSERT IGNORE INTO drops (battleid,itemid,count) VALUES (battle_id, item_id, item_count)
		ON DUPLICATE KEY UPDATE count=count+item_count;
END ;;

DELIMITER ;
DROP PROCEDURE IF EXISTS `record_dungeon_entry`;
DELIMITER ;;
CREATE PROCEDURE `record_dungeon_entry`(
	IN did INT,
    IN world_id INT,
    IN dname VARCHAR(45),
    IN dtype TINYINT,
    IN ddiff SMALLINT)
BEGIN
	# If it fails, it's because there is already an entry with this dungeon id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO dungeons VALUES (did, world_id, dname, dtype, ddiff);
END ;;
DELIMITER ;
DROP PROCEDURE IF EXISTS `wipe_drops`;
DELIMITER ;;
CREATE PROCEDURE `wipe_drops`()
BEGIN
	TRUNCATE TABLE drops;
    UPDATE battles SET times_run=0;
END ;;
DELIMITER ;

DROP VIEW IF EXISTS `dungeon_drops`;
CREATE ALGORITHM=UNDEFINED 
VIEW `dungeon_drops` AS select `battles`.`id` AS `battleid`,`items`.`id` AS `itemid`,`dungeons`.`name` AS `dungeon_name`,`dungeons`.`type` AS `dungeon_type`,`battles`.`name` AS `battle_name`,`battles`.`times_run` AS `times_run`,`items`.`name` AS `item_name`,`items`.`rarity` AS `item_rarity`,`items`.`realm` AS `item_realm`,`items`.`type` AS `item_type`,`items`.`subtype` AS `item_subtype`,`drops`.`count` AS `drop_count` from (((`dungeons` join `battles`) join `items`) join `drops`) where ((`items`.`id` = `drops`.`itemid`) and (`battles`.`id` = `drops`.`battleid`) and (`dungeons`.`id` = `battles`.`dungeon`));

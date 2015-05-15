
# Create the series table.  The rest of this schema update is mostly
# dedicated to adding the appropriate references from other tables.
CREATE TABLE `series` (
  `id` INT UNSIGNED NOT NULL,
  `name` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));

# Insert known series values.  These are static and a
# description of them is not provided by the game.
INSERT INTO `series` (`id`, `name`) VALUES ('1', 'None');
INSERT INTO `series` (`id`, `name`) VALUES ('101001', 'FF1');
INSERT INTO `series` (`id`, `name`) VALUES ('102001', 'FF2');
INSERT INTO `series` (`id`, `name`) VALUES ('103001', 'FF3');
INSERT INTO `series` (`id`, `name`) VALUES ('104001', 'FF4');
INSERT INTO `series` (`id`, `name`) VALUES ('105001', 'FF5');
INSERT INTO `series` (`id`, `name`) VALUES ('106001', 'FF6');
INSERT INTO `series` (`id`, `name`) VALUES ('107001', 'FF7');
INSERT INTO `series` (`id`, `name`) VALUES ('108001', 'FF8');
INSERT INTO `series` (`id`, `name`) VALUES ('109001', 'FF9');
INSERT INTO `series` (`id`, `name`) VALUES ('110001', 'FF10');
INSERT INTO `series` (`id`, `name`) VALUES ('111001', 'FF11');
INSERT INTO `series` (`id`, `name`) VALUES ('112001', 'FF12');
INSERT INTO `series` (`id`, `name`) VALUES ('113001', 'FF13');
INSERT INTO `series` (`id`, `name`) VALUES ('200001', 'Core');
INSERT INTO `series` (`id`, `name`) VALUES ('300001', 'Variable');

# Make the world.series table non-null.
ALTER TABLE `worlds` 
CHANGE COLUMN `series` `series` INT(10) UNSIGNED NOT NULL ;

# Add a foreign key constraint on worlds.series referencing the series
# table.
ALTER TABLE `worlds` 
ADD INDEX `FK_world_series_idx` (`series` ASC);

ALTER TABLE `worlds` 
ADD CONSTRAINT `FK_world_series`
  FOREIGN KEY (`series`)
  REFERENCES `series` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

# Add a 'series' column to the dungeon table.  This can be NULL, in
# which case a dungeon's series is that of its parent world.
ALTER TABLE `dungeons` 
ADD COLUMN `series` INT UNSIGNED NULL DEFAULT NULL AFTER `world`,
ADD INDEX `FK_dungeon_series_idx` (`series` ASC);

# Add a foreign key constraint on dungeons.series referencing the series
# table.
ALTER TABLE `dungeons` 
ADD CONSTRAINT `FK_dungeon_series`
  FOREIGN KEY (`series`)
  REFERENCES `series` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

# Add a foreign key constraint on dungeons.world referencing the worlds
# table.
ALTER TABLE `dungeons` 
CHANGE COLUMN `world` `world` INT(10) UNSIGNED NOT NULL ;

ALTER TABLE `dungeons` 
ADD CONSTRAINT `FK_dungeon_world`
  FOREIGN KEY (`world`)
  REFERENCES `worlds` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

# Change the 'realm' column to be called 'series', and give it a foreign key
# constraint on the 'series' table.
ALTER TABLE `items` 
CHANGE COLUMN `realm` `series` INT UNSIGNED NULL DEFAULT NULL ;

# Update existing series from the previous realm value to the new series value
# so that we can apply the foreign key constraint.
UPDATE `items` SET `series`= '101001' WHERE `series`=1;
UPDATE `items` SET `series`= '102001' WHERE `series`=2;
UPDATE `items` SET `series`= '103001' WHERE `series`=3;
UPDATE `items` SET `series`= '104001' WHERE `series`=4;
UPDATE `items` SET `series`= '105001' WHERE `series`=5;
UPDATE `items` SET `series`= '106001' WHERE `series`=6;
UPDATE `items` SET `series`= '107001' WHERE `series`=7;
UPDATE `items` SET `series`= '108001' WHERE `series`=8;
UPDATE `items` SET `series`= '109001' WHERE `series`=9;
UPDATE `items` SET `series`= '110001' WHERE `series`=10;
UPDATE `items` SET `series`= '111001' WHERE `series`=11;
UPDATE `items` SET `series`= '112001' WHERE `series`=12;
UPDATE `items` SET `series`= '113001' WHERE `series`=13;
UPDATE `items` SET `series`= '200001' WHERE `series`=0;

ALTER TABLE `items` 
ADD CONSTRAINT `FK_item_series`
  FOREIGN KEY (`series`)
  REFERENCES `series` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


# Fix the dungeon_drops view since the 'realm' column was renamed to 'series'
CREATE OR REPLACE 
VIEW `dungeon_drops` AS
    SELECT 
        `battles`.`id` AS `battleid`,
        `items`.`id` AS `itemid`,
        `enemies`.`id` AS `enemyid`,
        `enemies`.`name` AS `enemy_name`,
        `dungeons`.`id` AS `dungeon_id`,
        `dungeons`.`name` AS `dungeon_name`,
        `dungeons`.`type` AS `dungeon_type`,
        `battles`.`name` AS `battle_name`,
        `battles`.`times_run` AS `times_run`,
        `battles`.`stamina` AS `battle_stamina`,
        `items`.`name` AS `item_name`,
        `items`.`rarity` AS `item_rarity`,
        `items`.`series` AS `item_series`,
        `items`.`type` AS `item_type`,
        `items`.`subtype` AS `item_subtype`,
        `drops`.`count` AS `drop_count`
    FROM
        ((((`dungeons` JOIN `battles`) JOIN `items`) JOIN `drops`) JOIN `enemies`)
    WHERE
        ((`items`.`id` = `drops`.`itemid`)
            AND (`battles`.`id` = `drops`.`battleid`)
            AND (`dungeons`.`id` = `battles`.`dungeon`)
            AND (`enemies`.`id` = `drops`.`enemyid`));

# Fix the insert_dungeon_entry proc so that it can receive the series as a param.
DROP procedure IF EXISTS `insert_dungeon_entry`;

DELIMITER $$
CREATE PROCEDURE `insert_dungeon_entry` (
	IN did INT,
    IN world_id INT,
    IN series_id INT,
    IN dname VARCHAR(45),
    IN dtype TINYINT,
    IN ddiff SMALLINT,
    IN dsyn TINYINT)
BEGIN
	# If it fails, it's because there is already an entry with this dungeon id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO dungeons VALUES (did, world_id, series_id, dname, dtype, ddiff, dsyn);
END$$

DELIMITER ;

INSERT INTO schema_version VALUES (6);

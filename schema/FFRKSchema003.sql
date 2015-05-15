ALTER TABLE `drops` 
ADD COLUMN `enemyid` INT(10) UNSIGNED NOT NULL AFTER `itemid`,
DROP PRIMARY KEY,
ADD PRIMARY KEY (`battleid`, `itemid`, `enemyid`);

ALTER TABLE `dungeons` 
CHANGE COLUMN `difficulty` `difficulty` SMALLINT UNSIGNED NOT NULL ;

CREATE TABLE `enemies` (
  `id` INT UNSIGNED NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`));

CREATE TABLE `schema_version` (
  `version` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`version`),
  UNIQUE INDEX `version_UNIQUE` (`version` ASC));

INSERT INTO `schema_version` (`version`) VALUES ('1');
INSERT INTO `schema_version` (`version`) VALUES ('2');
INSERT INTO `schema_version` (`version`) VALUES ('3');

CREATE OR REPLACE VIEW `dungeon_drops` AS
    SELECT 
        `battles`.`id` AS `battleid`,
        `items`.`id` AS `itemid`,
        `enemies`.`id` AS `enemyid`,
        `enemies`.`name` AS `enemy_name`,
        `dungeons`.`name` AS `dungeon_name`,
        `dungeons`.`type` AS `dungeon_type`,
        `battles`.`name` AS `battle_name`,
        `battles`.`times_run` AS `times_run`,
        `items`.`name` AS `item_name`,
        `items`.`rarity` AS `item_rarity`,
        `items`.`realm` AS `item_realm`,
        `items`.`type` AS `item_type`,
        `items`.`subtype` AS `item_subtype`,
        `drops`.`count` AS `drop_count`
    FROM
        (((`dungeons`
        JOIN `battles`)
        JOIN `items`)
        JOIN `drops`
        JOIN `enemies`)
    WHERE
        ((`items`.`id` = `drops`.`itemid`)
            AND (`battles`.`id` = `drops`.`battleid`)
            AND (`dungeons`.`id` = `battles`.`dungeon`)
            AND (`enemies`.`id` = `drops`.`enemyid`));

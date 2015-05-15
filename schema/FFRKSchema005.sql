DROP procedure IF EXISTS `record_drop_event`;

ALTER TABLE `drops` 
CHANGE COLUMN `enemyid` `enemyid` INT(10) UNSIGNED NOT NULL AFTER `itemid`,
ADD INDEX `FK_drop_enemy_idx` (`enemyid` ASC);

ALTER TABLE `drops`
ADD CONSTRAINT `FK_drop_enemy` FOREIGN KEY (`enemyid`) REFERENCES `enemies` (`id`)
	ON DELETE NO ACTION
	ON UPDATE NO ACTION;

DROP procedure IF EXISTS `insert_enemy_entry`;

DELIMITER $$
CREATE PROCEDURE `insert_enemy_entry` (
	IN enemy_id INT,
    IN enemy_name VARCHAR(45))
BEGIN
	# If it fails, it's because there is already an entry with this battle id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO enemies (id, name) VALUES (enemy_id, enemy_name);
END
$$

DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `record_drop_event`(
	IN battle_id INT,
    IN item_id INT,
    IN enemy_id INT,
    IN item_count INT)
BEGIN
	# If it fails, it's probably due to a foreign key violation (There is no record
    # of this battle).  Ignore the error as well as this entire operation.
	INSERT IGNORE INTO drops (battleid,itemid,enemyid,count) VALUES (battle_id, item_id, enemy_id, item_count)
		ON DUPLICATE KEY UPDATE count=count+item_count;
END$$

DELIMITER ;

CREATE OR REPLACE VIEW `dungeon_drops` AS
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
        `items`.`realm` AS `item_realm`,
        `items`.`type` AS `item_type`,
        `items`.`subtype` AS `item_subtype`,
        `drops`.`count` AS `drop_count`
    FROM
        ((((`dungeons`
        JOIN `battles`)
        JOIN `items`)
        JOIN `drops`)
        JOIN `enemies`)
    WHERE
        ((`items`.`id` = `drops`.`itemid`)
            AND (`battles`.`id` = `drops`.`battleid`)
            AND (`dungeons`.`id` = `battles`.`dungeon`)
            AND (`enemies`.`id` = `drops`.`enemyid`));


INSERT INTO schema_version VALUES (5);

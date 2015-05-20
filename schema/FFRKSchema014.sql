# Change the per battle drops table to store a histogram of observations
# and samples.  This allows us to not lose any information about the sample
# set, while remaining memory efficient.

ALTER TABLE `per_battle_drops` 
ADD COLUMN `histo_bucket` INT(10) NOT NULL DEFAULT -1 AFTER `itemid`,
CHANGE COLUMN `count` `histo_value` INT(10) UNSIGNED NOT NULL AFTER `histo_bucket`,
DROP COLUMN `stdev_sum_of_squares_of_drops`,
DROP COLUMN `stdev_sum_of_drops`,
DROP PRIMARY KEY,
ADD PRIMARY KEY (`battleid`, `itemid`, `histo_bucket`);

# Remove the default value now that it's been populated with all -1s
ALTER TABLE `per_battle_drops`
CHANGE COLUMN `histo_bucket` `histo_bucket` INT(10) NOT NULL;

ALTER TABLE `per_enemy_drops` 
ADD COLUMN `histo_bucket` INT(10) NOT NULL DEFAULT -1 AFTER `enemyid`,
CHANGE COLUMN `count` `histo_value` INT(10) UNSIGNED NOT NULL AFTER `histo_bucket`,
DROP PRIMARY KEY,
ADD PRIMARY KEY (`battleid`, `itemid`, `enemyid`, `histo_bucket`);

# Remove the default value now that it's been populated with all -1s
ALTER TABLE `per_enemy_drops`
CHANGE COLUMN `histo_bucket` `histo_bucket` INT(10) NOT NULL;

ALTER TABLE `battles` 
CHANGE COLUMN `times_run` `samples` INT(10) UNSIGNED NOT NULL,
DROP COLUMN `stdev_times_run`,
ADD COLUMN `histo_samples` INT(10) UNSIGNED NOT NULL AFTER `samples`;


DROP procedure IF EXISTS `record_drops_for_battle`;

DELIMITER $$
CREATE PROCEDURE `record_drops_for_battle`(
	IN battle_id INT,
    IN item_id INT,
    IN item_count INT)
BEGIN
	# If it fails, it's probably due to a foreign key violation (There is no record
    # of this battle).  Ignore the error as well as this entire operation.
	INSERT IGNORE INTO per_battle_drops (battleid, itemid, histo_bucket, histo_value)
					  VALUES (battle_id, item_id, -1, item_count),  # The "global" bucket is the total number of drops received.
							 (battle_id, item_id, item_count, 1)    # The count-specific bucket goes up by 1 each time that many drops is observed.
		ON DUPLICATE KEY UPDATE histo_value = histo_value+VALUES(histo_value);
END$$

DELIMITER ;

DROP procedure IF EXISTS `record_drops_for_battle_and_enemy`;

DELIMITER $$
CREATE PROCEDURE `record_drops_for_battle_and_enemy`(
	IN battle_id INT,
    IN item_id INT,
    IN enemy_id INT,
    IN item_count INT)
BEGIN
	# If it fails, it's probably due to a foreign key violation (There is no record
    # of this battle).  Ignore the error as well as this entire operation.
	INSERT IGNORE INTO per_enemy_drops (battleid,  itemid,  enemyid,  histo_bucket, histo_value)
					  VALUES (battle_id, item_id, enemy_id, -1, item_count),    # The "global" bucket is the total number of drops received.
							 (battle_id, item_id, enemy_id, item_count, 1)      # The count-specific bucket goes up by 1 each time that many drops is observed.
		ON DUPLICATE KEY UPDATE histo_value=histo_value+VALUES(histo_value);
END$$

DELIMITER ;

DROP procedure IF EXISTS `record_battle_encounter`;

DELIMITER $$
CREATE PROCEDURE `record_battle_encounter`(
	IN battle_id INT)
BEGIN
	UPDATE battles SET samples=samples+1, histo_samples=histo_samples+1 WHERE id=battle_id;
END$$

DELIMITER ;

CREATE OR REPLACE VIEW `dungeon_drops` AS
    SELECT 
        `battles`.`id` AS `battleid`,
        `items`.`id` AS `itemid`,
        `dungeons`.`id` AS `dungeon_id`,
        `dungeons`.`name` AS `dungeon_name`,
        `dungeons`.`type` AS `dungeon_type`,
        `battles`.`name` AS `battle_name`,
        `battles`.`samples` AS `times_run`,
        `battles`.`stamina` AS `battle_stamina`,
        `battles`.`histo_samples` AS `times_run_with_histogram`,
        `items`.`name` AS `item_name`,
        `items`.`rarity` AS `item_rarity`,
        `items`.`series` AS `item_series`,
        `items`.`type` AS `item_type`,
        `items`.`subtype` AS `item_subtype`,
        `per_battle_drops`.`histo_bucket` AS `histo_bucket`,
        `per_battle_drops`.`histo_value` AS `histo_value`
    FROM
        (((`dungeons`
        JOIN `battles`)
        JOIN `items`)
        JOIN `per_battle_drops`)
    WHERE
        ((`dungeons`.`id` = `battles`.`dungeon`)
            AND (`battles`.`id` = `per_battle_drops`.`battleid`)
            AND (`items`.`id` = `per_battle_drops`.`itemid`));

INSERT INTO schema_version VALUES (14, true);
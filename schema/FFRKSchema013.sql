# Add support to the schema for computing confidence level of statistics.
# The idea is to store the sum of drop counts and the sum of squares so
# that the client application can use these fields to compute standard
# deviation.  Since this information was previously not recorded, we use
# a separate column instead of battles.times_run and drops.count.  Averages
# will still be displayed off of the whole data set, but confidence interval
# will be computed only based on samples that were recorded since this
# collection began.
ALTER TABLE `battles` 
ADD COLUMN `stdev_times_run` INT UNSIGNED NOT NULL DEFAULT 0 AFTER `times_run`;

# Table is renamed to make it clear that there are now 2 drops tables.  One that
# breaks drops up by enemies, and one that doesn't.
ALTER TABLE `drops` 
RENAME TO  `per_enemy_drops` ;


CREATE TABLE `per_battle_drops` (
  `battleid` INT UNSIGNED NOT NULL,
  `itemid` INT UNSIGNED NOT NULL,
  `count` INT UNSIGNED NULL,
  `stdev_sum_of_drops` INT UNSIGNED NOT NULL,
  `stdev_sum_of_squares_of_drops` BIGINT UNSIGNED NOT NULL,
  PRIMARY KEY (`battleid`, `itemid`),
  INDEX `FK_per_battle_drop_item_idx` (`itemid` ASC),
  CONSTRAINT `FK_per_battle_drop_battle`
    FOREIGN KEY (`battleid`)
    REFERENCES `battles` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_per_battle_drop_item`
    FOREIGN KEY (`itemid`)
    REFERENCES `items` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

DROP procedure IF EXISTS `record_battle_encounter`;

DELIMITER $$
CREATE PROCEDURE `record_battle_encounter`(
	IN battle_id INT)
BEGIN
	UPDATE battles SET times_run=times_run+1, stdev_times_run=stdev_times_run+1 WHERE id=battle_id;
END$$

DELIMITER ;


# `record_drop_event` is renamed to more clearly indicate it splits drops by enemy.
# Also updates the INSERT statement to use the new table name.
DROP procedure IF EXISTS `record_drop_event`;
DELIMITER $$
CREATE PROCEDURE `record_drops_for_battle_and_enemy`(
	IN battle_id INT,
    IN item_id INT,
    IN enemy_id INT,
    IN item_count INT)
BEGIN
	# If it fails, it's probably due to a foreign key violation (There is no record
    # of this battle).  Ignore the error as well as this entire operation.
	INSERT IGNORE INTO per_enemy_drops (battleid,  itemid,  enemyid,  count)
					  VALUES (battle_id, item_id, enemy_id, item_count)
		ON DUPLICATE KEY UPDATE count=count+item_count;
END$$

DELIMITER ;

# A new procedure `record_drops_for_battle` is introduced which lumps together all drops of the same
# item, regardless of what enemy dropped it.
DROP procedure IF EXISTS `record_drops_for_battle`;

DELIMITER $$
CREATE PROCEDURE `record_drops_for_battle`(
	IN battle_id INT,
    IN item_id INT,
    IN item_count INT)
BEGIN
	DECLARE item_count_squared INT;
    SET item_count_squared = item_count*item_count;
	# If it fails, it's probably due to a foreign key violation (There is no record
    # of this battle).  Ignore the error as well as this entire operation.
	INSERT IGNORE INTO per_battle_drops (battleid,  itemid,  count, stdev_sum_of_drops, stdev_sum_of_squares_of_drops)
					  VALUES (battle_id, item_id, item_count, item_count, item_count_squared)
		ON DUPLICATE KEY UPDATE count=count+item_count,
								stdev_sum_of_drops=stdev_sum_of_drops+item_count,
                                stdev_sum_of_squares_of_drops=stdev_sum_of_squares_of_drops+item_count_squared;
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
        `battles`.`times_run` AS `times_run`,
        `battles`.`stdev_times_run` AS `stdev_samples`,
        `battles`.`stamina` AS `battle_stamina`,
        `items`.`name` AS `item_name`,
        `items`.`rarity` AS `item_rarity`,
        `items`.`series` AS `item_series`,
        `items`.`type` AS `item_type`,
        `items`.`subtype` AS `item_subtype`,
        `per_battle_drops`.`count` AS `total_drops`,
        `per_battle_drops`.`stdev_sum_of_drops` AS `stdev_sum_of_drops`,
        `per_battle_drops`.`stdev_sum_of_squares_of_drops` AS `stdev_sum_of_squares_of_drops`
    FROM
        (((`dungeons`
        JOIN `battles`)
        JOIN `items`)
        JOIN `per_battle_drops`)
    WHERE
        ((`dungeons`.`id` = `battles`.`dungeon`)
            AND (`battles`.`id` = `per_battle_drops`.`battleid`)
            AND (`items`.`id` = `per_battle_drops`.`itemid`));

# This procedure is the same as it was before, except there is 1 more column in the table it
# inserts to, so we need to update the INSERT statement accordingly.
DROP procedure IF EXISTS `insert_battle_entry`;
DELIMITER $$
CREATE PROCEDURE `insert_battle_entry`(
	IN bid INT,
    IN did INT,
    IN bname VARCHAR(100),
    IN bstam SMALLINT)
BEGIN
	# If it fails, it's because there is already an entry with this battle id.
    # Ignore the insertion, as well as this entire operation.
    
    # Even though `stdev_times_run` has a default value, for some reason we
    # still need to explicitly specify it as 0 here.
	INSERT IGNORE INTO battles VALUES (bid, did, bname, bstam, 0, 0);
END$$

DELIMITER ;

# Copy over any data that was in the per enemy drops table.  Grouping by battleid, itemid only returns
# a single row for each unique (battleid, itemid) combination, which merges all entries with the same
# enemy id.  And summing on count makes sure that all items which are merged have their count column
# summed.
INSERT INTO per_battle_drops (battleid, itemid, count, stdev_sum_of_drops, stdev_sum_of_squares_of_drops)
	SELECT battleid, itemid, SUM(count), 0, 0 FROM per_enemy_drops GROUP BY battleid, itemid;


INSERT INTO schema_version VALUES (13, true);
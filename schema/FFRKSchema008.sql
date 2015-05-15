# Change the dungeon_drops view to group by battle id and item id.
# In other words, stop splitting up the results by enemy.  This
# provides a more accurate view of each battle's drop rates for
# individual items.
CREATE OR REPLACE 
VIEW `dungeon_drops` AS
	SELECT 
		`battles`.`id` AS `battleid`,
		`items`.`id` AS `itemid`,
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
		SUM(`drops`.`count`) AS `drop_count`
	FROM
		(((`dungeons`
		JOIN `battles`)
		JOIN `items`)
		JOIN `drops`)
	WHERE
		((`items`.`id` = `drops`.`itemid`)
			AND (`battles`.`id` = `drops`.`battleid`)
			AND (`dungeons`.`id` = `battles`.`dungeon`))
	GROUP BY `battles`.`id`, `items`.`id`;

INSERT INTO `schema_version` VALUES (8)
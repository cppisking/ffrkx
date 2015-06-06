# Add world information to the `dungeon_drops` table.

CREATE OR REPLACE VIEW `dungeon_drops` AS
    SELECT 
        `battles`.`id` AS `battleid`,
        `items`.`id` AS `itemid`,
        `worlds`.`id` AS `world_id`,
        `worlds`.`name` AS `world_name`,
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
        ((((`dungeons`
        JOIN `battles`)
        JOIN `items`)
        JOIN `per_battle_drops`)
        JOIN `worlds`)
    WHERE
        ((`dungeons`.`id` = `battles`.`dungeon`)
            AND (`battles`.`id` = `per_battle_drops`.`battleid`)
            AND (`items`.`id` = `per_battle_drops`.`itemid`)
            AND (`dungeons`.`world` = `worlds`.`id`));
            
INSERT INTO `schema_version` VALUES (19, 1)
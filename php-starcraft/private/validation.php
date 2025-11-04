<?php

/**
 * Summary of validate_unit_input
 * @param mixed $unit_name
 * @param mixed $race
 * @param mixed $unit_type
 * @param mixed $health
 * @param mixed $shield
 * @param mixed $armor
 * @param mixed $attack_damage
 * @param mixed $attack_range
 * @param mixed $movement_speed
 * @param mixed $mineral_cost
 * @param mixed $gas_cost
 * @param mixed $build_time
 * @param mixed $description
 * @param mixed $tags
 * @param mixed $has_cloak
 * @param mixed $img_description
 * @param mixed $file_name
 * @param mixed $unit_types
 * @param mixed $races
 * @return array{data: array, errors: string[], is_valid: bool}
 */
function validate_unit_input($unit_name, $race, $unit_type, $health, $shield, $armor, $attack_damage, $attack_range, $movement_speed, $mineral_cost, $gas_cost, $build_time, $description, $tags, $has_cloak, $img_description, $file_name, $unit_types, $races)
{


    global $connection;
    $errors = [];
    $validated_data = [];

    // Validate Unit Name
    $unit_name = trim($unit_name);
    if (empty($unit_name)) {
        $errors[] = "Unit Name is required.";
    } elseif (strlen($unit_name) > 50) {
        $errors[] = "Unit Name must be less than or equal to 50 characters.";
    }




    $validated_data['name'] = $unit_name;


    //Race Check
    if (empty($race)) {
        $errors[] = "Race is required.";
    } elseif (!in_array($race, $races)) {
        $errors[] = "Invalid Race selected.";
    }

    $validated_data['race'] = $race;



    //unit type Check
    if (empty($unit_type)) {
        $errors[] = "Unit Type is required.";
    } elseif (!in_array($unit_type, $unit_types)) {
        $errors[] = "Invalid Unit Type selected.";
    }

    $validated_data['unit_type'] = $unit_type;




    // Validate Health
    $health = filter_var($health, FILTER_SANITIZE_NUMBER_INT);
    if ($health === false || $health === '' || $health === null) {
        $errors[] = "Health is required.";
    } elseif (!filter_var($health, FILTER_VALIDATE_INT, ["options" => ["min_range" => 1, "max_range" => 500]])) {
        $errors[] = "Health should be between (1-500).";
    }

    $validated_data['health'] = $health;


    // Validate Shield
    $shield = filter_var($shield, FILTER_SANITIZE_NUMBER_INT);

    if ($shield === '' || $shield === null) {
        $errors[] = "Shield is required.";
    } else {
        $valid = filter_var((int) $shield, FILTER_VALIDATE_INT, ["options" => ["min_range" => 0, "max_range" => 500]]);
        if ($valid === false) {
            $errors[] = "Shield should be between (0-500).";
        }
    }

    $validated_data['shield'] = $shield;

    // Validate Attack Damage
    $attack_damage = filter_var($attack_damage, FILTER_SANITIZE_NUMBER_INT);
    if ($attack_damage === '' || $attack_damage === null) {
        $errors[] = "Attack Damage is required.";
    } elseif (filter_var($attack_damage, FILTER_VALIDATE_INT, ["options" => ["min_range" => 0, "max_range" => 500]]) === false) {
        $errors[] = "Attack Damage should be between (0-500).";
    }
    $validated_data['attack_damage'] = $attack_damage;

    // Validate Armor
    $armor = filter_var($armor, FILTER_SANITIZE_NUMBER_INT);
    if ($armor === '' || $armor === null) {
        $errors[] = "Armor is required.";
    } elseif (filter_var($armor, FILTER_VALIDATE_INT, ["options" => ["min_range" => 0, "max_range" => 500]]) === false) {
        $errors[] = "Armor should be between (0-500).";
    }
    $validated_data['armor'] = $armor;

    // Validate Attack Range
    $attack_range = filter_var($attack_range, FILTER_SANITIZE_NUMBER_INT);
    if ($attack_range === '' || $attack_range === null) {
        $errors[] = "Attack Range is required.";
    } elseif (filter_var($attack_range, FILTER_VALIDATE_INT, ["options" => ["min_range" => 0, "max_range" => 20]]) === false) {
        $errors[] = "Attack Range should be between (0-20).";
    }
    $validated_data['attack_range'] = $attack_range;


    // Validate Movement Speed
    // Decimal(4,2) so changed a bit for filter_var
    $movement_speed = filter_var($movement_speed, FILTER_SANITIZE_NUMBER_FLOAT, FILTER_FLAG_ALLOW_FRACTION);

    if ($movement_speed === '' || $movement_speed === null) {
        $errors[] = "Movement speed is required.";
    } elseif (!is_numeric($movement_speed) || $movement_speed < 0 || $movement_speed > 10) {
        $errors[] = "Movement speed should be a valid number between 0 and 10.";
    } else {

        $movement_speed = round((float)$movement_speed, 2);
        $validated_data['movement_speed'] = $movement_speed;
    }


    // Validate Mineral Cost
    $mineral_cost = filter_var($mineral_cost, FILTER_SANITIZE_NUMBER_INT);
    if ($mineral_cost === '' || $mineral_cost === null) {
        $errors[] = "Mineral_cost is required.";
    } elseif (!filter_var($mineral_cost, FILTER_VALIDATE_INT, ["options" => ["min_range" => 1, "max_range" => 1000]])) {
        $errors[] = "Mineral_cost should be between (1-1000).";
    }

    $validated_data['mineral_cost'] = $mineral_cost;



    // Validate Gas Cost
    $gas_cost = filter_var($gas_cost, FILTER_SANITIZE_NUMBER_INT);
    if ($gas_cost === '' || $gas_cost === null) {
        $errors[] = "Gas_cost is required.";
    } elseif (filter_var($gas_cost, FILTER_VALIDATE_INT, ["options" => ["min_range" => 0, "max_range" => 1000]]) === false) {
        $errors[] = "Gas_cost should be between (0-1000).";
    }

    $validated_data['gas_cost'] = $gas_cost;



    // Validate Build Time
    $build_time = filter_var($build_time, FILTER_SANITIZE_NUMBER_INT);
    if ($build_time === false || $build_time === '' || $build_time === null) {
        $errors[] = "Build time is required.";
    } elseif (!filter_var($build_time, FILTER_VALIDATE_INT, ["options" => ["min_range" => 1, "max_range" => 500]])) {
        $errors[] = "Build Time should be between (1-500).";
    }

    $validated_data['build_time'] = $build_time;




    // Validate Description
    $description = trim($description);
    if (empty($description)) {
        $errors[] = "Description is required.";
    } elseif (strlen($description) > 128) {
        $errors[] = "Description must be less than or equal to 128 characters.";
    }

    $validated_data['description'] = $description;


    // Validate Tags
    $tags = trim($tags);
    if (empty($tags)) {
        $validated_data['tags'] = null;
    } elseif (strlen($tags) > 255) {
        $errors[] = "Tags must be less than or equal to 255 characters.";
    } else {
        $tags_array = explode(',', $tags);
        $tags_array = array_map('trim', $tags_array);
        $tags = implode(',', $tags_array);
        $validated_data['tags'] = $tags;
    }


    // Validate Has Cloaking
    if (!isset($has_cloak)) {
        $errors[] = "Has Cloaking selection is missing.";
    } elseif ($has_cloak !== '1' && $has_cloak !== '0') {
        $errors[] = "Invalid selection for Has Cloaking.";
    } else {
        $validated_data['has_cloak'] = ($has_cloak === '1');
    }


    // Validate Image Description

    $img_description = trim($img_description);
    if (empty($img_description)) {
        $validated_data['img_description'] = null;
    } elseif (strlen($img_description) > 128) {
        $errors[] = "Unit Name must be less than or equal to 128 characters.";
    } else {
        $validated_data['img_description'] = $img_description;
    }


    // Validate Image Description

    $file_name = trim($file_name);
    if (empty($file_name)) {
        $errors[] = "File Name is required.";
    } elseif (strlen($file_name) > 255) {
        $errors[] = "File Name must be less than or equal to 255 characters.";
    } else {
        $validated_data['file_name'] = $file_name;
    }



    // A function can only return one value, so we're packing a few things into an array.
    return [
        'is_valid' => empty($errors),

        'errors' => $errors,
        'data' => $validated_data
    ];
}



function validate_picture_name($file_name)
{


    global $connection;
    $errors = [];
    $validated_data = [];


    // Validate Image Description

    $file_name = trim($file_name);
    if (empty($file_name)) {
        $errors[] = "File Name is required.";
    } elseif (strlen($file_name) > 128) {
        $errors[] = "File Name must be less than or equal to 128 characters.";
    } else {
        $validated_data['file_name'] = $file_name;
    }

    return [
        'is_valid' => empty($errors),
        'errors' => $errors,
        'data' => $validated_data
    ];
}

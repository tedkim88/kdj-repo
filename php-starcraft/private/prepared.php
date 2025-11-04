<?php

/*
    This script will use prepared statements, which adds a layer of abstraction between our user's (potentially dangerous) input and the SQL statements that we're executing. 

    NOTE: If we're only reading out data to the user and not accepting any input from something like a web form, we don't really need to use prepared statements because everything is procedural at that point; however, we're going to use this method for all of the other pages in our CRUD application, so we'll try to get in the habit of using it now (and set up this file for later additions). 

    Just like our simple MySQLi methods, using prepared statements for our queries means we need to follow a certain series of events. 

    1. Make sure we're connected to the database (this is in our included header.php file).
    2. Write the SQL query with placeholders (?) for each parameter.
    3. Prepare the query using $connection->prepare($query) while handling any errors if this fails.
    4. Bind the input values to the placeholders in the query using $statement->bind_param() and specify the data type of each parameter.
    5. Pass the variables or values as arguments to bind_param().
    6. Call $statement->execute() to execute the query with the bound parameters.
    7. For SELECT queries, retrieve the result set using $statement->get_result().
    8. Close the prepared statement after finished to free up server resources.
*/

function execute_prepared_statement($query, $params = [], $types = "") {
    global $connection;

    $statement = $connection->prepare($query);

    // If our preparations fail, we need to handle the error and quit this function.
    if (!$statement) {
        die("Preparation failed: " . $connection->error);
    }

    // If we need to bind any parameters (i.e. if we're adding, editing, or deleting), we'll do so here. 
    if (!empty($params)) {
        $statement->bind_param($types, ...$params);
    }

    // This executes the statement right from within our IF condition. If it's FALSE or doesn't work for whatever reason, we'll handle the error and quit the function.
    if (!$statement->execute()) {
        die("Execution failed: " . $connection->error);
    }

    // If it's a SELECT query, we should return the results so that we can print them out for the user. 
    if (str_starts_with($query, "SELECT")) {
        return $statement->get_result();
    }

    // If it's NOT a SELECT query and we successfully executed our prepared statement, we'll just return TRUE to indicate it was successful.
    return TRUE;
}



/**
 * Summary of get_all_movies
 * @return array
 */
function get_all_units() {
    $query = "SELECT * FROM dkim54_catalogue_items;";
    $result = execute_prepared_statement($query);

    return $result->fetch_all(MYSQLI_ASSOC);
}





/**
 * Summary of insert_unit
 * @param mixed $name
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
 * @return bool|mysqli_result
 */
function insert_unit($name, $race, $unit_type, $health, $shield, $armor, $attack_damage, $attack_range, $movement_speed, $mineral_cost, $gas_cost, $build_time, $description, $tags, $has_cloak, $img_description, $file_name) {
    $query = "INSERT INTO dkim54_catalogue_items
        (name, race, unit_type, health, shield, armor, attack_damage, attack_range, movement_speed, mineral_cost, gas_cost, build_time, description, tags, has_cloak, img_description, filename)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
        
    return execute_prepared_statement(
        $query, 
        [$name, $race, $unit_type, $health, $shield, $armor, $attack_damage, $attack_range, $movement_speed, $mineral_cost, $gas_cost, $build_time, $description, $tags, $has_cloak, $img_description, $file_name], 
        "sssiiiiidiiississ"  
    );
}







/**
 * Summary of delete_unit
 * @param mixed $id
 * @return bool|mysqli_result
 */
function delete_unit($id) {
    $query = "DELETE FROM dkim54_catalogue_items WHERE id = ?;";
    return execute_prepared_statement($query, [$id], "i");
}





/**
 * Summary of select_unit_by_id
 * @param mixed $uid
 * @return array|bool|null
 */
function select_unit_by_id($uid) {
    $query = "SELECT * FROM dkim54_catalogue_items WHERE id = ?;";
    $result = execute_prepared_statement($query, [$uid], "i");

    return $result->fetch_assoc();
}




/**
 * Summary of update_unit
 * @param mixed $name
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
 * @param mixed $uid
 * @return bool|mysqli_result
 */
function update_unit($name, $race, $unit_type, $health, $shield, $armor, $attack_damage, $attack_range, $movement_speed, $mineral_cost, $gas_cost, $build_time, $description, $tags, $has_cloak, $img_description, $file_name, $uid) {
    $query = "UPDATE dkim54_catalogue_items SET name = ?, race = ?, unit_type = ?, health = ?, shield = ?, armor  = ?, attack_damage = ?, attack_range = ?, movement_speed = ?, mineral_cost = ?, gas_cost = ?, build_time = ?, description = ?, tags = ?, has_cloak = ?, img_description = ?, filename = ? WHERE id = ?;";

    return execute_prepared_statement($query, [$name, $race, $unit_type, $health, $shield, $armor, $attack_damage, $attack_range, $movement_speed, $mineral_cost, $gas_cost, $build_time, $description, $tags, $has_cloak, $img_description, $file_name, $uid], 
    "sssiiiiidiiississi");
}


function update_pic($file_name, $pid) {
    $query = "UPDATE dkim54_catalogue_admin SET profile_name = ? WHERE account_id = ?;";

    return execute_prepared_statement($query, [$file_name, $pid], 
    "si");
}




?>


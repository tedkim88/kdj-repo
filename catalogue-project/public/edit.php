<?php
session_start();

if(!isset($_SESSION['user_id'])) {
    header("Location: login.php");
    exit;
}




$title = "Edit Units!";
$introduction = "To edit a record in our database, click 'Edit' beside the title you would like to change. Next, add the updated information into the form and hit 'Save'.";
include 'includes/admin-header.php';


$unit_id = $_GET['unit_id'] ?? $_POST['unit-id'] ?? "";
$is_editing = isset($_GET['unit_id']);




$unit = $unit_id ? select_unit_by_id($unit_id) : NULL;


$existing_name = $unit['name'] ?? "";
$existing_race = $unit['race'] ?? "";
$existing_unit_type = $unit['unit_type'] ?? "";
$existing_health = $unit['health'] ?? '1';
$existing_shield = $unit['shield'] ?? "";
$existing_armor = $unit['armor'] ?? "";
$existing_attack_damage = $unit['attack_damage'] ?? "";
$existing_attack_range = $unit['attack_range'] ?? "";
$existing_movement_speed = $unit['movement_speed'] ?? "";
$existing_mineral_cost = $unit['mineral_cost'] ?? "";
$existing_gas_cost = $unit['gas_cost'] ?? "";
$existing_build_time = $unit['build_time'] ?? "";
$existing_description = $unit['description'] ?? "";
$existing_tags = $unit['tags'] ?? "";
$existing_has_cloak = $unit['has_cloak'] ?? "";
$existing_img_description = $unit['img_description'] ?? "";




$user_name = $_POST['name'] ?? "";
$user_race = $_POST['race'] ?? "";
$user_unit_type = $_POST['unit_type'] ?? "";
$user_health = $_POST['health'] ?? '1';  
$user_shield = $_POST['shield'] ?? "";
$user_armor = $_POST['armor'] ?? "";
$user_attack_damage = $_POST['attack_damage'] ?? "";
$user_attack_range = $_POST['attack_range'] ?? "";
$user_movement_speed = $_POST['movement_speed'] ?? "";
$user_mineral_cost = $_POST['mineral_cost'] ?? "";
$user_gas_cost = $_POST['gas_cost'] ?? "";
$user_build_time = $_POST['build_time'] ?? "";
$user_description = $_POST['description'] ?? "";
$user_tags = $_POST['tags'] ?? "";
$user_has_cloak = $_POST['has_cloak'] ?? "";
$user_img_description = $_POST['img_description'] ?? "";


$message = "";
$alert_class = "alert-danger";


include 'includes/upload.php';


if ($message != ""): ?>
    <div class="alert <?= $alert_class; ?>" role="alert">
        <p><?php echo $message; ?></p>
    </div>
<?php endif;


if ($unit_id) : ?>

    <h2 class="fw-light mb-3">Editing <?= $existing_name; ?></h2>
    <?php include 'includes/form.php'; ?>

<? endif;

echo "<h2 class=\"fw-light mb-3 mt-5\">Current Units in Our Database</h2>";

generate_table(function($unit) {
    $unit_id = $unit['id'];
    return "<a href=\"edit.php?unit_id=" . urlencode($unit_id) . "\" class=\"btn btn-warning\">Edit</a>";
}, false, [], 6, 0, true  );

include 'includes/footer.php';

?>
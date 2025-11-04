<?php
session_start();


if(!isset($_SESSION['user_id'])) {
    header("Location: login.php");
    exit;
}


$title = "Delete a game";
$introduction = "To remove a record from our database, click 'Delete' beside the game you would like to remove. You will then be taken to a confirmation page where you can complete the deletion process.";
include 'includes/admin-header.php';

echo "<h2 class=\"fw-light mb-3\">Current Games in Our Database</h2>";


generate_table(function($unit) {
    
    $id = $unit['id'];
    $unit_name = $unit['name'];
    return "<a href=\"delete-confirmation.php?id=" . urlencode($id) . "&unit_name=" . urlencode($unit_name) . "\" class=\"btn btn-danger\">Delete</a>";
}, false, [], 6, 0, true);

include 'includes/footer.php';

?>
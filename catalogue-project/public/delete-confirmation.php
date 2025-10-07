<?php
session_start();

if(!isset($_SESSION['user_id'])) {
    header("Location: login.php");
    exit;
}

$id = filter_input(INPUT_GET, 'id', FILTER_VALIDATE_INT);
$unit_name = filter_input(INPUT_GET, 'unit_name', FILTER_SANITIZE_FULL_SPECIAL_CHARS);

$title = "Deletion Confirmation";
$introduction = "";
include 'includes/admin-header.php';

$message = "";

// This checks to see if there is any information missing from the query string.
if (!$id || !$unit_name) {
    $message = "<p>Please return to the <a href=\"delete.php\" class=\"link-danger\">delete page</a> and select an option from the table.</p>";
}


if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Even though these values are hidden from the user, we are sanitising them because they are coming from $_POST and they can be altered in the browser console before submission.
    $hidden_id = filter_input(INPUT_POST, 'hidden_id', FILTER_VALIDATE_INT);
    $hidden_name = filter_input(INPUT_POST, 'hidden_name', FILTER_SANITIZE_FULL_SPECIAL_CHARS);

    if ($hidden_id) {
        delete_unit($hidden_id);
        $message = "<p>" . urldecode($hidden_name) . " was deleted from the database.</p>";
        $game_id = NULL; // This is to make sure the confirmation / delete form doesn't appear again.
    }
}

// If a message is defined (and there should be if the query string is invalid or if the user has just finished deleting something!), we'll print it out for the user here.
if ($message) : ?>

<div class="alert alert-danger text-center" role="alert">
    <?= $message; ?>
</div>

<?php endif;


if ($id) : ?>

<h4 class="text-danger lead mb-5 text-center">Are you sure that you want to delete <?= urldecode($unit_name); ?>? There is no undo action.</h4>

<!-- Deletion Confirmation Form -->
<form action="<?= htmlspecialchars($_SERVER['PHP_SELF']); ?>" method="POST" class="text-center">
    <!-- Hidden Values -->
    <input type="hidden" name="hidden_id" id="hidden_id" value="<?= $id; ?>">
    <input type="hidden" name="hidden_name" id="hidden_name" value="<?= $unit_name; ?>">

    <!-- Submit Button -->
    <input type="submit" name="confirm" id="confirm" value="Yes, I'm sure." class="btn btn-danger">
</form>

<?php endif; ?>

<!-- No matter what state the page is in, we'll make sure this link is here so the user can navigate back. -->
<p class="text-center mt-5">
    <a href="delete.php" class="btn btn-outline-primary">Return to 'Delete a unit' Page</a>
</p>


<?php

include 'includes/footer.php';

?>
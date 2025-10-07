<?php
session_start();

if(!isset($_SESSION['user_id'])) {
    header("Location: login.php");
    exit;
}


$title = "Add a Starcraft Unit";
$introduction = "To add a new unit to our database, simply fill out the form below and hit 'Save'.";
$is_editing = isset($_GET['unit_id']);

$message = "";
include 'includes/admin-header.php';


include 'includes/upload.php';


                if ($message != "") : ?>

                    <div class="alert p-3 <?= $alert_class ?? 'alert-danger'; ?>" role="alert">
                        <p><?= $message; ?></p>
                    </div>
                <?php endif;

include 'includes/form.php';

include 'includes/footer.php';

?>
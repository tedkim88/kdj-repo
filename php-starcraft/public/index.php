<?php

require_once '../private/authentication.php';

$title = 'Public Page (Home)';
$introduction = 'Welcome to our site! In order to use <strong class="text-danger">CRUD</strong> features, you must be logged in. If you have an account, click <strong class="text-danger">Log in</strong> to access your account page. If you\'re not logged in, you won\'t be able to access the admin area.';
include 'includes/header.php';

echo "<h1 class=\"text-center\"><span class=\"text-danger\">Starcraft Catalogue </span> run by PHP</h1>";
echo '<div class="text-center my-4">';
echo '<img src="../public/images/image.png" alt="Default Image" class="img-fluid">';
echo '<a style="display: block; text-align: center; margin-top: 10px;" target="_blank" href=\'https://www.404media.co/hackers-are-injecting-mass-shooting-videos-into-random-starcraft-2-matches/\'>Image Source</a>';


echo '</div>';


include 'includes/footer.php';

?>
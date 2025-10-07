<?php

// Almost every page in this application will have a header, so we will include or require all of our other dependencies here.
require_once dirname(__DIR__, 2) . '/data/connect.php';
$connection = db_connect();

include '../private/prepared.php';
include '../private/functions.php';
include '../private/validation.php';
?>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title><?php echo $title; ?> | My Watch List</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-LN+7fdVzj6u52u30Kp6M/trliBMCMKTyK833zpbD+pXdCLuTusPj697FH4R/5mcr" crossorigin="anonymous">
</head>

<body class="min-vh-100 d-flex flex-column justify-content-between">
    <header class="text-center p-3 bg-dark">
        <nav class="my-5 text-center">
            <a href="index.php" class="btn btn-primary">Home</a>
            <a href="add.php" class="btn btn-success">Add</a>
            <a href="edit.php" class="btn btn-warning">Edit</a>
            <a href="delete.php" class="btn btn-danger">Delete</a>
            <a href="profile.php" class="btn btn-info">Profile Setting</a>
     <?php if (isset($_SESSION['username'])) : ?>
    <div class="d-flex justify-content-center align-items-center mt-3">
        <p class="fw-bold text-warning lead mb-0 me-3">
            <?= "Welcome " . htmlspecialchars($_SESSION['username']); ?>
        </p>

        <?php if ($_SESSION['profile_name'] === 'default.jpeg') : ?>
            <img src="../public/images/default.jpeg" alt="Profile" class="rounded-circle me-2" style="width:50px; height:50px; object-fit: cover;">
        <?php else : ?>
            <img src="../public/images/full/<?= htmlspecialchars($_SESSION['profile_name']); ?>" alt="Profile" class="rounded-circle me-2" style="width:50px; height:50px; object-fit: cover;">
        <?php endif; ?>

    </div>
<?php endif; ?>



        </nav>
    </header>
    <main class="container p-5">
        <!-- Introduction -->
        <section class="row justify-content-center text-center">
            <div class="col col-md-10 col-xl-8">
                <h1 class="fw-light text-center"><?php echo $title; ?></h1>
                <p class="text-muted text-center lead mb-5"><?php echo $introduction; ?></p>



            </div>
        </section>

        <!-- Page Content -->
        <section class="row justify-content-center">
            
            <div class="col">
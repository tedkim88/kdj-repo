<?php
session_start();

if (!isset($_SESSION['user_id'])) {
    header("Location: login.php");
    exit;
}



$title = "Profile Picture Setting";
$introduction = "Upload an image to set your profile picture!";
include 'includes/admin-header.php';
include 'includes/profile-upload.php';


?>

<section class="row justify-content-center my-5">
    <div class="col-md-6">
        <h2 class="display-4">Upload Image Files</h2>
        <p class="lead">To add an image to your profile, choose an image file from your device to upload to our server.</p>


        <hr class="my-5">

        <?php if ($message != "") : ?>
            <!-- Error Message Box -->
            <div class="alert alert-secondary my-5" role="alert">
                <?= $message; ?>
            </div>
        <?php endif; ?>

        <?php if (isset($file_name_new)) : ?>
            <!-- Preview: If there's a newly created image, we'll show a preview to the user. -->
            <div class="card text-bg-dark">
                <img src="images/thumbs/<?= $file_name_new; ?>" alt="<?= $img_description; ?>">

            </div>
        <?php endif; ?>

        <!-- Upload Form -->

        <!-- enctype attribute: This attribute is used to specify the encoding format, in which the data submitted in the form has to be encoded before sending it to the server. This attribute is very important and without specifying this, the image will not be uploaded to the server. -->
        <form action="<?= htmlspecialchars($_SERVER['PHP_SELF']); ?>" method="POST" enctype="multipart/form-data">
            <h2 class="fw-light fs-3 mb-4">Submission Form</h2>


            <!-- File Upload -->
            <div class="mb-3">
                <label for="img-file" class="form-label">Image File</label>
                <input type="file" id="img-file" name="img-file" class="form-control" accept=".avif, .jpg, .jpeg, .png, .webp" required>
                <p class="form-text">The following file types are accepted: AVIF, JPG, JPEG, PNG, WEBP</p>
            </div>

            <!-- Submit Button -->
            <input type="submit" name="submit" id="submit" value="Upload Image" class="btn btn-primary mt-5">
        </form>
    </div>
</section>


<?php

include 'includes/footer.php';

?>
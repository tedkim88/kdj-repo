<?php
require_once '../private/authentication.php';
require_login();

$title = 'Private Page (Admin)';
$introduction = 'You have successfully logged in!';
include 'includes/header.php';
?>


<div class="d-flex flex-column justify-content-center align-items-center" style="height: 50vh;">
  <div class="text-center">
    <div class="spinner-border text-primary" role="status" style="width: 4rem; height: 4rem;">
      <span class="visually-hidden">Loading...</span>
    </div>
    <p class="mt-3">Redirecting to browse page...</p>
  </div>
</div>


<script>
  setTimeout(() => {
    window.location.href = 'browse.php';
  }, 3500);
</script>

<?php include 'includes/footer.php'; ?>

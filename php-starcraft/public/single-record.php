<?php

session_start();

$id = isset($_GET['id']) ? urldecode($_GET['id']) : "";
$id = htmlspecialchars($id, ENT_QUOTES, 'UTF-8');


$name = isset($_GET['name']) ? urldecode($_GET['name']) : "";
$name = htmlspecialchars($name, ENT_QUOTES, 'UTF-8');



$title = "Unit Details";
$introduction = "Below is the detailed information for the unit you selected.";
include('includes/admin-header.php');


?>

<div class="container flex-column d-flex align-items-center">
    <div class="card col-md-10 col-lg-8 col-xxl-6 mt-5">

        <?php
        if ($id == "" || $name == "") {
            echo "<h2 class=\"display-5\">Oh no!</h2>";
            echo "<p class=\"lead\">We couldn't find the information you were looking for.</p>";
        } else {

            $query = "SELECT * FROM dkim54_catalogue_items WHERE id = ?;";

            if ($statement = $connection->prepare($query)) {

                // Next, we need to tell MySQL what data type (integer, in this case) we are using and which parameter (value) to bind to our earlier 'template'.
                $statement->bind_param("i", $id);

                // Now, we get to actually run it ... 
                $statement->execute();

                // ... and fetch the results.
                $result = $statement->get_result();

                if ($row = $result->fetch_assoc()) {

                    include 'includes/info-card.php';
                } else {
                    echo "<p>No data found for the selected game.</p>";
                }

                // When we're finished, we can release the data and free up server resources. 
                $statement->close();
            } else {
                die("Query preparation failed: " . $connection->error);
            }
        }

        ?>
    </div>




    <a href="index.php" class="btn btn-danger mt-3">Back to Index</a>

</div>

<?php include('includes/footer.php'); ?>
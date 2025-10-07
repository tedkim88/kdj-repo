<?php
session_start();
$title = 'Browse Page';
$introduction = "Enjoy the Catalogue of Starcraft Units.";

require_once dirname(__DIR__, 1) . '/data/connect.php';
$connection = db_connect();
if (!isset($_SESSION['user_id'])) {
    include '../private/prepared.php';
    include '../private/functions.php';
}

if (isset($_SESSION['user_id'])) {
    // If the user is logged in, we'll give them these options.
    include 'includes/admin-header.php';
} else {
    // If the user is logged out, we'll give them this option.
    include 'includes/header.php';
}



echo '<div class="col-12">';

$tagCounts = most_popular_tags();

if (is_array($tagCounts)) {
    echo '<p class="text-center lead mb-4"> <strong>Top 3 Tags:</strong>';
    foreach ($tagCounts as $tag => $count) {
        echo " <span class=\"badge bg-primary me-2\">$tag <small class=\"text-light\">($count)</small></span>";
    }
    echo '</p>';
} else {
    echo '<p class="text-muted text-center lead mb-5">No tags available.</p>';
}

include '../private/filters.php';


$active_filters = [];

foreach ($_GET as $filter => $values) {
    // If any of the values are not arrays, let's convert them into one.
    $values = is_array($values) ? $values : [$values];

    // Now, let's sanitise the values and add them to $active_filters.
    $active_filters[$filter] = array_map(fn($v) => htmlspecialchars($v, ENT_QUOTES | ENT_HTML5, 'UTF-8'), $values);
}


$per_page = 6;
$total_count = find_records(0, 0, null, null, $active_filters, true);
$total_pages = ceil($total_count / $per_page);
$current_page = (int) ($_GET['page'] ?? 1);
if ($current_page < 1 || $current_page > $total_pages || !is_int($current_page)) {
    $current_page = 1;
}
$offset = $per_page * ($current_page - 1);

?>

<section class="row align-items-start my-5">
    <h2 class="display-5 mb-3 text-center mb-3">Starcraft Unit Collection</h2>
      <aside class="col-lg-3 border border-secondary-subtle mb-3 mb-md-0 p-3 rounded">
        <h3 class="fw-light mt-4">Filter The Data</h3>
        <p class="muted">Select any combination of the buttons below to filter the data.</p>
        <hr class="my-5">

        <form action="<?= htmlspecialchars($_SERVER['PHP_SELF']); ?>" method="GET">

            <!-- Keyword Search -->
            <div>
                <label for="keyword" class="form-label h3 fw-light">Keyword</label>
                <input type="text" class="form-control" name="keyword" id="keyword" value="<?= htmlspecialchars($_GET['keyword'] ?? ''); ?>">
            </div>

            <!-- Checkboxes for other filters -->
            <div>
                <?php

                foreach ($filters as $filter => $options) {
                    // Let's create some headings from our category names. Our categories currently match the column names, however, which makes them unsuitable for heading output. str_replace() will remove all underscores, while ucwords() will capitalise each word.
                    $heading = ucfirst($filter);
                    // for init capitalisation, https://www.w3schools.com/Php/phptryit.asp?filename=tryphp_func_string_ucfirst

                    echo "<h3 class=\"fw-light mt-5 mb-2\">" . htmlspecialchars($heading) . "</h3>";
                    echo '<div class=" d-flex flex-wrap mb-3" role="group" aria-label="' . htmlspecialchars($heading) . ' Filter Group">';

                    // Now, let's do our inner for each loop. This will generate all of the buttons for the category using all of the key => value pairs.

                    foreach ($options as $value => $label) {

                        //for sticky values
                        $is_checked = in_array($value, $active_filters[$filter] ?? []);


                        $input_name = htmlspecialchars($filter) . '[]';


                        //for input id, when I didn't use str_replace, I had an w3c validation error because, in filters, we have ' "science fiction"  => "Science Fiction", 
                        //and this one has 'space', which is not allowed as an input ID by w3c. 
                        $input_id = htmlspecialchars($filter . '_' . str_replace(" ", "_", $value));

                        echo '<input type="checkbox" class="btn-check" name="' . $input_name . '" value="' . htmlspecialchars($value) . '" id="' . $input_id . '" ' . ($is_checked ? 'checked' : '') . '>';
                        echo '<label class="btn btn-outline-success me-2 mb-2" for="' . $input_id . '">' . htmlspecialchars($label) . '</label>';
                    }


                    echo "</div>";
                }
                ?>

            </div>


            <div class="d-flex gap-2 mt-5">
                <button type="submit" class="btn btn-primary">Apply Filters</button>
                <a href="<?= htmlspecialchars($_SERVER['PHP_SELF']) ?>" class="btn btn-secondary">Clear Filters</a>
            </div>

        </form>
    </aside>

     <div class="col-lg-9">


        <?php
        generate_table(null, true, $active_filters, $per_page, $offset);

        ?>
    </div>
</section>




<?php
// here, I made query strings so that the user can maintain the filters they selected while switching pages
$allowed_filters = ['race', 'unit_type', 'attack_damage']; // here, I didn't include page, sort, order because logically they are not user filters
//keyword is going to be added later below because it's not an array

$query_params = [];
foreach ($active_filters as $filter => $values) {
    if (!in_array($filter, $allowed_filters)) {
        continue;
    }
    foreach ($values as $value) {
        $query_params[] = urlencode($filter) . '%5B%5D=' . urlencode($value);
    }
}
$filter_query = implode('&', $query_params);


if (isset($_GET['keyword']) && trim($_GET['keyword']) !== '') {
    $filter_query .= ($filter_query !== '' ? '&' : '') . 'keyword=' . urlencode($_GET['keyword']);
}

//this $filtered_query is going to be added to clickable links, so that the user can maintain the filters they selected while switching pages
?>


<nav aria-label="Page Number">
    <ul class="pagination justify-content-center">
        <!-- PREVIOUS: If the current page is greater than one, we'll include the 'Previous' button. -->
        <?php if ($current_page > 1) : ?>
            <li class="page-item">
                <a href="browse.php?page=<?= $current_page - 1; ?>&<?= $filter_query; ?>" class="page-link link-success">Previous</a>
            </li>
        <?php endif; ?>

        <!-- NUMBERED PAGES -->
        <?php

        // If we have a massive amount of pages, we don't want to generate a link for each individual page. Instead, we want to obscure some of these pages with a gap. The 'gap' in our case will be an ellipses (...).
        $gap = FALSE;

        // The window is how many pages on either side of the current page (or next/previous buttons) we would like to see of have generated in our loop.
        $window = 1;

        for ($i = 1; $i <= $total_pages; $i++) {
            /**
             * We're checking three conditions to see if a gap should be inserted here:
             *  
             * 1. we're not near the beginning
             * 2. we're not near the end
             * 3. we're not near the current page
             * 
             * If all three are 'true': this is a 'middle' page number that doesn't need to be shown.
             */
            if ($i > 1 + $window && $i < $total_pages - $window && abs($i - $current_page) > $window) {
                if (!$gap): ?>

                    <li class="page-item"><span class="page-link link-success">...</span></li>

                <?php endif;

                // If we've inserted a gap (...), we need to flip this variable to TRUE so that we can carry on (and we don't insert more than one at once).
                $gap = TRUE;
                continue;
            }

            // After inserting the gap or rendering a visible page, the loop resets `$gap = false;` so that it knows it’s safe to insert another ellipses the next time it skips over pages.
            $gap = FALSE;

            /*
                    After figuring out whether or not we need to print a gap and skip some pages, we have two possibilities when it comes to the numbered pages: 

                    1. We print out the active page (a page we're currently on);
                    2. We print out an inactive page link (a page we're NOT on).
                */

            if ($current_page == $i) : ?>

                <li class="page-item bg-success active">
                    <!-- We're using a moot value (placeholder value) so that the user doesn't accidentally click the current page and reload everything. -->
                    <a href="#" class="page-link bg-success link-white border border-success"><?= $i; ?></a>
                </li>

            <?php else : ?>

                <!-- This will be an 'inactive' page, or one that the user can navigate to. -->
                <li class="page-item">
                    <a href="browse.php?page=<?= $i; ?>&<?= $filter_query; ?>" class="page-link link-success"><?= $i; ?></a>
                </li>

        <?php endif;
        }

        ?>

        <!-- NEXT: If the current page is less than the total number of page, we'll include the 'Next' button. -->
        <?php if ($current_page < $total_pages) : ?>
            <li class="page-item">
                <a href="browse.php?page=<?= $current_page + 1; ?>&<?= $filter_query; ?>" class="page-link link-success">Next</a>
            </li>
        <?php endif; ?>
    </ul>
</nav>
<?php
echo '</div>';




include 'includes/footer.php';

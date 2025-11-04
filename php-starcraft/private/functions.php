<?php

/* VARIABLES or ..Constants I should say */
$unit_types = ['Ground', 'Air', 'Building'];
$races = ['Terran', 'Zerg', 'Protoss'];

/**
 * Summary of generate_table
 * @param mixed $button_callback
 * @param mixed $display_as_cards
 * @return void
 */

function most_popular_tags()
{
    $query = "SELECT tags FROM dkim54_catalogue_items;";
    $result = execute_prepared_statement($query);
    $all_tags = [];

    while ($row = $result->fetch_assoc()) {
        $tags = explode(',', $row['tags']);
        foreach ($tags as $tag) {
            $clean_tag = strtolower(trim($tag));
            if ($clean_tag !== '') {
                $all_tags[] = $clean_tag;
            }
        }
    }


    $tag_counts = array_count_values($all_tags);


    arsort($tag_counts);


    return array_slice($tag_counts, 0, 3, true);
}


function find_records($limit = 6, $offset = 0, $sort = null, $order = null, $active_filters = [], $count_only = false)
{
    require __DIR__ . '/filters.php';
    global $connection;

    $sql = $count_only
        ? "SELECT COUNT(*) FROM dkim54_catalogue_items WHERE 1=1"
        : "SELECT id, name, race, unit_type, health, shield, armor, attack_damage, attack_range, movement_speed, mineral_cost, gas_cost, build_time, description, tags, has_cloak, img_description, filename FROM dkim54_catalogue_items WHERE 1=1";
    $types = "";
    $parameters = [];


    foreach ($active_filters as $filter => $filter_values) {
        if ($filter === "attack_damage") {
            $damage_ranges = [];

            foreach ($filter_values as $value) {
                // 예: "1-10", "11-20", "41-500"
                if (!preg_match('/^\d+-\d+$/', $value)) continue;

                list($min, $max) = explode("-", $value, 2);
                $damage_ranges[] = "attack_damage BETWEEN ? AND ?";
                $types .= "ii";
                $parameters[] = (int)$min;
                $parameters[] = (int)$max;
            }

            if (!empty($damage_ranges)) {
                $sql .= " AND (" . implode(" OR ", $damage_ranges) . ")";
            }
        } elseif ($filter === "race") {
            $placeholders = implode(",", array_fill(0, count($filter_values), "?"));
            $sql .= " AND race IN ($placeholders)";
            $types .= str_repeat("s", count($filter_values));
            foreach ($filter_values as $key) {
                if (isset($filters[$filter][$key])) {
                    $parameters[] = $filters[$filter][$key];
                }
            }
        } elseif ($filter === "unit_type") {
            $placeholders = implode(",", array_fill(0, count($filter_values), "?"));
            $sql .= " AND unit_type IN ($placeholders)";
            $types .= str_repeat("s", count($filter_values));
            foreach ($filter_values as $key) {
                if (isset($filters[$filter][$key])) {
                    $parameters[] = $filters[$filter][$key];
                }
            }
        } elseif ($filter === "keyword") {
            foreach ($filter_values as $value) {
                if (trim($value) === '') continue;
                $sql .= " AND (name LIKE ? OR race LIKE ? OR unit_type LIKE ? OR description LIKE ? OR tags LIKE ? OR img_description LIKE ?)";
                $types .= "ssssss";
                $param = '%' . $value . '%';

                $parameters[] = $param;
                $parameters[] = $param;
                $parameters[] = $param;
                $parameters[] = $param;
                $parameters[] = $param;
                $parameters[] = $param;
            }
        }
    }


    if (!$count_only) {
        if ($sort && $order) {
            $sql .= " ORDER BY $sort $order";
        }

        if ($limit > 0) {
            $sql .= " LIMIT ?";
            $types .= "i";
            $parameters[] = $limit;

            if ($offset > 0) {
                $sql .= " OFFSET ?";
                $types .= "i";
                $parameters[] = $offset;
            }
        }
    }


    $statement = $connection->prepare($sql);
    if (!empty($types)) {
        $statement->bind_param($types, ...$parameters);
    }
    $statement->execute();

    // echo "<p>$sql</p>";
    // print_r($parameters);

    if ($count_only) {
        $result = $statement->get_result();
        $row = $result->fetch_row();
        return $row[0] ?? 0;
    }


    return $statement->get_result();
}



function generate_table($button_callback = NULL, $display_as_cards = false, $active_filters = [], $per_page = 6, $offset = 0, $is_edit_or_delete = false)
{

    // if (!empty($active_filters)) {
    //     $units_result = find_records($per_page, $offset, null, null, $active_filters);

    //     $units = [];
    //     if ($units_result) {
    //         while ($row = $units_result->fetch_assoc()) {
    //             $units[] = $row;
    //         }
    //     }
    // } else {
    //     $units = get_all_units();
    // }

    if (!$is_edit_or_delete) {
        $units_result = find_records($per_page, $offset, null, null, $active_filters);

        $units = [];
        if ($units_result) {
            while ($row = $units_result->fetch_assoc()) {
                $units[] = $row;
            }
        }
    } else {
        $units = get_all_units();
    }


    if (count($units) > 0) {

        if ($display_as_cards) {
            echo "<div class=\"d-flex flex-wrap gap-4 justify-content-center\">";

            foreach ($units as $unit) {
                extract($unit);

                switch ($race) {
                    case 'Zerg':
                        $btnClass = 'btn-danger';
                        break;
                    case 'Terran':
                        $btnClass = 'btn-primary';
                        break;
                    case 'Protoss':
                        $btnClass = 'btn-warning';
                        break;
                    default:
                        $btnClass = 'btn-secondary';
                }

                switch ($race) {
                    case 'Zerg':
                        $textClass = 'text-danger';
                        break;
                    case 'Terran':
                        $textClass = 'text-primary';
                        break;
                    case 'Protoss':
                        $textClass = 'text-warning';
                        break;
                    default:
                        $textClass = 'text-muted';
                }



                echo "<div class=\"card shadow-sm\" style=\"width: 18rem; min-width: 250px; height: 800px;\">";




                echo "  <img src=\"../public/images/thumbs/$filename\" class=\"card-img-top\" alt=\"$img_description\" style=\"height:450px; object-fit:cover;\">";

                echo "  <div class=\"card-body d-flex flex-column\">";
                echo "<h5 class=\"card-title fst-italic text-uppercase text-dark text-center mb-4\">$name</h5>";

                echo "    <h6 class=\"card-subtitle mb-2 $textClass\">Race: $race</h6>";
                echo "    <p class=\"card-text mb-2\"><strong>Unit Type:</strong> $unit_type</p>";
                echo "    <p class=\"card-text mb-2\"><strong>Health:</strong> $health</p>";
                echo "    <p class=\"card-text mb-2\"><strong>Attack Damage:</strong> $attack_damage</p>";
                echo "    <p class=\"card-text mb-2\"><strong>Description:</strong> $description</p>";


                $tagArray = explode(',', $tags);
                echo '<p class="card-text mb-2"><strong>Tags:</strong> ';
                foreach ($tagArray as $tag) {
                    $cleanTag = trim($tag); // 공백 제거
                    echo "<span class=\"badge bg-secondary me-1\">$cleanTag</span> ";
                }
                echo '</p>';



                echo "<a href=\"single-record.php?id=" . urlencode($id) . "&name=" . urlencode($name) . "\" class=\"btn $btnClass w-100 mt-auto\">View Details</a>";
                echo "  </div>";
                echo "</div>";
            }

            echo "</div>";
        } else {
            echo "<table class=\"table table-bordered table-hover\"> \n
                 <thead class=\"table-dark\"> \n
                 <tr> \n
                 <th scope=\"col\">Unit Name</th> \n
                 <th scope=\"col\">Race</th> \n
                 <th scope=\"col\">Unit Type</th> \n
                 <th scope=\"col\">Health</th> \n
                 <th scope=\"col\">Attack Damage</th> \n
                 <th scope=\"col\">Description</th> \n";

            if ($button_callback != NULL) {
                echo "<th scope=\"col\">Actions</th> \n";
            }

            echo "</tr> \n
                 </thead> \n
                 <tbody> \n";

            foreach ($units as $unit) {
                extract($unit);

                echo "<tr> \n
                     <td>$name</td> \n
                     <td>$race</td> \n
                     <td>$unit_type</td> \n
                     <td>$health</td> \n
                     <td>$attack_damage</td> \n
                     <td>$description</td> \n";

                if ($button_callback != NULL) {
                    $buttons = call_user_func($button_callback, $unit);
                    echo "<td>$buttons</td> \n";
                }

                echo "</tr> \n";
            }

            echo "</tbody> \n
                 </table> \n";
        }
    } else {
        echo "<div class=\"alert alert-warning text-center\">";
        echo "<h4 class=\"fw-light\">Oh no!</h4>";
        echo "<p>We're sorry, but we weren't able to find any units in our database.</p>";
        echo "</div>";
    }
}

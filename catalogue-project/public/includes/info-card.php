<!-- Card Output -->

<div class="text-center mb-3">
    <img src="../public/images/full/<?= $row['filename']; ?>" alt="unit_image" class="img-fluid mx-auto d-block">
</div>


<div class="card px-0">
    <div class="card-header text-bg-danger">
        <h3 class="card-title fw-bold text-center h-2"><?= $row['name']; ?></h3>
    </div>
    <div class="card-body">


     
       

        <div class="d-flex gap-4 flex-wrap">
            <div class="flex-fill min-w-250px">
                <p class="card-text"><span class="fw-bold">Unit Name</span>: <?= $row['name']; ?></p>
                <p class="card-text"><span class="fw-bold">Race</span>: <?= $row['race']; ?></p>
                <p class="card-text"><span class="fw-bold">Unit Type</span>: <?= $row['unit_type']; ?></p>
                <p class="card-text"><span class="fw-bold">Health</span>: <?= $row['health']; ?></p>
                <p class="card-text"><span class="fw-bold">Shield</span>: <?= $row['shield']; ?></p>
                <p class="card-text"><span class="fw-bold">Armor</span>: <?= $row['armor']; ?></p>
                <p class="card-text"><span class="fw-bold">Attack Damage</span>: <?= $row['attack_damage']; ?></p>
                <p class="card-text"><span class="fw-bold">Attack Range</span>: <?= $row['attack_range']; ?></p>
                <p class="card-text"><span class="fw-bold">Movement Speed</span>: <?= $row['movement_speed']; ?></p>
            </div>

            <div class="flex-fill min-w-250px">

                <p class="card-text"><span class="fw-bold">Mineral Cost</span>: <?= $row['mineral_cost']; ?></p>
                <p class="card-text"><span class="fw-bold">Gas Cost</span>: <?= $row['gas_cost']; ?></p>
                <p class="card-text"><span class="fw-bold">Build Time</span>: <?= $row['build_time']; ?></p>
                <p class="card-text"><span class="fw-bold">Description</span>: <?= $row['description']; ?></p>
                <p class="card-text"><span class="fw-bold">Tags</span>: <?= $row['tags']; ?></p>
                <p class="card-text"><span class="fw-bold">Has Cloak</span>: <?= $row['has_cloak'] ? 'Yes' : 'No'; ?></p>
                <p class="card-text"><span class="fw-bold">Image Description</span>: <?= $row['img_description']; ?></p>
              
            </div>
        </div>


    </div> <!-- end of .card-body -->
</div> <!-- end of .card -->
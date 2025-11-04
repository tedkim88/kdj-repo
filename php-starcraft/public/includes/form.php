<form action="<?= htmlspecialchars($_SERVER['PHP_SELF'] . (isset($_GET['unit_id']) ? '?unit_id=' . urlencode($_GET['unit_id']) : '')); ?>" method="POST" enctype="multipart/form-data" class="border border-secondary-subtle rounded shadow-sm p-3">


    <h2 class="fs-4 fw-light mb-4">Unit Info</h2>

 
    <div class="mb-4">
        <label for="unit-name" class="form-label fw-bold">Unit Name <span class="text-danger"> (Required)</span></label>
        <input type="text" id="unit-name" name="unit-name" placeholder="ex. Marine" class="form-control" value="<?= htmlspecialchars($_POST['unit-name'] ?? ($unit['name'] ?? '')); ?>">
        <p class="form-text">What is the name of the unit? (50 letters or fewer)</p>
    </div>


   
    <div class="mb-4">
        <label for="race" class="form-label fw-bold">Select the race <span class="text-danger"> (Required)</span></label>
        <select name="race" id="race" class="form-select">
            <option value="">-- Please Select --</option>
            <?php


            foreach ($races as $race) {

                $selected = ($_POST['race'] ?? ($unit['race'] ?? '')) === $race ? 'selected' : '';
                echo "<option value=\"$race\" $selected>$race</option>";
            }

            ?>
        </select>
    </div>


  
    <div class="mb-4">
        <label for="unit-type" class="form-label fw-bold">Select the unit type <span class="text-danger"> (Required)</span></label>
        <select name="unit-type" id="unit-type" class="form-select">
            <option value="">-- Please Select --</option>
            <?php


            foreach ($unit_types as $unit_type) {

                $selected = ($_POST['unit-type'] ?? ($unit['unit_type'] ?? '')) === $unit_type ? 'selected' : '';
                echo "<option value=\"$unit_type\" $selected>$unit_type</option>";
            }

            ?>
        </select>
    </div>




    <div class="mb-4">
        <label for="health" class="form-label fw-bold">Health <span class="text-danger"> (Required)</span></label>
        <input type="number" name="health" id="health" class="form-control" placeholder="Enter the unit HP" value="<?= htmlspecialchars($_POST['health'] ?? ($unit['health'] ?? '')); ?>">
        <p class="form-text">When is health of the unit? between 1 and 500</p>
    </div>

    <div class="mb-4">
        <label for="shield" class="form-label fw-bold">Shield <span class="text-danger"> (Required)</span></label>
        <input type="number" name="shield" id="shield" class="form-control" placeholder="Enter the unit shield" value="<?= htmlspecialchars($_POST['shield'] ?? ($unit['shield'] ?? '')); ?>">
        <p class="form-text">How much shield does the unit have? between 0 and 500</p>
    </div>

    <div class="mb-4">
        <label for="armor" class="form-label fw-bold">Armor <span class="text-danger"> (Required)</span></label>
        <input type="number" name="armor" id="armor" class="form-control" placeholder="Enter the unit armor" value="<?= htmlspecialchars($_POST['armor'] ?? ($unit['armor'] ?? '')); ?>">
        <p class="form-text">How much armor does the unit have? between 0 and 500</p>
    </div>

    <div class="mb-4">
        <label for="attack-damage" class="form-label fw-bold">Attack Damage <span class="text-danger"> (Required)</span></label>
        <input type="number" name="attack-damage" id="attack-damage" class="form-control" placeholder="Enter the attack damage" value="<?= htmlspecialchars($_POST['attack-damage'] ?? ($unit['attack_damage'] ?? '')); ?>">
        <p class="form-text">Enter the attack damage of the unit between 0 and 500</p>
    </div>

    <div class="mb-4">
        <label for="attack-range" class="form-label fw-bold">Attack Range <span class="text-danger"> (Required)</span></label>
        <input type="number" name="attack-range" id="attack-range" class="form-control" placeholder="Enter the attack range" value="<?= htmlspecialchars($_POST['attack-range'] ?? ($unit['attack_range'] ?? '')); ?>">
        <p class="form-text">What is the attack range of the unit? between 0 and 20</p>
    </div>

    <div class="mb-4">
        <label for="movement-speed" class="form-label fw-bold">Movement Speed <span class="text-danger"> (Required)</span></label>
        <input type="number" name="movement-speed" id="movement-speed" step="0.01" min="0" max="10.00" class="form-control" placeholder="Enter the movement speed" value="<?= htmlspecialchars($_POST['movement-speed'] ?? ($unit['movement_speed'] ?? '')); ?>">
        <p class="form-text">What is the movement speed of the unit? between 0 and 10(it could have 2 decimal points) </p>
    </div>

    <div class="mb-4">
        <label for="mineral-cost" class="form-label fw-bold">Mineral Cost <span class="text-danger"> (Required)</span></label>
        <input type="number" name="mineral-cost" id="mineral-cost" class="form-control" placeholder="Enter the Mineral cost" value="<?= htmlspecialchars($_POST['mineral-cost'] ?? ($unit['mineral_cost'] ?? '')); ?>">
        <p class="form-text">What is the mineral cost needed to make the unit? between 1 and 1000</p>
    </div>

    <div class="mb-4">
        <label for="gas-cost" class="form-label fw-bold">Gas Cost <span class="text-danger"> (Required)</span></label>
        <input type="number" name="gas-cost" id="gas-cost" class="form-control" placeholder="Enter the gas cost" value="<?= htmlspecialchars($_POST['gas-cost'] ?? ($unit['gas_cost'] ?? '')); ?>">
        <p class="form-text">When is the gas cost needed to make the unit between 0 and 1000?</p>
    </div>

    <div class="mb-4">
        <label for="build-time" class="form-label fw-bold">Build Time <span class="text-danger"> (Required)</span></label>
        <input type="number" name="build-time" id="build-time" class="form-control" placeholder="Enter the Build time" value="<?= htmlspecialchars($_POST['build-time'] ?? ($unit['build_time'] ?? '')); ?>">
        <p class="form-text">What is the build time of the unit between 1 and 500?</p>
    </div>



   
    <div class="mb-4">
        <label for="description" class="form-label fw-bold">Description <span class="text-danger"> (Required)</span></label>
        <input type="text" name="description" id="description" class="form-control" value="<?= htmlspecialchars($_POST['description'] ?? ($unit['description'] ?? '')); ?>">
        <p class="form-text">Description of the unit equal to or less than 128 letters</p>
    </div>

   
    <div class="mb-4">
        <label for="tags" class="form-label fw-bold">Tags (Optional)</label>
        <input type="text" name="tags" id="tags" class="form-control" placeholder="Ex. biological,light" value="<?= htmlspecialchars($_POST['tags'] ?? ($unit['tags'] ?? '')); ?>">
        <p class="form-text">Enter the tags using commas.(equal to or less than 255 letters) </p>
    </div>


   

    <fieldset class="mb-4">
        <legend class="fw-normal fs-6">Does this unit have cloaking?</legend>

        <?php

        $has_cloaking = $_POST['cloaking'] ?? (isset($unit['has_cloak']) ? (string) $unit['has_cloak'] : '0');
        ?>

        <div class="form-check">
            <input type="radio" name="cloaking" id="has-cloaking" value="1" class="form-check-input fw-bold" <?= $has_cloaking === '1' ? 'checked' : ''; ?>>
            <label for="has-cloaking" class="form-check-label">Yes</label>
        </div>

        <div class="form-check">
            <input type="radio" name="cloaking" id="no-cloaking" value="0" class="form-check-input fw-bold" <?= $has_cloaking === '0' ? 'checked' : ''; ?>>
            <label for="no-cloaking" class="form-check-label">No</label>
        </div>
    </fieldset>


    <div class="mb-4">
        <label for="img-description" class="form-label fw-bold">Image Description (Optional)</label>
        <input type="text" name="img-description" id="img-description" class="form-control " value="<?= htmlspecialchars($_POST['img-description'] ?? ($unit['img_description'] ?? '')); ?>">
        <p class="form-text">Enter Description of the Image you are going to upload. (equal to or less than 128 letters)</p>
    </div>

    <!-- File Upload -->
    <div class="mb-3">
        <label for="img-file" class="form-label fw-bold">Image File <?= $is_editing ? '(Optional)' : '<span class="text-danger">(Required) </span>'; ?></label>
        <input type="file" id="img-file" name="img-file" class="form-control" accept=".avif, .jpg, .jpeg, .png, .webp" <?= $is_editing ? '' : 'required'; ?>>
        <p class="form-text">The following file types are accepted: AVIF, JPG, JPEG, PNG, WEBP</p>
        <?php if ($is_editing) : ?>
            <p class="form-text text-danger">If you don't want to change the existing image, leave this field blank</p>
        <?php endif; ?>

    </div>

    <?php if ($is_editing) : ?>

        <p class="fw-bold">Currently Registered Image</p>

        <div class="mb-4">
            <img src="../public/images/thumbs/<?= $unit['filename']; ?>" alt="pre-existing image">
        </div>

    <?php endif; ?>



    <!-- Hidden Values for Primary Key -->
    <input type="hidden" name="unit-id" id="unit-id" value="<?= htmlspecialchars($_GET['unit_id'] ?? ($_POST['unit-id'] ?? '')) ?>">

    <!-- Submit -->
    <input type="submit" value="Save" name="submit" id="submit" class="btn btn-dark my-4">

</form>
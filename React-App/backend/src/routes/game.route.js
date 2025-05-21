import express from "express";
import { signup, login, logout } from "../controllers/auth.controller.js";
import { getMainGames } from "../controllers/game.controller.js";
import {getGamesbyPlatformAndGenre} from "../controllers/game.controller.js"
const router = express.Router();


//ps5 / pc / nintendo switch / xbox games list
router.get("/get", getMainGames);


router.get("/get/detailSearch", getGamesbyPlatformAndGenre);

// router.post("/login", login);

// router.post("/logout", logout);

export default router;

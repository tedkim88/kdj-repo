import express from "express";
import { signup, login, logout } from "../controllers/auth.controller.js";
import { protectRoute } from "../middlewares/auth.middleware.js";
const router = express.Router();


//if I put my auth middleware here, I can't signup, login, logout.
router.post("/signup", signup);

router.post("/login", login);

router.post("/logout", logout);

export default router;

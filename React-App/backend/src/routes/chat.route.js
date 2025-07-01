import express from "express";
import { sendMessage, getUsersForSidebar, getChatMessages } from "../controllers/chat.controller.js";
import { protectRoute } from "../middlewares/auth.middleware.js";

const router = express.Router();

router.post("/send", protectRoute, sendMessage);

router.get("/getUsers", protectRoute, getUsersForSidebar);

router.get("/getMessages", protectRoute, getChatMessages);

export default router;

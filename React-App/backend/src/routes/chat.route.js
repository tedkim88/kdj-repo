import express from "express";
import { sendMessage, getUsersForSidebar, getChatMessages } from "../controllers/chat.controller.js";


const router = express.Router();

router.post("/send", sendMessage);

router.get("/getUsers", getUsersForSidebar);

router.get("/getMessages", getChatMessages);

export default router;

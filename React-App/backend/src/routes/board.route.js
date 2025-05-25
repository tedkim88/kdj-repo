import express from "express";
import {
  getTotalMessages,
  getMessagesByPlatform,
  saveMessagetoDB,
  editDBmessage
} from "../controllers/board.controller.js";
import { protectRoute } from "../middlewares/auth.middleware.js";

const router = express.Router();

//board for all platform games
//but getTotalMessages Controller. will get req.query for page input
//this isn't using URL params. it's using query string request.
router.get("/all", protectRoute, getTotalMessages);

//board for a specific platform
router.get("/platform/:platformId", protectRoute, getMessagesByPlatform);

//save board message to the database
router.post("/save", protectRoute, saveMessagetoDB);


// //edit board message
router.put("/edit/:messageId", protectRoute, editDBmessage);
//msg model's unique id as a parameter

// //delete board message
// router.delete("/delete", protectRoute, deleteMessageFromDB);


export default router;

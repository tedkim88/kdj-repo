import express from "express";
import {
  getTotalMessages,
  getMessagesByPlatform,
  saveMessagetoDB,
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


// //delete board message
// router.delete("/delete", protectRoute, deleteMessageFromDB);

// //edit board message
// router.put("/edit", protectRoute, editDBmessage);

export default router;

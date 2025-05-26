import express from "express";
import {
  getTotalMessages,
  getMessagesByPlatform,
  saveMessagetoDB,
  editDBmessage,
  deleteMessageFromDB,
  checkMessagePassword,
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

//edit board message
//msg model's unique id as a parameter
router.put("/edit/:messageId", protectRoute, editDBmessage);

//before delete board message password must be checked.
//message id as a parameter, password as a body
router.post("/checkMsgPassword/:messageId", protectRoute, checkMessagePassword);

//delete board message
//in frontend, when delete button is clicked, it will check the password first and if password is correct, delete request would be called.
// router.delete("/delete/:messageId", protectRoute, deleteDBmessage);
//delete board message
router.delete("/delete/:messageId", protectRoute, deleteMessageFromDB);

export default router;

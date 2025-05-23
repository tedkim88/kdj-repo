import express from "express";
import { getTotalMessages, getMessagesByPlatform } from "../controllers/board.controller.js";


const router = express.Router();

// "/api/board"
//base route


//board for all plantform-based games
//but getTotalMessages Controller. will get req.query for page input
//this isn't using URL params. it's using query string request.
router.get("/all", getTotalMessages);

// //board for a specific platform
router.get("/platform/:platformId", getMessagesByPlatform);


// //saving a message to the database(board)
// router.post("/save", saveMessage);


export default router;

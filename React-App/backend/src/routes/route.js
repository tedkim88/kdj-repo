import express from "express";
import respond from "../controllers/controller.js";

const router = express.Router();

router.get("/", respond);

export default router;

import express from "express";
import mongoose from "mongoose";
import dotenv from "dotenv";
import authRouter from "./src/routes/auth.route.js";
import { connectDB } from "./src/lib/db.js";
import gameRouter from "./src/routes/game.route.js";
import { createAdmin } from "./src/seed/admin.seed.js";
const app = express();
dotenv.config();

app.use(express.json());

const PORT = process.env.PORT || 5000;

app.use("/api/auth", authRouter);

app.use("/api/games", gameRouter);

app.listen(PORT, async () => {
  
  await connectDB();
  console.log(`Server is running on port ${PORT}`);
  createAdmin(); // admin seed, to be used later for accessing admin panel(role-based)
});

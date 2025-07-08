import express from "express";
import mongoose from "mongoose";
import dotenv from "dotenv";
import authRouter from "./src/routes/auth.route.js";
import { connectDB } from "./src/lib/db.js";
import gameRouter from "./src/routes/game.route.js";
import { createAdmin } from "./src/seed/admin.seed.js";
import boardRouter from "./src/routes/board.route.js";
import chatRouter from "./src/routes/chat.route.js";
import cookieParser from "cookie-parser";
import cors from "cors";
//for socket
import { createServer } from "http";
import { initSocket } from "./src/lib/socket.js";
import { create } from "domain";

const app = express();
dotenv.config();

const server = createServer(app);


app.use(cookieParser());
app.use(express.json());

const PORT = process.env.PORT || 5000;

app.use(
  cors({
    //cross origin resource sharing 내 컴퓨터 문서참고
    origin: "http://localhost:5173",
    credentials: true,
  })
);

//base url for auth API
app.use("/api/auth", authRouter);

//base url for game API
app.use("/api/games", gameRouter);

//base url for board API
app.use("/api/board", boardRouter);

//base url for livechat
app.use("/api/chat", chatRouter);

// app.listen(PORT, async () => {
//   await connectDB();
//   console.log(`Server is running on port ${PORT}`);
//   createAdmin(); // admin seed, to be used later for accessing admin panel(role-based)
// });

server.listen(PORT, async () => {
  await connectDB();
  console.log(`Server is running on port ${PORT}`);
  createAdmin(); // admin seed, to be used later for accessing admin panel(role-based)
  initSocket(server);
});
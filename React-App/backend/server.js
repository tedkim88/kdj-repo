import express from 'express';
import mongoose from 'mongoose';
import dotenv from 'dotenv';
import router from "./src/routes/route.js"


const app = express();
dotenv.config();

app.use(express.json());

const PORT = process.env.PORT || 5000;


app.use("/api/auth", router);


app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});



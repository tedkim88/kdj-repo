import mongoose from "mongoose";

const boardmsgSchema = new mongoose.Schema(
    {
        writerId: {
            type: mongoose.Schema.Types.ObjectId,
            ref: "User",
            required: true,
        },
        password: {
            type: String,
            required: true,
        },
        title: {
            type: String,
            required: true,
        },
        content: {
            type: String,
            required: true,
        },
        //platforId may be needed for making a board for a specific game platform rendering
        platformId:{
            type: String,
            required: true,
        },

    },
    { timestamps: true }
);

const Boardmsg = mongoose.model("Boardmsg", boardmsgSchema);
export default Boardmsg;
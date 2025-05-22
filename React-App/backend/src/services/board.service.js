import Boardmsg from "../models/boardmsg.model.js";

export const getBoardMessagesService = async (pageNum) => {
  try {
    const limit = 10;

    //pagination + optimization for performance.
    //not loading all messages at once.
    //loading only 10 messages per page depending on user page choice
    const messages = await Boardmsg.find({})
      .sort({ createdAt: -1 })
      .skip((pageNum - 1) * limit)
      .limit(limit);

    return messages;
  } catch (error) {
    console.log(error);
    throw new Error("Board Service Error(getBoardMessages): " + error.message);
  }
};

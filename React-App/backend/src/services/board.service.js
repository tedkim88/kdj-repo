import Boardmsg from "../models/boardmsg.model.js";

export const getBoardMessagesService = async (pageNum, platformId = null) => {
  try {
    const limit = 10;

    //if platformId is not null, then filter by platformId
    //with this filter for finding messages, we can get either all messages or ones from a specific platform
    const filter = platformId ? { platformId } : {};

    //pagination + optimization for performance.
    //not loading all messages at once.
    //loading only 10 messages per page depending on user page choice
    const messages = await Boardmsg.find(filter) // === .find({platformId : platformId}) object shorthand
      .sort({ createdAt: -1 })
      .skip((pageNum - 1) * limit)
      .limit(limit);

    return messages;
  } catch (error) {
    console.log(error);
    throw new Error("Board Service Error(getBoardMessages): " + error.message);
  }
};

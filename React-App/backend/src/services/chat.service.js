import ChatMsg from "../models/chatmsg.model.js";
import User from "../models/user.model.js";
export const sendMessageService = async (senderId, receiverId, text) => {
  try {
    const newMsg = new ChatMsg({ senderId, receiverId, text });
    const savedMsg = await newMsg.save();
    return savedMsg;
  } catch (error) {
    console.error("Error saving message:", error);
    throw new Error("Chat Service Error: Failed to save message");
  }
};

export const getUsersService = async (loggedInUserId) => {
  try {
    const userExists = await User.findById(loggedInUserId);
    if (!userExists) {
      throw new Error("User not found");
    }

    const users = await User.find({ _id: { $ne: loggedInUserId } });
    console.log(users);
    return users;
  } catch (error) {
    console.error("Error fetching users:", error);
    throw new Error(
      "Chat Service Error from chat.service.js: " + error.message
    );
  }
};

export const getChatMsgService = async (senderId, receiverId) => {
  try {
    const messages = await ChatMsg.find({
      $or: [
        { senderId: senderId, receiverId: receiverId },
        { senderId: receiverId, receiverId: senderId },
      ],
    });

    if (messages.length === 0) {
      throw new Error("No messages found");
    }

    return messages;
  } catch (error) {
    console.error("Error fetching messages:", error);
    throw new Error(
      "Chat Service Error from chat.service.js: " + error.message
    );
  }
};

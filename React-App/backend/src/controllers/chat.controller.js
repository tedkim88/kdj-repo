import {
  sendMessageService,
  getUsersService,
  getChatMsgService,
} from "../services/chat.service.js";

export const sendMessage = async (req, res) => {
  try {
    const { receiverId, text } = req.body;
    const senderId = req.user._id;
    if (!receiverId || !text)
      return res.status(400).json({ message: "Missing required fields" });
    const newMessage = await sendMessageService(senderId, receiverId, text);

    res.status(200).json(newMessage);
  } catch (error) {
    console.log(error);
    res.status(500).json({ message: "Error sending message: " });
  }
};

export const getUsersForSidebar = async (req, res) => {
  try {
    const loggedInUserId = req.user._id;
    // const { _id: loggedInUserId } = req.body; //for postman testing

    const filteredUsers = await getUsersService(loggedInUserId);
    res.status(200).json(filteredUsers);
  } catch (error) {
    console.log(
      "Error in getUsersForSidebar from chat.controller",
      error.message
    );
    res.status(500).json({ message: "Internal server error in getting" });
  }
};

export const getChatMessages = async (req, res) => {
  try {
    const { receiverId } = req.body;
    const senderId = req.user._id;
    
    const chatMessages = await getChatMsgService(senderId, receiverId);
    res.status(200).json(chatMessages);
  } catch (error) {
    console.log(error);
    res.status(500).json({ message: "Error getting messages: " });
  }
};

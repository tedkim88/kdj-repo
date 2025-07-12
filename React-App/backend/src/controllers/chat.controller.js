import {
  sendMessageService,
  getUsersService,
  getChatMsgService,
} from "../services/chat.service.js";

import { getReceiverSocketId, getIO } from "../lib/socket.js";

export const sendMessage = async (req, res) => {
  try {
    const { receiverId, text } = req.body;
    const senderId = req.user._id;
    if (!receiverId || !text)
      return res.status(400).json({ message: "Missing required fields" });
    const newMessage = await sendMessageService(senderId, receiverId, text);

    //for real time
    const receiverSocketId = getReceiverSocketId(receiverId);

    if (receiverSocketId) {
      console.log("receiverSocketId", receiverSocketId);
      getIO().to(receiverSocketId).emit("newMessage", newMessage);
    }
    else{
      console.log("receiverSocketId not found");
    }


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
    console.log(loggedInUserId);
    const filteredUsers = await getUsersService(loggedInUserId);
    console.log(filteredUsers);
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

    //I tried req.body first but, seems like 'GET' doesn't allow req.body
    const { receiverId } = req.query;
    const senderId = req.user._id;

    const chatMessages = await getChatMsgService(senderId, receiverId);
    res.status(200).json(chatMessages);
  } catch (error) {
    console.log(error);
    res.status(500).json({ message: "Error getting messages: " });
  }
};

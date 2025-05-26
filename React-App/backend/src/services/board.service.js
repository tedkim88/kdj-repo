import Boardmsg from "../models/boardmsg.model.js";
import bcrypt from "bcryptjs";

//for all messages or messages from a specific platform
export const getBoardMessagesService = async (pageNum, platformId = null) => {
  try {
    const limit = 10;

    //if platformId is not null, then filter by platformId
    //with this filter for finding messages, we can get either all messages or ones from a specific platform
    const filter = platformId ? { platformId } : {};

    //pagination + optimization for performance.
    //not loading all messages at once.
    //loading only 10 messages per page depending on user page choice
    const messages = await Boardmsg.find(filter)
      .select(
        "-password" //no password return to client even if it's encrypted
      )
      .sort({ createdAt: -1 })
      .skip((pageNum - 1) * limit)
      .limit(limit);

    return messages;
  } catch (error) {
    console.log(error);
    throw new Error("Board Service Error(getBoardMessages): " + error.message);
  }
};

//save message to the database
export const saveMessageService = async (
  userId,
  password,
  title,
  content,
  platformId
) => {
  try {
    const salt = await bcrypt.genSalt(10);
    const hashedPassword = await bcrypt.hash(password, salt);

    const newMsg = new Boardmsg({
      writerId: userId,
      password: hashedPassword,
      title,
      content,
      platformId,
    });

    const savedMessage = await newMsg.save();
    const newMessage = await Boardmsg.findById(savedMessage._id).select(
      "-password"
    );
    return newMessage;
  } catch (error) {
    console.log(error);
    throw new Error("Board Service Error(saveMessage): " + error.message);
  }
};

export const editMessageService = async (
  messageId,
  userId,
  title,
  content,
  platformId
) => {
  try {
    const messageFound = await Boardmsg.findById(messageId);
    if (messageFound.writerId.toString() !== userId.toString())
      //without toString() it was throwing an error due to type mismatch
      throw new Error(
        "You don't have permission to edit this message. You can only edit your own messages."
      );

    const updatedMessage = await Boardmsg.findByIdAndUpdate(
      messageId,
      { title, content, platformId },
      { new: true }
    );

    return updatedMessage;
  } catch (error) {
    console.log(error);
    throw new Error("Board Service Error(editMessage): " + error.message);
  }
};

export const checkMsgPasswordService = async (messageId, password) => {
  try {
    const messageFound = await Boardmsg.findById(messageId);
    const isPasswordCorrect = await bcrypt.compare(
      password,
      messageFound.password
    );
    if (!isPasswordCorrect) {
      throw new Error("Incorrect password");
    }
    return true;
  } catch (error) {
    console.log(error);
    throw new Error(
      "Board Service Error(checkMessagePassword): " + error.message
    );
  }
};

export const deleteMessageService = async (messageId, userId) => {
  try {
    const messageFound = await Boardmsg.findById(messageId);
    if (messageFound.writerId.toString() !== userId.toString())
      throw new Error(
        "You don't have permission to delete this message. You can only delete your own messages."
      );

    return Boardmsg.findByIdAndDelete(messageId);
  } catch (error) {
    console.log(error);
    throw new Error("Board Service Error(deleteMessage): " + error.message);
  }
};

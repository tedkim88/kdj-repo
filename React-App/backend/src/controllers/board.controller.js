import {
  getBoardMessagesService,
  saveMessageService,
  editMessageService,
  checkMsgPasswordService,
  deleteMessageService,
  getSingleMessageService
} from "../services/board.service.js";

export const getTotalMessages = async (req, res) => {
  try {
    //when requesting from front, there should be query string info about page
    const { page } = req.query;

    //the index page of the board is 1 as default before users select a certain page
    const pageNum = parseInt(page, 10) || 1;

    const response = await getBoardMessagesService(pageNum);
    res.status(200).json(response);
  } catch (error) {
    console.log(error);
    res
      .status(500)
      .json({ message: "GetTotalMessages for Board Error, " + error.message });
  }
};



export const getMessage = async (req, res) => {
  try {
    const { messageId } = req.params;
    const response = await getSingleMessageService(messageId);
    res.status(200).json(response);
  } catch (error) {
    console.log(error);
    res
      .status(500)
      .json({ message: "GetMessage for Board Error, " + error.message });
  }
}



export const getMessagesByPlatform = async (req, res) => {
  try {
    const { page } = req.query;
    const pageNum = parseInt(page, 10) || 1;
    const { platformId } = req.params;
    const response = await getBoardMessagesService(pageNum, platformId);
    res.status(200).json(response);
  } catch (error) {
    console.log(error);
    res.status(500).json({
      message: "GetMessagesByPlatform for Board Error, " + error.message,
    });
  }
};

export const saveMessagetoDB = async (req, res) => {
  try {
    const { _id: userId } = req.user;
    const { password, title, content, platformId } = req.body;

    if (!password || !title || !content || !platformId) {
      return res.status(400).json({ message: "Missing required fields" });
    }

    if (password.length < 4)
      return res
        .status(400)
        .json({ message: "Password must be at least 4 characters long" });

    const savedMessage = await saveMessageService(
      userId,
      password,
      title,
      content,
      platformId
    );
    res.status(200).json(savedMessage);
  } catch (error) {
    console.log(error);
    res.status(500).json({
      message: "board.controller.js SaveMessage Error, " + error.message,
    });
  }
};

export const editDBmessage = async (req, res) => {
  try {
    const userId = req.user._id;
    const { messageId } = req.params;
    const { title, content, platformId } = req.body;

    //I'm not going to use the password to authenticate here
    //logged in user can only edit their own messages by showing edit button on their own messages in the front
    //but in the service I'm gonna recheck if the message writer(writerId) is the same as the logged in user(req.user._id)
    if (!title || !content || !platformId) {
      return res.status(400).json({ message: "Missing required fields" });
    }

    const response = await editMessageService(
      messageId,
      userId,
      title,
      content,
      platformId
    );

    res.status(200).json(response);
  } catch (error) {
    console.log(error);
    res.status(500).json({
      message: "board.controller.js EditMessage Error, " + error.message,
    });
  }
};

export const checkMessagePassword = async (req, res) => {
  try {
    const { messageId } = req.params;
    const { password } = req.body;
    if (!password)
      return res
        .status(400)
        .json({ message: "password is required to delete the message" });

    const response = await checkMsgPasswordService(messageId, password);
    res.status(200).json({ success: true });
  } catch (error) {
    console.log(error);
    res.status(500).json({
      message:
        "board.controller.js CheckMessagePassword Error, " + error.message,
    });
  }
};

export const deleteMessageFromDB = async (req, res) => {
  try {
    const { messageId } = req.params;
    const userId = req.user._id;
    const response = await deleteMessageService(messageId, userId);
    if (!response)
      return res
        .status(404)
        .json({ success: false, message: "Message not found" });
    res
      .status(200)
      .json({ success: true, message: "Message deleted successfully" });
  } catch (error) {
    console.log(error);
    res.status(500).json({
      message: "board.controller.js DeleteMessage Error, " + error.message,
    });
  }
};

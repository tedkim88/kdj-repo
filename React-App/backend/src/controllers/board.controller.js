import {
  getBoardMessagesService,
  saveMessageService,
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

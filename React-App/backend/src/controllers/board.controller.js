import {getBoardMessagesService} from "../services/board.service.js";

export const getTotalMessages = async (req, res) => {
  try {
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

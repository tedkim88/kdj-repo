import { getMainGamesService } from "../services/game.service.js";

export const getMainGames = async (req, res) => {
  try {
    const response = await getMainGamesService();
    res.status(200).json(response);
  } catch (error) {
    console.log(error);
    res
      .status(500)
      .json({ message: "Internal Server Error, " + error.message });
  }
};

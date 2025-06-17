import {
  getMainGamesService,
  getPlatformGenreService,
} from "../services/game.service.js";

//top rating games recommendation by 4 platforms(to be used for index page)
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

export const getGamesbyPlatformAndGenre = async (req, res) => {
  try {
    console.log('hey')
    const { platform = "", genre = "" } = req.query;
    console.log(req.query);
    if (platform === "" && genre === "") {
      return res
        .status(400)
        .json({ message: "At least one of platform or genre is required." });
    }

    const response = await getPlatformGenreService(platform, genre);
    res.status(200).json(response);
    // This uses JSON.stringify to send the response in JSON string format
    // HTTP requests/responses can only send data as strings, not as raw objects
  } catch (error) {
    console.log("getGamesbyPlatformAndGenre Error: ", error);
    res
      .status(500)
      .json({ message: "Internal Server Error, " + error.message });
  }
};

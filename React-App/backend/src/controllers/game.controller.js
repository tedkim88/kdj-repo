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
    // const { platform, genre } = req.params;
    
    //actually below is a platfromId and genreId, gonna leave as is for convenience
    const { platform = "", genre = "" } = req.query;
    //initializing

    //instead of using url params for platform and genre, let's use query params.

    // I'm gonna use the code below in frontend component to send request to backend
    // const fetchGames = async (platform, genre) => {
    //   try {
    //     const params = {};
    //     if (platform) params.platform = platform;
    //     if (genre) params.genre = genre;

    //     const response = await axios.get("/get/detailSearch", { params });
    //     console.log(response.data);
    //   } catch (error) {
    //     console.error("Error fetching games:", error);
    //   }
    // };

    if (!platform && !genre) {
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

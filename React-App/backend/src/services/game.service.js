import dotenv from "dotenv";
dotenv.config(); // to use process.env

const RAWG_BASE_URL = "https://api.rawg.io/api";
const RAWG_KEY = process.env.RAWG_KEY;
const platformIds = [4, 187, 7, 186];

export const getMainGamesService = async () => {
  try {
    const mainGamesArray = await Promise.all(
      platformIds.map(async (id) => {
        const response = await fetch(
          `${RAWG_BASE_URL}/games?key=${RAWG_KEY}&platforms=${id}&ordering=-rating&page_size=4`
        );
        if (!response.ok) throw new Error(`Failed to fetch for platform ${id}`);
        const data = await response.json();
        return { platformId: id, games: data.results };
      })
    );

    console.log("mainGamesArray: ", mainGamesArray);

    //refactored like above
    // const result = await fetch(
    //   `${RAWG_BASE_URL}/games?key=${RAWG_KEY}&platforms=4,187,7,186`
    // );
    // const data = await result.json();

    return mainGamesArray;
    //this is going to be used for the index page.(front) to show top 4 most popular games depending on platforms.
  } catch (error) {
    console.log(error);
    throw new Error("Service Error: " + error.message);
  }
};

import dotenv from "dotenv";
dotenv.config(); // to use process.env

const RAWG_BASE_URL = "https://api.rawg.io/api";
const RAWG_KEY = process.env.RAWG_KEY;
const platformIds = [4, 187, 7, 186];

//this is going to be used for the index page.(front) to show top 4 most popular games depending on platforms.
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

    //below was refactored like above
    // const result = await fetch(
    //   `${RAWG_BASE_URL}/games?key=${RAWG_KEY}&platforms=4,187,7,186`
    // );
    // const data = await result.json();

    return mainGamesArray;
  } catch (error) {
    console.log(error);
    throw new Error("Service Error: " + error.message);
  }
};

//this is going to be used for detailSearch (by platform and/or genre)
export const getPlatformGenreService = async (platform, genre) => {
  let url = `${RAWG_BASE_URL}/games?key=${RAWG_KEY}&ordering=-rating&page_size=10`;

  if (platform) url += `&platforms=${platform}`;
  if (genre) url += `&genre=${genre}`;

  const response = await fetch(url);
  if (!response.ok) throw new Error("Failed to fetch games");
  return await response.json();
  // JSON parsing is needed to get the body part from response object because fetch returns JSON as a string

  //below was refactored like above to avoid using the same base url for different cases..hmm..
  // if (!platform) {
  //   const response = await fetch(
  //     `${RAWG_BASE_URL}/games?key=${RAWG_KEY}&ordering=-rating&genre=${genre}&page_size=10`
  //   );
  // } else if (!genre) {
  //   await fetch(
  //     `${RAWG_BASE_URL}/games?key=${RAWG_KEY}&ordering=-rating&platforms=${platform}&page_size=10`
  //   );
  // } else {
  //   await fetch(
  //     `${RAWG_BASE_URL}/games?key=${RAWG_KEY}&ordering=-rating&platforms=${platform}&genre=${genre}&page_size=10`
  //   );
  // }
};

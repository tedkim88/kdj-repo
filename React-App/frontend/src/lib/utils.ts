import axiosInstance from "../lib/axios";
export const fetchGames = async () => {
  try {
    const response = await axiosInstance.get("/games/get");
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

export const fetchGamesByPlatformAndGenre = async (
  platform: string,
  genre: string
) => {
  try {
    const params = { platform: "", genre: "" };
    if (platform !== "") params.platform = platform;
    if (genre !== "") params.genre = genre;

    console.log(params);

    
    const response = await axiosInstance.get("games/get/detailSearch", { params });
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error("Error fetching games:", error);
  }
};

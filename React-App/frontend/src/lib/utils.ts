import axiosInstance from "../lib/axios";
export const fetchGames = async () => {
      try {
        const response = await axiosInstance.get("/games/get");
        return response.data;
      } catch (error) {
        console.error(error);
      }
    };
import React, { useState } from "react";
import GameCard from "../components/GameCard";
import axios from "axios";
import { fetchGamesByPlatformAndGenre } from "../lib/utils";
import { PLATFORM_SEARCH, GENRES } from "../lib/constants";
import { useEffect } from "react";
import { BG_IMAGES } from "../lib/constants";
export default function Search() {
  const [games, setGames] = useState([]);
  const [platform, setPlatform] = useState("");
  const [genre, setGenre] = useState("");
  const [bgIndex, setBgIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setBgIndex((prevIndex) => (prevIndex + 1) % BG_IMAGES.length);
    }, 5000);
    return () => clearInterval(interval);
  }, []);

  const handleSubmit = async (e: { preventDefault: () => void }) => {
    e.preventDefault();
    const fetchedGames = await fetchGamesByPlatformAndGenre(platform, genre);
    setGames(fetchedGames);
  };

  return (
    <div
      className="min-h-screen flex items-center justify-center p-4"
      style={{
        backgroundImage: `url(${BG_IMAGES[bgIndex]})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
        transition: "background-image 1s ease-in-out",
      }}
    >
      <form
        onSubmit={handleSubmit}
        className="bg-white bg-opacity-60 p-8 rounded-lg shadow-lg relative z-10 w-full max-w-md
               flex flex-col items-center gap-4"
      >
        <div className="form-control w-full flex-1">
          <select
            className="select select-bordered w-full"
            onChange={(e) => setPlatform(e.target.value)}
            value={platform}
          >
            <option value="" selected>
              Select Platform
            </option>
            {PLATFORM_SEARCH.map((platform) => (
              <option key={platform.id} value={platform.id}>
                {platform.name}
              </option>
            ))}
          </select>
        </div>

        <div className="form-control w-full flex-1">
          <select
            className="select select-bordered w-full"
            onChange={(e) => setGenre(e.target.value)}
            value={genre}
          >
            <option value="" selected>
              Select Genre
            </option>
            {GENRES.map((genre) => (
              <option key={genre.id} value={genre.id}>
                {genre.name}
              </option>
            ))}
          </select>
        </div>

        <button className="btn btn-primary w-full" type="submit">
          Search
        </button>
      </form>
    </div>
  );
}

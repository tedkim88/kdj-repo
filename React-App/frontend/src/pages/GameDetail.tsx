import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axiosInstance from "../lib/axios";
import type { Game } from "../lib/types";

export default function GameDetail() {
  const { id } = useParams();
  const [game, setGame] = useState<Game | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    axiosInstance.get(`/games/get/${id}`).then((response) => {
      console.log(response.data);
      setGame(response.data);
    });
  }, [id]);

  if (!game)
    return (
      <div className="flex flex-col items-center h-[calc(100vh-76px)] justify-center text-yellow-500 text-xl font-semibold space-y-4">
        <div className="animate-spin rounded-full h-12 w-12 border-t-4 border-b-4 border-red-500"></div>
        <div>Loading Game Info</div>
      </div>
    );

  return (
    <div className="flex justify-center py-2 px-4 items-center min-h-[calc(100vh-76px)] bg-gradient-to-tr from-blue-900 via-gray-900 to-indigo-800 text-white">
      <div className="card w-full max-w-6xl bg-base-200 shadow-xl">
        <figure className="relative h-96 overflow-hidden">
          <img
            src={game.background_image}
            alt="Game background"
            className="w-full h-full object-cover"
          />
          <div className="absolute bottom-0 left-0 right-0 bg-black bg-opacity-60 text-white px-4 py-2 text-xl font-semibold">
            {game.name}
          </div>
        </figure>

        <div className="card-body">
          <p className="text-lg">
            <span className="font-bold text-secondary">Rating:</span>{" "}
            {game.rating}
          </p>
          <p className="text-lg">
            <span className="font-bold text-secondary">Released:</span>{" "}
            {game.released}
          </p>

          <p className="text-lg">
            <span className="font-bold text-secondary">Platform:</span>{" "}
            {game.platforms
              .map((platform) => platform.platform.name)
              .join(", ")}
          </p>

          {game.website ? (
            <p className="text-lg">
              <span className="font-bold text-secondary">Website:</span>{" "}
              <a href={game.website} target="_blank" rel="noopener noreferrer">
                {game.website}
              </a>
            </p>
          ) : (
            <p className="text-lg">
              {" "}
              <span className="font-bold text-secondary">Website:</span> No
              Website Found{" "}
            </p>
          )}

          <div className="mt-4">
            <h3 className="font-bold text-lg mb-2">Tags</h3>
            {game.tags.length === 0 ? (
              <p>No tags available.</p>
            ) : (
              <div className="flex flex-wrap gap-2">
                {game.tags.map((tag) => (
                  <div
                    key={tag.id}
                    className="badge badge-accent badge-outline"
                  >
                    {tag.name}
                  </div>
                ))}
              </div>
            )}
          </div>

          <div className="card-actions justify-end mt-6">
            <button
              className="btn btn-primary"
              onClick={() => navigate("/search")}
            >
              Back to Search
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

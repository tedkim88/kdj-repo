import  { useEffect, useState } from "react";
import { fetchGames } from "../lib/utils";
import type { GamePlatform } from "../lib/types";
import GameCard from "../components/GameCard";
import { PLATFORM_NAMES } from "../lib/constants";

export default function HomePage() {
  const [games, setGames] = useState<GamePlatform[]>([]);
  useEffect(() => {
    fetchGames().then((data) => {
      setGames(data);
      console.log(data);
    });
  }, []);

  if (games.length === 0) {
    return (
      <div className="flex flex-col items-center h-[calc(100vh-76px)] justify-center text-yellow-500 text-xl font-semibold space-y-4">
        <div className="animate-spin rounded-full h-12 w-12 border-t-4 border-b-4 border-red-500"></div>
        <div>Loading Games...</div>
      </div>
    );
  }

  return (
    <div className="container mx-auto max-w-full px-4 mt-12">
      {games.map((gamePlatform, idx) => (
        <div key={gamePlatform.platformId} className="mb-8 w-full">
          <h2 className="text-3xl text-center font-bold italic mb-4 text-yellow-400">
            {PLATFORM_NAMES[gamePlatform.platformId]}
          </h2>
          <ul className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-6 justify-items-center">
            {gamePlatform.games.map((game, idx) => (
              //isBest is boolean checking if this is the first game(which has top rating)
              <GameCard key={game.id} game={game} isBest={idx === 0} />
            ))}
          </ul>

          {idx < games.length - 1 && (
            <hr className="border-t border-gray-300 my-8" />
          )}
        </div>
      ))}
    </div>
  );
}

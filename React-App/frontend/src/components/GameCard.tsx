import { useEffect } from "react";
import { Link } from "react-router-dom";
import type { Game } from "../lib/types";

type Props = {
  game: Game;
  isBest: boolean;
};

export default function GameCard({ game, isBest }: Props) {
  useEffect(() => {
    console.log(game);
  }, [game]);

  return (
   <div className="card card-compact bg-stone-950 w-full max-w-xs shadow-xl mx-auto">

      <figure className="h-48 overflow-hidden">
        <img
          className="w-full h-full object-cover"
          src={game.background_image}
          alt="game background image"
        />
      </figure>
      <div className="card-body">
        <h2 className="card-title">{game.name}</h2>
        <p
          className={`text-center text-xl ${
            isBest ? "text-yellow-400 font-bold" : "text-primary"
          }  flex justify-center place-items-end`}
        >
          {isBest ? "TOP" : ""} Rating : {game.rating} {isBest ? "🔥🔥🔥" : ""}
        </p>
        <Link to={`/games/${game.id}`} className="btn btn-secondary">
          Details
        </Link>
      </div>
    </div>
  );
}

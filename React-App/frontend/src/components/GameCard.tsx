import React from "react";
import type { Game } from "../lib/types";
type Props = {
  game: Game;
  isBest : boolean;
};

export default function GameCard({ game, isBest }: Props) {
  return (
    <div className="card card-compact bg-stone-950 w-96 shadow-xl">
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
            isBest ? "text-yellow-400" : "text-primary"
          }  flex justify-center place-items-end`}
        >
          {isBest ? "TOP" : ""} Rating : {game.rating} {isBest ? "🔥🔥🔥" : ""}
        </p>
        <div className="card-actions justify-end">
          <button className="btn btn-secondary">Details</button>
        </div>
      </div>
    </div>
  );
}

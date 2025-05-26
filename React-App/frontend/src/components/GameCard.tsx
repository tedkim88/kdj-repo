import React from "react";
import type { Game } from "../lib/types";
type Props = {
  game: Game;
};

export default function GameCard({ game }: Props) {
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
        <p className="text-center text-primary">Rating : {game.rating}</p>
        <div className="card-actions justify-end">
          <button className="btn btn-secondary">Details</button>
        </div>
      </div>
    </div>
  );
}

export type Game = {
  id: number;
  name: string;
  background_image: string;
  rating: number;
};

export interface GamePlatform {
  platformId: 4;
  games: Game[];
}

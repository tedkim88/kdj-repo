//adding type info that I need, not all of them.
export type Game = {
  id: number;
  name: string;
  background_image: string;
  rating: number;
};

export interface User {
  _id: string;
  name: string;
  email: string;
  password: string;
  role: string;
}

export interface GamePlatform {
  platformId: 4;
  games: Game[];
}

export type SignupData = {
  name: string;
  email: string;
  password: string;
};

export type LoginData = {
  email: string;
  password: string;
};


export interface AuthState {
  authUser: User | null;
  isSigningUp: boolean;
  isLoggingIn: boolean;
  isCheckingAuth: boolean;
  signup: (data: SignupData) => Promise<User>;
  checkAuth: () => Promise<void>;
  login: (data: LoginData) => Promise<User>;
  logout: () => Promise<void>;
}

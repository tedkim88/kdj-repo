
import { Socket } from "socket.io-client";
//adding type info that I need, not all of them.

export type GameTag = {
  id: number;
  name: string;
  
}
export type Game = {
  id: number;
  name: string;
  background_image: string;
  rating: number;
  released: string;
  tags: GameTag[];
  platforms: { platform: { name: string } }[];
  website: string;
};

export interface User {
  _id: string;
  name: string;
  email: string;
  password: string;
  role: string;
  online: boolean;
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
  socket : Socket | null;
  onlineUsers: string[] | null;
  connectSocket: () => void;
  disconnectSocket: () => void;

  signup: (data: SignupData) => Promise<User>;
  checkAuth: () => Promise<void>;
  login: (data: LoginData) => Promise<User>;
  logout: () => Promise<void>;
}

export type Messages = {
  _id: string;
  writerId: string;
  writerNickname: string;
  title: string;
  content: string;
  platformId: string;
};

export type ChatMessages = {
  _id: string;
  senderId: User | string;
  receiverId: User | string;
  text: string;
  createdAt: string;

};


import { create } from "zustand";
import { toast } from "react-hot-toast";
import axiosInstance from "../lib/axios";
import type { SignupData, LoginData, AuthState } from "../lib/types";
import { AxiosError } from "axios";
import { io } from "socket.io-client";

const BASE_URL = "http://localhost:3000";

//this shape is a bit tricky. this is returning an object. instead of {{}}, ({}) has been used.
export const useAuthStore = create<AuthState>((set, get) => ({
  authUser: null,
  isSigningUp: false,
  isLoggingIn: false,
  // isUpdatingProfile: false,
  isCheckingAuth: true,
  socket: null,
  onlineUsers: [],

  signup: async (data: SignupData) => {
    try {
      set({ isSigningUp: true });
      const res = await axiosInstance.post("/auth/signup", data);
      const delay = (ms: number) =>
        new Promise((resolve) => setTimeout(resolve, ms));

      //artificial delay
      await delay(3000);

      set({ authUser: res.data });
      toast.success("Account created successfully");
      get().connectSocket();

      return res.data;
    } catch (error) {
      const err = error as AxiosError<{ message: string }>;
      console.log("Error in signup", error);
      toast.error(err.response?.data.message ?? "An unexpected error occurred");
    } finally {
      set({ isSigningUp: false });
    }
  },

  login: async (data: LoginData) => {
    try {
      set({ isLoggingIn: true });
      const res = await axiosInstance.post("/auth/login", data);
      const delay = (ms: number) =>
        new Promise((resolve) => setTimeout(resolve, ms));
      await delay(2000); //artificial delay
      set({ authUser: res.data });
      toast.success("Login successful");

      get().connectSocket();

      return res.data;
    } catch (error) {
      console.log("Error in login", error);
      const err = error as AxiosError<{ message: string }>;
      toast.error(err.response?.data.message ?? "An unexpected error occurred");
    } finally {
      set({ isLoggingIn: false });
    }
  },

  logout: async () => {
    try {
      const response = await axiosInstance.post("/auth/logout");
      set({ authUser: null });
      toast.success("Logout successful");

      get().disconnectSocket();
      return response.data;
    } catch (error) {
      console.log("Error in logout", error);
      const err = error as AxiosError<{ message: string }>;
      toast.error(err.response?.data.message ?? "An unexpected error occurred");
    }
  },

  checkAuth: async () => {
    try {
      const res = await axiosInstance.get("/auth/check");
      set({ authUser: res.data });
    } catch (error) {
      console.log("Error in checkAuth", error);
      set({ authUser: null });
    } finally {
      set({ isCheckingAuth: false });
    }
  },

  connectSocket: async () => {
    //훅은 컴포넌트 안에서만 호출가능하다는 제약이있음.
    // const {authUser} = useAuthStore(); 불가능. 여기는 컴포넌트가아니라 훅 정의하는 파일이기 때문

    const { authUser } = get();
    if (!authUser || get().socket?.connected) return; //인증안되는 애는 소켓연결시도 막기 혹은 인증 되어있는애는 재연결막기?
    //백엔드 포트에 연결
    const socket = io(BASE_URL, {
      query: {
        userId: authUser._id,
      },
    }); //소켓객체생성성

    socket.connect(); // 명시적연결
    socket.on("connect", () => {
      console.log("SOCKET CONNECTED :", socket.id);
    });
    set({ socket: socket }); //이렇게 상태변화 명시안해주면 로그인후에도 socket이 null(초기값)이 되어있어서 밑에  disonnectSocket이 실행되지않음Logout했을때조차.

    //socket객체가(개인의...) 여기서 만들어졌기 때문에 socket객체의 메서드는 계속 여기서 콜하는 것
    //""안에 잇는 명칭의 요청(?)이 오면 함수실행?
    socket.on("getOnlineUsers", (userIds) => {
      set({ onlineUsers: userIds });
    });
  },

  disconnectSocket: async () => {
    // const socket = io("http://localhost:3000");
    //새로운객체만들면아됨 위에서 이미 소켓객체만들었음. io(Base_url여기서) 새로운객체만들면 그건 서로다른 인스턴스..기존연결끊을수없음

    //socket?.의 의미 socket이 널일수있음. 널이면 실행하지마라..라기보다 널이면 당연히 실행이안되는거
    console.log("SOCKET DISCONNECTED : ", get().socket?.id);
    if (get().socket?.connected) get().socket?.disconnect();
  },
}));

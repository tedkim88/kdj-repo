import { create } from "zustand";
import { toast } from "react-hot-toast";
import axiosInstance from "../lib/axios";
import type { SignupData, LoginData, AuthState } from "../lib/types";
import { AxiosError } from "axios";

//this shape is a bit tricky. this is returning an object. instead of {{}}, ({}) has been used.
export const useAuthStore = create<AuthState>((set) => ({
  authUser: null,
  isSigningUp: false,
  isLoggingIn: false,
  // isUpdatingProfile: false,
  isCheckingAuth: true,

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
}));

import React from "react";
import { useEffect } from "react";
import { useAuthStore } from "../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import Sidebar from "../components/Sidebar";
import ChatWindow from "../components/ChatWindow";
import ChatInput from "../components/ChatInput";
import type { User } from "../lib/types";

export default function LiveChat() {
  const navigate = useNavigate();
  const { authUser } = useAuthStore();
  const [redirecting, setRedirecting] = useState(false);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);

  useEffect(() => {
    if (!authUser) {
      setRedirecting(true);
      const timer = setTimeout(() => {
        navigate("/login");
      }, 3000);

      return () => clearTimeout(timer); // cleanup
    }
  }, []);

  return (
    <div className="overflow-x-auto h-[calc(100vh-76px)] bg-gradient-to-r from-blue-500 via-indigo-500 to-red-700 p-6">
      {redirecting && (
        <div className="flex flex-col items-center min-h-[calc(100vh-76px)] justify-center text-yellow-400 text-2xl font-extrabold space-y-6 bg-black bg-opacity-50 rounded-lg shadow-lg mx-4">
          <div className="animate-spin rounded-full h-16 w-16 border-t-4 border-b-4 border-yellow-300"></div>
          <div>
            Please login first to use Live Chat, redirecting to login page
          </div>
        </div>
      )}

      {authUser && (
        <div className="flex h-[calc(100vh-160px)] bg-gray-800 rounded-lg shadow-xl overflow-hidden mx-6 my-4">
          <Sidebar onSelectUser={setSelectedUser} />
          {selectedUser ? (
            <div className="flex flex-col flex-1 border-l border-gray-300">
              <ChatWindow selectedUser={selectedUser} />
              <ChatInput selectedUser={selectedUser} />
            </div>
          ) : (
            <div className="flex-1 flex items-center justify-center bg-gray-900 text-gray-200 italic text-lg">
              Select a user to start chatting
            </div>
          )}
        </div>
      )}
    </div>
  );
}

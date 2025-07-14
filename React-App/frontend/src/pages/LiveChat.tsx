import React from "react";
import { useEffect } from "react";
import { useAuthStore } from "../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import Sidebar from "../components/Sidebar";
import ChatWindow from "../components/ChatWindow";
import ChatInput from "../components/ChatInput";
import axiosInstance from "../lib/axios";
import { toast } from "react-hot-toast";
import type { User, ChatMessages } from "../lib/types";

export default function LiveChat() {
  const navigate = useNavigate();
  const { authUser } = useAuthStore();
  const [redirecting, setRedirecting] = useState(false);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [messages, setMessages] = useState<ChatMessages[]>([]);
  const [messageError, setMessageError] = useState<string | null>(null);

  useEffect(() => {
    if (!authUser) {
      setRedirecting(true);
      const timer = setTimeout(() => {
        navigate("/login");
      }, 3000);

      return () => clearTimeout(timer); // cleanup
    }
  }, [authUser]);

  useEffect(() => {
    if (selectedUser) {
      axiosInstance
        .get(`/chat/getMessages/?receiverId=${selectedUser._id}`)
        .then((res) => {
          setMessages(res.data);
          console.log(res.data);
          setMessageError(null);
        })
        .catch((error) => {
          console.error("Failed to load messages:", error);
          setMessageError("Failed to load messages.");
          setMessages([]);
        });
    }
  }, [selectedUser]);

  useEffect(() => {
    const socket = useAuthStore.getState().socket;
    if (!socket) return;

    const handleNewMessage = (newMessage: ChatMessages) => {
      // 새 메시지가 현재 선택된 채팅방과 관련 있는지 확인
      const isCurrentChat =
        selectedUser &&
        (newMessage.senderId === selectedUser._id ||
          newMessage.receiverId === selectedUser._id);

      if (isCurrentChat) {
        setMessages((prev) => [...prev, newMessage]);
        setMessageError(null);
      } else {
        

        // chatMsg model doesn't have User's name as a field, so I touched backend
        // to populate it

        const senderName =
          typeof newMessage.senderId === "string"
            ? "Unknown User"
            : newMessage.senderId.name;

        toast(`New message from user ${senderName}`, {
          icon: "📩",
          duration: 5000,
          position: "top-right",
          style: {
            background: "#333",
            color: "#fff",
          },
          
        });
      }
    };

    socket.on("newMessage", handleNewMessage);

    return () => {
      socket.off("newMessage", handleNewMessage);
    };
  }, [selectedUser]);

  const addMessage = (newMessage: ChatMessages) => {
    setMessages((prev) => [...prev, newMessage]);
    setMessageError(null);
  };

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
              <ChatWindow
                selectedUser={selectedUser}
                messages={messages}
                error={messageError}
              />
              <ChatInput
                selectedUser={selectedUser}
                onSendMessage={addMessage}
              />
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

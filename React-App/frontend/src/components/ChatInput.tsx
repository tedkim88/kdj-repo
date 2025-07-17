import type { ChatMessages, User } from "../lib/types";
import axiosInstance from "../lib/axios";
import { useState } from "react";
import { toast } from "react-hot-toast";
type Props = {
  selectedUser: User | null;
  onSendMessage: (message: ChatMessages) => void;
};



export default function ChatInput({ selectedUser, onSendMessage }: Props) {
  const [message, setMessage] = useState("");

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      if (!message.trim() || !selectedUser) {
        throw new Error("Please enter a message");
      }

      const res = await axiosInstance.post("/chat/send", {
        receiverId: selectedUser._id,
        text: message,
      });

      setMessage("");
      onSendMessage(res.data);
    } catch (err) {
      const errorMessage =
        err instanceof Error ? err.message : "An unknown error occurred.";
      toast.error(errorMessage);
    }
  };

  return (
    <div className="p-3 sm:p-4 border-t border-gray-700 bg-gray-900">
      <form className="flex gap-2" onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder={`Message to ${selectedUser?.name || "..."}`}
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          className="flex-1 px-4 py-2 rounded-md bg-gray-800 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500 text-sm sm:text-base"
        />
        <button
          type="submit"
          className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition text-sm sm:text-base"
        >
          Send
        </button>
      </form>
    </div>
  );
}

import { useEffect, useRef } from "react";
import type { ChatMessages, User } from "../lib/types";

type Props = {
  selectedUser: User | null;
  messages: ChatMessages[];
  error: string | null;
  authUser: User | null;
};

export default function ChatWindow({
  selectedUser,
  messages,
  error,
  authUser,
}: Props) {
  const messagesEndRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages, selectedUser]);

  if (!authUser) {
    return (
      <div className="text-gray-500 italic p-6">
        Please log in to view messages.
      </div>
    );
  }

  return (
    <div className="flex-1 bg-gray-900 text-gray-100 p-4 overflow-y-auto flex flex-col space-y-4">
      {selectedUser ? (
        <>
          <div className="text-lg sm:text-xl text-yellow-300 font-semibold">
            To: <span className="uppercase">{selectedUser.name}</span>
          </div>

          {error && messages.length === 0 ? (
            <div className="text-red-500 font-semibold">No Messages yet.</div>
          ) : error && messages.length !== 0 ? (
            <div className="text-gray-400 italic">{error}</div>
          ) : (
            messages.map((msg) => {
              const senderId =
                typeof msg.senderId === "string"
                  ? msg.senderId
                  : msg.senderId._id;
              const isSender = senderId === authUser._id;

              return (
                <div
                  key={msg._id}
                  className={`max-w-[75%] px-4 py-3 rounded-lg shadow-sm break-words ${
                    isSender
                      ? "bg-blue-600 self-end text-white"
                      : "bg-gray-700 self-start text-gray-200"
                  }`}
                >
                  <p className="text-sm sm:text-base">{msg.text}</p>
                  <div className="text-xs sm:text-sm text-gray-400 mt-1 text-right">
                    {new Date(msg.createdAt).toLocaleTimeString()}
                  </div>
                </div>
              );
            })
          )}

          <div ref={messagesEndRef} />
        </>
      ) : (
        <div className="text-gray-500 italic text-center mt-10">
          No user selected
        </div>
      )}
    </div>
  );
}

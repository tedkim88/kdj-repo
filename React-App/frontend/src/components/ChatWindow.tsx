import type { ChatMessages, User } from "../lib/types";

type Props = {
  selectedUser: User | null;
  messages: ChatMessages[];
  error: string | null;
};

export default function ChatWindow({ selectedUser, messages, error }: Props) {
  return (
    <div className="flex-1 bg-gray-900 text-gray-100 p-6 overflow-y-auto space-y-4">
      {selectedUser ? (
        <>
          <div className="text-xl text-yellow-300 font-bold mb-4">
            To: <span className="uppercase">{selectedUser.name}</span>
          </div>

          {error ? (
            <div className="text-red-500 font-semibold">{error}</div>
          ) : messages.length === 0 ? (
            <div className="text-gray-400 italic">No messages yet.</div>
          ) : (
            messages.map((msg) => (
              <div
                key={msg._id}
                className={`max-w-xs px-4 py-2 rounded-md ${
                  msg.senderId === selectedUser._id
                    ? "bg-gray-700 self-start"
                    : "bg-blue-600 self-end ml-auto"
                }`}
              >
                <p>{msg.text}</p>
                <div className="text-xs text-gray-400 mt-1">
                  {new Date(msg.createdAt).toLocaleTimeString()}
                </div>
              </div>
            ))
          )}
        </>
      ) : (
        <div className="text-gray-500 italic">No user selected</div>
      )}
    </div>
  );
}

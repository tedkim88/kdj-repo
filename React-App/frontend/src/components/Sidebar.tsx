import { useEffect, useState } from "react";
import type { User } from "../lib/types";
import axiosInstance from "../lib/axios";
import { useAuthStore } from "../store/useAuthStore";

type Props = {
  onSelectUser: (user: User | null) => void;
  selectedUser: User | null;
};

export default function Sidebar({ onSelectUser, selectedUser }: Props) {
  const [users, setUsers] = useState<User[]>([]);
  const { onlineUsers } = useAuthStore();

  useEffect(() => {
    axiosInstance
      .get("/chat/getUsers")
      .then((res) => setUsers(res.data))
      .catch((err) => console.error("Failed to load users", err));
  }, []);

  return (
    <>
      {/* 모바일에서만 보이는 드롭다운 */}
      <div className="sm:hidden p-4 bg-gray-900 text-gray-200 border-b border-gray-700">
        <select
          className="w-full bg-gray-800 text-white rounded-md p-2"
          value={selectedUser?._id || ""}
          onChange={(e) => {
            const user = users.find((u) => u._id === e.target.value) || null;
            onSelectUser(user);
          }}
        >
          <option value="">Select user</option>
          {users.map((user) => (
            <option key={user._id} value={user._id}>
              {user.name} {onlineUsers?.includes(user._id) ? "🟢" : "⚪️"}
            </option>
          ))}
        </select>
      </div>

      {/* 데스크톱에서 보이는 기존 사이드바 */}
      <div className="hidden sm:flex flex-col min-w-[80px] sm:w-72 bg-gray-900 text-gray-200 border-r border-gray-700">
        <h2 className="px-4 py-3 text-xl font-bold border-b border-gray-700">
          Users
        </h2>
        <ul className="flex-1 overflow-y-auto p-2 space-y-2">
          {users.map((user) => (
            <li
              key={user._id}
              className={`flex items-center justify-between gap-2 p-2 rounded-md cursor-pointer select-none transition
                ${
                  selectedUser?._id === user._id
                    ? "bg-indigo-700 text-white"
                    : "hover:bg-indigo-600 hover:text-white"
                }
              `}
              onClick={() => onSelectUser(user)}
              tabIndex={0}
              onKeyDown={(e) => {
                if (e.key === "Enter" || e.key === " ") onSelectUser(user);
              }}
              title={user.name}
            >
              <span className="truncate text-sm sm:text-base max-w-[140px] sm:max-w-[180px]">
                {user.name}
              </span>
              <span
                className={`w-3 h-3 sm:w-4 sm:h-4 rounded-full ${
                  onlineUsers?.includes(user._id)
                    ? "bg-green-400"
                    : "bg-gray-500"
                }`}
                title={onlineUsers?.includes(user._id) ? "Online" : "Offline"}
              />
            </li>
          ))}
        </ul>
      </div>
    </>
  );
}

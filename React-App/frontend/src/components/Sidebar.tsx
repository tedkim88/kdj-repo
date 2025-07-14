import React, { useEffect, useState } from "react";
import type { User } from "../lib/types";
import axiosInstance from "../lib/axios";
import { useAuthStore } from "../store/useAuthStore";

type Props = {
  onSelectUser: (user: User | null) => void;
};

export default function Sidebar({ onSelectUser }: Props) {
  const [users, setUsers] = useState<User[]>([]);
  const { onlineUsers } = useAuthStore();

  useEffect(() => {
    axiosInstance
      .get("/chat/getUsers")
      .then((res) => setUsers(res.data))
      .catch((err) => console.error("Failed to load users", err));
  }, []);

  return (
    <div className="w-64 bg-gray-900 text-gray-200 border-r border-gray-700 flex flex-col">
      <h2 className="p-4 text-xl font-extrabold border-b border-gray-700 select-none">
        Users
      </h2>
      <ul className="flex-1 overflow-y-auto">
        {users.map((user) => (
          <li
            key={user._id}
            className="p-3 cursor-pointer hover:bg-indigo-600 hover:text-white transition-colors duration-300 rounded-md mx-2 my-1 select-none flex items-center justify-between"
            onClick={() => onSelectUser(user)}
            tabIndex={0}
            onKeyDown={(e) => {
              if (e.key === "Enter" || e.key === " ") onSelectUser(user);
            }}
          >
            <span>{user.name}</span>
            <span
              className={`ml-2 w-3 h-3 rounded-full ${
                onlineUsers?.includes(user._id) ? "bg-green-400" : "bg-gray-500"
              }`}
              title={user.online ? "Online" : "Offline"}
            ></span>
          </li>
        ))}
      </ul>
    </div>
  );
}

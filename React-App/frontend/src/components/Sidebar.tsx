import React, { useEffect, useState } from "react";
import type { User } from "../lib/types";
import axiosInstance from "../lib/axios";

type Props = {
  onSelectUser: (user: User | null) => void;
};

export default function Sidebar({ onSelectUser }: Props) {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    axiosInstance.get("/chat/getUsers")
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
            className="p-3 cursor-pointer hover:bg-indigo-600 hover:text-white transition-colors duration-300 rounded-md mx-2 my-1 select-none"
            onClick={() => onSelectUser(user)}
            tabIndex={0} // 키보드 접근성
            onKeyDown={(e) => {
              if (e.key === "Enter" || e.key === " ") onSelectUser(user);
            }}
          >
            {user.name}
          </li>
        ))}
      </ul>
    </div>
  );
}
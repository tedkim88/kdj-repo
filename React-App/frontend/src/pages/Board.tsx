import React, { useEffect, useState } from "react";
import axiosInstance from "../lib/axios";
import type { Messages } from "../lib/types";
import { Link } from "react-router-dom";
import { useAuthStore } from "../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import { PLATFORM_SEARCH } from "../lib/constants";

export default function Board() {
  const [messages, setMessages] = useState<Messages[]>([]);
  const { authUser } = useAuthStore();
  const navigate = useNavigate();
  const [redirecting, setRedirecting] = useState(false);

  useEffect(() => {
    if (!authUser) {
      setRedirecting(true);
      const timer = setTimeout(() => {
        navigate("/login");
      }, 3000);

      return () => clearTimeout(timer); // cleanup
    }
  }, []);

  useEffect(() => {
    axiosInstance
      .get("/board/all")
      .then((res) => setMessages(res.data))
      .catch((err) => console.error("Error fetching messages", err));
  }, []);

  return (
    <div className="overflow-x-auto min-h-[calc(100vh-76px)]">
      {redirecting && (
        <div className="flex flex-col items-center h-[calc(100vh-76px)] justify-center text-yellow-500 text-xl font-semibold space-y-4">
          <div className="animate-spin rounded-full h-12 w-12 border-t-4 border-b-4 border-red-500"></div>
          <div>
            Please login first to use the Board, redirecting to login page
          </div>
        </div>
      )}
      {messages.length > 0 && (
        <table className="table w-full text-white border border-gray-700">
          <thead className="bg-gray-800 text-yellow-300">
            <tr>
              <th className="text-center w-1/6 border-b border-gray-600">#</th>
              <th className="text-center w-1/6 border-b border-gray-600">
                Nick Name
              </th>
              <th className="text-center w-1/2 border-b border-gray-600">
                Title
              </th>
              <th className="text-center w-1/6 border-b border-gray-600">
                Platform
              </th>
            </tr>
          </thead>
          <tbody className="bg-gray-900 divide-y divide-gray-700">
            {messages.map((msg, idx) => (
              <tr
                key={msg._id}
                className={`hover:bg-blue-50 hover:text-lg ${
                  idx % 2 === 0 ? "bg-white" : "bg-gray-400"
                }`}
              >
                <td className="border border-gray-300 px-4 py-2 text-center text-gray-900">
                  {idx + 1}
                </td>
                <td className="border border-gray-300 px-4 py-2 text-center text-gray-900">
                  {msg.writerNickname}
                </td>
                <td className="border border-gray-300 px-4 py-2 text-center">
                  <Link
                    to={`/board/${msg._id}`}
                    className="text-blue-700 underline hover:text-blue-900 font-medium"
                  >
                    {msg.title}
                  </Link>
                </td>
                <td className="border border-gray-300 px-4 py-2 text-center text-gray-900">
                  {PLATFORM_SEARCH.find(
                    (p) => p.id.toString() === msg.platformId
                  )?.name ?? "N/A"}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

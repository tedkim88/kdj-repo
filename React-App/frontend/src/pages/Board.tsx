import React, { useEffect, useState } from "react";
import axiosInstance from "../lib/axios";
import type { Messages } from "../lib/types";
import { Link } from "react-router-dom";
export default function Board() {
  const [messages, setMessages] = useState<Messages[]>([]);

  useEffect(() => {
    axiosInstance
      .get("/board/all")
      .then((res) => setMessages(res.data))
      .catch((err) => console.error("Error fetching messages", err));
  }, []);

  return (
    <div className="overflow-x-auto">
      <table className="table w-full text-white border border-gray-700">
        <thead className="bg-gray-800 text-yellow-300">
          <tr>
            <th className="text-center w-1/6 border-b border-gray-600">#</th>
            <th className="text-center w-1/6 border-b border-gray-600">
              Nick Name
            </th>
            <th className="text-center w-full border-b border-gray-600">
              Title
            </th>
            <th className="text-center w-1/6 border-b border-gray-600">
              Platform
            </th>
          </tr>
        </thead>
        <tbody className="bg-gray-900 divide-y divide-gray-700">
          {messages.map((msg, idx) => (
            <tr className="text-center hover:bg-gray-800" key={msg._id}>
              <th className="text-center bg-primary-500">{idx + 1}</th>
              <td className="text-center bg-primary-500">
                {msg.writerNickname}
              </td>
              <td className="text-center bg-primary-500">
                <Link
                  to={`/board/${msg._id}`}
                  className="text-red-500 underline hover:text-lg "
                >
                  {msg.title}
                </Link>
              </td>
              <td className="text-center bg-primary-500">{msg.platformId}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

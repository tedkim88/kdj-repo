import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import axiosInstance from "../lib/axios";
import { useState } from "react";
import type { Messages } from "../lib/types";
import { PLATFORM_SEARCH } from "../lib/constants";
export default function BoardDetail() {
  const { id } = useParams();
  const [message, setMessage] = useState<Messages | null>(null);

  useEffect(() => {
    axiosInstance
      .get(`/board/${id}`)
      .then((response) => {
        console.log(response.data);
        setMessage(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, [id]);

  if (!message) {
    return <div>Loading...</div>;
  }
  return (
    <div className="px-6">
      <div className="max-w-3xl  min-h-[calc(100vh-152px)] mx-auto my-10 p-8 bg-gradient-to-br from-gray-50 to-white border border-gray-200 rounded-2xl shadow-lg">
        <h1 className="text-4xl font-extrabold mb-6 text-gray-900">
          {message.title}
        </h1>

        <div className="flex flex-col text-xl text-gray-500 mb-4 space-y-1 ">
          <span>
            ✍️ Writer :{" "}
            <span className="text-indigo-600 font-medium">
              {message.writerNickname ?? " N/A"}
            </span>
          </span>
          <span>
            🕹️ Platform :{" "}
            <span className="text-gray-700 font-semibold">
              {PLATFORM_SEARCH.find(
                (p) => p.id.toString() === message.platformId
              )?.name || " N/A"}
            </span>
          </span>
        </div>

        <div className="border min-h-96 prose max-w-none text-gray-800 leading-relaxed">
          {message.content}
        </div>
      </div>
    </div>
  );
}

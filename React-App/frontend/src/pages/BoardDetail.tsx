import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import axiosInstance from "../lib/axios";
import { useState } from "react";
import type { Messages } from "../lib/types";
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

  if (!message) return <p>로딩 중...</p>;

  return (
    <div>
      <h1>{message.title}</h1>
      <p>작성자: {message.writerNickname}</p>
      <p>내용: {message.content}</p>
      <p>플랫폼아이디: {message.platformId}</p>
    </div>
  );
}

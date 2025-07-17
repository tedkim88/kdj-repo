// src/pages/BoardForm.tsx

import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import axiosInstance from "../lib/axios";
import { toast } from "react-hot-toast";
import { PLATFORM_SEARCH } from "../lib/constants";
export default function BoardForm() {
  const { id } = useParams(); // if this exists, we are in edit mode. Otherwise, we are in write mode
  const navigate = useNavigate();

  const [title, setTitle] = useState("");
  const [writerNickname, setWriterNickname] = useState("");
  const [content, setContent] = useState("");
  const [password, setPassword] = useState("");
  const [platformId, setPlatformId] = useState("");
  const isEditing = Boolean(id);

  useEffect(() => {
    if (isEditing) {
      axiosInstance.get(`/board/${id}`).then((res) => {
        setTitle(res.data.title);
        setContent(res.data.content);
        setWriterNickname(res.data.writerNickname);
        setPlatformId(res.data.platformId);
      });
    }
  }, [isEditing, id]);

  const handleSubmit = async () => {
    if (
      !title ||
      !content ||
      !platformId ||
      (!isEditing && (!writerNickname || !password))
    ) {
      toast.error("All fields are required");
      return;
    }

    try {
      if (isEditing) {
        await axiosInstance.put(`/board/edit/${id}`, {
          title,
          content,
          platformId,
        });
        toast.success("Edit success");
      } else {
        await axiosInstance.post("/board/save", {
          title,
          content,
          password,
          platformId,
          writerNickname,
        });
        toast.success("Post Success");
      }
      navigate("/board");
    } catch (err) {
      console.log(err);
      toast.error("Unexpected error occurred.");
    }
  };

  return (
    <div className="max-w-3xl mx-auto my-10 p-8 bg-white border rounded shadow">
      <h2 className="text-2xl font-bold mb-4 text-red-400">
        {isEditing ? "Edit Post" : "Write a New Post"}
      </h2>

      <label htmlFor="title" className="block mb-1 font-medium text-gray-700">
        Title
      </label>
      <input
        id="title"
        type="text"
        className="w-full mb-4 p-2 border rounded"
        placeholder="Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
      />

      {/* using dropdown for platform, as it is hard to type the platform name and match the expected result */}
      <label
        htmlFor="platformId"
        className="block mb-1 font-medium text-gray-700"
      >
        Platform
      </label>
      <select
        id="platformId"
        className="w-full mb-4 p-2 border rounded"
        value={platformId}
        onChange={(e) => setPlatformId(e.target.value)}
      >
        <option value="">Select a platform</option>
        {PLATFORM_SEARCH.map((platform) => (
          <option key={platform.id} value={platform.id}>
            {platform.name}
          </option>
        ))}
      </select>

      <label
        htmlFor="writerNickname"
        className="block mb-1 font-medium text-gray-700"
      >
        Writer Nickname
      </label>
      <input
        id="writerNickname"
        type="text"
        className="w-full mb-4 p-2 border rounded"
        placeholder="Writer Nickname"
        disabled={isEditing}
        value={writerNickname}
        onChange={(e) => setWriterNickname(e.target.value)}
      />

      <label htmlFor="content" className="block mb-1 font-medium text-gray-700">
        Content
      </label>
      <textarea
        id="content"
        className="w-full mb-4 p-2 border rounded min-h-[200px]"
        placeholder="Content"
        value={content}
        onChange={(e) => setContent(e.target.value)}
      />

      {!isEditing && (
        <>
          <label
            htmlFor="password"
            className="block mb-1 font-medium text-gray-700"
          >
            Password
          </label>
          <input
            id="password"
            type="password"
            className="w-full mb-4 p-2 border rounded"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </>
      )}

      <button
        onClick={handleSubmit}
        className="bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded"
      >
        {isEditing ? "Update" : "Submit"}
      </button>
    </div>
  );
}

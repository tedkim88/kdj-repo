import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axiosInstance from "../lib/axios";
import type { Messages } from "../lib/types";
import { PLATFORM_SEARCH } from "../lib/constants";
import { toast } from "react-hot-toast";
import type { AxiosError } from "axios";
import { useAuthStore } from "../store/useAuthStore";

export default function BoardDetail() {
  const { authUser } = useAuthStore();
  const { id } = useParams();
  const [message, setMessage] = useState<Messages | null>(null);
  const navigate = useNavigate();
  const [modalAction, setModalAction] = useState<"edit" | "delete" | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [password, setPassword] = useState("");
  const [isMyMessage, setIsMyMessage] = useState(false);

  useEffect(() => {
    axiosInstance
      .get(`/board/${id}`)
      .then((response) => {
        setMessage(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, [id]);

  useEffect(() => {
    if (authUser && message) {
      if (authUser._id === message.writerId) {
        setIsMyMessage(true);
      }
    }
  }, [authUser, message]);

  const BackToBoard = () => {
    navigate("/board");
  };

  const handleConfirmWithPassword = async () => {
    try {
      const res = await axiosInstance.post(`/board/checkMsgPassword/${id}`, {
        password,
      });

      if (res.data.success) {
        if (modalAction === "delete") {
          await axiosInstance.delete(`/board/delete/${id}`);
          toast.success("Delete success");
          navigate("/board");
        } else if (modalAction === "edit") {
          toast.success("Password verified");
          navigate(`/board/write/${id}`);
        }

        setIsModalOpen(false);
        setModalAction(null);
      } else {
        toast.error("Wrong password");
      }
    } catch (error) {
      const axiosError = error as AxiosError<{
        success: boolean;
        message: string;
      }>;
      toast.error(
        axiosError.response?.data?.message || "Unexpected error occurred"
      );
    }
  };

  if (!message) {
    return <div>Loading...</div>;
  }

  return (
    <div className="px-4 sm:px-6">
      <div className="max-w-3xl min-h-[calc(100vh-152px)] mx-auto my-10 p-6 sm:p-8 bg-gradient-to-br from-gray-50 to-white border border-gray-200 rounded-2xl shadow-lg">
        <h1 className="text-3xl sm:text-4xl font-extrabold mb-6 text-gray-900">
          {message.title}
        </h1>

        <div className="flex flex-col text-lg sm:text-xl text-gray-500 mb-4 space-y-1">
          <span>
            ✍️ Writer:{" "}
            <span className="text-indigo-600 font-medium">
              {message.writerNickname ?? "N/A"}
            </span>
          </span>
          <span>
            🕹️ Platform:{" "}
            <span className="text-gray-700 font-semibold">
              {PLATFORM_SEARCH.find(
                (p) => p.id.toString() === message.platformId
              )?.name || "N/A"}
            </span>
          </span>
        </div>

        <div className="border min-h-96 prose max-w-none text-gray-800 leading-relaxed">
          {message.content}
        </div>

        {/* ✅ 반응형 버튼 영역 */}
        <div className="flex flex-col md:flex-row gap-4 justify-center mt-12">
          {isMyMessage && (
            <>
              <button
                className="bg-indigo-600 text-white py-2 px-4 rounded w-full md:w-40 hover:bg-red-300"
                onClick={() => {
                  setModalAction("edit");
                  setIsModalOpen(true);
                }}
              >
                Edit
              </button>

              <button
                className="bg-indigo-600 text-white py-2 px-4 rounded w-full md:w-40 hover:bg-red-300"
                onClick={() => {
                  setModalAction("delete");
                  setIsModalOpen(true);
                }}
              >
                Delete
              </button>
            </>
          )}

          <button
            className="bg-indigo-600 text-white py-2 px-4 rounded w-full md:w-40 hover:bg-red-300"
            onClick={BackToBoard}
          >
            Back to Board
          </button>
        </div>
      </div>

      {isModalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-white p-6 rounded-lg shadow-lg w-11/12 max-w-md space-y-4">
            <h2 className="text-xl font-semibold text-gray-800">
              Enter Password for {modalAction === "delete" ? "Delete" : "Edit"}
            </h2>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
              placeholder="Password"
            />
            <div className="flex justify-end space-x-2">
              <button
                onClick={() => {
                  setIsModalOpen(false);
                  setModalAction(null);
                }}
                className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
              >
                Cancel
              </button>
              <button
                onClick={handleConfirmWithPassword}
                className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
              >
                {modalAction === "delete" ? "Delete" : "Confirm"}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

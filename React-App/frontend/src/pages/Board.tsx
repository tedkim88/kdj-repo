import { useEffect, useState } from "react";
import axiosInstance from "../lib/axios";
import type { Messages } from "../lib/types";
import { Link } from "react-router-dom";
import { useAuthStore } from "../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import { PLATFORM_SEARCH } from "../lib/constants";
import { PencilLine } from "lucide-react";

export default function Board() {
  const [messages, setMessages] = useState<Messages[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [platform, setPlatform] = useState("");
  const { authUser } = useAuthStore();
  const navigate = useNavigate();
  const [redirecting, setRedirecting] = useState(false);

  // 로그인 체크 후 리다이렉트
  useEffect(() => {
    if (!authUser) {
      setRedirecting(true);
      const timer = setTimeout(() => {
        navigate("/login");
      }, 3000);

      return () => clearTimeout(timer);
    }
  }, [authUser, navigate]);

  // 페이지 또는 플랫폼 변경 시 데이터 재요청
  useEffect(() => {
    // platform 쿼리 추가
    const platformQuery = platform ? `&platform=${platform}` : "";
    axiosInstance
      .get(`/board/all?page=${currentPage}${platformQuery}`)
      .then((res) => {
        setMessages(res.data.messages);
        setTotalPages(res.data.totalPages);
      })
      .catch((err) => console.error("Error fetching messages", err));
  }, [currentPage, platform]);

  // 플랫폼 변경 시 페이지 1로 초기화
  useEffect(() => {
    setCurrentPage(1);
  }, [platform]);

  return (
    <div className="overflow-x-auto min-h-[calc(100vh-76px)] p-4">
      {redirecting && (
        <div className="flex flex-col items-center h-[calc(100vh-76px)] justify-center text-yellow-500 text-xl font-semibold space-y-4">
          <div className="animate-spin rounded-full h-12 w-12 border-t-4 border-b-4 border-red-500"></div>
          <div>
            Please login first to use the Board, redirecting to login page
          </div>
        </div>
      )}

      {/* 게시판 테이블 */}
      {messages.length > 0 && (
        <table className="table w-full text-white border border-gray-700">
          <thead className="bg-gray-800 text-yellow-300">
            <tr>
              <th className="text-center w-1/12 border-b border-gray-600">#</th>
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
                  {(currentPage - 1) * 10 + idx + 1}
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

      {/* 페이지네이션 번호 버튼 */}
      <div className="join flex justify-center mt-6 flex-wrap">
        {Array.from({ length: totalPages }, (_, idx) => (
          <button
            key={idx}
            className={`join-item btn hover:bg-red-400 ${
              currentPage === idx + 1 ? "bg-red-600 text-white" : ""
            }`}
            onClick={() => setCurrentPage(idx + 1)}
          >
            {idx + 1}
          </button>
        ))}
      </div>

      {/* Prev / Next 버튼 */}
      <div className="flex justify-center gap-2 mt-4">
        <button
          disabled={currentPage === 1}
          onClick={() => setCurrentPage((prev) => prev - 1)}
          className="btn hover:bg-indigo-500 text-white disabled:opacity-50 disabled:bg-gray-500 disabled:text-white"
        >
          Prev
        </button>
        <button
          disabled={currentPage === totalPages}
          onClick={() => setCurrentPage((prev) => prev + 1)}
          className="btn hover:bg-indigo-500 text-white disabled:opacity-50 disabled:bg-gray-500 disabled:text-white"
        >
          Next
        </button>
      </div>

      <div className="flex items-center justify-between mb-4 gap-4 w-full max-w-md mx-auto mt-5">
        {/* 플랫폼 필터 */}
        <div className="form-control flex-1">
          <select
            className="select select-bordered w-full bg-red-700 text-white hover:bg-red-400"
            onChange={(e) => setPlatform(e.target.value)}
            value={platform}
          >
            <option value="">Select Platform</option>
            {PLATFORM_SEARCH.map((p) => (
              <option key={p.id} value={p.id}>
                {p.name}
              </option>
            ))}
          </select>
        </div>

        {/* 포스트 버튼 */}
        <Link
          to="/board/write"
          className="flex items-center gap-2 bg-gradient-to-r from-blue-600 to-indigo-700 hover:from-pink-500 hover:to-yellow-500 text-white font-bold py-3 px-6 rounded-xl shadow-lg transition-all duration-300 transform hover:scale-105 whitespace-nowrap"
        >
          <PencilLine size={20} />
          Post
        </Link>
      </div>
    </div>
  );
}

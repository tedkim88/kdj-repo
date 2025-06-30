import React from "react";
import { useEffect } from "react";
import { useAuthStore } from "../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
export default function LiveChat() {
  const navigate = useNavigate();
  const { authUser } = useAuthStore();
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

  return (
    <div className="overflow-x-auto min-h-[calc(100vh-76px)]">
      {redirecting && (
        <div className="flex flex-col items-center h-[calc(100vh-76px)] justify-center text-yellow-500 text-xl font-semibold space-y-4">
          <div className="animate-spin rounded-full h-12 w-12 border-t-4 border-b-4 border-red-500"></div>
          <div>
            Please login first to use Live Chat, redirecting to login page
          </div>
        </div>
      )}

      <h1>Live Chat</h1>
    </div>
  );
}

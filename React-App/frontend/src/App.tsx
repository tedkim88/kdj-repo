import Navbar from "./components/Navbar";
import { Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import SignUp from "./pages/SignUp";
import Login from "./pages/Login";
import LiveChat from "./pages/LiveChat";
import Board from "./pages/Board";
import Search from "./pages/Search";
import { useEffect } from "react";
import { useAuthStore } from "./store/useAuthStore";
import { Toaster } from "react-hot-toast";
import BoardDetail from "./pages/BoardDetail";
import BoardForm from "./pages/BoardForm";
import GameDetail from "./pages/GameDetail";
function App() {
  //when APP component is loaded, check if user is authenticated
  //and if not, adjust the navbar accordingly
  const { checkAuth } = useAuthStore();

  useEffect(() => {
    checkAuth();
  }, [checkAuth]);

  return (
    <div>
      {/* Navbar always opens up */}
      <Navbar />

      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/login" element={<Login />} />
        <Route path="/chat" element={<LiveChat />} />
        <Route path="/board" element={<Board />} />
        <Route path="/board/:id" element={<BoardDetail />} />
        <Route path="/board/write" element={<BoardForm />} />
        <Route path="/board/write/:id" element={<BoardForm />} />
        <Route path="/search" element={<Search />} />
        <Route path="/games/:id" element={<GameDetail />} />
        {/* <Route path="/" element={<HomePage />} />
            <Route path="/" element={<HomePage />} /> */}
      </Routes>
      <Toaster position="bottom-right" toastOptions={{ duration: 4000 }} />
    </div>
  );
}

export default App;

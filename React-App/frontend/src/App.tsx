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
function App() {
  //when APP component is loaded, check if user is authenticated
  //and if not, adjust the navbar accordingly
  const { checkAuth } = useAuthStore();

  useEffect(() => {
    checkAuth();
  }, []);

  return (
    <div>
      {/* always opens up */}
      <Navbar />

      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/login" element={<Login />} />
        <Route path="/chat" element={<LiveChat />} />
        <Route path="/board" element={<Board />} />
        <Route path="/search" element={<Search />} />
        {/* <Route path="/" element={<HomePage />} />
            <Route path="/" element={<HomePage />} /> */}
      </Routes>
      <Toaster position="bottom-right" toastOptions={{ duration: 3000 }} />
    </div>
  );
}

export default App;

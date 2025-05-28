import Navbar from "./components/Navbar";
import { Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import SignUp from "./pages/SignUp";
import Login from "./pages/Login";
import LiveChat from "./pages/LiveChat";
import Board from "./pages/Board";
import Search from "./pages/Search";
function App() {
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
    </div>
  );
}

export default App;

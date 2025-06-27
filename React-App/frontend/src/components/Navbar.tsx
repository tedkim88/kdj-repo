import React from "react";
import { Link } from "react-router-dom";
import { AxiosError } from "axios";
import { useAuthStore } from "../store/useAuthStore";
export default function Navbar() {
  const authUser = useAuthStore((state) => state.authUser);
  const { logout } = useAuthStore();
  console.log(authUser);
  const handleLogout = async () => {
    try {
      const response = await logout();
      console.log(response);
    } catch (error) {
      const axiosError = error as AxiosError<{ message: string }>;
      console.log("Logout error", axiosError.response?.data.message);
    }
  };

  return (
    <div className="navbar bg-white shadow-md px-6 py-2 flex justify-between items-center sticky top-0 left-0 right-0 z-30 ">
      <div className="navbar-start">
        <div className="dropdown">
          <div tabIndex={0} role="button" className="btn btn-ghost lg:hidden">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-6 w-6 text-gray-600"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M4 6h16M4 12h16M4 18h16"
              />
            </svg>
          </div>
          <ul
            tabIndex={0}
            className="menu menu-sm dropdown-content mt-3 z-[1] p-4 shadow-xl bg-white w-60 rounded-lg"
          >
            <Link
              to={"/search"}
              className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg text-black transition duration-300"
            >
              Search Games
            </Link>

            <Link
              to={"/board"}
              className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg text-black transition duration-300"
            >
              {" "}
              Board
            </Link>

            <Link
              to={"/chat"}
              className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg text-black transition duration-300"
            >
              Live Chat
            </Link>
            {!authUser && (
              <>
                <Link
                  to={"/signup"}
                  className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg text-black transition duration-300"
                >
                  Signup
                </Link>

                <Link
                  to={"/login"}
                  className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg text-black transition duration-300"
                >
                  Login
                </Link>
              </>
            )}
            {authUser && (
              <Link
                to={"/"}
                onClick={handleLogout}
                className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg text-red-600 transition duration-300"
              >
                Logout
              </Link>
            )}
          </ul>
        </div>
        <Link
          to={"/"}
          className="text-2xl font-bold text-red-600 hover:text-warning transition duration-300 cursor-pointer"
        >
          GameHUB
        </Link>
      </div>

      <div className="navbar-center hidden lg:flex">
        <ul className="menu menu-horizontal gap-8 text-gray-700 font-semibold text-lg">
          <Link
            to={"/search"}
            className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg transition duration-300"
          >
            Search Games
          </Link>

          <Link
            to={"/board"}
            className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg transition duration-300"
          >
            {" "}
            Board
          </Link>

          <Link
            to={"/chat"}
            className="hover:text-red-600 hover:bg-red-100 rounded-md px-4 py-2 font-semibold text-lg transition duration-300"
          >
            Live Chat
          </Link>
        </ul>
      </div>

      {/* Authentication status is going to be managed through zustand and depending on the status, I'm gonna show different buttons */}
      <div className="navbar-end gap-2 hidden lg:flex">
        {!authUser && (
          <>
            <Link to={"/signup"} className="btn btn-outline btn-error">
              Sign Up
            </Link>
            <Link to={"/login"} className="btn btn-outline btn-error">
              Login
            </Link>
          </>
        )}
        {authUser && (
          <div className="flex items-center space-x-4 px-4 py-2">
            <span className="text-gray-700 font-semibold text-lg">
              👋 Welcome, {authUser.name}
            </span>

            <Link
              to={"/"}
              onClick={handleLogout}
              className="hover:text-white btn rounded-md px-4 py-2 font-semibold text-lg text-red-600 transition duration-300"
            >
              Logout
            </Link>
          </div>
        )}
      </div>
    </div>
  );
}

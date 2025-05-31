import React, { useState } from "react";
import { useAuthStore } from "../store/useAuthStore";
import { AxiosError } from "axios";
import { useNavigate } from "react-router-dom";
export default function SignUp() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const { isSigningUp, signup } = useAuthStore();
  const navigate = useNavigate();
  const handleSubmit = async (e: { preventDefault: () => void }) => {
    e.preventDefault();
    try {

      //simple input validation in frontend is needed before sending request to backend to reduce server burden. 


      const createdUser = await signup({ name, email, password });
      setName("");
      setEmail("");
      setPassword("");
      if (createdUser) navigate("/");
    } catch (error) {
      const axiosError = error as AxiosError<{ message: string }>;
      console.log(axiosError);
      console.log("Signup error", axiosError.response?.data.message);
    }
  };

  if (isSigningUp)
    return (
      <div className="flex justify-center items-center h-screen">
        <span className="loading loading-spinner loading-lg text-primary"></span>
      </div>
    );

  return (
    <form
      onSubmit={handleSubmit}
      className="flex flex-col gap-4 mx-auto p-4 max-w-lg h-screen justify-center"
    >
      <label className="input input-bordered input-accent flex items-center gap-2">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 16 16"
          fill="currentColor"
          className="h-4 w-4 opacity-70"
        >
          <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6ZM12.735 14c.618 0 1.093-.561.872-1.139a6.002 6.002 0 0 0-11.215 0c-.22.578.254 1.139.872 1.139h9.47Z" />
        </svg>
        <input
          type="text"
          className="grow"
          placeholder="Username"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </label>

      <label className="input input-bordered input-info flex items-center gap-2">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 16 16"
          fill="currentColor"
          className="h-4 w-4 opacity-70"
        >
          <path d="M2.5 3A1.5 1.5 0 0 0 1 4.5v.793c.026.009.051.02.076.032L7.674 8.51c.206.1.446.1.652 0l6.598-3.185A.755.755 0 0 1 15 5.293V4.5A1.5 1.5 0 0 0 13.5 3h-11Z" />
          <path d="M15 6.954 8.978 9.86a2.25 2.25 0 0 1-1.956 0L1 6.954V11.5A1.5 1.5 0 0 0 2.5 13h11a1.5 1.5 0 0 0 1.5-1.5V6.954Z" />
        </svg>
        <input
          type="email"
          className="grow"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </label>

      <label className="input input-bordered input-secondary flex items-center gap-2">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 16 16"
          fill="currentColor"
          className="h-4 w-4 opacity-70"
        >
          <path
            fillRule="evenodd"
            d="M14 6a4 4 0 0 1-4.899 3.899l-1.955 1.955a.5.5 0 0 1-.353.146H5v1.5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-2.293a.5.5 0 0 1 .146-.353l3.955-3.955A4 4 0 1 1 14 6Zm-4-2a.75.75 0 0 0 0 1.5.5.5 0 0 1 .5.5.75.75 0 0 0 1.5 0 2 2 0 0 0-2-2Z"
            clipRule="evenodd"
          />
        </svg>
        <input
          type="password"
          placeholder="Password"
          className="grow"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </label>
      <button type="submit" className="btn btn-primary">
        Sign Up
      </button>
    </form>
  );
}

import { generateToken } from "../lib/utils.js";
import { signupService, signinService } from "../services/auth.service.js";

export const signup = async (req, res) => {
  try {
    const newUser = await signupService(req.body);

    if (newUser) {
      generateToken(newUser._id, res);

      res
        .status(201)
        .json({ _id: newUser._id, name: newUser.name, email: newUser.email, role: newUser.role });
    } else {
      res
        .status(500)
        .json({ message: "Internal Server Error, Failed to create new User" });
    }
  } catch (error) {
    console.log(error);
    res
      .status(500)
      .json({ message: "Internal Server Error, " + error.message });
  }
};

export const login = async (req, res) => {
  try {
    const user = await signinService(req.body);

    if (user) {
      generateToken(user._id, res);
      res
        .status(200)
        .json({ _id: user._id, name: user.name, email: user.email, role: user.role });
    }
  } catch (error) {
    console.log(error);
    res.status(401).json({ message: "Error: " + error.message });
  }
};

export const logout = async (req, res) => {
  try {
    res.cookie("jwt", "", { maxAge: 0 });
    res.status(200).json({ message: "Logout successful" });
  } catch (error) {
    console.log("Error in logout", error.message);
    res.status(500).json({ message: "Internal server error" });
  }
};

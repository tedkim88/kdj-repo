import User from "../models/user.model.js";
import bcrypt from "bcryptjs";

export const signupService = async ({ name, email, password }) => {
  if (!name || !email || !password) {
    throw new Error("All fields are required");
  }

  if (password.length < 8) {
    throw new Error("Password must be at least 8 characters long");
  }

  const user = await User.findOne({ email });
  if (user) {
    throw new Error("User already exists");
  }

  const salt = await bcrypt.genSalt(10);
  const hashedPassword = await bcrypt.hash(password, salt);

  const newUser = new User({
    name: name,
    email: email,
    password: hashedPassword,
  });

  await newUser.save();

  return newUser;
};

export const signinService = async ({ email, password }) => {
  try {
    if (!email || !password) {
      throw new Error("All fields are required");
    }

    const user = await User.findOne({ email });

    if (!user) {
      throw new Error("User does not exist");
    }

    const isPasswordCorrect = await bcrypt.compare(password, user.password);
    if (!isPasswordCorrect) {
      throw new Error("Invalid credentials");
    }

    return user;
    
  } catch (error) {
    console.log(error);
    throw new Error(error.message);
  }
};

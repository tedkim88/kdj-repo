import User from '../models/user.model.js';
import bcrypt from 'bcryptjs';


export async function createAdmin() {
  const existingAdmin = await User.findOne({ role: 'admin' });
  if (existingAdmin) {
    console.log('Admin already exists');
    return;
  }

  const hashedPassword = await bcrypt.hash('654321', 10);
  const adminUser = new User({
    name: 'Admin',
    email: 'admin@example.com',
    password: hashedPassword,
    role: 'admin',
  });
  await adminUser.save();
  console.log('Admin user created');
}

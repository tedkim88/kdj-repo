import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import tailwindcss from '@tailwindcss/vite'
export default defineConfig({
  plugins: [
    react(), // React SWC 플러그인
    tailwindcss(),
  ],
})

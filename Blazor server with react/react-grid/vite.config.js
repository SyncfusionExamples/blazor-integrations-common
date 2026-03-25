import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/_framework': {
        target: 'http://localhost:5098', // Provide the hosted URL of the Blazor application.
        changeOrigin: true,
        ws: true
      },
      '/_content': {
        target: 'http://localhost:5098', // Same Blazor hosted URL
        changeOrigin: true
      },
      '/_blazor': {
        target: 'http://localhost:5098', // Same Blazor hosted URL
        changeOrigin: true,
        ws: true
      }
    }
  }
})
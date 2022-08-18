/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    // Example content paths...
    "./Views/*/*.cshtml",
    "./Views/Shared/*.cshtml",
  ],
  theme: {
    extend: {
      borderWidth: {
        '1': '1px',
        '4': '4px'
      },
    },
  },
  plugins: [],
}

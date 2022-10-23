/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx}",
    "./components/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      borderWidth: {
        '1': '1px'
      },
      outlineWidth: {
        '1': '1px'
      },
      fontFamily: {

      }
    },
  },
  plugins: [],
}
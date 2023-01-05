/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx}",
    "./components/**/*.{js,ts,jsx,tsx}",
    "./components/*/*/*.{js,ts,jsx,tsx}",
    "./components/*/*/*/*.{js,ts,jsx,tsx}",
    "./components/*/*/*/*.{js,ts,jsx,tsx}",
    "./components/*/*.{js,ts,jsx,tsx}",
    "*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {},
      colors: {
        "main-red": "#ff1330",
        "main-blue": "#471cff",
      },
      maxWidth: {
        "8xl": "92rem",
        "9xl": "100rem",
        "10xl": "108rem",
      },
    },
  },
  plugins: [],
};

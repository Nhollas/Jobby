/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./app/**/*.{js,ts,jsx,tsx}",
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
        "main-red": "#F51720",
        "main-blue": "#471cff",
        "main-gold": "#ffd600",
        "main-green": "#00FF00",
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

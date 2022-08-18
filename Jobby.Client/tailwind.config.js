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
      outlineWidth: {
        '1': '1px',
        '4': '4px'
      },
      fontSize: {
        '0': '0',
      },
      colors: {
        'main-blue': '#471cff',
        'main-red': '#ff0000',
        'main-orange': '#ffa410',
      },
      fontFamily: {
        raleway: ["Raleway", "sans-serif"]
      },
      scale: {
        'base': '1.025'
      }
    },
  },
  plugins: [],
}

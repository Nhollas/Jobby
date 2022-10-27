/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx}",
    "./components/**/*.{js,ts,jsx,tsx}",
  ],
  safelist: [
    'order-1',
    'order-2',
    'order-3',
    'order-4',
    'order-5',
    'order-6',
    'order-7',
    'order-8',
    'order-9',
    'order-10',
    'order-11',
    'order-12',
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

      },
      colors: {
        'main-red': '#ff1330',
        'main-blue': '#471cff',
      }
    },
  },
  plugins: [],
}
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    // Example content paths...
    "./Pages/*/*.cshtml",
    "./Pages/*.cshtml",
  ],
  safelist: [
    'bg-red-400',
    'bg-red-600',
    'bg-orange-400',
    'bg-orange-600',
    'bg-amber-400',
    'bg-amber-400',
    'bg-lime-400',
    'bg-lime-600',
    'bg-emerald-400',
    'bg-emerald-600',
    'bg-teal-400',
    'bg-teal-600',
    'bg-blue-400',
    'bg-blue-600',
    'bg-violet-400',
    'bg-violet-600',
    'bg-purple-400',
    'bg-purple-600',
    'bg-fuchsia-400',
    'bg-fuchsia-600',
    'bg-pink-400',
    'bg-pink-600',
    'bg-rose-600'
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
        'xxs': '0.6rem'
      },
      colors: {
        'main-blue': '#471cff',
        'main-red': '#ff1330',
        'main-orange': '#FF7246',
      },
      fontFamily: {
        raleway: ["Raleway", "sans-serif"]
      },
      screens: {
        'TwoJobList': '625px',
        'ThreeJobList': '920px',
        'FourJobList': '1200px',
        'FiveJobList': '1500px',    
      },
      maxWidth: {
        '8xl': '100rem',
        'xxs': '15rem'
      },
    },
  },
  plugins: [],
}

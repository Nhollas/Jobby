/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    // Example content paths...
    "./Pages/*/*.cshtml",
    "./Pages/*.cshtml",
  ],
  safelist: [
    'bg-red-50', 
    'bg-orange-50', 
    'bg-amber-50', 
    'bg-yellow-50', 
    'bg-lime-50', 
    'bg-green-50', 
    'bg-emerald-50', 
    'bg-teal-50', 
    'bg-cyan-50', 
    'bg-sky-50', 
    'bg-blue-50', 
    'bg-indigo-50', 
    'bg-violet-50', 
    'bg-purple-50', 
    'bg-fuchsia-50', 
    'bg-pink-50', 
    'bg-rose-50',
    'text-red-500',
    'text-orange-500',
    'text-amber-500',
    'text-yellow-500',
    'text-lime-500',
    'text-green-500',
    'text-emerald-500',
    'text-teal-500',
    'text-cyan-500',
    'text-sky-500',
    'text-blue-500',
    'text-indigo-500',
    'text-violet-500',
    'text-purple-500',
    'text-fuchsia-500',
    'text-pink-500',
    'text-rose-500'
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
      transitionProperty: {
          'height': 'height'
      }
    },
  },
  plugins: [],
}

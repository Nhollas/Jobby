import { motion } from "framer-motion";
import Link from "next/link";

export const MenuItem = ({
  selected,
  icon,
  name,
  leaf,
  href,
  layoutId,
  colour,
}) => (
  <motion.div className='relative'>
    <Link
      key={leaf}
      href={href}
      className={
        "flex flex-row items-center gap-x-2 border border-gray-300 bg-white px-4 py-2 text-base"
      }
    >
      {icon}
      {name}
    </Link>
    {selected && (
      <motion.div
        className={`absolute -bottom-1 z-10 h-1 w-full ${colour}`}
        layoutId={layoutId}
      />
    )}
  </motion.div>
);

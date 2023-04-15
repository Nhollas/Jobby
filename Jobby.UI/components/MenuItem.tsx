"use client";

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
      href={href}
      className={
        "flex flex-row items-center gap-x-2 border border-gray-300 bg-white px-3 py-1 text-sm"
      }
    >
      {icon}
      {name}
    </Link>
    {selected && (
      <motion.div
        className={`absolute -bottom-1 h-1 w-full ${colour}`}
        layoutId={layoutId}
      />
    )}
  </motion.div>
);

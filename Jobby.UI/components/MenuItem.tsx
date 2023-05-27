"use client";

import { motion } from "framer-motion";
import Link from "next/link";
import { Button } from "./ui/button";

export const MenuItem = ({
  selected,
  icon,
  name,
  leaf,
  href,
  layoutId,
  colour,
}) => (
  <motion.div className="relative">
    <Button
      asChild
      size="sm"
      variant="outline"
      className="flex flex-row gap-x-2"
    >
      <Link href={href}>
        {icon}
        {name}
      </Link>
    </Button>
    {selected && (
      <motion.div
        className={`absolute -bottom-2 h-1 w-full rounded-lg bg-primary`}
        layoutId={layoutId}
      />
    )}
  </motion.div>
);

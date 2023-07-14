"use client";

import { cn } from "@/lib/utils";
import { motion } from "framer-motion";
import Link from "next/link";
import { Button } from "./ui/button";

interface MenuItemProps {
  selected: boolean;
  icon: React.ReactNode;
  name: string;
  href: string;
  layoutId: string;
}

export const MenuItem = ({
  selected,
  icon,
  name,
  href,
  layoutId,
}: MenuItemProps) => (
  <motion.div className="relative p-1">
    <Button
      asChild
      size="sm"
      variant="outline"
      className={cn(
        "flex h-10 items-center gap-x-2 rounded-md px-4 py-2 text-sm",
        selected
          ? "bg-white font-medium text-primary"
          : "font-medium text-muted-foreground"
      )}
    >
      <Link href={href}>
        {icon}
        {name}
      </Link>
    </Button>
    {selected && (
      <motion.div
        className="absolute inset-0 -z-10 h-full w-full rounded-lg bg-muted"
        layoutId={layoutId}
      />
    )}
  </motion.div>
);

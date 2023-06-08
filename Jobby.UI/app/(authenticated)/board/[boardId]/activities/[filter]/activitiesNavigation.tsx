"use client";

import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { cn } from "@/lib/utils";
import { set } from "date-fns";
import { motion } from "framer-motion";
import Link from "next/link";
import { useEffect, useState } from "react";

interface ActivityItemProps {
  title: string;
  href: string;
  active: boolean;
}

const ActivityItem = ({ title, href, active }: ActivityItemProps) => {
  const [isAnimating, setIsAnimating] = useState(true);

  useEffect(() => {
    setIsAnimating(false);
  }, []);

  return (
    <div className="relative p-1">
      <Button
        asChild
        size="sm"
        variant={active && !isAnimating ? "default" : "ghost"}
        className={cn(
          "w-full text-sm hover:bg-transparent",
          active && "text-white"
        )}
      >
        <Link href={href} className="!justify-start">
          {title}
        </Link>
      </Button>
      {active && (
        <motion.div
          onLayoutAnimationStart={() => setIsAnimating(true)}
          onLayoutAnimationComplete={() => setIsAnimating(false)}
          className="absolute inset-0 -z-10 h-full w-full rounded-lg bg-primary"
          layoutId={"activity-item"}
        />
      )}
    </div>
  );
};

export function ActivitiesNavigation({
  filter,
  boardId,
}: {
  filter: string;
  boardId: string;
}) {
  return (
    <div className="w-[250px]">
      <div className="fixed flex w-[250px] flex-col gap-y-2">
        <div className="flex flex-col gap-y-1 p-2">
          <ActivityItem
            title="All"
            href={`/board/${boardId}/activities/all`}
            active={filter === "all"}
          />
          <ActivityItem
            title="Due Today"
            href={`/board/${boardId}/activities/due-today`}
            active={filter === "due-today"}
          />
          <ActivityItem
            title="Past Due"
            href={`/board/${boardId}/activities/past-due`}
            active={filter === "past-due"}
          />
          <ActivityItem
            title="Completed"
            href={`/board/${boardId}/activities/completed`}
            active={filter === "completed"}
          />
          <ActivityItem
            title="Pending"
            href={`/board/${boardId}/activities/pending`}
            active={filter === "pending"}
          />
        </div>
        <Separator className="-z-20" />
        <div className="flex flex-col gap-y-1 p-2">
          <ActivityItem
            title="Applications"
            href={`/board/${boardId}/activities/applications`}
            active={filter === "applications"}
          />
          <ActivityItem
            title="Interviews"
            href={`/board/${boardId}/activities/interviews`}
            active={filter === "interviews"}
          />
          <ActivityItem
            title="Offers"
            href={`/board/${boardId}/activities/offers`}
            active={filter === "offers"}
          />
          <ActivityItem
            title="Networking"
            href={`/board/${boardId}/activities/networking`}
            active={filter === "networking"}
          />
        </div>
      </div>
    </div>
  );
}

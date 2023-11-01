"use client";

import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { cn } from "@/lib/utils";
import { motion } from "framer-motion";
import Link from "next/link";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

interface ActivityItemProps {
  title: string;
  href: string;
  active: boolean;
  layoutId: string;
}

const ActivityItem = ({ title, href, active, layoutId }: ActivityItemProps) => {
  const [isAnimating, setIsAnimating] = useState(true);
  const params = useParams();

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
          layoutId={layoutId}
        />
      )}
    </div>
  );
};

export function ActivitiesNavigation({
  filter,
  boardRef,
  jobRef,
}: {
  filter: string;
  boardRef: string;
  jobRef?: string;
}) {
  const jobPath = jobRef ? `/job/${jobRef}` : "";

  return (
    <div className="w-[250px] pt-4">
      <div className="fixed flex w-[250px] flex-col gap-y-2">
        <div className="flex flex-col gap-y-1 p-2">
          <ActivityItem
            title="All"
            href={`/track/board/${boardRef}${jobPath}/activities/all`}
            active={filter === "all"}
            layoutId={jobRef ? jobRef : boardRef}
          />
          <ActivityItem
            title="Due Today"
            href={`/track/board/${boardRef}${jobPath}/activities/due-today`}
            active={filter === "due-today"}
            layoutId={jobRef ? jobRef : boardRef}
          />
          <ActivityItem
            title="Past Due"
            href={`/track/board/${boardRef}${jobPath}/activities/past-due`}
            active={filter === "past-due"}
            layoutId={jobRef ? jobRef : boardRef}
          />
          <ActivityItem
            title="Completed"
            href={`/track/board/${boardRef}${jobPath}/activities/completed`}
            active={filter === "completed"}
            layoutId={jobRef ? jobRef : boardRef}
          />
          <ActivityItem
            title="Pending"
            href={`/track/board/${boardRef}${jobPath}/activities/pending`}
            active={filter === "pending"}
            layoutId={jobRef ? jobRef : boardRef}
          />
        </div>
        <Separator className="-z-20" />
        <div className="flex flex-col gap-y-1 p-2">
          <ActivityItem
            title="Applications"
            href={`/track/board/${boardRef}${jobPath}/activities/applications`}
            active={filter === "applications"}
            layoutId={jobRef ? jobRef : boardRef}
          />
          <ActivityItem
            title="Interviews"
            href={`/track/board/${boardRef}${jobPath}/activities/interviews`}
            active={filter === "interviews"}
            layoutId={jobRef ? jobRef : boardRef}
          />
          <ActivityItem
            title="Offers"
            href={`/track/board/${boardRef}${jobPath}/activities/offers`}
            active={filter === "offers"}
            layoutId={jobRef ? jobRef : boardRef}
          />
          <ActivityItem
            title="Networking"
            href={`/track/board/${boardRef}${jobPath}/activities/networking`}
            active={filter === "networking"}
            layoutId={jobRef ? jobRef : boardRef}
          />
        </div>
      </div>
    </div>
  );
}

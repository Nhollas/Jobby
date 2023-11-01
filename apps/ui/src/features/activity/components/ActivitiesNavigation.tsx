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
  boardId,
  jobId,
}: {
  filter: string;
  boardId: string;
  jobId?: string;
}) {
  const jobPath = jobId ? `/job/${jobId}` : "";

  return (
    <div className="w-[250px] pt-4">
      <div className="fixed flex w-[250px] flex-col gap-y-2">
        <div className="flex flex-col gap-y-1 p-2">
          <ActivityItem
            title="All"
            href={`/track/board/${boardId}${jobPath}/activities/all`}
            active={filter === "all"}
            layoutId={jobId ? jobId : boardId}
          />
          <ActivityItem
            title="Due Today"
            href={`/track/board/${boardId}${jobPath}/activities/due-today`}
            active={filter === "due-today"}
            layoutId={jobId ? jobId : boardId}
          />
          <ActivityItem
            title="Past Due"
            href={`/track/board/${boardId}${jobPath}/activities/past-due`}
            active={filter === "past-due"}
            layoutId={jobId ? jobId : boardId}
          />
          <ActivityItem
            title="Completed"
            href={`/track/board/${boardId}${jobPath}/activities/completed`}
            active={filter === "completed"}
            layoutId={jobId ? jobId : boardId}
          />
          <ActivityItem
            title="Pending"
            href={`/track/board/${boardId}${jobPath}/activities/pending`}
            active={filter === "pending"}
            layoutId={jobId ? jobId : boardId}
          />
        </div>
        <Separator className="-z-20" />
        <div className="flex flex-col gap-y-1 p-2">
          <ActivityItem
            title="Applications"
            href={`/track/board/${boardId}${jobPath}/activities/applications`}
            active={filter === "applications"}
            layoutId={jobId ? jobId : boardId}
          />
          <ActivityItem
            title="Interviews"
            href={`/track/board/${boardId}${jobPath}/activities/interviews`}
            active={filter === "interviews"}
            layoutId={jobId ? jobId : boardId}
          />
          <ActivityItem
            title="Offers"
            href={`/track/board/${boardId}${jobPath}/activities/offers`}
            active={filter === "offers"}
            layoutId={jobId ? jobId : boardId}
          />
          <ActivityItem
            title="Networking"
            href={`/track/board/${boardId}${jobPath}/activities/networking`}
            active={filter === "networking"}
            layoutId={jobId ? jobId : boardId}
          />
        </div>
      </div>
    </div>
  );
}

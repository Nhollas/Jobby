"use client";
import { Button } from "@/components/ui/button";
import { useAuth } from "@clerk/nextjs";

export const SignOut = () => {
  const { signOut } = useAuth();

  return (
    <div className="w-full px-4">
      <Button
        className="w-full justify-start font-normal"
        onClick={() => signOut()}
      >
        Sign Out
      </Button>
    </div>
  );
};

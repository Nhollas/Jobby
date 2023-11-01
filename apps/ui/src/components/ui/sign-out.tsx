"use client";

import { Button } from "@/components/ui/button";
import { useAuth } from "@clerk/nextjs";
import { useRouter } from "next/navigation";

export const SignOut = () => {
  const { signOut } = useAuth();
  const router = useRouter();

  async function handleSignOut() {
    await signOut();

    router.push("/signed-out");
  }

  return (
    <div className="w-full px-4">
      <Button
        className="w-full justify-start font-normal"
        onClick={handleSignOut}
      >
        Sign Out
      </Button>
    </div>
  );
};

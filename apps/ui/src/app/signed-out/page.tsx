import { Button } from "@/components/ui/button";
import { User } from "lucide-react";
import Link from "next/link";

export default function SignedOutPage() {
  return (
    <div className="flex flex-col gap-y-2 p-6">
      <h1 className="text-lg font-semibold tracking-tight">
        Sucessfully signed out.
      </h1>
      <p className="text-sm text-gray-500">See you next time!</p>
      <div className="flex flex-row items-center gap-x-4">
        <h2 className="text-sm font-semibold tracking-tight">
          Was this a mistake?
        </h2>
        <Button
          asChild
          variant="outline"
          className="w-max justify-start font-normal"
        >
          <Link href="/sign-in">
            <User className="mr-2 h-4 w-4" />
            Sign In
          </Link>
        </Button>
      </div>
    </div>
  );
}

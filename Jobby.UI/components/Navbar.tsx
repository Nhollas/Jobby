"use client";

import Link from "next/link";
import { useUser } from "@clerk/nextjs";
export const Navbar = () => {
  const { user, isSignedIn } = useUser();

  return (
    <nav className="flex h-20 w-full border-b border-gray-300 p-4">
      <section className="grid w-full grid-cols-[max-content_1fr_max-content]">
        <Link href="/" className="self-center text-xl font-medium">
          Jobby
        </Link>
        {isSignedIn ? (
          <h1>Hi {user.primaryEmailAddress?.emailAddress}</h1>
        ) : (
          <h1>Not logged in.</h1>
        )}
      </section>
    </nav>
  );
};

export default Navbar;

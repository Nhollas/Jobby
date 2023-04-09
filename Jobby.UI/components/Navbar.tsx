"use client";

import Link from "next/link";
import { useSession, signOut, signIn } from "next-auth/react";
import { ActionButton } from "./Common";
export const Navbar = () => {
  const { data: session } = useSession();

  if (session?.bearerToken) {
    localStorage.setItem("access_token", session?.bearerToken);
  }

  return (
    <nav className='flex h-16 w-full justify-center px-6 lg:px-8'>
      <section className='grid w-full max-w-6xl grid-cols-[max-content_1fr_max-content]'>
        <Link href='/' className='self-center text-xl font-medium'>
          Jobby
        </Link>
        <ul className='flex flex-row gap-x-8 self-center justify-self-center'>
          <li className='text-sm font-medium'>
            <Link href='/dashboard' className='text-sm font-medium'>
              Dashboard
            </Link>
          </li>
        </ul>
        {session ? (
          <div className='flex flex-row items-center gap-x-6'>
            <h1>Hi {session.user?.name}</h1>
            <ActionButton
              text='Sign Out'
              variant='secondary'
              onClick={() => signOut({ callbackUrl: "/" })}
            />
          </div>
        ) : (
          <ActionButton
            text='Log in'
            variant='primary'
            onClick={() => signIn()}
          />
        )}
      </section>
    </nav>
  );
};

export default Navbar;

"use client";

import clsx from "clsx";
import { usePathname } from "next/navigation";

type Props = {
  small?: boolean;
  title?: string;
  children: React.ReactNode;
};

export const PageContainer = (props: Props) => {
  const { children, small, title } = props;

  let extended = false;

  const test = usePathname();
  const paths = test.split("/");

  if (paths.length <= 5) {
    extended = true;
  }

  return (
    <div className='flex w-full items-center justify-center'>
      <section
        className={clsx(
          "flex min-h-screen w-full justify-center",
          extended ? "" : "max-w-5xl"
        )}
      >
        <div className='relative flex w-full flex-col'>
          <div className='flex h-full flex-col'>
            {title && (
              <div className='flex flex-row'>
                <h1 className='text-2xl font-medium'>{title}</h1>
              </div>
            )}
            {children}
          </div>
        </div>
      </section>
    </div>
  );
};

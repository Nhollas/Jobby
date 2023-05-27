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
    <div className="flex w-full items-center justify-center">
      <section className={clsx("flex min-h-screen w-full justify-center")}>
        <div className="relative flex w-full flex-col">
          <div className="flex h-full w-full flex-col">{children}</div>
        </div>
      </section>
    </div>
  );
};

import { Button } from "@/components/ui";
import Link from "next/link";

export default async function Layout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
}) {
  return (
    <>
      <div className="flex flex-col gap-y-6 p-6">
        <div className="flex flex-col gap-y-2">
          <h1 className="text-2xl font-medium">Boards</h1>
          <p className="text-sm text-gray-500">View and manage boards</p>
        </div>
        <Button asChild className="w-max">
          <Link href="/track/create-board" passHref>
            Create Board
          </Link>
        </Button>
        {children}
        {modal}
      </div>
    </>
  );
}

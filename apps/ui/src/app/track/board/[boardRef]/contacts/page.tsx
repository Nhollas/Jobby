import { Contacts } from "@/features/contact/components";

export default async function Page({
  params: { boardRef },
}: {
  params: { boardRef: string };
}) {
  const url = `/board/${boardRef}/contacts`;

  return <Contacts boardRef={boardRef} url={url} querykeyVariable={url} />;
}

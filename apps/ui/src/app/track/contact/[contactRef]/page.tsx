"use client";

import { ContactWrapper } from "@/features/contact";

export default function Page({
  params: { contactRef },
}: {
  params: { contactRef: string };
}) {
  return <ContactWrapper contactRef={contactRef} />;
}

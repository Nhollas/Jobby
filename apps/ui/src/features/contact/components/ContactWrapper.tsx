"use client";

import { Contact, useContactQuery } from "@/features/contact";

export function ContactWrapper({ contactRef }: { contactRef: string }) {
  const { data: contact } = useContactQuery(contactRef);

  if (!contact) {
    return <p>Loading Contact...</p>;
  }

  return <Contact contact={contact} />;
}

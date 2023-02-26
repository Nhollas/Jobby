"use client";

import { Contact } from "../../../../types";

type Props = {
  contacts: Contact[];
};

export function Contacts({ contacts }: Props) {
  return (
    <section className='grid grid-cols-3 gap-4'>
      {contacts.map((contact) => {
        return (
          <div
            className='border border-gray-300 bg-gray-50 p-4'
            key={contact.id}
          >
            <div>
              <p className='text-lg font-medium text-gray-900'>
                {contact.firstName} {contact.lastName}
              </p>
              <p className='text-base'>{contact.jobTitle}</p>
              <div className='flex flex-row flex-wrap gap-2 text-sm'>
                <p>
                  {contact.companies
                    .map((company) => {
                      return company.name;
                    })
                    .join(", ")}
                </p>
              </div>
            </div>
          </div>
        );
      })}
    </section>
  );
}

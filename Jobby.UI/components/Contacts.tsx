"use client";

import { Contact } from "types";

type Props = {
  contacts: Contact[] | null;
};

export function Contacts({ contacts }: Props) {
  if (!contacts) return null;

  return (
    <section className='grid grid-cols-3 gap-4'>
      {contacts.map((contact) => {
        const socials = [];

        const socialDict = {
          twitterUrl: <i className='bi bi-twitter text-cyan-400'></i>,
          facebookUrl: <i className='bi bi-facebook text-blue-700'></i>,
          linkedInUrl: <i className='bi bi-linkedin text-blue-500'></i>,
          githubUrl: <i className='bi bi-github text-gray-800'></i>,
        };

        for (const property in contact.socials) {
          if (contact.socials[property] === "") continue;

          socials.push({ name: property, url: contact.socials[property] });
        }

        return (
          <div
            className='w-full max-w-md border border-gray-300 bg-gray-50'
            key={contact.id}
          >
            <div className='p-4'>
              <p className='text-lg font-medium text-gray-900'>
                {contact.firstName} {contact.lastName}
              </p>
              <p className='text-base'>{contact.jobTitle}</p>
              <p className='text-sm'>
                {contact.companies
                  .map((company) => {
                    return company.name;
                  })
                  .join(", ")}
              </p>
            </div>
            <div className='flex flex-col gap-y-2 border-y border-gray-300 bg-white p-4'>
              <div className='flex flex-row items-center gap-x-2'>
                <i className='bi bi-geo-alt text-gray-900'></i>
                <p className='text-sm text-gray-600'>{contact.location}</p>
              </div>
              <div className='flex flex-row items-center gap-x-2'>
                <i className='bi bi-envelope text-gray-900'></i>
                <p className='text-sm text-gray-600'>
                  {contact.emails
                    .map((email) => {
                      return email.name;
                    })
                    .join(", ")}
                </p>
              </div>
              <div className='flex flex-row items-center gap-x-2'>
                <i className='bi bi-telephone text-gray-900'></i>
                <p className='text-sm text-gray-600'>
                  {contact.phones
                    .map((phone) => {
                      return phone.number;
                    })
                    .join(", ")}
                </p>
              </div>
            </div>
            <div className='flex flex-row gap-x-4 p-4 text-xl'>
              {socials.map((social) => {
                return (
                  <a
                    key={social.name}
                    href={social.url}
                    target='_blank'
                    rel='noreferrer'
                  >
                    {socialDict[social.name]}
                  </a>
                );
              })}
            </div>
          </div>
        );
      })}
    </section>
  );
}

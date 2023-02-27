"use client";

import { Contact } from "../../../../types";

type Props = {
  contacts: Contact[];
};

export function Contacts({ contacts }: Props) {
  console.log(contacts);

  return (
    <section className='grid grid-cols-3 gap-4'>
      {contacts.map((contact) => {
          const socials = [];

          const socialDict = {
            "twitterUrl": <i className="bi bi-twitter text-cyan-400"></i>,
            "facebookUrl": <i className="bi bi-facebook text-blue-700"></i>,
            "linkedInUrl": <i className="bi bi-linkedin text-blue-500"></i>,
            "githubUrl": <i className="bi bi-github text-gray-800"></i>
          }

          for (const property in contact.socials) {
            socials.push(
              <a href={`https://${contact.socials[property]}`}>
                {socialDict[property]}
              </a>
            )
          }

          console.log(socials);

        return (
          <div
            className='border border-gray-300 bg-gray-50'
            key={contact.id}
          >
              <div className="p-4">
              <p className='text-lg font-medium text-gray-900'>
                {contact.firstName} {contact.lastName}
              </p>
              <p className='text-base'>{contact.jobTitle}</p>
                <p className="text-sm">
                  {contact.companies
                    .map((company) => {
                      return company.name;
                    })
                    .join(", ")}
                </p>
                </div>
                <div className="p-4 border-y border-gray-300 flex flex-col gap-y-2 ">
                  <div className="flex flex-row gap-x-2 items-center">
                  <i className="bi bi-geo-alt"></i>
                  <p className="text-sm">{contact.location}</p>
                  </div>
                  <div className="flex flex-row gap-x-2 items-center">
                  <i className="bi bi-envelope"></i>
                  <p className="text-sm">
                    {contact.emails
                      .map((email) => {
                        return email.name;
                      })
                      .join(", ")}
                  </p>
                  </div>
                  <div className="flex flex-row gap-x-2 items-center">
                  <i className="bi bi-telephone"></i>
                  <p className="text-sm">
                    {contact.phones
                      .map((phone) => {
                        return phone.number;
                      })
                      .join(", ")}
                  </p>  
                  </div>
                </div>
                <div className="flex flex-row gap-x-4 p-4 text-xl">
                    {socials.map((social) => {
                      return social
                    })}
                </div>
              </div>
        );
      })}
    </section>
  );
}

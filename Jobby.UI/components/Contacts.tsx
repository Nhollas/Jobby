"use client";

import Link from "next/link";
import { Contact, Social } from "types";
import { ActionButton } from "./Common";

type Props = {
  contacts: Contact[];
  boardId?: string;
};

export function Contacts({ contacts, boardId }: Props) {
  return (
    <div className="flex flex-col gap-y-4 border-t border-gray-300 p-4 lg:px-8">
      <Link
        href={!boardId ? "/create-contact" : `/create-contact?boardId=${boardId}`}
        className="w-max rounded-full border bg-main-blue py-2 px-8 text-base font-medium text-white hover:border-main-blue hover:bg-gray-50 hover:text-black"
      >
        Create Contact
      </Link>
      {contacts.length === 0 ? (
        <h1>No Contacts Found.</h1>
      ) : (
        <section className="grid grid-cols-[repeat(auto-fill,minmax(250px,1fr))] gap-8">
          {contacts.map((contact) => {
            const socials = [];

            const socialDict = {
              twitterUrl: <i className="bi bi-twitter text-cyan-400"></i>,
              facebookUrl: <i className="bi bi-facebook text-blue-700"></i>,
              linkedInUrl: <i className="bi bi-linkedin text-blue-500"></i>,
              githubUrl: <i className="bi bi-github text-gray-800"></i>,
            };

            const disabledSocialDict = {
              twitterUrl: <i className="bi bi-twitter text-gray-300"></i>,
              facebookUrl: <i className="bi bi-facebook text-gray-300"></i>,
              linkedInUrl: <i className="bi bi-linkedin text-gray-300"></i>,
              githubUrl: <i className="bi bi-github text-gray-300"></i>,
            };

            for (const property in contact.socials) {
              socials.push({
                name: property,
                url: contact.socials[property as keyof Social],
              });
            }

            return (
              <div
                className="w-full max-w-xs border border-gray-300 bg-gray-50"
                key={contact.id}
              >
                <div className="p-4">
                  <p className="text-lg font-medium text-gray-900">
                    {contact.firstName} {contact.lastName}
                  </p>
                  <p className="mt-1.5 mb-0.5 text-gray-800">
                    {contact.jobTitle}
                  </p>
                  <p className="text-sm text-gray-700">
                    {contact.companies
                      .map((company) => {
                        return company.name;
                      })
                      .join(", ")}
                  </p>
                </div>
                <div className="flex flex-col gap-y-2 border-y border-gray-300 bg-white p-4">
                  <div className="flex flex-row items-center gap-x-2">
                    <i className="bi bi-geo-alt text-gray-900"></i>
                    <p className="text-sm text-gray-700">
                      {contact.location ? contact.location : "Location"}
                    </p>
                  </div>
                  <div className="flex flex-row items-center gap-x-2">
                    <i className="bi bi-envelope text-gray-900"></i>
                    <p className="truncate text-sm text-gray-700">
                      {contact.emails.length === 0
                        ? "Email"
                        : contact.emails
                            .slice(0, 2)
                            .map((email) => {
                              return email.name;
                            })
                            .join(", ")}
                    </p>
                  </div>
                  <div className="flex flex-row items-center gap-x-2">
                    <i className="bi bi-telephone text-gray-900"></i>
                    <p className="truncate text-sm text-gray-700">
                      {contact.phones.length === 0
                        ? "Phone"
                        : contact.phones
                            .map((phone) => {
                              return phone.number;
                            })
                            .join(", ")}
                    </p>
                  </div>
                </div>
                <div className="flex flex-row items-center gap-x-4 p-4 text-xl">
                  {socials.map((social) => {
                    return (
                      <a
                        key={social.name}
                        href={social.url}
                        target="_blank"
                        rel="noreferrer"
                        className={
                          social.url === "" ? "pointer-events-none" : ""
                        }
                      >
                        {social.url === ""
                          ? disabledSocialDict[
                              social.name as keyof typeof disabledSocialDict
                            ]
                          : socialDict[social.name as keyof typeof socialDict]}
                      </a>
                    );
                  })}
                  <ActionButton
                    text="View"
                    variant="secondary"
                    rounded
                    className="ml-auto text-base"
                  />
                </div>
              </div>
            );
          })}
        </section>
      )}
    </div>
  );
}

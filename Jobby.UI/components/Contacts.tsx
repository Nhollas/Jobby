"use client";

import { MoreVertical, Mail, Phone, Eye, Trash2 } from "lucide-react";
import Link from "next/link";
import { Contact, Social } from "types";
import { Avatar, AvatarFallback, AvatarImage } from "./ui/avatar";
import { Badge } from "./ui/badge";
import { Button } from "./ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "./ui/card";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "./ui/dropdown-menu";
import { Input } from "./ui/input";

type Props = {
  contacts: Contact[];
  boardId?: string;
};

const contactNameToInitials = (name: string | undefined) => {
  if (!name) return "";

  const [firstName, lastName] = name?.split(" ");
  return `${firstName[0]}${lastName[0]}`;
};

export function Contacts({ contacts, boardId }: Props) {
  console.log(contacts);

  return (
    <div className="flex flex-col gap-y-6 overscroll-contain border-t border-gray-300 p-4 lg:px-8">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-2xl font-medium">Contacts</h1>
        <p className="text-sm text-gray-500">View and manage contacts</p>
      </div>
      <div className="flex flex-row gap-x-4">
        <Input type="text" placeholder="Search.." className="w-full max-w-xs" />
        <Button asChild>
          <Link
            href={
              !boardId
                ? "/create-contact"
                : `/create-contact?boardId=${boardId}`
            }
            className="w-max"
          >
            Create Contact
          </Link>
        </Button>
      </div>
      {contacts.length === 0 ? (
        <h1>No Contacts Found.</h1>
      ) : (
        <section className="grid grid-cols-[repeat(auto-fill,minmax(265px,1fr))] gap-8">
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
              <Card key={contact.id}>
                <CardHeader className="flex flex-col items-start gap-4 space-y-0 p-4 pb-2">
                  <div className="flex w-full flex-row items-center gap-x-4">
                    <Avatar>
                      <AvatarImage
                        src="https://github.com/nhollas.png"
                        alt="@nhollas"
                      />
                      <AvatarFallback>
                        {contactNameToInitials(
                          `${contact.firstName} ${contact.lastName}`
                        )}
                      </AvatarFallback>
                    </Avatar>
                    <div className="flex flex-col gap-y-1">
                      <CardTitle className="whitespace-nowrap">
                        {contact.firstName} {contact.lastName}
                      </CardTitle>
                      <CardDescription>{contact.jobTitle}</CardDescription>
                      <p className="text-xs text-muted-foreground">
                        {contact.location}
                      </p>
                    </div>
                    <DropdownMenu>
                      <DropdownMenuTrigger asChild>
                        <Button
                          variant="outline"
                          className="ml-auto w-10 rounded-full p-0"
                        >
                          <MoreVertical className="h-4 w-4 text-secondary-foreground" />
                          <span className="sr-only">Actions</span>
                        </Button>
                      </DropdownMenuTrigger>
                      <DropdownMenuContent
                        align="end"
                        className="w-[200px]"
                        forceMount
                      >
                        <DropdownMenuLabel>Actions</DropdownMenuLabel>
                        <DropdownMenuSeparator />
                        <DropdownMenuItem>
                          <Eye className="mr-2 h-4 w-4" />
                          <span>View</span>
                        </DropdownMenuItem>
                        <DropdownMenuItem>
                          <Link
                            href={`/delete-contact/${contact.id}`}
                            className="flex w-full flex-row"
                          >
                            <Trash2 className="mr-2 h-4 w-4" />
                            <span>Delete</span>
                          </Link>
                        </DropdownMenuItem>
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </div>
                  <div className="flex w-full flex-row items-center gap-x-2 overflow-hidden">
                    {contact.companies.map((company) => (
                      <Badge key={company.id}>{company.name}</Badge>
                    ))}
                  </div>
                </CardHeader>
                <CardContent className="flex flex-col p-2">
                  <div className="flex flex-col gap-y-2 rounded-lg border bg-muted p-4 text-sm">
                    <div className="flex flex-row items-center gap-x-2">
                      <Mail className="h-4 w-4" />
                      <p className="w-full truncate text-xs">
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
                      <Phone className="h-4 w-4" />
                      <p className="w-full truncate text-xs">
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
                  <p className="ml-auto w-max py-2 pt-4 text-xs text-gray-600">
                    Created {new Date(contact.createdDate).toDateString()}
                  </p>
                </CardContent>
              </Card>
            );
          })}
        </section>
      )}
    </div>
  );
}

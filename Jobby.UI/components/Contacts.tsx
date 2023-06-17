"use client";

import { useContactsQuery } from "@/hooks/useContactData";
import { MoreVertical, Mail, Phone, Eye, Trash2, Layout } from "lucide-react";
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
  url: string;
  boardId?: string;
  jobId?: string;
  querykeyVariable?: any;
};

const contactNameToInitials = (name: string | undefined) => {
  if (!name) return "";

  const [firstName, lastName] = name?.split(" ");
  return `${firstName[0]}${lastName[0]}`;
};

const createUrl = (boardId?: string, jobId?: string) => {
  const params = new URLSearchParams();

  if (boardId) params.set("boardId", boardId);
  if (jobId) params.set("jobId", jobId);

  return `/create-contact${params.toString() ? `?${params.toString()}` : ""}`;
};

export function Contacts({
  contacts: initialContacts,
  boardId,
  jobId,
  url,
  querykeyVariable,
}: Props) {
  const { data: contacts } = useContactsQuery(
    initialContacts,
    url,
    querykeyVariable
  );

  return (
    <div className="flex flex-col gap-y-6 overscroll-contain border-gray-300 p-4 lg:px-8">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-2xl font-medium">Contacts</h1>
        <p className="text-sm text-gray-500">View and manage contacts</p>
      </div>
      <div className="flex flex-row gap-x-4">
        <Input type="text" placeholder="Search.." className="w-full max-w-xs" />
        <Button asChild>
          <Link
            href={createUrl(boardId, jobId)}
            className="w-max whitespace-nowrap"
          >
            Create Contact
          </Link>
        </Button>
      </div>
      {contacts.length === 0 ? (
        <h1>No Contacts Found.</h1>
      ) : (
        <section className="grid grid-cols-[repeat(auto-fill,minmax(300px,1fr))] gap-8">
          {contacts.map((contact) => {
            const socials = [];

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
                    <div className="flex flex-col gap-y-1 truncate">
                      <CardTitle className="">
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
                          className="ml-auto w-10 flex-shrink-0 rounded-full p-0"
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
                          <Link
                            href={`/contact/${contact.id}`}
                            className="flex w-full flex-row"
                          >
                            <Eye className="mr-2 h-4 w-4" />
                            <span>View</span>
                          </Link>
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
                <CardContent className="flex flex-col gap-y-4 p-2">
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
                  {contact.board && (
                    <Button variant="outline" asChild>
                      <Link
                        href={`/board/${contact.board.id}`}
                        className="ml-auto flex h-7 w-max items-center gap-x-2 text-xs"
                      >
                        <Layout className="h-4 w-4" />
                        <p>{contact.board.name}</p>
                      </Link>
                    </Button>
                  )}
                  <p className="ml-auto w-max text-xs text-gray-600">
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

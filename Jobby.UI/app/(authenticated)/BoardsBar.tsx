"use client";

import { Button } from "@/components/ui/button";
import Link from "next/link";
import { Separator } from "@/components/ui/separator";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuPortal,
  DropdownMenuSeparator,
  DropdownMenuShortcut,
  DropdownMenuSub,
  DropdownMenuSubContent,
  DropdownMenuSubTrigger,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
  ChevronsUpDown,
  Cloud,
  CreditCard,
  Github,
  Keyboard,
  Layout,
  LifeBuoy,
  List,
  LogOut,
  Mail,
  MessageSquare,
  Plus,
  PlusCircle,
  Settings,
  User,
  UserCircle2,
  UserPlus,
  Users,
} from "lucide-react";
import { useAuth, useUser } from "@clerk/nextjs";
import { useQuery } from "@tanstack/react-query";
import { Board } from "@/types";
import { clientApi } from "@/lib/clients/clientApi";
import { useBoardsQuery } from "@/hooks/useBoardsData";

export const BoardsBar = () => {
  const { data: boards } = useBoardsQuery();

  const { user } = useUser();

  return (
    <div className="relative z-50 hidden lg:block">
      <div className="fixed top-0 left-0 h-screen w-[250px] border-r border-gray-200 py-4">
        <div className="flex w-full flex-col gap-y-4">
          <div className="py-2 pt-0">
            <h2 className="mb-2 px-6 text-lg font-semibold tracking-tight">
              Track
            </h2>
            <div className="space-y-1 px-2">
              <Button
                asChild
                variant="ghost"
                className="w-full justify-start font-normal"
              >
                <Link href={`/contacts`}>
                  <Users className="mr-2 h-4 w-4" />
                  Contacts
                </Link>
              </Button>
              <Button
                asChild
                variant="ghost"
                className="w-full justify-start font-normal"
              >
                <Link href={`/activities`}>
                  <List className="mr-2 h-4 w-4" />
                  Activities
                </Link>
              </Button>
            </div>
          </div>
          <Separator />
          <div className="m-0 py-2">
            <h2 className="mb-2 px-6 text-lg font-semibold tracking-tight">
              Boards
            </h2>
            <div dir="ltr" className="relative h-[300px] overflow-hidden px-2">
              <div
                data-radix-scroll-area-viewport=""
                className="h-full w-full rounded-[inherit]"
                style={{ overflow: "hidden scroll" }}
              >
                <div style={{ minWidth: "100%", display: "table" }}>
                  <div className="space-y-1">
                    {boards?.map((board) => (
                      <Button
                        asChild
                        variant="ghost"
                        key={board.id}
                        className="w-full justify-start font-normal"
                      >
                        <Link href={`/board/${board.id}`}>
                          <Layout className="mr-2 h-4 w-4" />
                          {board.name}
                        </Link>
                      </Button>
                    ))}
                  </div>
                </div>
              </div>
            </div>
          </div>
          <Separator />
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline" className="mx-2">
                <UserCircle2 className="mr-2 h-5 w-5" />
                <p className="mr-auto">{user?.username}</p>
                <ChevronsUpDown className="ml-2 h-5 w-5 shrink-0 bg-gray-50 opacity-50" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent>
              <DropdownMenuLabel>My Account</DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuGroup>
                <DropdownMenuItem>
                  <User className="mr-2 h-4 w-4" />
                  <span>Profile</span>
                  <DropdownMenuShortcut>⇧⌘P</DropdownMenuShortcut>
                </DropdownMenuItem>
                <DropdownMenuItem>
                  <CreditCard className="mr-2 h-4 w-4" />
                  <span>Billing</span>
                  <DropdownMenuShortcut>⌘B</DropdownMenuShortcut>
                </DropdownMenuItem>
                <DropdownMenuItem>
                  <Settings className="mr-2 h-4 w-4" />
                  <span>Settings</span>
                  <DropdownMenuShortcut>⌘S</DropdownMenuShortcut>
                </DropdownMenuItem>
                <DropdownMenuItem>
                  <Keyboard className="mr-2 h-4 w-4" />
                  <span>Keyboard shortcuts</span>
                  <DropdownMenuShortcut>⌘K</DropdownMenuShortcut>
                </DropdownMenuItem>
              </DropdownMenuGroup>
              <DropdownMenuSeparator />
              <DropdownMenuGroup>
                <DropdownMenuItem>
                  <Users className="mr-2 h-4 w-4" />
                  <span>Team</span>
                </DropdownMenuItem>
                <DropdownMenuSub>
                  <DropdownMenuSubTrigger>
                    <UserPlus className="mr-2 h-4 w-4" />
                    <span>Invite users</span>
                  </DropdownMenuSubTrigger>
                  <DropdownMenuPortal>
                    <DropdownMenuSubContent>
                      <DropdownMenuItem>
                        <Mail className="mr-2 h-4 w-4" />
                        <span>Email</span>
                      </DropdownMenuItem>
                      <DropdownMenuItem>
                        <MessageSquare className="mr-2 h-4 w-4" />
                        <span>Message</span>
                      </DropdownMenuItem>
                      <DropdownMenuSeparator />
                      <DropdownMenuItem>
                        <PlusCircle className="mr-2 h-4 w-4" />
                        <span>More...</span>
                      </DropdownMenuItem>
                    </DropdownMenuSubContent>
                  </DropdownMenuPortal>
                </DropdownMenuSub>
                <DropdownMenuItem>
                  <Plus className="mr-2 h-4 w-4" />
                  <span>New Team</span>
                  <DropdownMenuShortcut>⌘+T</DropdownMenuShortcut>
                </DropdownMenuItem>
              </DropdownMenuGroup>
              <DropdownMenuSeparator />
              <DropdownMenuItem>
                <Github className="mr-2 h-4 w-4" />
                <span>GitHub</span>
              </DropdownMenuItem>
              <DropdownMenuItem>
                <LifeBuoy className="mr-2 h-4 w-4" />
                <span>Support</span>
              </DropdownMenuItem>
              <DropdownMenuItem disabled>
                <Cloud className="mr-2 h-4 w-4" />
                <span>API</span>
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>
                <LogOut className="mr-2 h-4 w-4" />
                <span>Log out</span>
                <DropdownMenuShortcut>⇧⌘Q</DropdownMenuShortcut>
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      </div>
    </div>
  );
};

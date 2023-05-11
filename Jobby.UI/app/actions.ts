"use server";

import { deleteAsync } from "@/lib/serverFetch";
import { auth, currentUser } from "@clerk/nextjs";

export async function helloWorld(boardId: string) {
  await deleteAsync(`/board/delete/${boardId}`);
}

async function addItem(data: any) {
  console.log(await auth().getToken());
  console.log((await currentUser())?.firstName);
  console.log("add item server action", data);
}

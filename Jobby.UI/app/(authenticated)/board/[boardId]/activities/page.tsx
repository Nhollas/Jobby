import { getAsync } from "app/serverClient";
import { Activity } from "types";
import { Activities } from "components/Activities";
import { auth } from "@clerk/nextjs";

export async function Page({
  params: { boardId },
}: {
  params: { boardId: string };
}) {
  const { getToken } = auth();
  const activities = await getAsync<Activity[]>(
    `/board/${boardId}/activities`,
    {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    }
  );

  return <Activities activities={activities} />;
}

export default Page;

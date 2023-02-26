import { PageContainer } from "../../components/Common";
import { getServerSession } from "next-auth";
import { authOptions } from "../../pages/api/auth/[...nextauth]";
import axios from "axios";
import https from "https";
import { Board } from "../../types";
import { Boards } from "./boards";

async function getBoards() {
  const session = await getServerSession(authOptions);

  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const instance = axios.create({
    baseURL: "https://localhost:6001/api",
    httpsAgent: agent,
  });

  let options = {
    headers: {
      authorization: `Bearer ${session?.accessToken}`,
    },
  };

  const { data } = await instance.get<Board[]>("/boards", options);

  return data;
}

export default async function Page() {
  const boards = await getBoards();

  return (
    <PageContainer small title={"Dashboard"}>
      <Boards boards={boards} />
    </PageContainer>
  );
}

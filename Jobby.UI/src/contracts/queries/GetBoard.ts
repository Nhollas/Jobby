import { Activity, Contact, JobList } from "@/types";
import { AxiosInstance } from "axios"

export type GetBoardResponse = {
    id: string;
    name: string;
    jobLists: JobList[]
    activities: Activity[]
    contacts: Contact[]
}

export const getBoard = async (boardId: string, client: AxiosInstance) => {
    const { data } = await client.get<GetBoardResponse>(`/board/${boardId}`)

    return data
}
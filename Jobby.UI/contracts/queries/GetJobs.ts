import { AxiosInstance } from "axios"

export const getJobs = async (client: AxiosInstance) => {
    const { data } = await client.get("/jobs")

    return data
}
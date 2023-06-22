import { AxiosInstance } from "axios";
import { z } from "zod";

export type GetContactResponse = z.infer<typeof contactObj>;

const contactObj = z.object({
    id: z.string(),
    board: z.object({
      id: z.string(),
      name: z.string(),
      createdDate: z.date(),
      lastUpdated: z.date(),
    }),
    jobs: z.array(
      z.object({
        id: z.string(),
        createdDate: z.date(),
        lastUpdated: z.date(),
        title: z.string(),
        company: z.string(),
        index: z.number(),
        jobListId: z.string(),
        boardId: z.string(),
      })
    ),
    firstName: z
      .string()
      .nonempty({ message: "The First Name field is required." }),
    lastName: z
      .string()
      .nonempty({ message: "The Last Name field is required." }),
    jobTitle: z.string().nonempty({ message: "The Title field is required." }),
    location: z.string(),
    socials: z.object({
      twitterUrl: z.string().url().or(z.literal("")),
      facebookUrl: z.string().url().or(z.literal("")),
      linkedInUrl: z.string().url().or(z.literal("")),
      githubUrl: z.string().url().or(z.literal("")),
    }),
    emails: z.array(
      z.object({
        name: z.string().email({ message: "Invalid email address." }),
        type: z.string().transform((val) => parseInt(val)),
      })
    ),
    phones: z.array(
      z.object({
        number: z.string(),
        type: z.string().transform((val) => parseInt(val)),
      })
    ),
    companies: z
      .array(
        z.object({
          value: z
            .string()
            .nonempty({ message: "The Company field is required." }),
        })
      )
      .transform((val) => val.map((v) => v.value)),
  });

export const getContact = async (contactId: string, client: AxiosInstance) => {
    const { data } = await client.get<GetContactResponse>(`/contact/${contactId}`)

    return data
}
import { serverClient } from '../../clients';
import { Contact } from '../../types';

export const getJobContacts = (async (id: string) => {
  const contacts = await serverClient.get<Contact[]>(`/job/${id}/contacts`);
  return contacts;
});
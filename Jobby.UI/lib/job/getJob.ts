import { cache } from 'react';
import { serverClient } from '../../clients';
import { Job } from '../../types';

export const getJob = cache(async (id: string) => {
  const job = await serverClient.get<Job>(`/job/${id}`);
  return job;
});
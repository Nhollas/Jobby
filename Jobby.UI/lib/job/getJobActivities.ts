import { serverClient } from '../../clients';
import { Activity } from '../../types';

export const getJobActivities = (async (id: string) => {
  const activities = await serverClient.get<Activity[]>(`/job/${id}/activities`);
  return activities;
});
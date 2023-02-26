export default async function Page({ params }: { params: { jobId: string } }) {
  return <h1>Job page {params.jobId}</h1>;
}

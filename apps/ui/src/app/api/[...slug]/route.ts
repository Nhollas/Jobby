import { jobbyApiClient } from "@/lib/clients/jobbyApiClient";

function formatUrl(url: string) {
  const formattedUrl = new URL(url || "").pathname.replace("/api", "");

  return formattedUrl;
}

export async function GET(req: Request) {
  const response = await jobbyApiClient.get(formatUrl(req.url), {
    validateStatus: () => true,
  });

  return new Response(response.data, {
    status: response.status,
  });
}

export async function POST(req: Request) {
  const request = await req.json();

  const response = await jobbyApiClient.post(formatUrl(req.url), request, {
    validateStatus: () => true,
  });

  return new Response(response.data, {
    status: response.status,
  });
}

export async function PUT(req: Request) {
  const response = await jobbyApiClient.put(formatUrl(req.url), req.body, {
    validateStatus: () => true,
  });

  return new Response(response.data, {
    status: response.status,
  });
}

export async function DELETE(req: Request) {
  const response = await jobbyApiClient.delete(formatUrl(req.url), {
    validateStatus: () => true,
  });

  return new Response(response.data, {
    status: response.status,
    
  });
}

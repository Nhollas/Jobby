import { serverClient } from "@/lib/clients";

function formatUrl(url: string) {
  const formattedUrl = new URL(url || "").pathname.replace("/api", "");

  return formattedUrl;
}

export async function GET(req: Request) {
  const response = await serverClient.get(formatUrl(req.url), {
    validateStatus: () => true,
  });

  console.log("response", response)

  return new Response(JSON.stringify(response.data), {
    status: response.status,
  });
}

export async function POST(req: Request) {
  const request = await req.json();

  const response = await serverClient.post(formatUrl(req.url), request, {
    validateStatus: () => true,
  });

  return new Response(JSON.stringify(response.data), {
    status: response.status,
  });
}

export async function PUT(req: Request) {
  const response = await serverClient.put(formatUrl(req.url), req.body, {
    validateStatus: () => true,
  });

  return new Response(JSON.stringify(response.data), {
    status: response.status,
  });
}

export async function DELETE(req: Request) {
  const response = await serverClient.delete(formatUrl(req.url), {
    validateStatus: () => true,
  });

  return new Response(JSON.stringify(response.data), {
    status: response.status,
    
  });
}

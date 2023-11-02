import { serverClient } from "@/lib/client";

export async function GET(req: Request) {
  const url = new URL(req.url || "").pathname.replace("/api", "");

  const response = await serverClient.get(url, {
    validateStatus: () => true,
  });

  return new Response(JSON.stringify(response.data), {
    status: response.status,
  });
}

export async function POST(req: Request) {
  const url = new URL(req.url || "").pathname.replace("/api", "");

  const request = await req.json();

  const response = await serverClient.post(url, request, {
    validateStatus: () => true,
  });

  console.log("response", response);

  return new Response(JSON.stringify(response.data), {
    status: response.status,
  });
}

export async function PUT(req: Request) {
  const url = new URL(req.url || "").pathname.replace("/api", "");

  const response = await serverClient.put(url, req.body, {
    validateStatus: () => true,
  });

  return new Response(JSON.stringify(response.data), {
    status: response.status,
  });
}

export async function DELETE(req: Request) {
  const url = new URL(req.url || "").pathname.replace("/api", "");

  const response = await serverClient.delete(url, {
    validateStatus: () => true,
  });

  console.log("RESPONSE", response);

  return new Response(JSON.stringify(response.data), {
    status: response.status,
  });
}

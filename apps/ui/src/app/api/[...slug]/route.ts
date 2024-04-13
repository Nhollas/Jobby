import { jobbyApiClient } from "@/lib/clients/jobbyApiClient";
import { NextResponse } from "next/server";

function formatUrl(url: string) {
  const formattedUrl = new URL(url || "").pathname.replace("/api", "");

  return formattedUrl;
}

async function proxyRequest(req: Request) {
  let data = {};

  if (req.method === "POST" || req.method === "PUT") {
    data = await req.json();
  }

  const response = await jobbyApiClient({
    method: req.method,
    url: formatUrl(req.url),
    data: data,
    validateStatus: () => true,
  });

  // Artificial delay of 3 seconds
  await new Promise((resolve) => setTimeout(resolve, 3000));

  return NextResponse.json(response.data, { status: response.status });
}

export const GET = (req: Request) => proxyRequest(req);
export const POST = (req: Request) => proxyRequest(req);
export const PUT = (req: Request) => proxyRequest(req);
export const DELETE = (req: Request) => proxyRequest(req);

import { jobbyApiClient } from "@/lib/clients/jobbyApiClient";
import { NextResponse } from "next/server";

function formatUrl(url: string) {
  const formattedUrl = new URL(url || "").pathname.replace("/api", "");

  return formattedUrl;
}

async function proxyRequest(req: Request) {
  const requestBody = await req.json();

  const response = await jobbyApiClient({
    method: req.method,
    url: formatUrl(req.url),
    data: requestBody,
    validateStatus: () => true,
  });

  return NextResponse.json(response.data, { status: response.status });
}

export const GET = (req: Request) => proxyRequest(req);
export const POST = (req: Request) => proxyRequest(req);
export const PUT = (req: Request) => proxyRequest(req);
export const DELETE = (req: Request) => proxyRequest(req);

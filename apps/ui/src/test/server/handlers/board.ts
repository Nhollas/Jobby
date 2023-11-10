import { boardGenerator } from "@/test/data-generators";
import { Board } from "@/types";
import { http, HttpResponse } from "msw";

export const boardHandlers = [
  http.post("/api/board", () => {
    return HttpResponse.json<Board>(boardGenerator());
  }),
  http.get("/api/boards", () => {
    return HttpResponse.json<Board[]>([boardGenerator(), boardGenerator()]);
  }),
];

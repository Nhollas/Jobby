import { boardGenerator } from "@/test/data-generators";
import { Board } from "@/types";
import { http, HttpResponse } from "msw";

export const boardHandlers = [
  http.post(
    // The "/pets" string is a path predicate.
    // Only the GET requests whose path matches
    // the "/pets" string will be intercepted.
    "/api/board",
    // The function below is a "resolver" function.
    // It accepts a bunch of information about the
    // intercepted request, and decides how to handle it.
    ({ request, params, cookies }) => {
      return HttpResponse.json<Board>(boardGenerator());
    }
  ),
];

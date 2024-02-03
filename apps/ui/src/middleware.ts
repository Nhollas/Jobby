import { authMiddleware } from "@clerk/nextjs";

export default authMiddleware({
  publicRoutes: ["/signed-out"],
});

export const config = {
  matcher: ["/((?!.+\\.[\\w]+$|_next).*)", "/", "/(api|trpc)(.*)"],
};

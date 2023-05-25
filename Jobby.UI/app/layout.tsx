import "./globals.css";
import { Poppins } from "@next/font/google";
import { ClerkProvider } from "@clerk/nextjs/app-beta";

const poppins = Poppins({
  display: "swap",
  weight: ["100", "200", "300", "400", "500", "600", "700", "800", "900"],
  subsets: ["latin", "latin-ext", "devanagari"],
});

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en" className={poppins.className}>
      <head />
      <body>
        <ClerkProvider>{children}</ClerkProvider>
      </body>
    </html>
  );
}

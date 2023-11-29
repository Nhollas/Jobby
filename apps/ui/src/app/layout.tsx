import "./globals.css";
import { Poppins } from "next/font/google";
import { Providers } from "@/providers";
import { cn } from "@/lib/utils";

const poppins = Poppins({
  display: "swap",
  weight: ["100", "200", "300", "400", "500", "600", "700", "800", "900"],
  subsets: ["latin", "latin-ext", "devanagari"],
});

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <Providers>
      <html lang="en" className={cn(poppins.className)}>
        <head />
        <body>{children}</body>
      </html>
    </Providers>
  );
}

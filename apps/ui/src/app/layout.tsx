import "./globals.css";
import { Poppins } from "next/font/google";
import { Providers } from "@/providers";

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
    <html lang="en" className={poppins.className}>
      <head />
      <body>
        <Providers>{children}</Providers>
      </body>
    </html>
  );
}

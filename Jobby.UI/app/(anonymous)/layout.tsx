"use client";

import "../globals.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import { Poppins } from "@next/font/google";
import Providers from "../providers";
import Navbar from "components/Navbar";

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
        <Providers>
          <Navbar />
          <main>
          {children}
          </main>
        </Providers>
      </body>
    </html>
  );
}

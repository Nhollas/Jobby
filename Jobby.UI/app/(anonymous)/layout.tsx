import Providers from "app/providers";
import Navbar from "components/Navbar";

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <Providers>
      <Navbar />
      <main>{children}</main>
    </Providers>
  );
}

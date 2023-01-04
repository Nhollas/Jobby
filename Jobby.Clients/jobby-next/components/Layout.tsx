import Head from "next/head";
import Navbar from "./Navbar";
import Footer from "./Footer";

export const Layout = ({ children }) => {
  return (
    <div className='w-full'>
      <Head>
        <title>Jobby</title>
        <meta
          name='description'
          content='Jobby is an job application tracker!'
        />
      </Head>
      <Navbar />
      <main className='flex min-h-screen w-full justify-center bg-white'>
        {children}
      </main>
      <Footer />
    </div>
  );
}

export default Layout;

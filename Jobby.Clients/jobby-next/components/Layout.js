import Head from 'next/head'
import Navbar from './Navbar'
import Footer from './Footer'

export default function Layout({ children }) {
  return (
    <div className='w-full'>
      <Head>
        <title>Jobby</title>
        <meta name="description" content="Jobby is an job application tracker!" />
      </Head>
      <Navbar />
      <main className="bg-slate-50 flex w-full justify-center min-h-screen">{children}</main>
      <Footer />
    </div>
  )
}
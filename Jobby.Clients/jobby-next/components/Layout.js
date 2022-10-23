import Head from 'next/head'
import Navbar from './Navbar'
import Footer from './Footer'

export default function Layout({ children }) {
  return (
    <div className='bg-gray-900 w-full min-h-screen'>
      <Head>
        <title>Jobby</title>
        <meta name="description" content="Jobby is an job application tracker!" />
      </Head>
      <Navbar />
      <main className="flex w-full justify-center">{children}</main>
      <Footer />
    </div>
  )
}
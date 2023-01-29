import "../styles/globals.css";
import Layout from "../components/Layout";
import { SessionProvider } from "next-auth/react";
import { ModalContext } from "../contexts/ModalContext";
import { useState } from "react";
import { ViewJobModal } from "../components/Modals/Job/ViewJobModal";

export const App = ({ Component, pageProps, session }) => {
  const [showViewJobModal, setShowViewJobModal] = useState({
    visible: false,
    job: null,
  });

  return (
    <SessionProvider session={session}>
      <ModalContext.Provider
        value={{
          showViewJobModal,
          setShowViewJobModal,
        }}
      >
        <Layout>
          <Component {...pageProps} />
        </Layout>
        <ViewJobModal
          setShowViewJobModal={setShowViewJobModal}
          showViewJobModal={showViewJobModal}
          clearModal={() => setShowViewJobModal({ visible: false, job: null })}
        />
      </ModalContext.Provider>
    </SessionProvider>
  );
};

export default App;

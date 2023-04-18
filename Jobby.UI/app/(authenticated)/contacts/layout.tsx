export default async function Layout({
  children,
  modal,
}: {
  children: React.ReactNode;
  modal: React.ReactNode;
}) {
  console.log(modal);

  return (
    <>
      {children}
      {modal}
    </>
  );
}

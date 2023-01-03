const ModalContainer = (props) => {
  const { children } = props;

  return (
    <div className='absolute inset-0 top-12 z-10 flex h-full w-full justify-center bg-white/90'>
      <div className='h-max w-full max-w-md border border-gray-300 bg-gray-50 p-6'>
        {children}
      </div>
    </div>
  );
};

export default ModalContainer;

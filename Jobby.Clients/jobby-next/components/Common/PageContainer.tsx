export const PageContainer = (props) => {
  const { children, extended, small, title } = props;
  return (
    <section className='flex w-full justify-center p-6 lg:p-8'>
      <div className='relative flex w-full flex-col'>
        <div className='flex h-full flex-col gap-y-8'>
          {title && (
            <div className='flex flex-row'>
              <h1 className='text-2xl font-medium'>{title}</h1>
            </div>
          )}
          {children}
        </div>
      </div>
    </section>
  );
};

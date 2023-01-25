import { ReactNode } from "react";

export const ModalContainer = (props: { children: ReactNode }) => {
  const { children } = props;

  return (
    <div className='absolute inset-0 top-12 z-10 flex w-full justify-center bg-white/70'>
      <div className='h-max w-full max-w-md rounded-md border border-slate-200 bg-gray-100 p-6'>
        {children}
      </div>
    </div>
  );
};

export default ModalContainer;

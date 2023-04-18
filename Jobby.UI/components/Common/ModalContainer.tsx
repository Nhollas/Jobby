import { ReactNode } from "react";

export const ModalContainer = (props: { children: ReactNode }) => {
  const { children } = props;

  return (
    <div className='absolute inset-0 z-10 flex w-full justify-center bg-white/70'>
      <div className='mt-8 h-max w-full max-w-md rounded-md border border-slate-200 bg-white p-8'>
        {children}
      </div>
    </div>
  );
};

export default ModalContainer;

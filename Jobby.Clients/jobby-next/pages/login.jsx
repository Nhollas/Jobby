import { signIn } from "next-auth/react";

export default function Page() {
  async function submitLogin(event) {
    event.preventDefault();

    const result = await signIn("credentials", {
      username: event.target.username.value,
      password: event.target.password.value,
      redirect: true,
      callbackUrl: "/dashboard",
    });

    console.log({ result });
  }

  return (
    <section className='flex w-full flex-col items-center justify-center'>
      <div className='flex w-full max-w-md flex-col gap-y-6 rounded-xl bg-white p-8'>
        <h1 className='font-poppins text-3xl font-semibold'>Log In</h1>
        <p className='font-base -mt-2 mb-4 text-base'>
          Registration is free, set your account up to start using our services.
        </p>
        <form
          className='flex flex-col gap-6'
          method='post'
          onSubmit={submitLogin}
        >
          <div className='flex flex-col gap-y-2'>
            <label htmlFor='username'>Username</label>
            <input
              className='bg-gray-100 py-1.5 px-4'
              name='username'
              id='username'
              type='text'
            />
          </div>
          <p className='flex flex-col gap-y-2'>
            <label htmlFor='password'>Password</label>
            <input
              className='bg-gray-100 py-1.5 px-4'
              name='password'
              id='password'
              type='text'
            />
          </p>
          <button
            type='submit'
            className='rounded-md bg-blue-600 py-2 font-medium text-white'
          >
            Log In
          </button>
        </form>
        <div className='flex flex-row gap-2'>
          <p className='text-base'>Don't have an account?</p>
          <a
            asp-page='/Register'
            className='cursor-pointer text-base font-medium text-indigo-600 underline'
          >
            Register Here
          </a>
        </div>
      </div>
    </section>
  );
}

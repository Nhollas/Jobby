export default function Login() {

    const submitLogin = async (event) => {
        event.preventDefault();
    
        const data = {
            username: event.target.username.value,
            password: event.target.password.value
        }

        const JsonData = JSON.stringify(data)

        console.log(JsonData);

        // Form the request for sending data to the server.
        const options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JsonData,
        }

        // Send the form data to our forms API on Vercel and get a response.
        const response = await fetch('https://localhost:6001/api/auth/login', options)

        // Get the response data from server as JSON.
        // If server returns the name submitted, that means the form works.
        const result = await response.json()

        console.log(result);

        alert(`This is the response data: ${result.token}`)
    }

    return (
        <section className="flex flex-col w-full justify-center items-center">
            <div className="flex flex-col p-8 gap-y-6 w-full max-w-md bg-white rounded-xl">
                <h1 className="text-3xl font-poppins font-semibold">Log In</h1>
                <p className="text-base font-base -mt-2 mb-4">Registration is free, set your account up to start using our services.</p>
                <form className="flex flex-col gap-6" method="post" onSubmit={submitLogin}>
                    <div className="flex flex-col gap-y-2">
                        <label htmlFor="username">Username</label>
                        <input className="bg-gray-100 py-1.5 px-4" name="username" id="username" type="text" />
                    </div>
                    <p className="flex flex-col gap-y-2">
                        <label htmlFor="password">Password</label>
                        <input className="bg-gray-100 py-1.5 px-4" name="password" id="password" type="text" />
                    </p>
                    <button type="submit" className="font-medium bg-blue-600 text-white py-2 rounded-md">Log In</button>
                </form>
                <div className="flex flex-row gap-2">
                    <p className="text-base">Don't have an account?</p>
                    <a asp-page="/Register" className="underline text-indigo-600 font-medium cursor-pointer text-base">Register Here</a>
                </div>
            </div>
        </section>
    )
  }
  
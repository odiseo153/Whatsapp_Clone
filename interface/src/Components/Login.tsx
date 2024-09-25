import { useState } from 'react';
import { Auth } from '../Api/ApiController';
import { Loading } from './Loading';

export default function Login() {
    const [phone,setPhone] = useState("");
    const [isLoading, setIsLoading] = useState(false); // Estado para controlar la carga

    const sendPhone = async () =>{
        setIsLoading(true); // Iniciar carga
        await Auth(phone);
        setIsLoading(false); // Terminar carga
    }

  return (
    <div className="flex justify-center items-center min-h-screen relative">
      <div className="max-w-sm w-full rounded-lg shadow-lg bg-white p-6 space-y-6 border border-gray-200 dark:border-gray-700">
        <div className="space-y-2 text-center">
          <h1 className="text-3xl font-bold">Login</h1>
        </div>
        <div className="space-y-4">
          <div className="space-y-2">
            <label
              className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
              htmlFor="email"
            >
              PhoneNumber
            </label>
            <input
              className="w-full h-10 rounded-md border border-input bg-background px-3 py-2 text-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50"
              type="number"
              id="phone"
              pattern="[0-9]*"
              defaultValue={17323902310}
              placeholder="17323902310"
              onChange={(e) => setPhone(e.target.value)}
              required
            />
          </div>
          <div className="flex items-center space-x-2">
            <hr className="flex-grow border-zinc-200 dark:border-zinc-700" />
          </div>
         
          <button
           onClick={sendPhone}
           className="items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 border border-input hover:bg-accent hover:text-accent-foreground h-10 px-4 py-2 w-full bg-[#4285F4] text-white"
            title="Iniciar sesión"
          >
            <div className="flex items-center justify-center">
            <i className="fa-solid fa-right-to-bracket -mb-1"></i>
            </div>
          </button>
        </div>
      </div>
      {isLoading && <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center">
        <Loading />
      </div>}
    </div>
  );
}

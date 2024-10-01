import { useState } from "react";
import { Conversaciones } from "./Conversaciones";
import { Chat } from "./Chat";
import { chat } from "../Interfaces/Inteface";


export function Home() {
    const [selectedChat, setSelectedChat] = useState<chat>();  
  
    return (
      <div className="flex h-screen ">
        <Conversaciones setSelectedChat={setSelectedChat} />
  
        {selectedChat ?
         <Chat selectedChat={selectedChat} /> :
          <img src="https://c4.wallpaperflare.com/wallpaper/542/50/545/simple-background-blue-simple-minimalism-wallpaper-preview.jpg" alt="Imagen por defecto" />
          }
        
      </div>
    );
  }
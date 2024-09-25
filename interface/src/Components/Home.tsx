import { useState } from "react";
import { Conversaciones } from "./Conversaciones";
import { Chat } from "./Chat";

export function Home() {
    const [selectedChat, setSelectedChat] = useState(null);  
  
    return (
      <div className="flex h-screen ">
        <Conversaciones selectedChat={selectedChat} setSelectedChat={setSelectedChat} />
  
        {selectedChat && <Chat selectedChat={selectedChat} />}
        
      </div>
    );
  }
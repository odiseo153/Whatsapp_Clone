import { useEffect, useState } from 'react';
import { GetConversation } from '../Api/ApiController';
import { Loading } from './Loading';
import { AutoCompleteUserInput } from './AutoComplete';
import { chat } from '../Interfaces/Inteface';





export function Conversaciones({  setSelectedChat }: {  setSelectedChat: (chat: chat) => void }) {
  const [conversations, setConversations] = useState<chat[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [query, setQuery] = useState("");
  const [width, setWidth] = useState(25); // Estado para controlar el ancho del componente (inicialmente 33%)

  useEffect(() => {
    const getConversations = async () => {
      try {
        setIsLoading(true);
        const data = await GetConversation();
        setConversations(data ?? []);
      } finally {
        setIsLoading(false);
      }
    };

    getConversations();
  }, []);

  const ConversationFilter = (): chat[] => {
    return query
      ? conversations.filter(message =>
          message.name.toLowerCase().includes(query.toLowerCase())
        )
      : conversations;
  };

  const CerrarSession = () => {
    sessionStorage.clear();
    window.location.href = "/";
  };

  return (
    <div style={{ width: `${width}%` }} className="h-screen bg-[#111B21] border-r">
    <div className="p-4 bg-[#202C33] flex justify-between items-center" style={{ height: '40px' }}>
      <div className="flex items-center">
        {/* 
          <div className="w-10 h-10 bg-gray-300 rounded-full flex items-center justify-center">
            <span className="text-white font-bold">Y</span>
            <img className="object-cover w-10 h-10 rounded-full" src="" alt="User" />
          </div> 
        */}
        <span className="ml-2 text-white font-semibold">Chats</span>
      </div>
  
      <div className="relative space-x-2">
        <button
          className="p-2 text-white hover:text-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-[#E43F5A]"
          title="Configuración"
          onClick={() => setIsMenuOpen(!isMenuOpen)}
        >
          <i className="fas fa-cog"></i>
        </button>
  
        {isMenuOpen && (
          <div className="absolute right-0 mt-2 w-48 bg-[#2A3942] rounded-md shadow-lg z-10">
            <div className="py-2">
              <a
                href="#"
                onClick={CerrarSession}
                className="block px-4 py-2 text-sm text-gray-200 hover:bg-[#3B4A54] focus:bg-[#3B4A54] focus:outline-none"
              >
                Logout
              </a>
              <a
                href="#"
                className="block px-4 py-2 text-sm text-gray-200 hover:bg-[#3B4A54] focus:bg-[#3B4A54] focus:outline-none"
              >
                Editar
              </a>
            </div>
          </div>
        )}
      </div>
    </div>
  
    <div className="p-2">
      <AutoCompleteUserInput />
    </div>
  
    <div className="h-[calc(100vh-180px)] overflow-y-auto">
      {isLoading ? (
        <Loading />
      ) : (
        <>
          {ConversationFilter().length === 0 ? (
            <div className="text-gray-400">No hay conversaciones</div>
          ) : (
            ConversationFilter().map((chat) => (
              <div
                key={chat.id}
                className="flex items-center space-x-4 p-4 hover:bg-[#202C33] cursor-pointer"
                onClick={() => setSelectedChat(chat)}
              >
                <div className="w-12 h-12 bg-gray-300 rounded-full flex items-center justify-center">
                  <img className="object-cover w-12 h-12 rounded-full" src={chat.photo} alt={`Imagen de ${chat.name}`} />
                </div>
                <div className="flex-1 min-w-0">
                  <p className="text-base font-semibold text-gray-200 truncate">{chat.name}</p>
                  <p className="text-sm text-gray-400 truncate">{chat.lastMessage}</p>
                </div>
                <div className="flex flex-col items-end">
                  {chat.unread > 0 && (
                    <span className="inline-flex items-center justify-center w-6 h-6 text-xs font-bold text-white bg-[#25D366] rounded-full">
                      {chat.unread}
                    </span>
                  )}
                </div>
              </div>
            ))
          )}
        </>
      )}
    </div>
  </div>
  
  );
}

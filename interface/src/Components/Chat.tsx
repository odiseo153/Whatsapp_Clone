import { useEffect, useState } from 'react';
import { chat, GetMessages, message, SendMessage } from '../Api/ApiController';
import { Loading } from './Loading';

export function Chat({ selectedChat }: { selectedChat: chat }) {
  const [messages, setMessages] = useState<message[]>([]);
  const [query, setQuery] = useState<string>("");
  const [message, setMessage] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(true); // Estado para el indicador de carga

  const id = sessionStorage.getItem("Id");

  const fetchMessages = async () => {
    try {
      setIsLoading(true); // Iniciamos la carga
      const data = await GetMessages(selectedChat.id);
      setMessages(data ?? []);
    } catch (error) {
      console.error("Error al obtener los mensajes:", error);
    } finally {
      setIsLoading(false); // Finalizamos la carga
    }
  };

  useEffect(() => {
    if (selectedChat) {
      fetchMessages();
    }
  }, [selectedChat]);

  const messageFilter = (): message[] => {
    return query
      ? messages.filter(message =>
          message.content.toLowerCase().includes(query.toLowerCase())
        )
      : messages;
  };

  const sendMessage = async () => {
    
    const Newmessage = {
      content: message,
      senderId: id,
      receiverId: messages[0].receiverId == id ? messages[0].senderId : messages[0].receiverId,
    };

    await SendMessage(Newmessage);
    await fetchMessages();
    setMessage("");

  };

  function timeAgo(dateString: string): string {
    const now = new Date();
    const pastDate = new Date(dateString);
    const diff = now.getTime() - pastDate.getTime();

    const seconds = Math.floor(diff / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);
    const months = Math.floor(days / 30);
    const years = Math.floor(days / 365);

    if (years > 0) return years === 1 ? 'hace 1 año' : `hace ${years} años`;
    if (months > 0) return months === 1 ? 'hace 1 mes' : `hace ${months} meses`;
    if (days > 0) return days === 1 ? 'hace 1 día' : `hace ${days} días`;
    if (hours > 0) return hours === 1 ? 'hace 1 hora' : `hace ${hours} horas`;
    if (minutes > 0) return minutes === 1 ? 'hace 1 minuto' : `hace ${minutes} minutos`;
    return seconds === 1 ? 'hace 1 segundo' : `hace ${seconds} segundos`;
  }






  return (
    <div className="h-screen flex-1 flex flex-col">
      {/* Header de la conversación */}
      <div className="p-4 bg-[#1E3A8A] flex items-center space-x-4 border-b border-[#334155]">
        <div className="w-10 h-10 bg-[#334155] rounded-full flex items-center justify-center">
          <img className="object-cover w-10 h-10 rounded-full" src={selectedChat.photo} alt={`Imagen de ${selectedChat.name}`} />
        </div>
        <div className="flex-1 min-w-0">
          <p className="text-sm font-medium text-white truncate">{selectedChat.name}</p>
          <p className="text-sm text-[#9CA3AF] truncate">Online</p>
        </div>
        <div className="flex-1 min-w-0">
          <input className="form-control  text-dark border-none" onChange={(e) => setQuery(e.target.value)} type="text" placeholder="Buscar Mensaje" />
        </div>
        <button title="Opciones" aria-label="Opciones" className="p-2 text-white hover:text-gray-200">
          <i className="fas fa-ellipsis-h"></i>
        </button>
      </div>

      {/* Mensajes */}
      <div className="flex-1 p-4 bg-[#0f172a] bg-repeat overflow-y-auto">
        {isLoading ? (
          <Loading /> // Mostramos el indicador de carga
        ) : messageFilter().length === 0 ? (
          <p className="text-center text-gray-400">No hay mensajes.</p>
        ) : (
          messageFilter().map((message, i) => (
            <div
              key={i}
              className={`flex ${message.receiverId !== id ? 'justify-end' : 'justify-start'} mb-4`}
            >
              <div className={`max-w-[70%] rounded-lg p-3 ${message.receiverId === id ? 'bg-[#2563EB]' : 'bg-[#10B981]'}`}>
                <p className="text-sm text-white">{message.content}</p>
                <p className="text-xs text-white text-right mt-1">{timeAgo(message.date)}</p>
              </div>
            </div>
          ))
        )}
      </div>

      {/* Input para enviar mensajes */}
      <div className="p-4 bg-[#1E3A8A] flex items-center space-x-2">
        <button title="Enviar un emoticon" aria-label="Enviar emoticon" className="p-2 text-white hover:text-gray-200">
          <i className="fas fa-smile"></i>
        </button>
        <button title="Adjuntar archivo" aria-label="Adjuntar archivo" className="p-2 text-white hover:text-gray-200">
          <i className="fas fa-paperclip"></i>
        </button>
        <input type="text" value={message} onChange={(e) => setMessage(e.target.value)} placeholder="Escribe un mensaje" className="flex-1 p-2 border-none bg-[#1E3A8A] text-white rounded" />
        <button onClick={sendMessage} title="Enviar mensaje de voz" aria-label="Enviar mensaje de voz" className="p-2 text-white hover:text-gray-200">
          <i className="fa-solid fa-paper-plane"></i>
        </button>
      </div>
    </div>
  );
}

import { useEffect, useState } from 'react';
import { GetMessages, SendMessage, EditMessage, DeleteMessage } from '../Api/ApiController';
import { Loading } from './Loading';
import * as signalR from '@microsoft/signalr';
import { Url } from '../Url';
import { message, chat } from '../Interfaces/Inteface';



export function Chat({ selectedChat }: { selectedChat: chat }) {
  const [messages, setMessages] = useState<message[]>([]);
  const [query, setQuery] = useState<string>("");
  const [message, setMessage] = useState<string>("");
  const [imagePreview, setImagePreview] = useState<string | null>(null);
  const [selectedImage, setSelectedImage] = useState<File | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [connection, setConnection] = useState<any>();
  const [activeMenuId, setActiveMenuId] = useState<string | null>(null);

  const id = sessionStorage.getItem("Id");

  const fetchMessages = async () => {
    setIsLoading(true);
    selectedChat.unread = 0;
    const data = await GetMessages(selectedChat.id);
    setMessages(data ?? []);
    console.log(data);
    setIsLoading(false);
  };

  const connectToHub = async () => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(Url + "messageHub")
      .build();

    connection.on("ReceiveMessage", (receiveId: string, message: message) => {
      if (receiveId === id) {
        setMessages((prevMessages) => [...prevMessages, message]);
      }
    });

    await connection.start();
    setConnection(connection);
  };

  useEffect(() => {
    connectToHub();
    if (selectedChat) {
      fetchMessages();
    }
  }, [selectedChat]);

  const messageFilter = (): message[] => {
    return query 
      ? messages.filter((message) =>
        message.content.toLocaleLowerCase().includes(query.toLocaleLowerCase())
      )
      : messages;
  };

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file && file.type.startsWith("image/")) {
      setSelectedImage(file);
      const reader = new FileReader();
      reader.onload = () => {
        setImagePreview(reader.result as string);
      };
      reader.readAsDataURL(file);
    } else {
      setSelectedImage(null);
      setImagePreview(null);
    }
  };

  const sendMessage = async () => {
    const Newmessage = {
      content: message,
      senderId: id,
      receiverId: messages[0].receiverId == id ? messages[0].senderId : messages[0].receiverId,
    };

    if (selectedImage) {
      const reader = new FileReader();
      reader.onload = async (e) => {
        const base64Image = e?.target?.result;
        await SendMessage({ ...Newmessage, image: base64Image });
      };
      reader.readAsDataURL(selectedImage);
    } else {
      await SendMessage(Newmessage);
    }

    await fetchMessages();
    setMessage("");
    setSelectedImage(null);
    setImagePreview(null);
  };

  const timeAgo = (dateString: string): string => {
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
  };

  const toggleMenu = (messageId: string) => {
    setActiveMenuId((prevId) => (prevId === messageId ? null : messageId));
  };

  const handleDelete = async (messageId: string) => {
    console.log("Eliminar mensaje con id:", messageId);
    DeleteMessage(messageId);
    fetchMessages();
    const message = messages.filter(x => x.id != messageId);
    setMessages(message);
  };

  const handleEdit = (messageId: string) => {
    console.log("Editar mensaje con id:", messageId);
  };


  return (
    <div className="h-screen w-full flex flex-col bg-[#EDEDED]">
  {/* Encabezado de la conversación */}
  <div className="h-10 p-4 bg-[#008069] flex items-center space-x-4 border-b border-[#D1D1D1]">
    <div className="w-10 h-10 rounded-full overflow-hidden">
      <img
        className="object-cover w-full h-full"
        src={selectedChat.photo}
        alt={`Imagen de ${selectedChat.name}`}
      />
    </div>
    <div className="flex-1 min-w-0">
      <p className="text-sm font-semibold text-white truncate">
        {selectedChat.name}
      </p>
      <p className="text-xs text-[#D9D9D9] truncate">Online</p>
    </div>
    <div className="flex-1 min-w-0 relative">
      <input
        className="w-full bg-transparent focus:outline-none text-white pl-10 placeholder-[#B3B3B3]"
        onChange={(e) => setQuery(e.target.value)}
        type="text"
        placeholder="Buscar mensaje"
      />
      <i className="fas fa-search absolute left-2 top-1/2 transform -translate-y-1/2 text-[#B3B3B3]"></i>
    </div>
    <button
      title="Opciones"
      aria-label="Opciones"
      className="p-2 text-white hover:text-gray-200"
    >
      <i className="fas fa-ellipsis-h"></i>
    </button>
  </div>

  {/* Mensajes */}
  <div className="flex-1 p-4 bg-[#EFEFEF] overflow-y-auto">
    {isLoading ? (
      <Loading />
    ) : messageFilter().length === 0 ? (
      <p className="text-center text-gray-500">No hay mensajes.</p>
    ) : (
      messageFilter().map((message, i) => (
        <div
          key={i}
          className={`flex ${
            message.receiverId !== id ? 'justify-end' : 'justify-start'
          } mb-4`}
        >
          <div
            className={`rounded-lg p-3 max-w-sm shadow-md ${
              message.receiverId !== id
                ? 'bg-[#DCF8C6] text-left'
                : 'bg-white text-right'
            }`}
          >
            <p className="text-sm text-[#303030]">{message.content}</p>
            {message.image && (
              <img
                src={message.image}
                alt="Imagen del mensaje"
                className="w-full h-auto rounded-lg mt-2"
              />
            )}
            <p className="text-xs text-[#909090] mt-1">
              {timeAgo(message.date)}
            </p>
            {message.receiverId !== id && (
              <button onClick={() => toggleMenu(message.id)} className="text-[#909090] mt-1">
                <i className="fa-solid fa-ellipsis"></i>
              </button>
            )}

            {message.receiverId !== id && activeMenuId === message.id && (
              <div className="flex justify-end mt-2 space-x-2">
                <button
                  className="text-white text-xs bg-red-500 hover:bg-red-700 rounded px-2 py-1"
                  onClick={() => handleDelete(message.id)}
                >
                  Eliminar
                </button>
                <button
                  className="text-white text-xs bg-green-500 hover:bg-green-700 rounded px-2 py-1"
                  onClick={() => handleEdit(message.id)}
                >
                  Editar
                </button>
              </div>
            )}
          </div>
        </div>
      ))
    )}
  </div>

  {/* Vista previa de la imagen */}
  {imagePreview && (
    <div className="p-2 bg-[#008069]">
      <div className="flex justify-between items-center">
        <span className="text-white">Vista previa de la imagen</span>
        <button
          onClick={() => {
            setImagePreview(null);
            setSelectedImage(null);
          }}
        >
          <i className="fa-solid fa-times text-white"></i>
        </button>
      </div>
      <img
        src={imagePreview}
        alt="Vista previa"
        className="w-25 h-auto rounded-lg mt-2"
      />
    </div>
  )}

  {/* Enviar mensaje */}
  <div className="p-4 bg-[#F0F0F0] flex items-center space-x-4">
    <input
      className="flex-1 bg-white rounded-full py-2 px-4 text-[#303030] focus:outline-none placeholder-gray-500"
      value={message}
      onChange={(e) => setMessage(e.target.value)}
      type="text"
      placeholder="Escribir un mensaje..."
    />

    <label
      htmlFor="image-input"
      className="p-2 text-[#606060] hover:text-gray-400 cursor-pointer"
    >
      <i className="fas fa-paperclip"></i>
      <input
        id="image-input"
        type="file"
        accept="image/*"
        className="hidden"
        aria-label="Seleccionar imagen"
        onChange={handleImageChange}
      />
    </label>

    <button
      onClick={sendMessage}
      className="p-2 bg-[#008069] hover:bg-[#007B63] text-white rounded-full focus:outline-none"
      title="Enviar mensaje"
    >
      <i className="fas fa-paper-plane"></i>
    </button>
  </div>
</div>

  
  );
}

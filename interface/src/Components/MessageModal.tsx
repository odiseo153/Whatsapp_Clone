import { useEffect, useState } from 'react';
import { GetUsuarios, SendMessage } from '../Api/ApiController';
import { User } from '../Interfaces/Inteface';

const MessageModal = ({ isOpen, onClose,Conversation }) => {
  const [message, setMessage] = useState('');
  const [selectedUser, setSelectedUser] = useState('');
  const [users, setUsers] = useState<User[]>([]);
  const [showUserList, setShowUserList] = useState(false);

  const id = sessionStorage.getItem("Id")


  const sendMessage = async () => {
    const Newmessage = {
      content: message,
      senderId: id,
      receiverId: selectedUser ,
    };

    await SendMessage(Newmessage);
    setMessage('');
    setSelectedUser('');
    Conversation();
    onClose();
  };

  useEffect(() => {
    const getUsers = async () => {
      try {
        const usersData = await GetUsuarios();
        setUsers(usersData ?? []);
      } catch (error) {
        console.error("Error al obtener usuarios:", error);
      }
    };

    getUsers();
  }, []);

  const handleUserSelect = (user: User) => {
    setSelectedUser(user.id);
    setShowUserList(false); // Cerrar la lista de usuarios al seleccionar
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-50">
      <div className="bg-white rounded-lg p-6 shadow-lg w-1/3">
        <h2 className="text-xl font-semibold mb-4">Enviar Mensaje</h2>

        <div className="relative mb-4">
          <label className="block mb-2 text-gray-700" htmlFor="user-select">
            Seleccionar Usuario
          </label>
          <div 
            className="block w-full p-2 border border-gray-300 rounded cursor-pointer mb-4 focus:outline-none focus:ring-2 focus:ring-[#008069]"
            onClick={() => setShowUserList(!showUserList)}
          >
            {selectedUser ? (
              users.find(user => user.id === selectedUser)?.name || "Seleccione un usuario"
            ) : (
              "Seleccione un usuario"
            )}
          </div>

          {showUserList && (
            <div className="absolute z-10 w-full bg-white border border-gray-300 rounded shadow-lg">
              {users.map((user) => (
                <div
                  key={user.id}
                  className="flex items-center p-2 hover:bg-[#f0f0f0] cursor-pointer"
                  onClick={() => handleUserSelect(user)}
                >
                  <img src={user.profilePhoto} className="w-10 h-10 rounded-full mr-2" alt={user.name} />
                  <span>{user.name}</span>
                </div>
              ))}
            </div>
          )}
        </div>

        <label className="block mb-2 text-gray-700" htmlFor="message">
          Mensaje
        </label>
        <textarea
          id="message"
          className="block w-full p-2 border border-gray-300 rounded mb-4 focus:outline-none focus:ring-2 focus:ring-[#008069]"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          placeholder="Escriba su mensaje..."
          rows={4}
        />

        <div className="flex justify-end">
          <button
            className="bg-[#008069] hover:bg-[#007B63] text-white rounded px-4 py-2 mr-2"
            onClick={sendMessage}
            disabled={!message || !selectedUser}
          >
            Enviar
          </button>
          <button
            className="bg-gray-300 hover:bg-gray-400 text-gray-700 rounded px-4 py-2"
            onClick={onClose}
          >
            Cancelar
          </button>
        </div>
      </div>
    </div>
  );
};

export default MessageModal;

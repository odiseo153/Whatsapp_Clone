import React, { MouseEventHandler, useState, useEffect } from 'react';
import { GetUsuarios } from '../Api/ApiController';
import { User,chat } from '../Interfaces/Inteface';

const Autocomplete = ( ) => {
  const [filteredSuggestions, setFilteredSuggestions] = useState<User[]>([]);
  const [activeSuggestionIndex, setActiveSuggestionIndex] = useState(0);
  const [showSuggestions, setShowSuggestions] = useState(false);
  const [userInput, setUserInput] = useState('');
  const [users, setUsers] = useState<User[]>([]);
  const [chat, setChat] = useState<chat>({
    name:"odiseo",
    lastMessage:"",
    id:"",
    photo:"",
    unread:0,
  });


  useEffect(() => {
    const fetchUsers = async () => {
      const Users = await GetUsuarios();
      setUsers(Users ?? []);
    };
    fetchUsers();
  }, []);

  const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const userInput = e.currentTarget.value;
    const filteredSuggestions = users.filter(
      (user) =>
        user.name.toLowerCase().indexOf(userInput.toLowerCase()) > -1
    );

    setActiveSuggestionIndex(0);
    setFilteredSuggestions(filteredSuggestions);
    setShowSuggestions(true);
    setUserInput(userInput);
  };

  const onClick: MouseEventHandler<HTMLLIElement> = (e) => {
    const clickedUser = filteredSuggestions.find(
      (user) => user.name === e.currentTarget.innerText
    );

    if (clickedUser) {
      setUserInput(clickedUser.name);
    }

    setFilteredSuggestions([]);
    setActiveSuggestionIndex(0);
    setShowSuggestions(false);


  };

  const onKeyDown = (e: React.KeyboardEvent<HTMLDivElement>) => {
    if (e.key === 'Enter') {
      setUserInput(filteredSuggestions[activeSuggestionIndex].name);
      setActiveSuggestionIndex(0);
      setShowSuggestions(false);
    } else if (e.key === 'ArrowUp') {
      if (activeSuggestionIndex === 0) {
        return;
      }
      setActiveSuggestionIndex(activeSuggestionIndex - 1);
    } else if (e.key === 'ArrowDown') {
      if (activeSuggestionIndex === filteredSuggestions.length - 1) {
        return;
      }
      setActiveSuggestionIndex(activeSuggestionIndex + 1);
    }
  };

  const SuggestionsListComponent = () => {
    return filteredSuggestions.length ? (
      <ul className="absolute bg-black border border-gray-300 w-full rounded-md mt-1 max-h-60 overflow-y-auto z-10 ">
        {filteredSuggestions.map((suggestion, index) => {
          let className;
          if (index === activeSuggestionIndex) {
            className = 'bg-blue-500 text-white';
          } else {
            className = 'hover:bg-blue-500';
          }
          return (
            <li
              className={`cursor-pointer text-white p-2 flex items-center ${className}`}
              key={suggestion.name}
              onClick={onClick}
            >
              <a className="flex" href="#">
              <img src={suggestion.profilePhoto} alt={suggestion.name} className="w-6  h-6 rounded-full mr-2" />
              {suggestion.name}
              </a>
              
            </li>
          );
        })}
      </ul>
    ) : (
      <div className="absolute text-white bg-white border border-gray-300 w-full rounded-md mt-1 z-10 p-2">
        <em>No hay coincidencias</em>
      </div>
    );
  };

  return (
    <div className="relative w-64">
      <div className="absolute inset-y-0 left-0 pr-3 flex items-center pointer-events-none">
        <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 text-gray-400 sm:h-5 sm:w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth="2">
          <path strokeLinecap="round" strokeLinejoin="round" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
        </svg>
      </div>
      <input
        type="text"
        className="w-full bg-transparent focus:outline-none text-white pl-10"
        onChange={onChange}
        onKeyDown={onKeyDown}
        value={userInput}
        placeholder="Buscar"
      />
      {showSuggestions && userInput && <SuggestionsListComponent />}
    </div>
  );
};

// Ejemplo de uso
export const AutoCompleteUserInput = ( ) => {
  return (
    <div className="flex p-2 justify-center items-center ">
      <Autocomplete />
    </div>
  );
};


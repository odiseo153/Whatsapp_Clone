import { redirect } from "react-router-dom";
import { Url } from "../Url"
import { Message } from "../Mensaje";

interface Response {
  isSuccess:boolean;
    data:any;
    statusCode:number;
    message:string;
}

export interface chat {
  name:string;
  lastMessage:string;
  id:string;
  photo:string;
  unread:number;
}


export interface message {
  senderId:string;
  receiverId:string;
  content:string;
  date:string;
  read:boolean;
}

const getHeaders = (contentType = "application/json") => {
    const token = sessionStorage.getItem("token") || "";
    return {
        "Content-Type": contentType,
        "Authorization": `Bearer ${token}`

    };
};

const options = (method: string, data?: object, contentType = "application/json") => {
    return {
        method,
        headers: getHeaders(contentType),
        body: data ? JSON.stringify(data) : data
    };
};


export const Auth = async (phone: string) => {
    try {
      const response = await fetch(`${Url}Auth`, options('POST', { phone }));
      
      if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
      }
  
      const data: Response = await response.json(); 
  

      console.log(data);
      if (data.isSuccess ) {
        // Guardar ID en sessionStorage solo si la autenticación fue exitosa
        sessionStorage.setItem("Id", data.data.id);
        console.log(data.data);
        sessionStorage.setItem("User", data.data);
  
        // Redirigir al usuario a la página principal
        window.location.href = "/home";
      } else {
        console.log(data)
        Message.errorMessage(data.message);
      }
    } catch (error: any) {
      console.error("Error en la autenticación:", error.message);
      // Aquí puedes mostrar un mensaje de error al usuario o manejar el error de forma personalizada.
    }
  };



  export const GetConversation = async (): Promise<chat[] | null> => {
    try {
      const id = sessionStorage.getItem("Id");
  
      // Verifica si el ID está presente antes de hacer la petición
      if (!id) {
        console.error("ID de sesión no encontrado.");
        return null; // O puedes lanzar un error si prefieres
      }
  
      const peticionUrl = `${Url}Conversation/${id}`;
      const response = await fetch(peticionUrl, options('GET'));
  
      if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
      }
  
      const data: chat[] = await response.json(); 
  
      return data;

    } catch (error: any) {
      console.error("Error en la obtención de la conversación:", error.message);
      return null; // Devuelve `null` o maneja el error de otra manera
    }
  };
  


  export const GetMessages = async (ConversationId:string): Promise<message[] | null> => {
    try {
  
      // Verifica si el ID está presente antes de hacer la petición
      if (ConversationId == "") {
        console.error("ID de sesión no encontrado.");
        return null; // O puedes lanzar un error si prefieres
      }
  
      const peticionUrl = `${Url}Message/${ConversationId}`;
      const response = await fetch(peticionUrl, options('GET'));
  
      if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
      }
  
      const data: message[] = await response.json(); 
  
      return data;
    } catch (error: any) {
      console.error("Error en la obtención de la conversación:", error.message);
      return null; // Devuelve `null` o maneja el error de otra manera
    }
  };


  export const SendMessage = async (message:any) => {
    try {

  
      const peticionUrl = `${Url}Message`;
    
      const response = await fetch(peticionUrl, options('POST',message));
  
      if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
      }
  

    } catch (error: any) {
      console.error("Error en la obtención de la conversación:", error.message);
      return null; // Devuelve `null` o maneja el error de otra manera
    }
  };
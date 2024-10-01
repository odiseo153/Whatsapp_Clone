export interface Response {
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
  id:string;
    senderId:string;
    receiverId:string;
    image:string;
    content:string;
    date:string;
    read:boolean;
  }

export interface User {
    name:string;
    id:string;
    phone:string;
    profilePhoto:string;
  }
import React from 'react'
import { MessageForm } from "./MessageForm";
import { FormHoc } from "../../../HOC/FormHoc";


export const SendMessage = ({
    show,
    setShow,
    profId,
    PatientId
}) => {

    const endpoint = "/api/Messages"
    const initailMessageObj = {

        SenderId : PatientId,
        RecieverId : profId,
        Content : ""
    }

    
    return (FormHoc(
      endpoint,
      MessageForm,
      initailMessageObj,
      {show,setShow}
    )      
  );

    


}
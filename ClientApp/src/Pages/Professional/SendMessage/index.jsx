import { MessageForm } from "./MessageForm";
import { FormHoc } from "../../../HOC/FormHoc";


export const SendMessage = ({
    show,
    setShow,
    profId,
    patientId
}) => {

    const endpoint = "/api/Messages"
    const initailMessageObj = {

        SenderId : patientId,
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
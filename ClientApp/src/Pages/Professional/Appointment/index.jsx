
import { FormHoc } from "../../../HOC/FormHoc";
import { Appointmentform } from "./appointmentform";


export const MakeAppointment = ({
    show,
    setShow,
    profId,
    patientId,
    Services
}) => {

    

    const endpoint = "/api/appointments/transaction"
    const initialAppointmentObj = {
        AppDate: "",
        AppTime: "",
        AppReason: "",
        ProfessionalId: profId,
        totalcost : 5000,
        PatientId : patientId,
        ServiceList: []
    } 


    return (FormHoc(
        endpoint,
        Appointmentform,
        initialAppointmentObj,
        {show,setShow,Services}
      )
      
    );
}
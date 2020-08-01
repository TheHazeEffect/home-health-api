
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
        totalcost : 0.00,
        PatientId : patientId,
        ServiceList: [],
        ishomevisit: false,
        name : "",
        lat: 0.00,
        lng: 0.00
    } 


    return (FormHoc(
        endpoint,
        Appointmentform,
        initialAppointmentObj,
        {show,setShow,Services}
      )
      
    );
}
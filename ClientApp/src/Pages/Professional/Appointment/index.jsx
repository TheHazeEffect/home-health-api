
import { FormHoc } from "../../../HOC/FormHoc";
import { Appointmentform } from "./appointmentform";


export const MakeAppointment = ({
    show,
    setShow,
    profId,
    patientId,
    services
}) => {

    const endpoint = "/api/appointments"
    const initialAppointmentObj = {
        AppDate: "",
        AppTime: "",
        AppReason: "",
        ProfessionalId: profId,
        totalcost : 5000,
        PatientId : patientId
    } 


    return (FormHoc(
        endpoint,
        Appointmentform,
        initialAppointmentObj,
        {show,setShow}
      )
      
    );
}
import React, {useState,useEffect} from 'react'
import Modal from 'react-bootstrap/Modal'
import Button from 'react-bootstrap/Button'
import { Appointmentform } from "./appointmentform";
import { AlertFactory } from "../../../Factory/Alertfactory";
import { LoadingSpinner } from "../../../components/LoadingSpinner";
import axios from 'axios'

import { AlertComp } from "../../../components/AlertComp";


export const MakeAppointment = ({
    profId,
    services,
    show,
    setShow,
}) => {

    const initialAppointmentObj = {
        AppDate: "",
        AppTime: "",
        AppReason: "",
        ProfessionalId: "",
        totalcost : "",
        PatientId : ""
    } 

    const initialAlertObj =  AlertFactory("","","")

  
    const [AppointmentObj,setAppointmentObj] = useState({...initialAppointmentObj })
    const [errorObj,SetErrorObj] = useState({...initialAppointmentObj})
  
    const [AlertProps,setAlertProps] = useState(initialAlertObj)
    const [ShowAlert,setShowAlert] = useState(false)
    const [Loading,setLoading] = useState(false)

    const handleSubmit = async (event) =>{
      event.preventDefault()

      
      try {
        
        setLoading(true)
        var result = await axios
          .post("/api/appointments",AppointmentObj)
        

        const AlertObj = result.status === 200
        ?
          AlertFactory("sucess","Appointment Sucessfully","Your appointment was made")
        :
          AlertFactory("danger","Appointment failed","Appointment could not be made")




        setLoading(false)
        setAlertProps(AlertObj)
        setShowAlert(true)

      }catch(ex) {
        setLoading(false)
        const AlertObj = AlertFactory("danger","Appointment failed","Something went wrong")

        setShowAlert(true)
        setAlertProps(AlertObj)
        console.log(ex)
      }
    }

    const handleChange = (event) => {
      const {name,value} = event.target
      let errors = errorObj

      switch(name) {

        case "AppDate" :

        case "AppTime":

        case "AppReason" :

        default:
          console.log("incorrect name?: " + name)
      }
    }

    return (
        <>
           <Modal show={show} >
              <Modal.Dialog>
                <Modal.Header >
                    <Modal.Title> Make an Appointment</Modal.Title>
                </Modal.Header>

               <Modal.Body>

                <AlertComp
                    {...AlertProps}
                    Show={ShowAlert}
                    setShow={setShowAlert}
                />    

                <Appointmentform
                  handleChange={handleChange}      
                />

              </Modal.Body>
              <Modal.Footer>
                
              {
                  Loading === true ?
                  <Button 
                    className="prof-button"
                    variant="primary" 
                    type="submit"
                    onClick={ (e)=> e.preventDefault()}
                          >
                      <LoadingSpinner 
                        Show={Loading}
                      />
                    </Button>
                  :
                  <Button 
                    className="prof-button"
                    variant="primary" 
                    type="submit"
                    onClick={handleSubmit}>
                      Submit Appointment
                  </Button>
                }
                {` `}
                  <Button 
                    className="prof-button"
                    variant="danger" 
                    onClick={() =>setShow(false)}>Cancel
                </Button> 
              </Modal.Footer>
            </Modal.Dialog>
          </Modal>

        </>

    );
}
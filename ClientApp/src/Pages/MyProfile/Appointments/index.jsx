import React,{useState,useEffect} from "react";
import Accordion from 'react-bootstrap/Accordion'
import Card from 'react-bootstrap/Card'
import Table from 'react-bootstrap/Table'
import { LoadingSpinner } from "../../../components/LoadingSpinner";
import './Appointment.css'
import axios from "axios";



export const Appointments = ({user}) => {

    const [Appointments,setAppointments] = useState([])
    const [Loading,setLoading] = useState(false)


    useEffect( () => {

        const fetchData = async () => {

            try {

                setLoading(true)
                var result = await axios
                    .get(`/api/Appointments/profile/${user.id}`)
                
                if(result.status === 200){
                    setAppointments(result.data)
                }
                
                setLoading(false)
                
            }catch(ex) {
                setLoading(false)
                console.log(ex)
            }
            
        }

        fetchData()

    },[user.id])

    

    return (
        <>
            {Loading === true ? <div className="App-SpinnerStyle"> <LoadingSpinner Show={Loading}/> </div>
                :
                Appointments.map( (A,i) => (
                    <Accordion key={i} as={Table}>
                        <Accordion.Toggle as={"tr"} eventKey={A.appointmentId}>
                        {`${A.personFirstName} ${A.personLastName} - `} 
                        {`${new Date(A.appDate).getDate()}/${new Date(A.appDate).getMonth()}/${new Date(A.appDate).getFullYear()} - `}
                        {`${new Date(A.appTime).toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })}`} <br/>
                        {`$${A.totalcost.toFixed(2)}JMD`} <br/>
                        {`location`} 
                        </Accordion.Toggle>
                        <Accordion.Collapse eventKey={A.appointmentId}>
                            <Card.Body>
                            Details:  {A.appReason}
                            </Card.Body>
                        </Accordion.Collapse>
                    <hr />
                    {
                        A.charges.map( (C,i) => (
                            <Accordion.Collapse key={C.chargeId} eventKey={A.appointmentId}>
                                <Card.Body>
                                    {`${C.serviceName} - ${C.serviceCost}`}
                                </Card.Body>
                            </Accordion.Collapse>                     
                        ))
                    }
                </Accordion>
                ))
            }
        </>
    )
}







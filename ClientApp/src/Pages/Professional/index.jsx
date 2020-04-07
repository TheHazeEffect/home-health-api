import React, {useEffect,useState} from "react"
import Card from 'react-bootstrap/Card'
import { Link } from 'react-router-dom';
import axios from "axios";
import Tabs from 'react-bootstrap/Tabs'
import Tab from 'react-bootstrap/Tab'
import ListGroup from 'react-bootstrap/ListGroup'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../components/LoadingSpinner";
import { MakeAppointment } from "./Appointment";
import { SendMessage } from "./SendMessage";
import { MapComponent } from "./MapComponent";
import {  CommentSection } from "./Comments";
import './Professional.css'

import { DisabledOverlayButton } from "./DisabledOverlayButton";

export const Professional = ({match,user}) =>{

    const [Professional,setProfessional] = useState(null)
    const [loading, setLoading] = useState(false)
    const [ShowMessage,setShowMessage] = useState(false)
    const [ShowAppoinment,setShowAppointment] = useState(false)

    
    const id = match.params.id


    useEffect (() => {

        const fetchData = async () => {
            setLoading(true);

            try {

                var result = await axios.get(`/api/Professionals/${id}/details`)
                
                setProfessional(result.data)
                console.log(result.data)

            }catch(error){
                console.log(error)
            }
            setLoading(false)
        }

        fetchData();
    },[id])


    const returnImgForGender = (string) => {

        const imgurl = string === "Male" ? 
            "https://sahelhospital.com/images/doctors/anonymous_doctor_male.png"
            :
            "https://sahelhospital.com/images/doctors/anonymous_doctor_female.png"

        return imgurl;
    }


    return(
        <>
        
        {

            loading === true  || Professional === null ? 
                <div className="centerloader">
                    <LoadingSpinner Show={loading} /> <span>Loading . . .</span>
                </div> 
            : 
            <>
            <SendMessage 
                profId={Professional.user.id}
                show={ShowMessage}
                setShow={setShowMessage}
                patientId={user.id}
            
            />
            <MakeAppointment
                Services ={Professional.prof_services}
                profId={Professional.user.id}
                show={ShowAppoinment}
                setShow={setShowAppointment}
                patientId={user.id}
            />
            <Row>
                <Col>
                <Card className="text-center">
                    <Card.Body>
                    <Card.Title>
                       
                    </Card.Title>
                        <Card.Text>
                        <Link to='/services'>
                            <i className="fas fa-arrow-circle-left"></i> {' '}
                        </Link>
                            {Professional !== null ? `${Professional.user.firstName} ${Professional.user.lastName}` : false }
                        </Card.Text>
                    </Card.Body>
                </Card>
            </Col>
            </Row>
            <br />
            <br />
            <Row>
                <Col>
 
                    <Card className="prof-col" key={Professional.professionalsId}>
                    {/* <i className=" prof-icon fas fa-user-circle"></i> */}
                    <Card.Img className='prof-icon' variant="top" src={returnImgForGender(Professional.user.gender)} />

                        <Card.Body>
                            <Card.Title>
                                Location
                            </Card.Title>
                            <Card.Text>
                                {`${Professional.addressString}`}
                            </Card.Text>
                        </Card.Body>
                        <Card.Footer>

                            {
                                user.loggedin === true  ?
                                    <>
                                        <Button 
                                            onClick={()=> setShowAppointment(true)}
                                            className="prof-button" 
                                            variant="success">
                                            Make Appointment
                                        </Button>
                                        <Button
                                            onClick={ () => setShowMessage(true)}
                                            className="prof-button" 
                                            disabled={user.loggedin === false ? true :  false}
                                            variant="info">
                                                Send Message
                                        </Button>{' '}
                                    </>
                                :
                                    <>
                                        <DisabledOverlayButton
                                            variant="success"
                                            Text="Make Appointment"
                                        />
                                        <DisabledOverlayButton
                                            variant="info"
                                            Text="Send Message"
                                        />
                                    </>
                                }
                                    
                                </Card.Footer>
                            </Card>
                        </Col>
                        <Col  >
                        
                        <Tabs variant="pills" defaultActiveKey="Details" id="uncontrolled-tab-example">
                        
                            <Tab  eventKey="Details" title="Professional Details">
                                <Card className="prof-col prof-details">                   
                                    <Card.Body>
                                
                                    <Card.Subtitle>
                                        Email : {Professional.user.email}
                                    </Card.Subtitle>
                                    <hr/>
                                    <Card.Subtitle>
                                        Contact: {Professional.user.phoneNumber}
                                    </Card.Subtitle>
                                    <hr/>
                                    <Card.Subtitle>
                                        Biography
                                    </Card.Subtitle>
                                    <hr />
                                    <Card.Subtitle>
                                       {Professional.biography}
                                    </Card.Subtitle>
                                
                                    </Card.Body>
                                </Card>
                            </Tab>
                            <Tab eventKey="Services" title="Services" >
                                <Card className="prof-col prof-details">                   
                                    <Card.Body>
                                        <ListGroup variant="flush">
                                            {Professional.prof_services.map( (PS,i) => (
                                                <>
                                                    <ListGroup.Item as={Card.Subtitle} key={i}>
                                                        {`${PS.service.serviceName} - $${PS.serviceCost.toFixed(2)}JMD`}
                                                    </ListGroup.Item>
                                                    <hr />
                                                </> 
                                            ))}
    
                                        </ListGroup>
                                    </Card.Body>
                                </Card>
                            </Tab>
                        </Tabs>
                   
                </Col>
            </Row>
            <Row >
                <Col xs >
                    <MapComponent   
                        lat={Professional.lat}
                        lng={Professional.lng}
                        MarkerText={`${Professional.user.firstName} ${Professional.user.lastName}` }
                    />                    
                </Col>
               

            </Row>
            <Row>
            <Col>
                    <CommentSection
                        profId = {Professional.professionalsId}
                        userid = {user.id}
                    />
                        
                </Col>
            </Row>
            
           
          </>
        }
  
        </>
    )
}
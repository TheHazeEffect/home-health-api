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
import './Professional.css'


export const Professional = ({match,user}) => {

    const [Professional,setProfessional] = useState(null)
    const [loading, setLoading] = useState(false)
    const [ShowMessage,setShowMessage] = useState(false)
    const [ShowAppoinment,setShowAppointment] = useState(false)

    


    useEffect (() => {

        const fetchData = async () => {
            setLoading(true);

            try {
                const id = match.params.id

                var result = await axios.get(`/api/Professionals/${id}/details`)
                
                setProfessional(result.data)
                console.log(result.data)

            }catch(error){
                console.log(error)
            }
            setLoading(false)
        }

        fetchData();
    },[match.params.id])


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
                                {`${Professional.city} ${Professional.state_parish} ${Professional.country}`}
                            </Card.Text>
                        </Card.Body>
                        <Card.Footer>
                            <Button 
                                onClick={()=> setShowAppointment(true)}
                                className="prof-button" 
                                variant="success" 
                                disabled={user.loggedin === false ? true :  false}>
                                    Make Appointment
                            </Button>{' '}
                            <Button
                                onClick={ () => setShowMessage(true)}
                                className="prof-button" 
                                disabled={user.loggedin === false ? true :  false}
                                variant="info">
                                    Send Message
                            </Button>{' '}
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
                                        Lorem ipsum dolor sit amet consectetur, adipisicing elit. Maiores, ad inventore ipsa odit ex dolorum fugiat saepe voluptate molestias magni nostrum. Architecto repellendus repellat optio ut minima officiis obcaecati doloribus?
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
                                                        {`${PS.service.serviceName} - $${PS.serviceCost}JMD`}
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
            
           
          </>
        }
  
        </>
    )
}
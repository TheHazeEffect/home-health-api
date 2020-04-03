import React, {useEffect,useState} from "react"
import Card from 'react-bootstrap/Card'
import { Link } from 'react-router-dom';
import axios from "axios";
import Tabs from 'react-bootstrap/Tabs'
import Tab from 'react-bootstrap/Tab'
import ListGroup from 'react-bootstrap/ListGroup'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import { LoadingSpinner } from "../../components/LoadingSpinner";

import './Professional.css'


export const Professional = ({match}) => {

    const [Professional,setProfessional] = useState(null)
    const [loading, setLoading] = useState(false)

    


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
                    </Card>
                </Col>
                <Col  >
                        
                        <Tabs variant="pills" defaultActiveKey="Details" id="uncontrolled-tab-example">
                        
                            <Tab  eventKey="Details" title="Professional Details">
                                <Card className="prof-col prof-details">                   
                                    <Card.Body>
                                   
                                        <Card.Text>
                                        Email : 
                                        </Card.Text>
                                        <Card.Subtitle>
                                            {Professional.user.email}
                                        </Card.Subtitle>
                                    <Card.Title>
                                        Contact: {Professional.user.phoneNumber}
                                    </Card.Title>
                                
                                    </Card.Body>
                                </Card>
                            </Tab>
                            <Tab eventKey="Services" title="Services" >
                                <Card className="prof-col prof-details">                   
                                    <Card.Body>
                                        <ListGroup variant="flush">
                                            <ListGroup.Item >Cras justo odio</ListGroup.Item>
                                            <ListGroup.Item >Dapibus ac facilisis in</ListGroup.Item>
                                            <ListGroup.Item >Morbi leo risus</ListGroup.Item>
                                            <ListGroup.Item >Porta ac consectetur ac</ListGroup.Item>
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
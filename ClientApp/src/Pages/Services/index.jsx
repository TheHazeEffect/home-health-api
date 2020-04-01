import React, {useEffect,useState} from "react"
import Card from 'react-bootstrap/Card'
import axios from "axios";
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import CardDeck from 'react-bootstrap/CardDeck'



export const ServicesPage = () => {

    const [services,setServices] = useState([])
    useEffect (() => {

        const fetchServices = async () => {
            var result = await axios.get("/api/service")

            setServices(result.data)
            console.log(result.data)
        }

        fetchServices();
    },[])

    return(
        <>
        <Row>
            <Col>
            <Card className="text-center">
                <Card.Header>Services</Card.Header>
                <Card.Body>
                    <Card.Title>Special title treatment</Card.Title>
                    <Card.Text>
                        Search through our array of medical professionals by the serivces the offer
                    </Card.Text>
                </Card.Body>
            </Card>
        </Col>
        </Row>
        <br />
        <br />
        {console.log(services)}
        <CardDeck>
        { [...Array(3)].map( (s,i) => (
        
        
           < Card key={i}>
            <Card.Img variant="top" src="https://images.pexels.com/photos/3845677/pexels-photo-3845677.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" />

            <Card.Body>
              <Card.Title>Dentist</Card.Title>
              
            </Card.Body>
            </Card>
            
            
          
        ))}
  
    </CardDeck>
        </>
    )
}
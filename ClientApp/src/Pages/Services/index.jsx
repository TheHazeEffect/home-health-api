import React, {useEffect,useState} from "react"
import Card from 'react-bootstrap/Card'
import axios from "axios";
import { Link } from 'react-router-dom';

import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import CardDeck from 'react-bootstrap/CardDeck'
import { LoadingSpinner } from "../../components/LoadingSpinner";

import './Services.css'


export const ServicesPage = () => {

    const [services,setServices] = useState([])
    const [loading, setLoading] = useState(false)
    useEffect (() => {


        const fetchServices = async () => {
            setLoading(true);

            try {

                var result = await axios.get("/api/service")
                
                setServices(result.data)
                console.log(result.data)

            }catch(error){
                console.log(error)
            }
            setLoading(false)
        }

        fetchServices();
    },[])

    const createLayout = (array) => {

        const ArrayLength = array.length-1;
        var layout = []

        const col =  (x) => (
        <Card className="" key={array[x].serviceId}>
        <Col>

                <Link to={`services/${array[x].serviceId}/Professionals`} >

                    <Card.Img variant="top" src="https://images.pexels.com/photos/3845677/pexels-photo-3845677.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" />

                </Link>
                <Card.Body>
                <Card.Title>{array[x].serviceName}</Card.Title>
                
                </Card.Body>
            </Col>
        </Card> )
        const Row =  (y) => (
            <React.Fragment key={`${new Date().getTime()} ${y}`}>
            <CardDeck > 
            
                { y   <= ArrayLength   ? col(y)   : false }         
                { y+1 <= ArrayLength   ? col(y+1) : false }        
                { y+2 <= ArrayLength   ? col(y+2) : false }   
            
            </CardDeck>
            <br />
            <br />
            </React.Fragment>
        
        );

        var i = 0;
        while(i <= ArrayLength){

            const row = Row(i)
        
            layout.push(row);
            i=i+3
        }

        return layout;

    }

    return(
        <>
        <Row>
            <Col>
            <Card className="text-center">
                <Card.Body>
                    <Card.Title>Services</Card.Title>
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
        {
        
            
          loading === true ? 
            <div className="centerloader">
                <LoadingSpinner Show={loading} /> <span>Loading . . .</span>
            </div>
            : createLayout(services)
          
        }
  
        </>
    )
}
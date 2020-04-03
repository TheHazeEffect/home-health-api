import React, {useEffect,useState} from "react"
import Card from 'react-bootstrap/Card'
import { Link } from 'react-router-dom';
import axios from "axios";
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import CardDeck from 'react-bootstrap/CardDeck'
import { LoadingSpinner } from "../../components/LoadingSpinner";

import './profForService.css'


export const ProfForService = ({match}) => {

    const [Professionals,setProfessionals] = useState([])
    const [loading, setLoading] = useState(false)
    const [Service, setService] = useState(false)

    


    useEffect (() => {

        const fetchData = async () => {
            setLoading(true);

            try {
                const id = match.params.id

                var result = await axios.get(`/api/service/${id}/Professionals`)
                var service = await axios.get(`/api/service/${id}`)
                
                setProfessionals(result.data)
                setService(service.data)
                console.log(result.data)
                console.log(service.data)

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

    const createLayout = (array) => {

        const ArrayLength = array.length-1;
        var layout = []

        const col =  (x) => <Card className="" key={array[x].professionalsId}>
            
            <Card.Title>{`${array[x].user.firstName} ${array[x].user.lastName}`}</Card.Title>
            <Link to={`/Professional/${array[x].professionalsId}/`} >
                <Card.Img variant="top" src={returnImgForGender(array[x].user.gender)} />
            </Link>

            <Card.Body>
    <Card.Text>{`${array[x].city} ${array[x].state_parish} ${array[x].country}`}</Card.Text>
            </Card.Body>
            </Card>
        const Row =  (y) => <React.Fragment key={`${new Date().getTime()} ${y}`}><CardDeck>
            {y<=ArrayLength? col(y) : false}        
            {y+1<=ArrayLength? col(y+1) : false}        
            {y+2<=ArrayLength? col(y+2) : false}          
            </CardDeck>
            <br/>
            <br/>
            </React.Fragment>

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
                <Card.Title>
                    <Link to='/services'>
                        <i className="fas fa-arrow-circle-left"></i> {' '}
                    </Link>
                    {Service.serviceName}
                </Card.Title>
                    <Card.Text>
                        leading professionals in {Service.serviceName}
                    </Card.Text>
                </Card.Body>
            </Card>
        </Col>
        </Row>
        <br />
        <br />
        {
        
            
          loading === true ? 
            <div className="centerloader">
                <LoadingSpinner Show={loading} /> <span>Loading . . .</span>
            </div>
            : createLayout(Professionals)
          
        }
  
        </>
    )
}
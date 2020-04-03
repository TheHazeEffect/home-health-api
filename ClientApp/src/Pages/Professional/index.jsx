import React, {useEffect,useState} from "react"
import Card from 'react-bootstrap/Card'
import { Link } from 'react-router-dom';
import axios from "axios";
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import CardDeck from 'react-bootstrap/CardDeck'
import { LoadingSpinner } from "../../components/LoadingSpinner";

// import './profForService.css'


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



    const goback = () => {
        this.match.history.goback();
    }



    return(
        <>
        {
            loading === true ? 
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
                        <Link to='/services'>
                            <i className="fas fa-arrow-circle-left"></i> {' '}
                        </Link>
                    </Card.Title>
                        <Card.Text>
                            {Professional !== null ? `${Professional.user.firstName} ${Professional.user.lastName}` : false }
                        </Card.Text>
                    </Card.Body>
                </Card>
            </Col>
            </Row>
            <br />
            <br />
            
           <div> professional page</div>
          </>
        }
  
        </>
    )
}
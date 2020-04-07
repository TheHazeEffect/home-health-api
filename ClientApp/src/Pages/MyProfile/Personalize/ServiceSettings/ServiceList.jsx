import React from "react";
import Card from 'react-bootstrap/Card'
import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../../../components/LoadingSpinner";

import "./ServiceList.css"

export const ServiceList = ({
    UserServices,
    Loading,
    handleDelete,
}) =>{


        return (
        <>
            
            {Loading === true ? 
                <div className="Ser-SpinnerStyle">
                    <LoadingSpinner Show={Loading}/> {"  "} {"Loading Services. . ."}
                </div> 
                    : 
                        UserServices.length < 1 ?<div className="Ser-SpinnerStyle"><h1>You Have No Services</h1> </div> :
                    <Card>
                        {
                                UserServices.map( (US,i) => (
                                    <React.Fragment key={i}>
                                       <Card.Header >
                                            {US.name}
                                        </Card.Header>
                                        <Card.Subtitle>       
                                            {US.cost}
                                        </Card.Subtitle> 
                                        <Card.Body>
                                            <Button variant="info">Edit</Button>{' '}
                                            <Button onClick={handleDelete.bind(this,US.id) } variant="danger">Delete</Button>{' '}
                                        </Card.Body>
                                    </React.Fragment>
                                )) 
                        }
                    </Card>
                
            }
        </>
    )
}


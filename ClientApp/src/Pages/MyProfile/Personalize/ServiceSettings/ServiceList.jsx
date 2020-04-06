import React,{useState,useEffect} from "react";
import Table from 'react-bootstrap/Table'
import Button from 'react-bootstrap/Button'
import axios from "axios";
import {connect} from "react-redux";
import { LoadingSpinner } from "../../../../components/LoadingSpinner";

import "./ServiceList.css"

const ServiceList = ({user}) =>{


    const [UserServices,setUserServices] = useState([])
    const [Loading,setLoading] = useState(false)

    useEffect( () => {
        
        const fetchData = async () => {
            try {

                setLoading(true)
                var result = await axios
                    .get(`/api/professional_Service/profile/${user.id}`)

                if(result.status === 200) {
                    setUserServices(result.data);
                }
                
    
                setLoading(false)
            }catch(ex){
                setLoading(false)
                console.log(ex)
            }
        }

        fetchData()

    },[user.id])


    return (
        <>
            {Loading === true ? 
                <div className="Ser-SpinnerStyle">
                    <LoadingSpinner Show={Loading}/> {"  "} {"Loading Services. . ."}
                </div> 
                    :
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Service</th>
                                <th>Fee</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                        {
                                UserServices.map( (US,i) => (
                                    <tr key={i}>
                                        <td>{US.Name}</td>
                                        <td> {US.Cost}</td>
                                        <td>  
                                            <Button variant="info">Edit</Button>{' '}
                                            <Button variant="danger">Delete</Button>{' '}
                                        </td>
                                    </tr>
                                
                                )) 
                        }
            
                    </tbody>
                </Table>
                
            }
        </>
    )
}

const mapStateToprops = state => {
    return {
      user : state.user
    }
  };

  export default connect(mapStateToprops,null)(ServiceList)
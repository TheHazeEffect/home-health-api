
import React, {useState,useEffect} from "react";
import  {ServiceList}  from "./ServiceList";
import   {AddServiceForm}  from "./AddServiceForm";
import {  connect } from "react-redux";
import axios from 'axios'
import Button from 'react-bootstrap/Button'

const ServiceSettings  = ({user}) => {

    const initailAddServiceState = {
        ServiceId : 0,
        ServiceCost : 0.00,
        id : user.id
    }
    
    const AddServiceEndpoint = `/api/professional_Service/profile/`
    


    const [Services,setServices] = useState([])
    const [PayLoad,setPayLoad] = useState({...initailAddServiceState})
    const [Loading,setLoading] = useState(false)

    const [ShowAdd,setShowAdd] = useState(false)

    const [UserServices,setUserServices] = useState([])

    
    useEffect( () => {
        
        const fetchData = async () => {
            try {


                setLoading(true)
                var result = await axios
                    .get(`/api/professional_Service/profile/${user.id}`)

                var result2 = await axios.get("/api/service")

                if(result2.status === 200) {
                    
                    setServices(result2.data)
                }


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
 

    const handleChange = (event) => {
        const {name,value} = event.target

        setPayLoad({...PayLoad,[name]:value})
    }

    const handleDelete = async (id) => {


        try {
            
            setLoading(true)
            var result = await axios.delete(`api/professional_Service/${id}`)

            if(result.status === 200 || result.status === 404) {
                var list = UserServices
                setUserServices(list.filter( (S) => S.id !== id ))

            }


            setLoading(false)
        }catch(ex){

            setLoading(false)
            console.log(ex)
        }


    }

    const handleSubmit = async (event) => {
        event.preventDefault()

        try {
            setLoading(true);


            console.log("-------------------------------PayLoad")
            console.log(PayLoad)
            console.log("-------------------------------PayLoad")
            var result = await axios.post(AddServiceEndpoint,PayLoad)


            if(result.status === 200) {

                var newServiceList = UserServices
                newServiceList.push(result.data)
                setUserServices(newServiceList)
            }
            console.log(result)


            setLoading(false)
        }catch(ex) {
            setLoading(false)
            console.log(ex)

        }
    }
    
    return (

        <>
             <AddServiceForm 
                Loading={Loading}
                Services={Services}
                show={ShowAdd}
                setShow={setShowAdd}
                handleChange={handleChange}
                handleSubmit={handleSubmit}
            />
            <Button variant="success" className="addButton" onClick={() => setShowAdd(true)}>
                Add A Service
            </Button>
            <ServiceList
                UserServices={UserServices}
                Loading={Loading}
                handleDelete={handleDelete}
            />
        </>



    )
}

const mapStateToprops = state => {
    return {
      user : state.user
    }
  };

export default connect(mapStateToprops,null)(ServiceSettings)
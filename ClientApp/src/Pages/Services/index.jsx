import React, {useEffect,useState} from "react"
import axios from "axios";






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
            <h1>Services</h1>
        </>
    )
}
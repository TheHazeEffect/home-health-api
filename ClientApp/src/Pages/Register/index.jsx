import React ,{useState, useEffect} from 'react'
import {RegisterForm} from "./RegisterForm";
import { AlertComp } from "../../components/AlertComp";
import {axios} from 'axios'



export const RegisterPage = () => {
    
   

    const initialRegisterState = {
        Email : "",
        FirstName : "",
        LastName : "",
        Password : "",
        RoleName : ""
    }
    
    const [RegisterObj,setRegisterObj] = useState({...initialRegisterState })
    const [errorObj,SetErrorObj] = useState({...initialRegisterState})
    
    
    const handleChange = (event) => {

        const {name,value} = event.target;
        let errors = errorObj

        switch(name) {
            case "Email" :
            case "Password" :
            case "FirstName":
            case "LastName":
            case "RoleName":
                errors[name] = value.length > 0 ? "" : `must enter value for ${name} field`
                break;
                default:
                console.log("Unidentified field");
                break;
        }
    
        SetErrorObj({...errorObj,[name]:errors[name]})

        setRegisterObj({...RegisterObj,[name]:value})

    }
    
    const handleSubmit = async (event) => {
        event.preventDefault();
        
        var result = await axios
        .post("/auth/signup",RegisterObj)

        
        console.log(result)

    }
    
    useEffect( () => {
        console.table(RegisterObj)
        // console.table(errorObj)

    },[RegisterObj])

    return(
        <>
           
            <RegisterForm
                handleChange={handleChange}
                handleSubmit={handleSubmit}
            />
        </>
    );
}
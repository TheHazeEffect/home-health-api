import React ,{useState, useEffect} from 'react'
import {RegisterForm} from "./RegisterForm";
import { AlertComp } from "../../components/AlertComp";
import axios from 'axios'



export const RegisterPage = () => {

    const AlertFactory = (Show,Variant,Heading,Content) => (
        {
            Variant: Variant,
            Heading: Heading,
            Content: Content
        }

    )  
   

    const initialRegisterState = {
        Email : "",
        FirstName : "",
        LastName : "",
        Password : "",
        RoleName : ""
    }

    const initialAlertObj = {
        Variant : "",
        Heading : "",
        content : ""
    }
    
    const [RegisterObj,setRegisterObj] = useState({...initialRegisterState })
    const [errorObj,SetErrorObj] = useState({...initialRegisterState})

    const [AlertProps,setAlertProps] = useState({...initialAlertObj})
    const [ShowAlert,setShowAlert] = useState(false)
    const [Loading,setLoading] = useState(false)

    
    
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
        
        try {


            setLoading(true)            
            var result = await axios
            .post("/auth/signup",RegisterObj)
            setLoading(false)

        
            const AlertObj = result.status === 200 ?
            AlertFactory(true,"sucess","Login Success","you Have Sucessfully LoggedIn") 
            :
            AlertFactory(true,"danger","Login Attempt Failed","Incorrect User Credentials")
            
            setShowAlert(true)
            setAlertProps(AlertObj)
            
            console.log(result)
        }catch {

            setLoading(false)
            const AlertObj = AlertFactory(true,"danger","Login Attempt Failed","Incorrect User Credentials")
            
            setShowAlert(true)
            setAlertProps(AlertObj)            
        }
        
    }
    
    useEffect( () => {
        console.table(RegisterObj)
        // console.table(errorObj)

    },[RegisterObj])

    return(
        <>

            <AlertComp
                {...AlertProps}
                Show={ShowAlert}
                setShow={setShowAlert}
            />     
           
            <RegisterForm
                Show={Loading}
                handleChange={handleChange}
                handleSubmit={handleSubmit}
            />
        </>
    );
}
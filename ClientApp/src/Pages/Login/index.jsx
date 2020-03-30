import React, {useState,useEffect} from 'react'
import { LoginForm } from "./LoginForm";
import axios from 'axios'
import { AlertComp } from "../../components/AlertComp";
import { LoadingSpinner } from "../../components/LoadingSpinner";




export const LoginPage = () => {
    // static displayName = Login.name;

    const AlertFactory = (Show,Variant,Heading,Content) => (
        {
            Variant: Variant,
            Heading: Heading,
            Content: Content
        }

    )
   

    const initialLoginObj = {
        Email : "",
        Passowrd : ""
    } 

    const initialAlertObj = {
        Variant : "",
        Heading : "",
        content : ""
    }
  
    const [LoginObj,setLoginObj] = useState({...initialLoginObj })
    const [errorObj,SetErrorObj] = useState({...initialLoginObj})
  
    const [AlertProps,setAlertProps] = useState({...initialAlertObj})
    const [ShowAlert,setShowAlert] = useState(false)
    const [Loading,setLoading] = useState(false)
      
      useEffect( () => {
        console.table(LoginObj)
        console.table(errorObj)
  
      },[LoginObj,errorObj])
  
      const handleSubmit = async event => {
        event.preventDefault();
  
        try{

            setLoading(true)
            var result = await axios
            .post("/auth/login",LoginObj)
            setLoading(false)
            
            
            const AlertObj = result.status === 200 ?
            AlertFactory(true,"sucess","Login Success","you Have Sucessfully LoggedIn") 
            :
            AlertFactory(true,"danger","Login Attempt Failed","Incorrect User Credentials")
            
            setShowAlert(true)
            setAlertProps(AlertObj)
            
            console.log(result)
        }catch{


            setLoading(false)
            const AlertObj = AlertFactory(true,"danger","Login Attempt Failed","Incorrect User Credentials")
            
            setShowAlert(true)
            setAlertProps(AlertObj)
        }
    }
  
    const handleChange = (event) => {
      const {name,value} = event.target;
      let errors = errorObj
  
      switch(name) {
  
        case "Email" :
        case "Password" :
            errors[name] =  value.length > 0 ? "" : `must enter value for ${name} field`
                break;
            default:
                console.log("unidentified field")
                break;

        }
  
        SetErrorObj(errors)
       
        setLoginObj({...LoginObj, [name]:value})
    }
  
  
    return(
        <>
            <LoadingSpinner 
                Show={Loading}
            />
            <AlertComp
                {...AlertProps}
                Show={ShowAlert}
                setShow={setShowAlert}
            />     
                <LoginForm
                handleChange={handleChange}
                handleSubmit={handleSubmit}
                />
        </>
    );
}
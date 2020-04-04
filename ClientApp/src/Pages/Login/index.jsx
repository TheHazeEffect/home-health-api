import React, {useState,useEffect} from 'react'
import { LoginForm } from "./LoginForm";
import axios from 'axios'
import { AlertComp } from "../../components/AlertComp";

import { AlertFactory } from "../../Factory/Alertfactory";



export const LoginPage = ({LoginUser}) => {
    // static displayName = Login.name;

   

    const initialLoginObj = {
        Email : "",
        Passowrd : ""
    } 

    const initialAlertObj =  AlertFactory("","","")

  
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
            
            
            
            const AlertObj = result.status === 200 
            ?
            () =>{
                const {email,token,roleName,firstName} = result.data
                LoginUser(email,firstName,roleName,token);
                return AlertFactory("sucess","Login Success","you Have Sucessfully LoggedIn") 
            }
            :
            AlertFactory("danger","Login Attempt Failed","Incorrect User Credentials")
            
            setLoading(false)
            setShowAlert(true)
            setAlertProps(AlertObj)
            
            console.log(result)
        }catch{


            setLoading(false)
            const AlertObj = AlertFactory("danger","Login Attempt Failed","Incorrect User Credentials")
            
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
            <AlertComp
                {...AlertProps}
                Show={ShowAlert}
                setShow={setShowAlert}
            />     
                <LoginForm
                Show={Loading}
                handleChange={handleChange}
                handleSubmit={handleSubmit}
                />
        </>
    );
}
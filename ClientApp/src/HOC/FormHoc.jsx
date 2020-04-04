import React, {useState,useEffect} from 'react'
import axios from 'axios'

import { AlertComp } from "../components/AlertComp";


import {  AlertFactory } from "../Factory/Alertfactory";


export const FormHoc = ({
endpoint,
Component,
initialPayLoadObj

}) => {

    const initialAlertObj =  AlertFactory("","","")

    const [PayloadObj,setPayloadObj] = useState(...initialPayLoadObj)
    const [ErrorObj,setErrorObj] = useState(...initialPayLoadObj)

    const [AlertProps,setAlertProps] = useState({...initialAlertObj})
    const [ShowAlert,setShowAlert] = useState(false)
    const [Loading,setLoading] = useState(false)


    const handleSubmit = async event =>{
        event.preventDefault()
        try {
            setLoading(true)

            var result = await axios
                .post(endpoint,PayloadObj)

            const AlertObj = result.status === 200 
            ?
            AlertFactory("sucess","Success!",result.data.message) 
            :
            AlertFactory("danger","Oops!",result.data.message)


            setLoading(true)
            setShowAlert(true)
            setAlertProps(AlertObj)

            console.log(result)



        }catch(ex) {

            setLoading(false)
            const AlertObj = AlertFactory("danger","Oops!","Something went wrong")
            
            setShowAlert(true)
            setAlertProps(AlertObj)
        }
    }
    
    

    const handleChange = (event) => {
        const [name,value] = event.target
        let errors = ErrorObj
        switch(name) {

            case name:
                errors[name] = value.length > 0 ? "" : `Must enter a value for ${name}`
                break;
            default :
                console.log("How did it get here?")
                break;
            
        }

        setErrorObj(errors)
        setPayloadObj([...PayloadObj, [name]:value])

    }
    
    const CustomAlertComp =  <AlertComp
                        {...AlertProps}
                        Show={ShowAlert}
                        setShow={setShowAlert}
                    />  

    const CustomLoadingSpinner =  <LoadingSpinner 
                Show={Loading}
            />

    return (
        <Component 
           { ...otherprops}
            handleChange={handleChange}
            handleSubmit={handleSubmit}
            AlertComp={CustomAlertComp}
            LoadingSpinner={CustomLoadingSpinner}
        />
    );
}
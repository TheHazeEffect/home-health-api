import React, {useState} from 'react'
import axios from 'axios'
import { AlertComp } from "../components/AlertComp";


import {  AlertFactory } from "../Factory/Alertfactory";


export const FormHoc = (
endpoint,
Component,
initialPayLoadObj,
otherprops

) => {

    const initialAlertObj =  AlertFactory("","","")

    const [PayloadObj,setPayloadObj] = useState({...initialPayLoadObj})
    const [ErrorObj,setErrorObj] = useState({...initialPayLoadObj})

    const [AlertProps,setAlertProps] = useState({...initialAlertObj})
    const [ShowAlert,setShowAlert] = useState(false)
    const [Loading,setLoading] = useState(false)


    const handleSubmit = async event =>{
        event.preventDefault()
        try {
            setLoading(true)

            console.log("Payload-------------------")
            console.log(PayloadObj)
            console.log("Payload-------------------")
            var result = await axios
                .post(endpoint,PayloadObj)

            const AlertObj = result.status === 200 
            ?
            AlertFactory("sucess","Success!",result.data.message) 
            :
            AlertFactory("danger","Oops!",result.data.message)


            setLoading(false)
            setShowAlert(true)
            setAlertProps(AlertObj)

            console.log("Response-------------------")

            console.log(result)
            console.log("Response-------------------")



        }catch(ex) {

            console.log(ex)
            setLoading(false)
            const AlertObj = AlertFactory("danger","Oops!","Something went wrong")
            
            setShowAlert(true)
            setAlertProps(AlertObj)
        }
    }
    
    

    const handleChange = (event) => {
        const {name,value} = event.target
        let errors = ErrorObj
        switch(name) {

            case name:
                errors[name] = value.length > 0 ? "" : `Must enter a value for ${name}`
                break;
            default :
                console.log("How did it get here?")
                break;
            
        }

        console.table(PayloadObj)
        setErrorObj(errors)
        setPayloadObj({...PayloadObj, [name]:value})

    }
    
    const CustomAlertComp =  <AlertComp
                        {...AlertProps}
                        Show={ShowAlert}
                        setShow={setShowAlert}
                    />  


    return (
        <Component 
            {...otherprops}
            PayloadObj={PayloadObj}
            Loading={Loading}
            handleChange={handleChange}
            handleSubmit={handleSubmit}
            ErrorObj={ErrorObj}
            AlertComp={CustomAlertComp}
        />
    );
}
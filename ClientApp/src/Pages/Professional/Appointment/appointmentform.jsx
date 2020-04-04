import React from "react";
import { FormInput } from "../../../components/Forms/FormInput";
import { FormTextArea } from "../../../components/Forms/FormTextArea";
import Form from 'react-bootstrap/Form'


export const Appointmentform = ({
  handleChange,
  AlertComp
}) => {
    return (
  
          <>

            {AlertComp}
            <Form className="bottompadding">
              <FormInput
                fieldName="AppDate"
                FieldLabel = "Date"
                placeholder = ""
                fieldType = "date"
                onchange = {handleChange}
              />
              

              <FormInput
                fieldName="AppTime"
                FieldLabel = "Time"
                placeholder = ""
                fieldType = "time"
                onchange = {handleChange}
              />

              <FormTextArea
                fieldName="AppReason"
                FieldLabel="Reason"
                placeholder = "Reason for making an Appointment"
                onchange={handleChange}

              />
              
            
            </Form>
       </>
    );
} 
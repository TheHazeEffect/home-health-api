import React from "react";
import { FormInput } from "../../../components/Forms/FormInput";
import { FormTextArea } from "../../../components/Forms/FormTextArea";
import Form from 'react-bootstrap/Form'



export const MessageForm = ({
    handleChange,
    AlertComp

}) => {
    return (
        <>
            {AlertComp}
            <Form className="bottompadding">
              
              <FormTextArea
                fieldName="Content"
                FieldLabel="Message"
                placeholder = ""
                onchange={handleChange}

              />
              
            
            </Form>



        </>
    );
}
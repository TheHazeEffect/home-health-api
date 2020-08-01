import React from "react";
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import {FormInput  } from "../../../../components/Forms/FormInput";
import { LoadingSpinner } from "../../../../components/LoadingSpinner";

import './UserSettings.css'


export const UserSettingsForm =({
  Loading,
  handleChange,
  handleSubmit,
  AlertComp
  
}) => {
    return (
        <>
        <Form>
              {AlertComp}
                <FormInput
                  fieldName="FirstName"
                  FieldLabel = "First name"
                  placeholder = ""
                  fieldType = "text"
                  onchange = {handleChange}
                />
  
                <FormInput
                  fieldName="LastName"
                  FieldLabel = "Last Name"
                  placeholder = ""
                  fieldType = "text"
                  onchange = {handleChange}
                />  
                         
                <FormInput
                  fieldName="Gender"
                  FieldLabel = "Gender"
                  placeholder = ""
                  fieldType = "text"
                  onchange = {handleChange}
                />           
              
                <FormInput
                    fieldName="Dob"
                    FieldLabel = "Dob"
                    placeholder = ""
                    fieldType = "date"
                    onchange = {handleChange}
                  />     

                  {
                    Loading === true ?
                    <Button 
                      variant="primary" 
                      type="submit"
                      onClick={ (e)=> e.preventDefault()}
                            >
                        <LoadingSpinner 
                          Show={Loading}
                        />
                      </Button>
                    :
                    <Button 
                      variant="primary" 
                      type="submit"
                      onClick={handleSubmit}
                      >
                        Update
                    </Button>
                }
              </Form>
        </>
    );
}
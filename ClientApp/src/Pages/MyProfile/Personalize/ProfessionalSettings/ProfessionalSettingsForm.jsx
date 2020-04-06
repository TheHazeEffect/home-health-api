
import React from "react";
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'

import { FormTextArea } from "../../../../components/Forms/FormTextArea";
import { LoadingSpinner } from "../../../../components/LoadingSpinner";

import './ProfessionalSettimgs.css'

export const ProfessionalSettingsForm = ({

    Loading,
    handleChange,
    handleSubmit,
    AlertComp
}) => {
    return (
        <>
            <Form className="ProfSettings">
                {AlertComp}
                <FormTextArea
                    fieldName="Biography"
                    FieldLabel="Biography"
                    placeholder = "Impress your prospective Clients"
                    onchange={handleChange}
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
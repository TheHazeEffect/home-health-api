import React from "react";
import Modal from 'react-bootstrap/Modal'
import { FormInput } from "../../components/Forms/FormInput";
import { FormTextArea } from "../../components/Forms/FormTextArea";
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../components/LoadingSpinner";

export const Appointmentform = ({
    profId,
    services,
    show,
    setShow,
    handleChange,
    handleSubmit
}) => {


    return (
        <Modal show={show} >
        <Modal.Dialog>
            <Modal.Header >
                <Modal.Title> Make an Appointment</Modal.Title>
            </Modal.Header>

            <Modal.Body>
            <Form className="bottompadding">
            
              <FormInput
                fieldName="Date"
                FieldLabel = "Date"
                placeholder = ""
                fieldType = "date"
                onchange = {handleChange}
              />
              

              <FormInput
                fieldName="time"
                FieldLabel = "Time"
                placeholder = ""
                fieldType = "time"
                onchange = {handleChange}
              />
  
            
            
                {
                  show === true ?
                  <Button 
                    variant="primary" 
                    type="submit"
                    onClick={ (e)=> e.preventDefault()}
                          >
                      <LoadingSpinner 
                        Show={show}
                      />
                    </Button>
                  :
                  <Button 
                    variant="primary" 
                    type="submit"
                    onClick={handleSubmit}>
                      LogIn
                  </Button>
                }
                {` `}
                  <Button variant="danger" onClick={() =>setShow(false)}>Cancel</Button> 

            </Form>
        </Modal.Body>

    </Modal.Dialog>
    </Modal>

    );
}
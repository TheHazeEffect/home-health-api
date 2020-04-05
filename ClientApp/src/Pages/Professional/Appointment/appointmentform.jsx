import React from "react";

import Modal from 'react-bootstrap/Modal'
import { FormInput } from "../../../components/Forms/FormInput";
import { FormTextArea } from "../../../components/Forms/FormTextArea";
import Form from 'react-bootstrap/Form'

import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../../components/LoadingSpinner";

export const Appointmentform = ({
  handleSubmit,
  Loading,
  AlertComp,
  handleChange,
  show,
  setShow
}) => {
    return (
  
        <>
          <Modal show={show} onHide={() => setShow(false)} >
              <Modal.Dialog>
                <Modal.Header >
                    <Modal.Title> Make an Appointment</Modal.Title>
                </Modal.Header>

               <Modal.Body>

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

        </Modal.Body>
              <Modal.Footer>
              {
                  Loading === true ?
                  <Button 
                    className="prof-button"
                    variant="primary" 
                    type="submit"
                    onClick={ (e)=> e.preventDefault()} >
                      <LoadingSpinner 
                        Show={Loading}
                      />
                    </Button>
                  :
                  <Button 
                    className="prof-button"
                    variant="primary" 
                    type="submit"
                    onClick={handleSubmit}>
                      Submit Appointment
                  </Button>
                }
                  <Button 
                    className="prof-button"
                    variant="danger" 
                    onClick={() => setShow(false)}>Cancel
                </Button>
                      
              </Modal.Footer>
            </Modal.Dialog>
          </Modal>
            
       </>
    );
} 
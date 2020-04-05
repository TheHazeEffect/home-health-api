import React from "react";
import { FormTextArea } from "../../../components/Forms/FormTextArea";
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../../components/LoadingSpinner";
import Modal from 'react-bootstrap/Modal'





export const MessageForm = ({
  handleSubmit,
  Loading,
  AlertComp,
  handleChange,
  show,
  setShow
}) => {
    return (
        <>
             <Modal show={show} onHide={() => setShow(false)}>
              {AlertComp}
              <Modal.Dialog>
                <Modal.Header >
                    <Modal.Title> Send A Message</Modal.Title>
                </Modal.Header>

               <Modal.Body>  
                <Form className="bottompadding">
                  
                  <FormTextArea
                    fieldName="Content"
                    FieldLabel="Message"
                    placeholder = ""
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
                      onClick={ (e)=> e.preventDefault()}
                            >
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
                        Submit Message
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
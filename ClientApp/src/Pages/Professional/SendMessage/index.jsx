import React from 'react'
import Modal from 'react-bootstrap/Modal'
import { MessageForm } from "./MessageForm";
import { FormHoc } from "../../../HOC/FormHoc";

import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../../components/LoadingSpinner";


export const SendMessage = ({
    show,
    setShow,
    profId,
    PatientId
}) => {

    const endpoint = "/api/Messages"
    const initailMessageObj = {

        SenderId : PatientId,
        RecieverId : profId,
        Content : ""
    }

    const formComp = ({handleSubmit, Loading}) => {

        return <>
            
        <MessageForm/>
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


        </>
    }

    
    return (
        <>
           <Modal show={show} >
              <Modal.Dialog>
                <Modal.Header >
                    <Modal.Title> Send A Message</Modal.Title>
                </Modal.Header>

               <Modal.Body>

              {

                FormHoc(endpoint,formComp,initailMessageObj
                )
              }

              </Modal.Body>
              <Modal.Footer>
                
              
              </Modal.Footer>
            </Modal.Dialog>
          </Modal>

        </>

    );

    


}
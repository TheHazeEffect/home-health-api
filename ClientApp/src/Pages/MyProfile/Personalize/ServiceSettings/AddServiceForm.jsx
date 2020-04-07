import React from "react";
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import Modal from 'react-bootstrap/Modal'


import { FormInput } from "../../../../components/Forms/FormInput";

import {LoadingSpinner  } from "../../../../components/LoadingSpinner";


export const AddServiceForm = ({
    Loading,
    handleChange,
    
    handleSubmit,
    Services,
    show,
    setShow
}) => {


 return (
        <>

        <Modal show={show} onHide={() => setShow(false)} >
            {/* {AlertComp} */}
            <Modal.Dialog>
                <Modal.Header >
                    <Modal.Title> Add Service</Modal.Title>
              </Modal.Header>

                <Modal.Body>
                    <Form className="bottompadding">

                        <Form.Group controlId="exampleForm.SelectCustom">
                            <Form.Label>Select A Service </Form.Label>
                                <Form.Control defaultvalue={0} name="ServiceId" as="select" custom onChange={handleChange}>
                                        <option  value={0}>
                                            Selecto A service
                                        </option>
                                    {Services.map( (S,I) => (
                                        <option key={I} value={S.serviceId}>
                                            {S.serviceName}
                                        </option>
                                    ))}
                                </Form.Control>
                        </Form.Group>
                        <FormInput
                            fieldName="ServiceCost"
                            FieldLabel = "Service Fee"
                            placeholder = ""
                            fieldType = "number"
                            onchange = {handleChange}
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
                        Add Service
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


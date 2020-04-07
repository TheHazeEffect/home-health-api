import React from "react";
import { PaymentInputsWrapper, usePaymentInputs } from 'react-payment-inputs';
import images from 'react-payment-inputs/images';

import Modal from 'react-bootstrap/Modal'
import { FormInput } from "../../../components/Forms/FormInput";
import { FormTextArea } from "../../../components/Forms/FormTextArea";
import { FormCheckbox } from "../../../components/Forms/FormCheckbox";
import Form from 'react-bootstrap/Form'
import AlgoliaPlaces from 'algolia-places-react';

import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../../components/LoadingSpinner";

export const Appointmentform = ({
  handleSubmit,
  Loading,
  AlertComp,
  handleChange,
  show,
  setShow,
  Services
}) => {

  const {
    wrapperProps,
    getCardImageProps,
    getCardNumberProps,
    getExpiryDateProps,
    getCVCProps
  } = usePaymentInputs();

    return (
      
  
        <>
          <Modal show={show} onHide={() => setShow(false)} >
          {AlertComp}
            <Modal.Dialog>
              <Modal.Header >
                <Modal.Title> Make an Appointment</Modal.Title>
              </Modal.Header>

              <Modal.Body>
                
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

                {
                  Services.map( (S,i) => (
                    <FormCheckbox
                    key={i}
                    FieldLabel={`${S.service.serviceName} - $${S.serviceCost.toFixed(2)}`}
                    fieldName={"ServiceList"}
                    value={S.professional_ServiceId}
                    onchange={handleChange}
                    />
                ))}

                  <hr/>

                  <PaymentInputsWrapper {...wrapperProps}>
                        <svg {...getCardImageProps({ images })} />
                        <input {...getCardNumberProps()} />
                        <input {...getExpiryDateProps()} />
                        <input {...getCVCProps()} />
                    </PaymentInputsWrapper>

                  <FormInput
                    fieldName="Address1"
                    FieldLabel = "Street Address"
                    placeholder = ""
                    fieldType = "text"
                    // onchange = {handleChange}
                  />
                  <FormInput
                    fieldName="Address2"
                    FieldLabel = "Street Address 2"
                    placeholder = ""
                    fieldType = "text"
                    // onchange = {handleChange}
                  />
                

                   <AlgoliaPlaces
                    placeholder='Country'
                
                    options={{
                        appId: 'pl6KKCA8IQ7Q',
                        apiKey: '7e8742de8a04a136cd660e00fc9d7a02',
                        language: 'en',
                        // countries: ['JM'],
                        type: 'country',
                        // Other options from https://community.algolia.com/places/documentation.html#options
                    }}
                    // onChange={handleChange}
                    onError={({ message }) => 
                        console.log('Fired when we could not make the request to Algolia Places servers for any reason but reaching your rate limit.')}
                    />
                    <br/>
                    <br/>
                    <br/>
                    
                   <AlgoliaPlaces
                    placeholder='City'
                
                    options={{
                        appId: 'pl6KKCA8IQ7Q',
                        apiKey: '7e8742de8a04a136cd660e00fc9d7a02',
                        language: 'en',
                        // countries: ['JM'],
                        type: 'City',
                        // Other options from https://community.algolia.com/places/documentation.html#options
                    }}
                    // onChange={handleChange}
                    onError={({ message }) => 
                        console.log('Fired when we could not make the request to Algolia Places servers for any reason but reaching your rate limit.')}
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
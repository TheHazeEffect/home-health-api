
import React from "react";
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import { FormTextArea } from "../../../../components/Forms/FormTextArea";
import { LoadingSpinner } from "../../../../components/LoadingSpinner";
import AlgoliaPlaces from 'algolia-places-react';

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

                <AlgoliaPlaces
                    placeholder='Write an address here'
                
                    options={{
                        appId: 'pl6KKCA8IQ7Q',
                        apiKey: '7e8742de8a04a136cd660e00fc9d7a02',
                        language: 'en',
                        countries: ['JM'],
                        type: 'address',
                        // Other options from https://community.algolia.com/places/documentation.html#options
                    }}
                    onChange={handleChange}
                    onError={({ message }) => 
                        console.log('Fired when we could not make the request to Algolia Places servers for any reason but reaching your rate limit.')}
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
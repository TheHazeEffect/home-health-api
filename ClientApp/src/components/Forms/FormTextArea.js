import React from 'react';
import Form from 'react-bootstrap/Form'


export const FormTextArea = ({
    FieldLabel,
    onchange,
    fieldName,
    fieldType,
    placeholder,
    value
}) => {

    return (
        <>
            <Form.Group >
                <Form.Label>{FieldLabel}</Form.Label>
                <Form.Control 
                    as="textarea" rows="3"
                    name={fieldName}
                    // type={fieldType} 
                    placeholder={placeholder}
                    onChange={onchange}
                />
            </Form.Group>
        </>
    );
}
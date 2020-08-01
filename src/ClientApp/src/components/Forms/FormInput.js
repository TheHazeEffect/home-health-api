import React from 'react';
import Form from 'react-bootstrap/Form'


export const FormInput = ({
    FieldLabel,
    onchange,
    fieldName,
    fieldType,
    placeholder
}) => {

    return (
        <>
            <Form.Group >
                <Form.Label>{FieldLabel}</Form.Label>
                <Form.Control
                    name={fieldName}
                    type={fieldType} 
                    placeholder={placeholder}
                    onChange={onchange}
                />
            </Form.Group>
        </>
    );
}
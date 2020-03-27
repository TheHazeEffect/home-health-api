import React, {useState, useEffect} from 'react';
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
            <Form.Label>{FieldLabel}</Form.Label>
            <Form.Control
                name={fieldName}
                type={fieldType} 
                placeholder={placeholder}
                onChange={onchange}
            />
        </>
    );
}
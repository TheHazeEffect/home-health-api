import React from 'react';
import Form from 'react-bootstrap/Form'


export const FormCheckbox = ({
    FieldLabel,
    onchange,
    fieldName,
    value
}) => {

    return (
        <>
            <Form.Group controlId="formBasicCheckbox">
                <Form.Check
                    label={FieldLabel}
                    name={fieldName}
                    value={value}
                    type={"checkbox"} 
                    onChange={onchange}
                />
            </Form.Group>
        </>
    );
}


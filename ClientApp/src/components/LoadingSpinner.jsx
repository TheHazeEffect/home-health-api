import React from "react";
import Spinner from 'react-bootstrap/Spinner'
export const LoadingSpinner = ({
Show
}) => {
    return (
        <>
            {
                Show === true ? 
                    <Spinner className="spinner" animation="border" variant="primary" />
                :
                    false
            }
                
        </>
    )
}
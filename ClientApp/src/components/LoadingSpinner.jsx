import React from "react";
import Spinner from 'react-bootstrap/Spinner'

export const LoadingSpinner = ({
Show,
SetShow
}) => {
    return (
        <>
            {
                Show === true ? 
                    <Spinner animation="border" variant="primary" />
                :
                    false
            }
                
        </>
    )
}
import React from "react"
import Alert from 'react-bootstrap/Alert'


export const AlertComp = ({
Show,
Variant,
Heading,
Content,
setShow
}) => {
    return (
        <>
        {
            Show === true ?

                <Alert variant={Variant} onClose={() => setShow(false)}  dismissible>
                    <Alert.Heading>{Heading}</Alert.Heading>
                    <p>
                        {Content}
                    </p>
                </Alert>
            :
            false
        } 
        </>
    );
}


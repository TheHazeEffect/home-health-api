import React from "react";
import OverlayTrigger from 'react-bootstrap/OverlayTrigger'
import Tooltip from 'react-bootstrap/Tooltip'
import Button from 'react-bootstrap/Button'

export const DisabledOverlayButton = ({variant,Text}) => (

    <OverlayTrigger overlay={<Tooltip id="tooltip-disabled">Login in to use this feature</Tooltip>}>
    <span className="prof-button">
        <Button 
            className="prof-button"
            variant={variant}
            disabled 
            style={{ pointerEvents: 'none' }}>
        {Text}
        </Button>
    </span>
    </OverlayTrigger>
)

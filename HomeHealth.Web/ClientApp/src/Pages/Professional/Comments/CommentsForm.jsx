import React from "react";
import { FormInput } from "../../../components/Forms/FormInput";
import Button from 'react-bootstrap/Button'
import { LoadingSpinner } from "../../../components/LoadingSpinner";




export const CommentForm = ({
handleSubmit,
Loading,
AlertComp,
handleChange,
}) => {
    return (
        <>
            {AlertComp}
            <FormInput
                    fieldName="Content"
                    FieldLabel = "Leave a message"
                    placeholder = "Type a Comment"
                    fieldType = "text"
                    onchange = {handleChange}
            />
            {
                  Loading === true ?
                  <Button 
                    className="Comment-button"
                    variant="primary" 
                    type="submit"
                    onClick={ (e)=> e.preventDefault()} >
                      <LoadingSpinner 
                        Show={Loading}
                      />
                    </Button>
                  :
                  <Button 
                    className="Comment-button"
                    variant="primary" 
                    type="submit"
                    onClick={handleSubmit}>
                      Add Comment
                  </Button>
                }
        </>
    )
}
import React from 'react';
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import{FormInput} from '../../components/Forms/FormInput'
import Card from 'react-bootstrap/Card'
import { LoadingSpinner } from "../../components/LoadingSpinner";

import './LoginForm.css'


export const LoginForm = ({

  Show,
  handleChange,
  handleSubmit
}) => {
      return (
      <>
        <Card className="loginform">
            
          <h1>Log In</h1>
          <Form className="bottompadding">
            
              <FormInput
                fieldName="Email"
                FieldLabel = "Email address"
                placeholder = "Enter email"
                fieldType = "email"
                onchange = {handleChange}
              />
              <Form.Text className="text-muted">
                We'll never share your email with anyone else.
              </Form.Text>

              <FormInput
                fieldName="Password"
                FieldLabel = "Password"
                placeholder = "password"
                fieldType = "password"
                onchange = {handleChange}
              />           
            
            
                {
                  Show === true ?
                  <Button 
                    variant="primary" 
                    type="submit"
                    onClick={ (e)=> e.preventDefault()}
                          >
                      <LoadingSpinner 
                        Show={Show}
                      />
                    </Button>
                  :
                  <Button 
                    variant="primary" 
                    type="submit"
                    onClick={handleSubmit}>
                      LogIn
                  </Button>
                }
          </Form>
        </Card>
      </>
  );

}


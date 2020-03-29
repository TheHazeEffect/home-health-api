import React, {useState, useEffect} from 'react';

import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import{FormInput} from '../../components/Forms/FormInput'

import axios from 'axios'

export const Login = () => {
  // static displayName = Login.name;

    const initialLoginDtoState = {
      Email : "",
      Passowrd : ""
    }

    const [LoginObj,setLoginObj] = useState({...initialLoginDtoState })
    const [errorObj,SetErrorObj] = useState({...initialLoginDtoState})
    
    useEffect( () => {
      console.table(LoginObj)
      console.table(errorObj)

    },[LoginObj,errorObj])

    const handleSubmit = async event => {
      event.preventDefault();

      var result = await axios
        .post("/auth/login",LoginObj)
      console.log(result)
    }

  
  const handleChange = (event) => {
    const {name,value} = event.target;
    let errors = errorObj

    switch(name) {

        case "Email" :
        case "Passowrd" :
         errors[name] =  value.length > 0 ? "" : `must enter value for ${name} field`
          break;
        default:
          console.log("unidentified field")
          break;

      }

      SetErrorObj(errors)
     
      setLoginObj({...LoginObj, [name]:value})
  }

    return (
      <React.Fragment>
        <h1>Log In</h1>
        <Form>
          
          <Form.Group controlId="formBasicEmail">
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
          </Form.Group>

          <Form.Group controlId="formBasicPassword">
            <FormInput
              fieldName="Password"
              FieldLabel = "Password"
              placeholder = "password"
              fieldType = "password"
              onchange = {handleChange}
            />
          </Form.Group>
          <Button 
            variant="primary" 
            type="submit"
            onClick={handleSubmit}>
            LogIn
          </Button>
        </Form>
      </React.Fragment>
  );

}


import React, {useState, useEffect} from 'react';
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import{FormInput} from '../../components/Forms/FormInput'
import ButtonGroup from 'react-bootstrap/ButtonGroup'

import axios from 'axios'

import './Register.css'

export const Register = () => {
  // static displayName = Register.name;
 
  const initialRegisterState = {
    Email : "",
    FirstName : "",
    LastName : "",
    Password : "",
    RoleName : ""
  }

  const [RegisterObj,setRegisterObj] = useState({...initialRegisterState })
  const [errorObj,SetErrorObj] = useState({...initialRegisterState})

    const handleChange = (event) => {

      const {name,value} = event.target;
      let errors = errorObj

      switch(name) {
        case "Email" :
        case "Password" :
        case "FirstName":
        case "LastName":
        case "RoleName":
            errors[name] = value.length > 0 ? "" : `must enter value for ${name} field`
            break;
          default:
            console.log("Unidentified field");
          break;
      }

      SetErrorObj({...errorObj,[name]:errors[name]})

      setRegisterObj({...RegisterObj,[name]:value})

    }

    const handleSubmit = async (event) => {
      event.preventDefault();
      const {name,value} = event.target;
      
      var result =  await axios
        .post("/auth/signup",RegisterObj)

        console.log(result)

    }

    useEffect( () => {
      console.table(RegisterObj)
      // console.table(errorObj)

    },[RegisterObj,errorObj])

    return (
      <React.Fragment>
        <h1>Register</h1>
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

          <Form.Group controlId="formBasic">
            <FormInput
                fieldName="FirstName"
                FieldLabel = "First Name"
                placeholder = "First Name"
                fieldType = "text"
                onchange = {handleChange}
              />
          </Form.Group>

          <Form.Group controlId="formBasic">
            <FormInput
                fieldName="LastName"
                FieldLabel = "Last Name"
                placeholder = "Last Name"
                fieldType = "text"
                onchange = {handleChange}
              />
          </Form.Group>

          <Form.Group controlId="formBasicPassword">
            <FormInput
              fieldName="Password"
              FieldLabel = "Password"
              placeholder = "password"
              fieldType = "password"
              onchange = {handleChange}
            />
            <Form.Text className="text-muted">
              Home Health will never ask for your password
            </Form.Text>
          </Form.Group>

          <ButtonGroup >
            <Button 
              name="RoleName"
              value="Patient"
              onClick={handleChange} 
              variant="secondary">
                Patient
            </Button>
            <Button 
              name="RoleName"
              value="Medical Professional" 
              variant="secondary"
              onClick={handleChange}>
              Medical Professional
            </Button>
          </ButtonGroup>
          <br />
          <br />

          <Button 
            variant="primary" 
            type="submit"
            onClick={handleSubmit}>
            Register
          </Button>
        </Form>
      </React.Fragment>
    );
}
import {RegisterForm} from "./RegisterForm";

import { FormHoc } from "../../HOC/FormHoc";



export const RegisterPage = () => {

    const endpoint = "/auth/signup"
   
    const initialRegisterState = {
        Email : "",
        FirstName : "",
        LastName : "",
        Password : "",
        RoleName : ""
    } 
   
    
    return (
        FormHoc(
            endpoint, 
            RegisterForm,
            initialRegisterState
            ,{}
        )    
    );
}
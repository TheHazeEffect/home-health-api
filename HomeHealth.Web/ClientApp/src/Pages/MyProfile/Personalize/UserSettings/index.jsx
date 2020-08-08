import {  FormHoc } from "../../../../HOC/FormHoc";

import { UserSettingsForm } from "./UserSettingsForm";
import './UserSettings.css'
import { connect } from 'react-redux';


const UserSettingsSection =({user}) => {

  const endpoint = "/api/user/Update"

  const initialUpdateState = {
    Email: user.email,
    FirstName : "",
    LastName : "",
    Dob : "",
    Gender : ""
  }

    return (
        
      FormHoc(
        endpoint,
        UserSettingsForm,
        initialUpdateState,
        {}
      )
        
      
    );
}


const mapStateToprops = state => {
  return {
    user : state.user
  }
};

export default connect(mapStateToprops,null)(UserSettingsSection)
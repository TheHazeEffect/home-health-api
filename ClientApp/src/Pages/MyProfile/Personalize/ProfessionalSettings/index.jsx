import {  ProfessionalSettingsForm} from "./ProfessionalSettingsForm";

import { FormHoc } from "../../../../HOC/FormHoc";
import { connect} from 'react-redux'


export const ProfessionalSettingsSecttion = ({user}) => {

    const endpoint = "/api/user/professional/update"

    const initailUpdateState = {
        Email: user.email,
        Biography: ""
    }

    return (

        FormHoc(
            endpoint,
            ProfessionalSettingsForm,
            initailUpdateState,
            {}
        )
    );
}
const mapStateToprops = state => {
    return {
      user : state.user
    }
  };

  export default connect(mapStateToprops,null)(ProfessionalSettingsSecttion)
import { LOGIN,LOGOUT } from '../Actions/actions';

const initialState = {
  user :{
        loggedin : false,
        firstName: null,
        email: null,
        token: null,
        roleName: null
    }
};

function userReducer(user = initialState, action) {
  switch(action.type) {
    case LOGIN:
      return {
          user : {
                loggedin: true,
                firstName: action.firstName,
                email: action.email,
                token: action.token,
                roleName: action.roleName
            }
        };      
      
    case LOGOUT:
        return {
            user: {
                loggedin : false,
                firstName:null,
                email : null,
                token: null,
                roleName : null
            }
        };

    default:
      return user;
  };
}

export default userReducer;
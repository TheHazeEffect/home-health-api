import { LOGIN,LOGOUT } from '../Actions/actions';
import { loadState, saveState,clearState } from '../Store/SessionStorage';




const initialState = {
  user :{
        loggedin : false,
        id: null,
        firstName: null,
        email: null,
        token: null,
        roleName: null
    }
};

function userReducer(user = initialState, action) {
  switch(action.type) {
    case LOGIN:

        const userObj = {
            user : {
                  loggedin: true,
                  id: action.id,
                  firstName: action.firstName,
                  email: action.email,
                  token: action.token,
                  roleName: action.roleName
              }
          };      
        saveState(userObj)
        return userObj
      
    case LOGOUT:
        
        clearState()
        return {
            user: {
                id: null,
                loggedin : false,
                firstName:null,
                email : null,
                token: null,
                roleName : null
            }
        };

    default:
        window.sessionStorage.setItem('user', null);
      return user;
  };
}

export default userReducer;
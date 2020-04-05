
import { createStore } from 'redux';
import rootReducer from '../Reducers/reducers';


const initialState = {
   user : {
        loggedin : false,
        id: null,
        firstName:null,
        email: null,
        token: null,
        roleName: null}
}

export default createStore(
    rootReducer,
    initialState,
    window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
  );

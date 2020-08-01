
import { createStore } from 'redux';
import rootReducer from '../Reducers/reducers';
import { loadState } from './SessionStorage';


const persistedState = loadState();

export default createStore(
    rootReducer,
    persistedState,
    window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
  );

import React, { Component } from 'react';
import { Route} from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './Pages/Home/Home';
import { RegisterPage } from './Pages/Register';
import { LoginPage } from './Pages/Login/';
import { ServicesPage } from './Pages/Services';
import { ProfForService } from './Pages/ProfForService';
import { Professional } from './Pages/Professional';
import { connect } from 'react-redux';
import { LoginUser } from './Redux/Actions/actions';



import './custom.css'

export default class App extends Component {
  static displayName = App.name;



  render () {


    const mapDispatchToProps = {
      LoginUser:LoginUser
    }


    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/Login' component={connect(null,mapDispatchToProps)(LoginPage)} />
        <Route path='/Register' component={RegisterPage} />
        <Route exact path='/Services' component={ServicesPage} />
        <Route exact path='/Professional/:id' component={Professional} />
        <Route exact path='/Services/:id/professionals' component={ProfForService} />
      </Layout>
    );
  }



}


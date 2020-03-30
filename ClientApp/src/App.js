import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './Pages/Home/Home';
import { RegisterPage } from './Pages/Register';
import { LoginPage } from './Pages/Login/';
import { ServicesPage } from './Pages/Services';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/Login' component={LoginPage} />
        <Route path='/Register' component={RegisterPage} />
        <Route path='/Services' component={ServicesPage} />
      </Layout>
    );
  }
}

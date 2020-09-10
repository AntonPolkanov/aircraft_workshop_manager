import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Dashboard } from './components/Dashboard';
import Employees from './components/Employees';
import { Aircrafts } from './components/Aircrafts';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Dashboard} />
        <Route path='/aircrafts' component={Aircrafts} />
        <Route path='/employees' component={Employees} />
      </Layout>
    );
  }
}

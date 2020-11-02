import React, { Component } from 'react';
import { Layout } from './components/Layout';
import { Dashboard } from './components/Dashboard';
import Employees from './components/Employees';
import { Aircrafts } from './components/Aircrafts';

import './custom.css'
import Auth from "./auth/Auth";
import Callback from "./Callback";
import {Route, withRouter, Redirect} from 'react-router-dom'
import Profile from "./components/Profile";
import AircraftDetail from "./components/AircraftDetail";
import PrivateRoute from "./components/PrivateRoute";
import AuthContext from "./auth/AuthContext";
import {Work} from "./components/Work";


class App extends Component {
  static displayName = App.name;
  constructor(props) {
    super(props);
    this.state = {
      auth: new Auth(this.props.history)
    }
  }

  render () {
    const {auth} = this.state;
    return (
      <AuthContext.Provider value={auth}>
        <Layout>
          <Route exact path='/'
                 render={props => <Dashboard auth={auth} {...props}/>} />
          <Route path='/callback'
                 render={props => <Callback auth={auth} {...props}/>} />
          <Route path='/aircrafts' 
                 render={props => <Aircrafts auth={auth} {...props}/>} />
          <Route path='/employees' component={Employees} />
          <Route path='/workPackages' component={Work}/>
          <PrivateRoute path='/profile'
                        component={Profile}
          />
          <PrivateRoute path="/aircraftdetail"
                        component={AircraftDetail}
                        scopes={["read:aircraft"]}
          />         
        </Layout>
      </AuthContext.Provider>
    );
  }
}

export default withRouter(App)
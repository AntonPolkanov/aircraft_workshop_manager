import React, {Component} from 'react';
import {Layout} from './components/Layout';
import {Dashboard} from './components/Dashboard';
import Employees from './components/Employees';
import {Aircrafts} from './components/Aircrafts';

import './custom.css'
import Auth from "./auth/Auth";
import Callback from "./Callback";
import {Route, withRouter, Redirect} from 'react-router-dom'
import Profile from "./components/Profile";
import AircraftDetail from "./components/AircraftDetail";
import PrivateRoute from "./components/PrivateRoute";
import AuthContext from "./auth/AuthContext";
import {Work} from "./components/Work";
import {WorkPackageEditor} from "./components/WorkPackageEditor";
import {JobEditor} from "./components/JobEditor";
import {Home} from "./components/Home";


class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = {
      auth: new Auth(this.props.history)
    }
  }

  render() {
    const {auth} = this.state;
    return (
      <AuthContext.Provider value={auth}>
        <Layout>
          <Route exact path='/'
                 render={(props) => {
                   return (
                     auth.isAuthenticated()
                       ? <Redirect to='/dashboard'/>
                       : <Redirect to='/home'/>
                   )
                 }}
          />
          <Route exact path='/home' component={Home}/>
          <PrivateRoute path='/dashboard' component={Dashboard}/>
          <Route path='/callback' render={props => <Callback auth={auth} {...props}/>}/>
          <PrivateRoute path='/aircrafts' component={Aircrafts}/>
          <PrivateRoute path='/employees' component={Employees}/>
          <PrivateRoute path='/workPackages' component={Work}/>
          <PrivateRoute exact path='/workPackageEditor' component={WorkPackageEditor}/>
          <PrivateRoute path='/workPackageEditor/jobEditor' component={JobEditor}/>
          <PrivateRoute path='/profile' component={Profile}/>
          <PrivateRoute path="/aircraftdetail" component={AircraftDetail}/>
        </Layout>
      </AuthContext.Provider>
    );
  }
}

export default withRouter(App)
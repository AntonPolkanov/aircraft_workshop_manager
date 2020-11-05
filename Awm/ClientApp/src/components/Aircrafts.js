import React, {Component} from 'react';
import {Button, Container, Row, Col, NavItem, NavLink, Nav, TabContent, TabPane} from 'reactstrap';
import Moment from 'moment';
import {ServiceTimeline} from "./ServiceTimeline";
import classnames from 'classnames';
import {Fleet} from "./Fleet";

export class Aircrafts extends Component {
  static displayName = Aircrafts.name;

  constructor(props) {
    super(props);
    this.toggleTab = this.toggleTab.bind(this);
    this.state = {
      aircrafts: [], 
      loading: true,
      activeTab: '1'
    };
    this.errorMessage = "";
  }

  componentDidMount() {
    
  }
  
  toggleTab(tab, a) {
    if (this.state.activeTab !== tab) {
      this.setState({
        activeTab: tab
      })
    }
  }

  render() {
    
    return (
      <>
        <h1 id="tabelLabel">Aircrafts</h1>
        <Nav tabs>
          <NavItem>
            <NavLink
              className={classnames({active: this.state.activeTab === '1'})}
              onClick={() => {this.toggleTab('1');}}
            >
              Service Timeline
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              className={classnames({active: this.state.activeTab === '2'})}
              onClick={() => {this.toggleTab('2');}}
            >
              Fleet
            </NavLink>
          </NavItem>
        </Nav>
        <TabContent activeTab={this.state.activeTab}>
          <TabPane tabId='1' >
            <div style={{marginTop: 10}}>
              <ServiceTimeline/>
            </div>
          </TabPane>
          <TabPane tabId='2'>
            <div style={{marginTop: 10}}>
             <Fleet/>
            </div>
          </TabPane>
        </TabContent>
      </>
    );
  }

  async populateWeatherData() {
    const response = await fetch('api/aircraft', {
      headers: {
        Authorization: `Bearer ${this.props.auth.getAccessToken()}`
      }
    });
    const data = await response.json();
    this.setState({aircrafts: data, loading: false});
  }
}

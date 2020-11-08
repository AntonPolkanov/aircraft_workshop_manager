import React, {Component} from 'react';
import {NavItem, NavLink, Nav, TabContent, TabPane} from 'reactstrap';
import {ServiceTimeline} from "./ServiceTimeline";
import classnames from 'classnames';
import {Fleet} from "./Fleet";

export class Aircrafts extends Component {
  static displayName = Aircrafts.name;

  constructor(props) {
    super(props);
    const currentActiveTab = sessionStorage.getItem("activeAircraftTab") ?? '1';
    this.state = {
      aircrafts: [], 
      loading: true,
      activeTab: currentActiveTab
    };
    this.errorMessage = "";
  }

  componentDidMount() {
    
  }
  
  componentWillUnmount() {
    sessionStorage.setItem("activeAircraftTab", this.state.activeTab);
  }

  toggleTab(tab) {
    if (this.state.activeTab !== tab) {
      this.setState({
        activeTab: tab
      })
    }
  }

  render() {
    return (
      <>
        <h2>Aircrafts</h2>
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
}

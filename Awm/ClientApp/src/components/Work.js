import React, {Component} from 'react';
import {Nav, NavItem, NavLink, TabContent, TabPane} from "reactstrap";
import classnames from "classnames";
import {TasksAllocation} from "./TasksAllocation";
import {WorkPackage} from "./WorkPackage";

export class Work extends Component {
  constructor(props) {
    super(props);
    const currentActiveTab = sessionStorage.getItem("activeWorkTab") ?? '1';
    this.state={
      activeTab: currentActiveTab
    }
  }

  componentWillUnmount() {
    sessionStorage.setItem("activeWorkTab", this.state.activeTab);
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
        <h2>Work</h2>
        <Nav tabs>
          <NavItem>
            <NavLink
              className={classnames({active: this.state.activeTab === '1'})}
              onClick={() => {this.toggleTab('1');}}
            >
              Tasks Allocation
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              className={classnames({active: this.state.activeTab === '2'})}
              onClick={() => {this.toggleTab('2');}}
            >
              Work Packages
            </NavLink>
          </NavItem>
        </Nav>
        <TabContent activeTab={this.state.activeTab}>
          <TabPane tabId='1' >
            <div style={{marginTop: 10}}>
              <TasksAllocation {...this.props}/>
            </div>
          </TabPane>
          <TabPane tabId='2'>
            <div style={{marginTop: 10}}>
              <WorkPackage {...this.props}/>
            </div>
          </TabPane>
        </TabContent>
      </>
    )
  }
}
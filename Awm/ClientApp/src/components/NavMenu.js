import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import AuthContext from "../auth/AuthContext";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <AuthContext.Consumer>
        {auth => (
          <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
              <Container>
                <NavbarBrand>
                  <img
                    src="https://baijiu-random.s3-ap-southeast-2.amazonaws.com/AirWorks.png"
                    alt="AirWorks - Aviation Workshop Manager"
                    width="45"
                    height="45"
                  />
                </NavbarBrand>
                <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                <Collapse className="d-sm-inline-flex flex-sm-row" isOpen={!this.state.collapsed} navbar>
                  <div className="navbar-nav flex-grow-1">
                    {
                      !auth.isAuthenticated() &&
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                      </NavItem>
                    }
                    {
                      auth.isAuthenticated() &&
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/dashboard">Dashboard</NavLink>
                      </NavItem>
                    }
                    {
                      auth.isAuthenticated() &&
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/aircrafts">Aircrafts</NavLink>
                      </NavItem>  
                    }
                    {
                      auth.isAuthenticated() &&
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/workPackages">Work</NavLink>
                      </NavItem>  
                    }
                    {
                      auth.isAuthenticated() &&
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/employees">Employees</NavLink>
                      </NavItem>
                    }
                  </div>
                  <div className="navbar-nav d-sm-inline-flex">
                    {
                      auth.isAuthenticated() &&
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/profile">Profile</NavLink>
                      </NavItem>
                    }

                    <NavItem>
                      <NavLink tag={Link} to="#" className="text-dark" onClick={auth.isAuthenticated() ? auth.logout : auth.login}>
                        {auth.isAuthenticated() ? "Log Out" : "Log In"}
                      </NavLink>
                    </NavItem>
                  </div>
                </Collapse>
              </Container>
            </Navbar>
          </header>
        )}
      </AuthContext.Consumer>
    );
  }
}

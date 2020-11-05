import React, {Component} from 'react';
import {Button, Jumbotron} from "reactstrap";

export class Home extends Component {
  render() {
    return(
      <>
        <h2>Home</h2>
        <Jumbotron>
          <h1 className="display-4">AirWorks - Aviation Workshop Manager for your business</h1>
          <p className="lead">This is a simple hero unit, a simple Jumbotron-style component for calling extra attention to featured content or information.</p>
          <hr className="my-2" />
          <p>v0.1 Release notes.</p>
          <p className="lead">
            <ul>
              <li>Feature 1</li>
              <li>Feature 2</li>
              <li>Feature 3</li>
            </ul>
          </p>
        </Jumbotron>
      </>
    )
  }
}
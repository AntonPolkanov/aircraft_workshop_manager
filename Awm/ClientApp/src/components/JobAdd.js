import React, {Component} from 'react'
import {Button, Col, Form, FormGroup, Input, InputGroup, Label, Row} from "reactstrap";

export class JobAdd extends Component {
  constructor(props) {
    super(props);
    this.state = {
      jobNumber: '',
      state: '',
      description: '',
      estimatedDuration: '',
      actualDuration: ''
    }; 
    
  }

  componentDidMount() {
    
  }

  render() {
    return (
      <>
        <div>To be implemented</div>
      </>
    )
  }
}
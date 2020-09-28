import React, {Component} from 'react'
import Button from "reactstrap/es/Button";
import {Container, Row} from "reactstrap";
import Col from "reactstrap/es/Col";

class AircraftDetail extends Component {
  
  state = {
    aircraft: [],
    loading: true
  }
  
  componentDidMount() {
    this.fetchData()
      .catch(error => this.setState({aircraft: error.message}));
  }

  async fetchData() {
    const response = await fetch("api/aircraft/5",{
      headers: {
        Authorization: `Bearer ${this.props.auth.getAccessToken()}`
      }
    });
    const data = await response.json();
    this.setState({aircraft: data, loading: false})
  }
  
  render() {
    if (this.state.loading)
      return <div>Loading...</div>
    
    return (
      <>
        <Row>
          <Col>
            <Button color="warning" style={{marginRight:10}}>Update</Button>
            <Button color="danger" className="mx-10">Delete</Button>
          </Col>
        </Row>
        <br/>

        <Row>
          <Col>
            <p><b>Id:</b> {this.state.aircraft.aircraftId}</p>
          </Col>
        </Row>
        <Row>
          <Col>
            <p><b>Name:</b> {this.state.aircraft.name}</p>
          </Col>
        </Row>
        <Row>
          <Col>
            <p><b>Registration number: </b>{this.state.aircraft.registrationNumber}</p>
          </Col>
        </Row>
        <Row>
          <Col>
            <p><b>Engine:</b> {this.state.aircraft.engine}</p>
          </Col>
        </Row>
        <Row>
          <Col>
            <p><b>Last service date:</b> {this.state.aircraft.lastServiceDate}</p>
          </Col>
        </Row>
        
        

      </>
    )
  }
}

export default AircraftDetail;
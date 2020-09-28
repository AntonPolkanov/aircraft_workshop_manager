import React, {Component} from 'react';
import {Button, Container, Row, Col} from 'reactstrap';
import Moment from 'moment';

export class Aircrafts extends Component {
  static displayName = Aircrafts.name;

  constructor(props) {
    super(props);
    this.state = {aircrafts: [], loading: true};
    this.errorMessage = "";
  }

  componentDidMount() {
    this.populateWeatherData()
      .catch(error => this.setState({errorMessage: error.message, loading: false}));
  }

  static renderForecastsTable(aircrafts) {
    Moment.locale('en');
    return (
      <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
        <thead>
        <tr>
          <th/>
          <th>Id</th>
          <th>Name</th>
          <th>Registration Number</th>
          <th>Engine</th>
          <th>Last Service Date</th>
          
        </tr>
        </thead>
        <tbody>
        {aircrafts.map(aircraft =>
          <tr key={aircraft.aircraftId}>
            <th scope="row"><a href={"aircraftdetail/" + aircraft.aircraftId} className="stretched-link"/></th>
            <td>{aircraft.aircraftId}</td>
            <td>{aircraft.name}</td>
            <td>{aircraft.registrationNumber}</td>
            <td>{aircraft.engine}</td>
            <td>{Moment(new Date(aircraft.lastServiceDate)).format("DD-MM-yyyy")}</td>
            
          </tr>
        )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Aircrafts.renderForecastsTable(this.state.aircrafts);

    return (
      <Container>
        <h1 id="tabelLabel">Aircrafts</h1>
        <Row className="mb-2">
          <Col>
            <Button color="primary">Add</Button>
          </Col>
        </Row>
        <Row className="mb-2">
          <Col>
            {
              !this.state.errorMessage 
                ? contents
                : <div>{this.state.errorMessage}</div>
            }
          </Col>
        </Row>
      </Container>
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

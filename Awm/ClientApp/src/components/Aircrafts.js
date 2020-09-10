import React, {Component} from 'react';
import {Button, Container, Row, Col} from 'reactstrap';

export class Aircrafts extends Component {
  static displayName = Aircrafts.name;

  constructor(props) {
    super(props);
    this.state = {forecasts: [], loading: true};
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
        <tr>
          <th>Date</th>
          <th>Temp. (C)</th>
          <th>Temp. (F)</th>
          <th>Summary</th>
        </tr>
        </thead>
        <tbody>
        {forecasts.map(forecast =>
          <tr key={forecast.date}>
            <td>{forecast.date}</td>
            <td>{forecast.temperatureC}</td>
            <td>{forecast.temperatureF}</td>
            <td>{forecast.summary}</td>
          </tr>
        )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Aircrafts.renderForecastsTable(this.state.forecasts);

    return (
      <Container>
        <h1 id="tabelLabel">Aircrafts</h1>
        <Row className="mb-2">
          <Col>
            <Button color="success">Add</Button>
          </Col>
        </Row>
        <Row className="mb-2">
          <Col>
            {contents}
          </Col>
        </Row>
      </Container>
    );
  }

  async populateWeatherData() {
    const response = await fetch('employeemanagement');
    const data = await response.json();
    this.setState({forecasts: data, loading: false});
  }

}

import React, {Component} from 'react';
import moment from "moment";
import MaterialTable from "material-table";
import axios from "axios";
import {Badge, Button, Col, Container, Form, FormGroup, Input, InputGroup, Label, Row} from "reactstrap";
import DeferredSpinner from "./DeferredSpinner";

export class WorkPackageUpdate extends Component {
  constructor(props) {
    super(props);
    const {history} = props;
    this.history = history;
    this.currentDate = moment().format('YYYY-MM-DD');
    this.onChangeAircraftNumber = this.onChangeAircraftNumber.bind(this);
    this.onChangeVersion = this.onChangeVersion.bind(this);
    this.onChangeDate = this.onChangeDate.bind(this);
    this.onChangeDescription = this.onChangeDescription.bind(this);
    this.onFormSubmit = this.onFormSubmit.bind(this);
    this.workPackage = props.location.state.workPackage;
    this.state = {
      aircraftNumber: this.workPackage?.aircraftNumber,
      version: this.workPackage.version,
      date: this.currentDate,
      description: this.workPackage.description,
      jobsData: []
    }
  }

  componentDidMount() {
    this.fetchJobs();
  }

  fetchJobs() {
    axios.get(`${process.env.REACT_APP_API_URL}/jobs?workPackage=${this.workPackage.id}`)
      .then(response => {
        this.setState({
          jobsData: response.data,
          loading: false
        })
      })
      .catch(err => {
        console.log(`Cannot fetch Jobs`)
      });
  }

  onChangeAircraftNumber(e) {
    this.setState({
      aircraftNumber: e.target.value
    })
  }

  onChangeVersion(e) {
    this.setState({
      version: e.target.value
    })
  }

  onChangeDate(e) {
    this.setState({
      date: e.target.value
    })
  }

  onChangeDescription(e) {
    this.setState({
      description: e.target.value
    })
  }
  

  addJob(rowData) {
    this.history.push({
      path: `/workPackageEditor/jobEditor`,
      state: {
        isNew: true,
        job: rowData
      }
    });
  }
  
  openJobDetails(rowData) {
    this.history.push({
      pathname: `/workPackageEditor/jobEditor`,
      state: {
        isNew: false,
        job: rowData
      }
    });
  }

  renderTable(data) {
    return (
      <MaterialTable
        title='Jobs'
        data={data}
        columns={[
          {title: 'Job', field: 'number'},
          {title: 'Status', field: 'status'},
          {title: 'Estimated Duration', field: 'estimatedDuration'},
          {title: 'Actual Duration', field: 'actualDuration'},
          {title: 'Description', field: 'description'},
        ]}
        options={{
          paging: false
        }}
        actions={[
          {
            icon: 'add',
            tooltip: 'Add Job',
            isFreeAction: true,
            disabled: this.workPackage.sealed,
            onClick: (event, rowData) => this.addJob(rowData)
          }
        ]}
        onRowClick={(event, rowData, togglePanel) => this.openJobDetails(rowData)}
      />
    )
  }

  onFormSubmit(e) {
    e.preventDefault();
    alert("Update to be implemented");
  }

  render() {
    return (
      <>        
        <Form onSubmit={this.onFormSubmit}>
          <Button type="submit" color="primary" style={{marginBottom: 20}} disabled={this.workPackage.sealed}>Update</Button>
          <Row form>
            <Col md={4}>
              <FormGroup>
                <Label for="name">Aircraft</Label>
                <Input disabled={this.workPackage.sealed} type="text" name="name" id="name" placeholder="Aircraft Number" value={this.state?.aircraftNumber} onChange={this.onChangeAircraftNumber}/>
              </FormGroup>
            </Col>
            <Col md={4}>
              <FormGroup>
                <Label for="price">Version</Label>
                <InputGroup>
                  <Input disabled type="number" name="version" id="version" placeholder="Version Number" value={this.state?.version} onChange={this.onChangeVersion}/>
                </InputGroup>
              </FormGroup>
            </Col>
            <Col md={4}>
              <FormGroup>
                <Label for="id">Date</Label>
                <Input disabled type="date" name="date" id="date" placeholder="Date" defaultValue={this.currentDate} value={this.state?.date} onChange={this.onChangeDate}/>
              </FormGroup>
            </Col>
          </Row>

          <FormGroup>
            <Label for="description">Description</Label>
            <Input type="textarea" name="description" id="description" placeholder="Description" style={{"minHeight": 125}}
                   disabled={this.workPackage.sealed}
                   value={this.state?.description} onChange={this.onChangeDescription}/>
          </FormGroup>
        </Form>

        {this.state.loading
          ? <DeferredSpinner delay={250}/>
          : this.renderTable(this.state.jobsData)
        }
      </>
    )
  }
}
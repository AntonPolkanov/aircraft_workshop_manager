import React, {Component} from 'react';
import {Button, Col, Form, FormGroup, Input, InputGroup, Label, Row} from "reactstrap";
import moment from 'moment';
import axios from 'axios';
import MaterialTable from "material-table";
import DeferredSpinner from "./DeferredSpinner";

export class WorkPackageAdd extends Component {
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
    this.previosWorkPackage = props.location.state.workPackage;
    this.state = {
      aircraftNumber: this.previosWorkPackage?.aircraftNumber ?? '',
      version: this.previosWorkPackage ? this.previosWorkPackage.version + 1 : 0,
      date: this.currentDate,
      description: '',
      jobsData: [],
      loading: true
    }
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

  addJob() {
    alert("Job to be added");
  }

  renderTable(data) {
    return (
      <MaterialTable
        title='Jobs'
        data={data}

        columns={[
          {title: 'Job', field: 'number'},
          {title: 'Status', field: 'status'},
          {title: 'Description', field: 'description'}
        ]}
        options={{
          paging: false
        }}
        actions={[
          {
            icon: 'add',
            tooltip: 'Add Job',
            isFreeAction: true,
            onClick: (event) => this.addJob()
          }
        ]}
      />
    )
  }

  onFormSubmit(e) {
    e.preventDefault();
    const obj = {
      aircraftNumber: this.state.aircraftNumber,
      version: this.state.version,
      date: this.state.date,
      description: this.state.description,
      estimationId: this.previosWorkPackage?.estimationId ?? this.previosWorkPackage?.id,
      parentId: this.previosWorkPackage?.id
    }
    axios.post(`${process.env.REACT_APP_API_URL}/workPackages`, obj)
      .then(response => {
        // if it's not an Estimation - seal parent WorkPackage
        if (response.data.estimationId) {
          axios.patch(`${process.env.REACT_APP_API_URL}/workPackages/${response.data.parentId}`, {
            sealed: true
          }).then(response => {
            this.history.goBack();
          }).catch(patchErr => {
            console.log(`Cannot seal previous workPackage: ${patchErr}`);
          })
        } else {
          this.history.goBack();
        }
      })
      .catch(err => console.log(`Response error: ${err}`));
  }

  render() {
    return (
      <>
        <Form onSubmit={this.onFormSubmit}>
          <Row form>
            <Col>
              <Button type="submit" color="primary" style={{marginBottom: 20}}>Save</Button>
            </Col>
          </Row>
          <Row form>
            <Col md={4}>
              <FormGroup>
                <Label for="name">Aircraft</Label>
                <Input type="text" name="name" id="name" placeholder="Aircraft Number"
                       value={this.state?.aircraftNumber} onChange={this.onChangeAircraftNumber}/>
              </FormGroup>
            </Col>
            <Col md={4}>
              <FormGroup>
                <Label for="price">Version</Label>
                <InputGroup>
                  <Input disabled type="number" name="version" id="version" placeholder="Version Number"
                         value={this.state?.version} onChange={this.onChangeVersion}/>
                </InputGroup>
              </FormGroup>
            </Col>
            <Col md={4}>
              <FormGroup>
                <Label for="id">Date</Label>
                <Input disabled type="date" name="date" id="date" placeholder="Date" defaultValue={this.currentDate}
                       value={this.state?.date} onChange={this.onChangeDate}/>
              </FormGroup>
            </Col>
          </Row>

          <FormGroup>
            <Label for="description">Description</Label>
            <Input type="textarea" name="description" id="description" placeholder="Description"
                   style={{"minHeight": 125}}
                   value={this.state?.description} onChange={this.onChangeDescription}/>
          </FormGroup>
        </Form>

        {this.state.jobsData && this.renderTable(this.state.jobsData)}
      </>
    )
  }
}
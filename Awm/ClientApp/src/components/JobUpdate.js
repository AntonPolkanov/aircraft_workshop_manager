﻿import React, {Component} from 'react'
import {
  Button,
  Col, DropdownItem, DropdownMenu,
  DropdownToggle,
  Form,
  FormGroup,
  Input,
  Label,
  Row,
  UncontrolledDropdown
} from "reactstrap";
import DeferredSpinner from "./DeferredSpinner";
import axios from "axios";
import MaterialTable from "material-table";

const statusList = [
  {status: 'Open', isActive: false},
  {status: 'In Progress', isActive: false},
  {status: 'Requires Checking', isActive: false},
  {status: 'Done', isActive: false},
  {status: 'Reopen', isActive: false}
]

export class JobUpdate extends Component {
  constructor(props) {
    super(props);
    this.onChangeJobNumber = this.onChangeJobNumber.bind(this);
    this.onChangeStatus = this.onChangeStatus.bind(this);
    this.onChangeDescription = this.onChangeDescription.bind(this);
    this.onChangeEstimatedDuration = this.onChangeEstimatedDuration.bind(this);
    this.onChangeActualDuration = this.onChangeActualDuration.bind(this);
    this.handleStatusSelection = this.handleStatusSelection.bind(this);
    this.onFormSubmit = this.onFormSubmit.bind(this);
    this.job = props.job;
    this.setActive(props.job.status);
    this.state = {
      jobNumber: props.job.number,
      status: props.job.status,
      description: props.job.description,
      estimatedDuration: props.job.estimatedDuration,
      actualDuration: props.job.actualDuration,
      tasksData: [],
      loading: true
    }
  }

  async componentDidMount() {
    await this.fetchTasks();
  }
  
  onChangeJobNumber(e) {
    this.setState({
      jobNumber: e.target.value
    })
  }

  onChangeStatus(e) {
    this.setState({
      state: e.target.value
    })
  }

  onChangeDescription(e) {
    this.setState({
      description: e.target.value
    })
  }

  onChangeEstimatedDuration(e) {
    this.setState({
      estimatedDuration: e.target.value
    })
  }

  onChangeActualDuration(e) {
    this.setState({
      actualDuration: e.target.value
    })
  }

  onFormSubmit(e) {

  }

  openTaskDetails(rowData) {

  }

  async fetchTasks() {
    try {
      const response = await axios.get(`${process.env.REACT_APP_API_URL}/tasks?job=${this.job.id}`);
      this.setState({
        tasksData: response.data,
        loading: false
      });
    } catch {
      console.log(`Cannot fetch Tasks`);
    }
  }

  setActive(status) {
    const activeStatusIndex = statusList.findIndex(i => i.status === status);
    if (activeStatusIndex !== -1) {
      statusList.forEach(i => i.isActive = false);
      statusList[activeStatusIndex].isActive = true;
    }
  }

  handleStatusSelection(e) {
    const newStatus = e.currentTarget.textContent;
    this.setActive(newStatus);
    this.setState({
      status: newStatus
    });
  }

  async createNewTask(newTask) {
    newTask.job = this.job.id;
    return await axios.post(`${process.env.REACT_APP_API_URL}/tasks`, newTask);
  }
  
  async deleteTask(task) {
    await axios.delete(`${process.env.REACT_APP_API_URL}/tasks/${task.id}`, task);
  }

  renderTable(data) {
    return (
      <MaterialTable
        title='Tasks'
        data={data}
        columns={[
          {title: 'Task', field: 'number'},
          {title: 'Status', field: 'status'},
          {title: 'Description', field: 'description'},
          {title: 'Clocked Off', field: 'clockedOff', editable: 'never', emptyValue: 0},
          {title: 'Assigned To', field: 'assignedToText', editable: 'never'},
        ]}
        options={{
          paging: false,
          actionsColumnIndex: -1
        }}
        onRowClick={(event, rowData, togglePanel) => this.openTaskDetails(rowData)}
        editable={{
          onRowAdd: newData =>
            new Promise((resolve, reject) => {
              this.createNewTask(newData)
                .then(response => {
                  this.setState({tasksData: [...data, response.data]});
                  resolve();
                })
                .catch(err => {
                  reject();
                });
            }),
          onRowDelete: oldData =>
            new Promise((resolve, reject) => {
              this.deleteTask(oldData)
                .then(response => {
                  const dataDelete = [...data];
                  const index = oldData.tableData.id;
                  dataDelete.splice(index, 1);
                  this.setState({tasksData: [...dataDelete]});
                  resolve();
                })                
                .catch(err => {
                  reject();
                })
            })
        }}
      />
    )
  }

  render() {
    return (
      <>
        <Form onSubmit={this.onFormSubmit}>
          <Row form>
            <Col>
              <Button disabled type="submit" color="primary" style={{marginBottom: 20}}>Update</Button>
            </Col>
          </Row>
          <Row form>
            <Col md={3}>
              <FormGroup>
                <Label for="jobNumber">Job</Label>
                <Input type="text" name="jobNumber" id="jobNumber" placeholder="Job Number"
                       value={this.state?.jobNumber} onChange={this.onChangeJobNumber}/>
              </FormGroup>
            </Col>
            <Col md={3}>
              <FormGroup>
                <Label for="estimatedDuration">Estimated Duration</Label>
                <Input type="number" name="estimatedDuration" id="estimatedDuration" placeholder="Duration in Hours"
                       value={this.state?.estimatedDuration} onChange={this.onChangeEstimatedDuration}/>
              </FormGroup>
            </Col>
            <Col md={3}>
              <FormGroup>
                <Label for="actualDuration">Actual Duration</Label>
                <Input type="number" name="actualDuration" id="actualDuration" placeholder="Duration in Hours"
                       value={this.state?.actualDuration} onChange={this.onChangeActualDuration}/>
              </FormGroup>
            </Col>
            <Col md={3}>
              <FormGroup>
                <Label for="status">Status</Label>
                <UncontrolledDropdown id="status">
                  <DropdownToggle caret>
                    {this.state.status}
                  </DropdownToggle>
                  <DropdownMenu>
                    {statusList.map(i =>
                      <DropdownItem key={i.status} onClick={this.handleStatusSelection}
                                    active={i.isActive}>{i.status}</DropdownItem>
                    )}
                  </DropdownMenu>
                </UncontrolledDropdown>
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

        {this.state.loading
          ? <DeferredSpinner delay={250}/>
          : this.renderTable(this.state.tasksData)
        }
      </>
    )
  }
}
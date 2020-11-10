import React, {Component} from 'react';
import MaterialTable from "material-table";
import DeferredSpinner from "./DeferredSpinner";
import axios from 'axios';

export class TasksAllocation extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tasks: [],
      employees: [],
      loading: true
    }
  }
  
  async componentDidMount() {
    const data = await this.fetchTasks();
    const employees = await this.fetchEmployees();
    this.setState({
      tasks: data,
      employees: employees,
      loading: false
    });
  }
  
  async fetchTasks() {
    const response = await axios.get(`${process.env.REACT_APP_API_URL}/tasks?status=Planned`);
    return response.data;
  }
  
  async fetchEmployees() {
    const response = await axios.get(`${process.env.REACT_APP_API_URL}/employees`);
    const employees = {};
    response.data.forEach(e => {employees[e.id] = e.firstName[0] + "." + e.lastName});
    return employees;
  }
  
  async assignEmployee(assignedTask) {
    await axios.patch(`${process.env.REACT_APP_API_URL}/tasks/${assignedTask.id}`, {
      status: 'In Progress',
      assignedToText: this.state.employees[assignedTask.assignedTo],
      assignedTo: parseInt(assignedTask.assignedTo)
    });
  }
  
  renderTable(data) {
    return (
      <MaterialTable 
        title="Unallocated tasks"
        columns={[
          {title: "Task", field: "number", editable: "never"},
          {title: "Description", field: "description", editable: "never"},
          {title: "Status", field: "status", editable: "never"},
          {title: "Employee", field: "assignedTo", lookup: this.state.employees}
        ]}
        data={data}
        editable={{
          onRowUpdate: (newData, oldData) =>
            new Promise((resolve, reject) => {
              
              this.assignEmployee(newData)
                .then(response => {
                  // update the row to show that the changes have been applied 
                  const dataUpdate = [...data];
                  const index = oldData.tableData.id;
                  newData.status = 'In Progress';
                  dataUpdate[index] = newData;
                  this.setState({tasks: [...dataUpdate]});
                  
                  setTimeout(() => {
                    // delete the changed row after timeout because the list displays only unassigned tasks
                    const dataDelete = [...data];
                    const index = oldData.tableData.id;
                    dataDelete.splice(index, 1);
                    this.setState({tasks: [...dataDelete]});
                  }, 1000);
                  
                  resolve();
                })
                .catch(err => reject());
              resolve();
            })
        }}
        options={{
          actionsColumnIndex: -1,
          exportButton: true,
          exportAllData: true
        }}
      />
    )
  }
  
  render() {
    return (
      <>
        {
          this.state.loading
            ? <DeferredSpinner delay={250}/>
            : this.renderTable(this.state.tasks)
        }
      </>
    )
  }
}
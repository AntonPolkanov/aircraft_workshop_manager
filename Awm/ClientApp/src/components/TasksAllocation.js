import React, {Component} from 'react';
import MaterialTable from "material-table";
import DeferredSpinner from "./DeferredSpinner";
import axios from 'axios';

export class TasksAllocation extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tasks: [],
      loading: true
    }
  }
  
  async componentDidMount() {
    const data = await this.fetchTasks();
    this.setState({
      tasks: data,
      loading: false
    });
  }

  async fetchTasks() {
    const response = await axios.get(`${process.env.REACT_APP_API_URL}/tasks?status=Planned`);
    return response.data;
  }
  
  renderTable(data) {
    return (
      <MaterialTable 
        title="Unallocated tasks"
        columns={[
          {title: "Task", field: "number"},
          {title: "Status", field: "status"},
          {title: "Description", field: "description"}
        ]}
        data={data}
        editable={{
          onRowUpdate: (newData, oldData) =>
            new Promise((resolve, reject) => {
              const dataUpdate = [...data];
              const index = oldData.tableData.id;
              dataUpdate[index] = newData;
              this.setState({tasks: [...dataUpdate]});
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
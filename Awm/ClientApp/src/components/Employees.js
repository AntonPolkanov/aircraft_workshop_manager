import React, {Component} from 'react';
import MaterialTable from 'material-table';
import DeferredSpinner from "./DeferredSpinner";
import axios from "axios";

export default class Employees extends Component {
  constructor(props) {
    super(props);
    this.state = {
      employeesData: [],
      loading: true
    }
  }
  
  async componentDidMount() {
    const response = await axios.get(`${process.env.REACT_APP_API_URL}/employees`);
    const data = await response.data;
    this.setState({
      employeesData: data,
      loading: false
    })
  }
  
  renderTable(data) {
    return (
      <MaterialTable
        title="Employees"
        columns={[
          {title: 'First Name', field: 'firstName'},
          {title: 'Last Name', field: 'lastName'},
          {title: 'Position', field: 'position'},
        ]}
        data={data}
      />
    )
  }

  render() {
    return (
      <>
        {this.state.loading
          ? <DeferredSpinner delay={250}/>
          : this.renderTable(this.state.employeesData)
        }
      </>
    )  
  }
}

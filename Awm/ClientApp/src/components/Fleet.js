import React, {Component} from 'react';
import MaterialTable from "material-table";
import DeferredSpinner from "./DeferredSpinner";
import axios from 'axios';

export class Fleet extends Component {
  constructor(props) {
    super(props);
    
    this.state = {
      fleetData: [],
      loading: true
    }
  }
  
  async componentDidMount() {
    const response = await axios.get(`${process.env.REACT_APP_API_URL}/aircrafts`);
    const data = await response.data;
    this.setState({
      fleetData: data,
      loading: false
    })
  }

  renderTable(data) {
    return (
      <MaterialTable
        columns={[
          {title: 'Registration Number', field: 'registrationNumber'},
          {title: 'Build Year', field: 'builtYear'},
          {title: 'Engine', field: 'engine'},
          {title: 'Last Service Date', field: 'lastServiceDate'},
          {title: 'Contact Number', field: 'contactNumber'}
        ]}
        data={data}
        options={{
          showTitle: false
        }}
      />
    )
  }
  
  render() {
    return(
      <>
        {this.state.loading
          ? <DeferredSpinner delay={250}/>
          : this.renderTable(this.state.fleetData)
        }
      </>  
    )
  }
}
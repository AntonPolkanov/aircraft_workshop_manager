import React, {Component} from 'react';
import MaterialTable from 'material-table';
import axios from 'axios';
import DeferredSpinner from "./DeferredSpinner";
import {UncontrolledAlert} from "reactstrap";

export class Work extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: [],
      loading: true,
      errorMessage: ""
    }
  }
  
  fetchData() {
    const url = `${process.env.REACT_APP_API_URL}/workPackages`
    axios({
      method: "get",
      url: url
    }).then(response => {
      this.setState({
        data: response.data,
        loading: false
      })
    }).catch(error => {
      console.log(error);
      this.setState({
        errorMsg: error.message,
        loading: false
      })
    });
  }
  
  componentDidMount() {
    this.fetchData();
  }

  openWPDetails(rowData) {
    alert(`ready to open ${rowData.aircraftNumber}`)
  }
  
  renderTable(data) {
    return (
      <MaterialTable
        title="Work Estimation"
        data={data}
        columns={[
          {title: 'Aircraft', field: 'aircraftNumber'},
          {title: 'Date', field: 'date'},
          {title: 'Version', field: 'version'},
          {title: 'Description', field: 'description'}
        ]}
        parentChildData={(row, rows) => rows.find(a => a.id === row.parentId)}
        actions={[
          {
            icon: 'add',
            tooltip: 'Add Work Package',
            isFreeAction: true,
            onClick: (event) => alert("You want to add a new row")
          },
          rowData => ({
            icon: 'edit',
            tooltip: 'Edit',
            onClick: (event, rowData) => alert("You saved " + rowData.aircraftName),
            hidden: !rowData.hasOwnProperty('parentId') || rowData.sealed === true
          }),
          rowData => ({
            icon: 'delete',
            tooltip: 'Delete',
            onClick: (event, rowData) => alert("You want to delete " + rowData.aircraftName),
            hidden: !rowData.hasOwnProperty('parentId') || rowData.sealed === true
          })
        ]}
        options={{
          exportButton: true,
          exportAllData: true,
          actionsColumnIndex: -1,
          rowStyle: rowData => ({
            backgroundColor: !!rowData.parentId ? '#F5F5F5' : '#FFF'
          })
        }}
        onRowClick={(event, rowData, togglePanel) => this.openWPDetails(rowData)}
      >
      </MaterialTable>
    )
  }
  
  render() {
    const contents = this.state.loading
      ? <DeferredSpinner delay={250}/>
      : this.renderTable(this.state.data);
    
    return (
      <>
        <div>
          {
            this.state && this.state.errorMessage
              ? <UncontrolledAlert color='danger'>{`Cannot fetch related products: ${this.state.errorMsg}`}</UncontrolledAlert>
              : <></>
          }
          {contents}
        </div>
      </>
    )
  }
}
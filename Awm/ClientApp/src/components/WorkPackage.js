import React, {Component} from 'react';
import MaterialTable from 'material-table';
import axios from 'axios';
import DeferredSpinner from "./DeferredSpinner";
import {UncontrolledAlert} from "reactstrap";

export class WorkPackage extends Component {
  constructor(props) {
    super(props);
    const {history} = props;
    this.history = history;
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
  
  addEstimation() {
    this.history.push({
      pathname: `/workPackageEditor`,
      state: {
        isNew: true
      }
    });
  }

  addWorkPackage(rowData) {
    this.history.push({
      pathname: `/workPackageEditor`,
      state: {
        isNew: true,
        workPackage: rowData
      }
    })
  }

  deleteWorkPackage(rowData) {
    axios.delete(`${process.env.REACT_APP_API_URL}/workPackages/${rowData.id}`)
      .then(response => {
        if (rowData.parentId) {
          axios.patch(`${process.env.REACT_APP_API_URL}/workPackages/${rowData.parentId}`, {
            sealed: false
          }).then(response => {
            const data = this.state.data.filter(d => d.id !== rowData.id);
            const parentIndex = data.findIndex(d => d.id === rowData.parentId);
            data[parentIndex].sealed = false;
            this.setState({data});
          }).catch(patchErr => {
            console.log(`Cannot update id=${rowData.parentId}: ${patchErr}`);
          })
        } else {
          const data = this.state.data.filter(d => d.id !== rowData.id);
          this.setState({data});
        }
      }).catch(err => {
      console.log(`Cannot delete id=${rowData.id}: ${err}`);
    })
  }

  openWPDetails(rowData) {
    this.history.push({
      pathname: `/workPackageEditor`,
      state: {
        isNew: false,
        workPackage: rowData
      }
    })
  }

  renderTable(data) {
    return (
      <MaterialTable
        title="Work Packages"
        data={data}
        columns={[
          {title: 'Aircraft', field: 'aircraftNumber'},
          {title: 'Date', field: 'date'},
          {title: 'Version', field: 'version'},
          {title: 'Description', field: 'description'}
        ]}
        // parentChildData={(row, rows) => rows.find(a => a.id === row.estimationId)}
        actions={[
          {
            icon: 'add',
            tooltip: 'Add Estimation',
            isFreeAction: true,
            onClick: (event) => this.addEstimation()
          },
          rowData => ({
            icon: 'add',
            tooltip: 'Add Work Package',
            onClick: (event, rowData) => this.addWorkPackage(rowData),
            hidden: rowData.sealed === true
          }),
          rowData => ({
            icon: 'delete',
            tooltip: 'Delete',
            onClick: (event, rowData) => this.deleteWorkPackage(rowData),
            hidden: rowData.sealed === true
          })
        ]}
        options={{
          exportButton: true,
          exportAllData: true,
          grouping: true,
          actionsColumnIndex: -1,
          // rowStyle: rowData => ({
          //   backgroundColor: !!rowData.estimationId ? '#F5F5F5' : '#FFF'
          // })
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
import React, {Component} from 'react';
import {Breadcrumb, BreadcrumbItem, Button, Col, Row} from 'reactstrap';
import {WorkPackageAdd} from "./WorkPackageAdd";
import {WorkPackageUpdate} from "./WorkPackageUpdate";

export class WorkPackageEditor extends Component {
  constructor(props) {
    super(props);
    const {history} = props;
    this.isNew = props.location.state.isNew;
    this.history = history;
    this.workPackage = props.location.state.workPackage;
  }

  onBackClick = () => {
    this.history.goBack();
  }
  
  render() {
    return (
      <>
        <Row>
          <Col md={{size: 'auto'}}>
            <Button outline
                    color="primary"
                    onClick={this.onBackClick}
                    style={{marginBottom:15, marginTop:5}}
                    size="sm"
            >{"<"}- Back</Button>
          </Col>
          <Col md={{size: 'auto'}}>
            <Breadcrumb style={{backgroundColor: '#fff', fontSize:12}}>
              <BreadcrumbItem>Work</BreadcrumbItem>
              <BreadcrumbItem active>Work Package</BreadcrumbItem>
            </Breadcrumb>
          </Col>
        </Row>
        {this.isNew 
          ? <h2>Add {this.workPackage ? "Work Package" : "Estimation"} </h2>
          : <h2>{this.workPackage.estimationId ? "Work Package" : "Estimation"} Details</h2>
        }
        
        {this.isNew
          ? <WorkPackageAdd workPackage={this.workPackage} {...this.props}/>
          : <WorkPackageUpdate workPackage={this.workPackage} {...this.props}/>
        }
      </>
    )
  }
}
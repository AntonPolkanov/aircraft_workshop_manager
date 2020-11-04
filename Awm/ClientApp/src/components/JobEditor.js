import React, {Component} from 'react'
import {Breadcrumb, BreadcrumbItem, Button, Col, Row} from "reactstrap";
import {JobUpdate} from "./JobUpdate";
import {JobAdd} from "./JobAdd";

export class JobEditor extends Component {
  constructor(props) {
    super(props);
    const {history} = props;
    this.history = history; 
    this.isNew = props.location.state.isNew;
    this.job = props.location.state.job;
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
              <BreadcrumbItem >Work</BreadcrumbItem>
              <BreadcrumbItem >Work Package</BreadcrumbItem>
              <BreadcrumbItem active>Job</BreadcrumbItem>
            </Breadcrumb>
          </Col>
        </Row>
                
        {this.isNew
          ? <h2>Add Job </h2>
          : <h2>Job Details</h2>
        }
        {this.isNew
          ? <JobAdd {...this.props}/>
          : <JobUpdate job={this.job} {...this.props}/>
        }
      </>
    )    
  }
}
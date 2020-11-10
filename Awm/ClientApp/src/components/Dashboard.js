import React, {Component} from 'react';
import {XYPlot, HorizontalGridLines, LineSeries, XAxis, YAxis, RadialChart} from 'react-vis';
import {Col, Row} from "reactstrap";

export class Dashboard extends Component {
  static displayName = Dashboard.name;

  render() {
    const myData = [
      {angle: 1}, {
        angle: 2,
        label: 'Super Custom label',
        subLabel: 'With annotation',
        
      }, {angle: 5, label: 'Alt Label'}, {angle: 3}, {
        angle: 5,
        
        subLabel: 'Sub Label only',
        className: 'custom-class'
      }
    ]
    return (
      <>
        <div>
          <h1>Dashboard</h1>
        </div>
        <Row>
          <Col>
            <h5>Fixed aircrafts</h5>
            <XYPlot
              xType="ordinal"
              tickFormat={d => {
                return d + 100;
              }}
              width={300}
              height={300}>
              <HorizontalGridLines/>
              <LineSeries
                data={[
                  {x: '2017', y: 13},
                  {x: '2018', y: 15},
                  {x: '2019', y: 18}
                ]}/>
              <XAxis/>
              <YAxis/>
            </XYPlot>
          </Col>
          <Col>
            <h5>Tasks statuses</h5>
            <RadialChart
              width={300}
              height={300}
              innerRadius={100}
              radius={140}
              labelsAboveChildren={true}
              data={[
                {angle: 2},
                {angle: 6},
                {angle: 2},
                {angle: 3},
                {angle: 1}
              ]}/>  
          </Col>
        </Row>
      </>

    );
  }
}

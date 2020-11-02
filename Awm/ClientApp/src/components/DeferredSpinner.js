import React, {Component} from 'react'
import {Spinner, Container, Row, Col} from 'reactstrap'


class DeferredSpinner extends Component {
  constructor(props) {
    super(props);
    this.delay = props.delay; // in milliseconds 
    this.state = {
      showSpinner: false
    };
  }

  componentDidMount() {
    this.timer = setTimeout(
      () => this.setState({showSpinner: true}),
      this.delay
    );
  }

  componentWillUnmount() {
    clearTimeout(this.timer);
  }

  render() {
    return (
      this.state.showSpinner &&
      <Container>
        <Row className="justify-content-md-center">
          <Col md="auto">
            <Spinner style={{width: '5rem', height: '5rem'}} />
          </Col>
        </Row>
      </Container>
    );
  }
}

export default DeferredSpinner;
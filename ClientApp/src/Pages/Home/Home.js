import React, { Component } from 'react';
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import Image from 'react-bootstrap/Image'

import './Home.css'
import img1 from './Resources/humberto-chavez-FVh_yqLR9eA-unsplash.jpg'
import img2 from './Resources/greg-rosenke-7e2LoP__duU-unsplash.jpg'

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <React.Fragment>
        <Row>
            <Col lg={true}  className="card margin-top10">
              <Image src={img1} alt="Smiley face" height="auto" width="auto"/> 
            </Col>
            <Col lg={true} className="card text-box margin-top10">
              Connect with Professionals and get the expert consultation you need at the touch of a button
            </Col>
        </Row>
        <Row>
          <Col lg={true} className="card text-box margin-top10">
            Find Medical Facilities in your area and create appointments at your own convenience
          </Col>
          <Col lg={true} className="card margin-top10">
            <Image src={img2} alt="Smiley face" height="auto" width="auto"/> 
          </Col>
        </Row>
          <div className="card">Sign up Now!</div>
      </React.Fragment>
    );
  }
}

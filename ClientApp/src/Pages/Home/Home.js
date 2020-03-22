import React, { Component } from 'react';
import "./Home.css"
import img1 from './Resources/humberto-chavez-FVh_yqLR9eA-unsplash.jpg'
import img2 from './Resources/greg-rosenke-7e2LoP__duU-unsplash.jpg'

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <section className="basic-grid">

          <div className="card">
            <img src={img1} alt="Smiley face" height="auto" width="auto"/> 
          </div>
          <div className="card text-box">
            Connect with Professionals and get the expert consultation you need at the touch of a button
          </div>
          
          <div className="card text-box">
            Search for Medical Facilities in your area and create appointments at your own convenience
          </div>
          <div className="card">
            <img src={img2} alt="Smiley face" height="auto" width="auto"/> 
          </div>

        </section>
          <div className="card">Sign up Now!</div>
      </div>
    );
  }
}

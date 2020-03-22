import React, { Component } from 'react';

export class Login extends Component {
  static displayName = Login.name;

  constructor(props) {
    super(props);
    
  }


  render() {
    return (
      <div>
        <h1>Log In</h1>
        <form>
          <label for="email">Email</label>
          <br/>
          <input type="email" id="email" name="email"/>
          <br/>
          <label for="password">Password</label>
          <br/>
          <input type="password" id="password" name="password"/>
          <br/>
          <button className="btn btn-primary" onClick={this.incrementCounter}>Login</button>
        </form> 

       
      </div>
    );
  }
}

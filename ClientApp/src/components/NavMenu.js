import React, { Component } from 'react';
import { connect } from 'react-redux';
import { LogoutUser } from "../Redux/Actions/actions";
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import NavDropdown from 'react-bootstrap/NavDropdown'
import Button from 'react-bootstrap/Button'
import { Link } from 'react-router-dom';
import './NavMenu.css';

class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {

    const user = this.props.user
    console.log(user)
    console.log("--------------------------")
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">HomeHealth</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Services">Services</NavLink>
                </NavItem>
                <NavItem>
                  {user.firstName === null ? 
                      <NavLink tag={Link} className="text-dark" to="/Login">Login </NavLink>

                    : 
                      <NavDropdown title={`Hi ${user.firstName } =)`} id="nav-dropdown">
                        <NavDropdown.Item eventKey="4.1">Settings</NavDropdown.Item>
                        <NavDropdown.Divider />
                        <NavDropdown.Item eventKey="4.2">Log Out</NavDropdown.Item>
                      </NavDropdown>
   
                   }     
          
                </NavItem>
                <NavItem>
                <Button variant="primary">
                  Sign up
                </Button>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

const mapStateToprops = state => {
  return {
    user : state.user
  }
};

const mapDispatchToProps = {
  LogoutUser:LogoutUser
}

export default connect(mapStateToprops,mapDispatchToProps)(NavMenu)
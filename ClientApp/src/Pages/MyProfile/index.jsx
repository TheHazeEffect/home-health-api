
import React from "react";
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import Tab from 'react-bootstrap/Tab'
import Nav from 'react-bootstrap/Nav'

import { Tabitems } from "./Tabitems";


export const MyProfile = () => {
    return (
        <>
            <Tab.Container id="left-tabs-example" defaultActiveKey={Tabitems[2].eventKey}>
                <Row>
                    <Col sm={3}>
                    <Nav variant="pills" className="flex-column">
                        {
                            Tabitems.map( (tabitem,i) => (
                                <Nav.Item key={i}>
                                    <Nav.Link eventKey={tabitem.eventKey}>
                                        <i className={tabitem.IconClass} /> {" "}
                                        {tabitem.Name}
                                    </Nav.Link>
                                </Nav.Item>
                            ))
                        }
                        
                    </Nav>
                    </Col>
                    <Col sm={9}>
                    <Tab.Content>
                        {
                            Tabitems.map( (tabitem,i) => (
                                <Tab.Pane eventKey={tabitem.eventKey} key={i}>
                                    <tabitem.content/>
                                </Tab.Pane>
                            ))
                        }
                        
                    </Tab.Content>
                    </Col>
                </Row>
            </Tab.Container>
        </>
    )
}
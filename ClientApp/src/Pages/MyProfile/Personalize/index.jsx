import React from "react";
import Accordion from 'react-bootstrap/Accordion'
import Card from 'react-bootstrap/Card'
import  UserSettingsSection  from "./UserSettings";
import   ProfessionalSettingsSecttion  from "./ProfessionalSettings";
import { ServiceSettings } from "./ServiceSettings";


import "./Personalize.css"

export const Personalize = () => {
    return (
        <h1>
            <Accordion >
                <Card className="CardContainer">
                    <Accordion.Toggle 
                        className="personalize-headers" 
                        as={Card.Header} 
                        eventKey="0">
                        User Settings
                    </Accordion.Toggle>
                    <Accordion.Collapse eventKey="0">
    
                        <Card.Body className="">

                            <UserSettingsSection />
    
                        </Card.Body>

                    </Accordion.Collapse>
                </Card>
                <Card className="CardContainer">
                    <Accordion.Toggle 
                        className="personalize-headers" 
                        as={Card.Header} 
                        eventKey="1">
                        Professional Settings
                    </Accordion.Toggle>
                    <Accordion.Collapse eventKey="1">
                        <ProfessionalSettingsSecttion />
                    </Accordion.Collapse>
                </Card>
                <Card className="CardContainer">
                    <Accordion.Toggle 
                        className="personalize-headers" 
                        as={Card.Header} 
                        eventKey="2">
                        Service Settings
                    </Accordion.Toggle>
                    <Accordion.Collapse eventKey="2">
                        <ServiceSettings/>
                    </Accordion.Collapse>
                </Card>
            </Accordion>
        </h1>
    );
}
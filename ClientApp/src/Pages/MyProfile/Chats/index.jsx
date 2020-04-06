import React,{useState,useEffect} from "react";
import axios from 'axios';
import Accordion from 'react-bootstrap/Accordion'
import Card from 'react-bootstrap/Card'
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import Table from 'react-bootstrap/Table'

import { LoadingSpinner } from "../../../components/LoadingSpinner";
import './Chat.css'

export const Chats = ({user}) => {

    const InitialMessageObj = {
        ReceiverId : null,
        SenderId : user.id,
        Content : null,
    }

    const [History,setHistory] = useState([])
    const [Loading,setLoading] = useState(false)
    const [Message,setMessage] = useState(InitialMessageObj)

    const handleChange = (event) => {

        const {value,name} = event.target
        console.log(name)

        console.log(value)
        console.log(Message)
        setMessage({
                ...Message,
                Content : value,
                ReceiverId: name
            })

    }
    const handleSubmit  = async () => {

        try {
            setLoading(true)

            var payload = Message
            var result = await axios
                .post("/api/messages",Message)

            if(result.status === 200){
                var conv = History.conversation
                conv.push(payload)
                setHistory({...History,conversation: conv})
            }
            console.log(result.data)
            setLoading(false)
        }catch(ex){
            console.log(ex)
            setLoading(false)
        }

    }
    useEffect( () => {

        const fetchData = async () => {
            try {
                
                setLoading(true)
                var results = await axios.get(`api/messages/chat/${user.id}`)

                console.log(results.data)
                setHistory(results.data)

                setLoading(false)
            }catch(ex) {
                setLoading(false)
                console.log(ex)
            }
        }

        fetchData()

    },[user.id])


    
    return( 
    <>
        <Accordion as={Table}>
            { Loading === true ? <div className="App-SpinnerStyle"> <LoadingSpinner Show={Loading}/> </div> : 
                History.map( (H,x) => (
                    <Card>
                        <Accordion.Toggle as={"tr"} className="ChatHead"  eventKey={H.id} key={`${H.id} - ${x}`}>
                            <i className="fas fa-user-circle"></i> {" "} 
                            {`${H.firstName} ${H.lastName} - ${H.conversation.length}`} <br />
                        </Accordion.Toggle>
                       {
                           
                            H.conversation.map( (C,i) => (
                                <Accordion.Collapse
                                    className="msgContainer"
                                    eventKey={H.id} 
                                    key={C.messageId}>
                                    <>
                                        <Card
                                            className={`msgbox ${C.senderId === user.id ? "MyMessage" : "TheirMessage "}`} 
                                        >
                                            {C.senderId === user.id ? ` ${C.content}` : `${C.content}` }
                                            
                                        </Card>
                                     </>
                                </Accordion.Collapse>
                            ))
                       }
                        <Accordion.Collapse eventKey={H.id} key={H.id}>
                            <Card.Body>
                                <Form.Control 
                                    placeholder="Enter Mesaage"
                                    onChange={handleChange}
                                    aria-label="Large" 
                                    name={H.id}
                                    aria-describedby="inputGroup-sizing-sm" />{' '}
                                <Button 
                                    className="SendMessage"
                                    variant="success"
                                    onClick={handleSubmit.bind(this,H.id)}
                                >
                                    Send Message
                                </Button>
                            </Card.Body>
                        </Accordion.Collapse>
                    </Card>
                ))
            }
        </Accordion>

    </>
    )
}
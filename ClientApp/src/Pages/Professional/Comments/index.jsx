import React, {useState,useEffect} from 'react'
import Card from 'react-bootstrap/Card'
import Axios from 'axios'
import "./Comments.css"
import { FormHoc } from "../../../HOC/FormHoc";
import {  CommentForm } from "./CommentsForm";


export const CommentSection = ({profId,userid}) => {

    const CommentEndpoint = `/api/comments`
    const initialFormState = {
        Content : "",
        SenderId: userid,
        ProfessionalId : profId
    }

    const [Comments,setComments] = useState([])
    // const [Loading,setLoading] = useState(false)
    useEffect ( () => {

        const fetchData = async () => {

            try {
                // setLoading(true)
                var result = await Axios.get(`/api/comments/professional/${profId}`)               

                if(result.status === 200) {

                    setComments(result.data);

                }
                         
                // setLoading(false)
                
                
            }catch(ex){
                // setLoading(false)
                
                console.log(ex)
            }
        }

        fetchData()

    },[profId])


    return(
        <>
            <Card className="CommentSection">
                <Card.Title>
                    Comments 
                </Card.Title>
                {FormHoc(
                    CommentEndpoint,
                    CommentForm,
                    initialFormState
                )}
                <hr/>
                {Comments.map( (c,i) => (
                    <Card.Text className="Commentbox" key={i}>
                        {c.content}
                    </Card.Text>
                ))}
                
            </Card>
        </>
    );
}

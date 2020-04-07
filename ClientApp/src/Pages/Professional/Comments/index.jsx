import React, {useState,useEffect} from 'react'
import Card from 'react-bootstrap/Card'
import Axios from 'axios'
import "./Comments.css"


export const CommentSection = ({profId}) => {

    const [Comments,setComments] = useState([])
    const [Loading,setLoading] = useState(false)
    useEffect ( () => {

        const fetchData = async () => {

            try {
                setLoading(true)
                var result = await Axios.get(`/api/Comments/professional/${profId}`)               

                if(result.status === 200) {

                    setComments(result.data);

                }
                         
                setLoading(false)
                
                
            }catch(ex){
                setLoading(false)
                
                console.log(ex)
            }
        }

        fetchData()

    },[])


    return(
        <>
            <Card className="CommentSection">
                <Card.Title>
                    Comments
                </Card.Title>
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
import { connect } from 'react-redux';
import { Chats } from "./Chats";
import {Appointments} from "./Appointments";
import {MyProfession} from "./MyProfession";


const mapStateToprops = state => {
    return {
      user : state.user
    }
  };
  

const connectedChat = connect(mapStateToprops,null)(Chats)
const connectedAppointment= connect(mapStateToprops,null)(Appointments)
const ConnecedProfession = connect(mapStateToprops,null)(MyProfession)

export const Tabitems = [
    {
        eventKey: 1,
        IconClass: "far fa-comment-dots",
        Name : "Chats",
        content: connectedChat
    },
    {
        eventKey: 2,
        IconClass: "far fa-calendar-alt",
        Name : "Appointments",
        content: connectedAppointment
    },
    {
        eventKey: 3,
        IconClass: "fas fa-user-tie",
        Name : "My Profession",
        content: ConnecedProfession
    },
]


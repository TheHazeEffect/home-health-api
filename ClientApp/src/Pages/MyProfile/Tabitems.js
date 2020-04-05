import React from "react";
import { Chats } from "./Chats";

export const Tabitems = [
    {
        eventKey: 1,
        IconClass: "far fa-comment-dots",
        Name : "Chats",
        content: <Chats />
    },
    {
        eventKey: 2,
        IconClass: "far fa-calendar-alt",
        Name : "Appointments",
        content: "This is Appointment tab stuff"
    },
    {
        eventKey: 3,
        IconClass: "fas fa-user-tie",
        Name : "My Profession",
        content: "Profession stuff"
    },
]
import React from 'react';
import { NavLink } from 'react-router-dom';
import 'bulma/css/bulma.css';

const MainMenuItem = (props) =>
    <li>
        <NavLink to={props.link} activeClassName="is-active">
            {props.text}
        </NavLink>
    </li>

export default MainMenuItem;
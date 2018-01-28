import React, { Fragment } from 'react';
import 'bulma/css/bulma.css';
import MainMenuItem from "./MainMenuItem";

const MainMenuCategory = (props) =>
    <Fragment>
        <p className="menu-label">{props.name}</p>
        <ul className="menu-list">
            {props.items.map((item, index) => <MainMenuItem key={index} text={item.name} link={item.link} />)}
        </ul>
    </Fragment>


export default MainMenuCategory;
import React from 'react';
import 'bulma/css/bulma.css';
import MainMenuCategory from './MainMenuCategory'

const MainMenu = (props) =>
    <aside className="menu">
        {props.categories.map((item, index) => (<MainMenuCategory
            key={index}
            name={item.name}
            items={item.contentComponentInfoList} />))}
    </aside>

export default MainMenu;
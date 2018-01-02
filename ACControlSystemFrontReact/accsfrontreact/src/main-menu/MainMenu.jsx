import React, {Component} from 'react';
import 'bulma/css/bulma.css';
import MainMenuCategory from './MainMenuCategory'

class MainMenu extends Component {
    constructor(props) {
        super(props);
        this.menuCategories = this.props.categories;
    }

    render() {
        return (
            <aside className="menu">
                {this
                    .menuCategories
                    .map((item, index) => (<MainMenuCategory
                        key={index}
                        name={item.name}
                        items={item.contentComponentInfoList}/>))}
            </aside>
        );
    }
}

export default MainMenu;
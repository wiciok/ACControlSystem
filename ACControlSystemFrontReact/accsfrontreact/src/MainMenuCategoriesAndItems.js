import Home from './content-components/Home';
import AcState from './content-components/AcState';


class ContentComponentInfo {
    constructor(menuName, link, componentType) {
        this.name = menuName;
        this.link = link;
        this.componentType = componentType;
    }
}


class MainMenuCategoriesAndItems {
    menuCategoriesAndItems =
        [
            {
                id: 1,
                name: "System",
                contentComponentInfoList:
                    [
                        new ContentComponentInfo("Home", "/", Home),
                    ]
            },
            {
                id: 2,
                name: "Klimatyzator",
                contentComponentInfoList: [new ContentComponentInfo("AcState", "/acState", AcState)]
            },
        ];
}


export default MainMenuCategoriesAndItems;
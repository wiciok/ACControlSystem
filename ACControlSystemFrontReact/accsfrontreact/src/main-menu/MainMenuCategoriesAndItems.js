import Home from './../content-components/Home';
import AcState from './../content-components/acstate/AcState';
import AcDevice from './../content-components/acdevice/AcDevice';

class ContentComponentInfo {
    constructor(menuName, link, componentType) {
        this.name = menuName;
        this.link = link;
        this.componentType = componentType;
    }
}

class MainMenuCategoriesAndItems {
    menuCategoriesAndItems = [
        {
            id: 1,
            name: "System",
            contentComponentInfoList: [new ContentComponentInfo("Strona główna", "/index.html", Home)]
        }, {
            id: 2,
            name: "Sterowanie klimatyzatorem",
            contentComponentInfoList: [
                new ContentComponentInfo("Stan", "/acState", AcState),
                new ContentComponentInfo("Terminarz", "/acSchedule", AcState),
                new ContentComponentInfo("Ustawienia", "/acSetting", AcState)
            ]
        }, {
            id: 3,
            name: "Urządzenia",
            contentComponentInfoList: [
                new ContentComponentInfo("Klimatyzatory", "/acDevice", AcDevice),
                new ContentComponentInfo("Urządzenia hostujące", "/hostDevice", AcState)
            ]
        }, {
            id: 4,
            name: "Użytkownicy",
            contentComponentInfoList: [
                new ContentComponentInfo("Dodaj", "/addUser", AcState),
                new ContentComponentInfo("Zarządzaj", "/manageUsers", AcState)
            ]
        }
    ];
}

export default MainMenuCategoriesAndItems;
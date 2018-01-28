import Home from './../content-components/Home';
import AcState from './../content-components/acstate/AcState';
import AcDevice from './../content-components/acdevice/AcDevice';
import UserManage from './../content-components/user-manage/UserManage';
import AcSchedule from '../content-components/acschedule/AcSchedule';
import AcSettings from '../content-components/acsettings/AcSettings';

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
            contentComponentInfoList: [new ContentComponentInfo("Strona główna", "/index", Home)]
        }, {
            id: 2,
            name: "Sterowanie klimatyzatorem",
            contentComponentInfoList: [
                new ContentComponentInfo("Stan", "/acState", AcState),
                new ContentComponentInfo("Terminarz", "/acSchedule", AcSchedule),
                new ContentComponentInfo("Ustawienia", "/acSetting", AcSettings)
            ]
        }, {
            id: 3,
            name: "Urządzenia",
            contentComponentInfoList: [
                new ContentComponentInfo("Klimatyzatory", "/acDevice", AcDevice),
            ]
        }, {
            id: 4,
            name: "Użytkownicy",
            contentComponentInfoList: [
                new ContentComponentInfo("Użytkownicy", "/user", UserManage),
            ]
        }
    ];
}

export default MainMenuCategoriesAndItems;
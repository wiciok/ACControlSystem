# ACControlSystem
[Politechnika Krakowska] Praca inżynierska - System sterowania klimatyzacją w sali laboratoryjnej
<br>[Cracow University of Technology] Engineering Thesis - Air Conditioning Management System
<br>
<br>
Air Conditioning Controlling System
Main purpose of this project is creating device, which enables old air conditioning devices with feature of advanced scheduling and controlling of their uptime, based on user-defined rules.  
Hardware part of project is based on Raspberry Pi, with additional IR receiver and transmitter.  
Software part includes two subparts: user interface web application and backend application (providing endpoints for frontend app and controlling hardware)  
Frontend app is written in React.js framework using Javascript ES6. Also, CSS Framework Bulma.io is used. It consumes APIs provided by backend app.  
Backend app is written in C# language using .NET Core platform (since 2.0 version it provides native support for Linux running on ARM64 architecture). App is using ASP.NET Core for implementation of REST API used by frontend app. Application is separated into decoupled layers, you can for example change data persistence method by easily providing other implementation of DAO class. Program implements Dependency Injection pattern by using Autofac container. Raspberry Pi’s GPIO ports are handled by dedicated C# libraries, and also C library IRSlinger used through PInvoke.

Screenshot (UI is unfortunately not localized, only polish version available):
![Login module](https://i.imgur.com/asSDZm4.png "Login module")
![State control module](hhttps://i.imgur.com/UJsX81C.png "State control module")
![Scheduling module](https://i.imgur.com/FqTngw7.png "Scheduling module")
![Settings module](https://i.imgur.com/lqvNTrA.png "Settings module")
![Air conditioning management module](https://i.imgur.com/IOIVkm8.png "Air conditioning management module")
![Users management module](https://i.imgur.com/tXvicFy.png "Users management module")


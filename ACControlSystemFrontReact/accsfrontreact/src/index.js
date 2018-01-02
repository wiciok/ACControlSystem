import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

window["apiAddress"]="http://localhost:54060/api";
ReactDOM.render(
    <App/>, document.getElementById('root'));
registerServiceWorker();

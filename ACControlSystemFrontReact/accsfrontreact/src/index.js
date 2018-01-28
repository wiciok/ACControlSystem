import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Root from './Root';
import registerServiceWorker from './registerServiceWorker';

window["apiAddress"]="http://localhost:54060/api";
ReactDOM.render(
    <Root/>, document.getElementById('root'));
registerServiceWorker();

import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Root from './Root';

window["apiAddress"]="http://192.168.1.110:5001/api";
//window["apiAddress"]="http://localhost:54060/api";
ReactDOM.render(
    <Root/>, document.getElementById('root'));

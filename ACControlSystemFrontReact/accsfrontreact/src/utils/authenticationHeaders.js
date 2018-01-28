import Cookies from 'js-cookie';

export function headerAuthFun() { return new Headers({ "Authorization": 'Basic ' + btoa(":" + Cookies.get('token')) }); }
export function headerAuthAndContentTypeJsonFun() { return new Headers([["Content-Type", "application/json"], ["Authorization", 'Basic ' + btoa(":" + Cookies.get('token'))]]); }
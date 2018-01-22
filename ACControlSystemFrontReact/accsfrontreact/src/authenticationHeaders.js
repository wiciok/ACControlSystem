import Cookies from 'js-cookie';

export let headerAuthAndContentTypeJson = new Headers([[ "Content-Type", "application/json" ],[ "Authorization", 'Basic ' + btoa(":" + Cookies.get('token')) ]]);
export let headerAuth = new Headers({ "Authorization": 'Basic ' + btoa(":" + Cookies.get('token')) });

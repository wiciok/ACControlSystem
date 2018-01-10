import React, { Component, Fragment } from 'react';

class Home extends Component {
    constructor(props) {
        super(props);
        this.name = "Home";
        this.menuName = "Strona główna";
        this.link = "/";
    }

    render() {
        return (
            <Fragment>
                <h2 className="title is-2">Strona główna</h2>
                <div className="box">
                    <span>
                        Witaj w systemie sterowania klimatyzacją!
                        <br/>
                        System umożliwia zaprogramowanie klimatyzatora do uruchomiania się i wyłączania w określonych porach oraz ręczne kontrolowanie ustawień.
                </span>
                </div>
            </Fragment>
        );
    }
}

export default Home;
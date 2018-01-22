import React, { Fragment } from 'react';

const Home = () => {
    return (
        <Fragment>
            <h2 className="title is-2">Strona główna</h2>
            <div className="box">
                <span>
                    Witaj w systemie sterowania klimatyzacją!
                    <br />
                    System umożliwia zaprogramowanie klimatyzatora do uruchomiania się i wyłączania w określonych porach oraz ręczne kontrolowanie ustawień.
                </span>
            </div>
        </Fragment>
    );
}

export default Home;
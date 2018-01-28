import React, { Fragment} from 'react';
import {Route} from 'react-router-dom';

import 'bulma/css/bulma.css';

const ContentArea = (props) =>
    <Fragment>
        {props.componentList.map(item => (item.map((innerItem, index) => 
            (<Route
                key={index}
                exact
                path={innerItem.link}
                //component={innerItem.componentType}
                render={()=><innerItem.componentType onLogout={props.onLogout}/>}
            />)))
        )}
    </Fragment>

export default ContentArea;
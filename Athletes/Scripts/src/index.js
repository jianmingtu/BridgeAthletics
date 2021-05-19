import React from 'react';
import { Switch, Route, BrowserRouter as Router } from 'react-router-dom';
import ReactDOM from 'react-dom';
import { AthleteProfile } from './components/Athlete/AthleteProfile';
import { SchoolProfile } from './components/School/SchoolProfile';

const ReactContainer = () => {
    return (
        // ----- Routing for rendering different pages -----
        <Router>
            <Switch>
                <Route path="/Athlete">
                    <AthleteProfile/>
                </Route>
                <Route path="/School">
                    <SchoolProfile/>
                </Route>
            </Switch>
        </Router>
    )
    
}

ReactDOM.render(<ReactContainer />, document.getElementById("root"));


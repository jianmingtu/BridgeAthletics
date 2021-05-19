import React from "react";
import { Button } from "react-bootstrap";


export const SchoolProfile = () => {
    return (
        <div className="App">

            <div className="school-name-container">
                <h1>The University <br/>of British Columbia</h1>
            </div>


            <div className="coach-information-container">
                <h3 className="heading-style">Coach information</h3>
                <p><strong>Head Coach:</strong></p>
                <p><strong>Assistant Coach:</strong></p>
            </div>


            <div>
                <Button className="profile-info-edit-btn-school my-5" >
                    Edit Info
                </Button>
            </div>

            <br />
            <br />
            <br />

            <div className="highlights-container">
                <h3 className="heading-style">Highlights</h3>
                <div className="highlight-video-container">
                    <Button className="profile-info-edit-btn-school my-5" >
                        Add your videos here
                    </Button>
                </div>
            </div>

            <br />
            <br />
            <br />

            <div className="other-information-container mb-5">
                <h3 className="heading-style">Other Information</h3>
                <h4><strong>Coach Philosophy:</strong></h4>
                <p><i>Edit Info to add information here</i></p>

                <h4><strong>Academic Info:</strong></h4>
                <p><i>Edit Info to add information here</i></p>
                <h4><strong>Other Information:</strong></h4>
                <p><i>Edit Info to add information here</i></p>
            </div>

        </div>
    );
}

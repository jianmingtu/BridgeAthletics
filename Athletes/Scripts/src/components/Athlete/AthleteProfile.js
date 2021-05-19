import React, { useEffect, useState } from "react";
import axios from 'axios';
import { Card, Col, Container, Row, Image, Button, Modal, Form } from "react-bootstrap";
import VideoCarousels from './VideoCarousels';

export const AthleteProfile = () => {

// -------- Defining the initial state of athlete and associated properties. In future it could just be one state variable. 
    const [athlete, setAthlete] = useState({});
    const [show, setShow] = useState(false);
    const [showUpload, setShowUpload] = useState(false)
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [dateOfBirth, setDateOfBirth] = useState("");
    const [gender, setGender] = useState("");
    const [position, setPosition] = useState("");
    const [spikeTouchFeet, setSpikeTouchFeet] = useState("");
    const [spikeTouchInches, setSpikeTouchInches] = useState("");
    const [heightFeet, setHeightFeet] = useState("");
    const [heightInches, setHeightInches] = useState("");

    const [videos, setVideos] = useState([{}]);  // initialize the athlete highlight video links

    // ------- PROFILE IMG URL -------
    const img = document.getElementById("imgUrl").value

    // Array for storing the feet and inches values to avoid duplication
    const feetInches = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11];

    const videoAction = { ADD: -1, EDIT: 0, DELTE: 1 };


    // Api call to get tha data of the athlete from the database and display on the profile
    const getAthleteInfo = async () => {
        try {
            const response = await axios.get(`/api/athleteapi/id`, {
                headers: {
                    "Access-Control-Allow-Origin": "*"
                }
            })
            const { Athlete } = response.data
            
            var height = Athlete.Height 
            setAthlete(Athlete);
            setFirstName(Athlete.FirstName);
            setLastName(Athlete.LastName);
            setGender(Athlete.Gender);
            setDateOfBirth(Athlete.DateOfBirth);
            setPosition(Athlete.Position);
            setHeightFeet(height.substr(0,1))
            setHeightInches(height.substr(2, 1))
            setSpikeTouchFeet(Athlete.SpikeTouch.substr(0, 1))
            setSpikeTouchInches(Athlete.SpikeTouch.substr(2, 1))

            if (response?.data?.HighlightVideos?.length) {
                // add an empty video after the last video if there is existing videos, for users to add a new video link
                response.data.HighlightVideos.push({ Id: -1, UrlLink: "" });
                setVideos(response.data.HighlightVideos);
            }
            else {
                // add two empty videos if there is no videos, for users to add new video link
                setVideos([{ Id: -1, UrlLink: "" }, { Id: -1, UrlLink: "" }])
            }
        } catch (error) {
            console.log("error getting athlete info", error);
        }

    }

    useEffect(() => {
        (async () => {
            await getAthleteInfo();
        })();
    }, []);

    // Api call to get edit the information of the athlete

    const updateAthleteInfo = () => {
        fetch(`/api/athleteapi/`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Id: athlete.Id,
                FirstName : firstName,
                LastName : lastName,
                DateOfBirth : dateOfBirth,
                Gender : gender,
                Position : position,
                SpikeTouch :`${spikeTouchFeet}'${spikeTouchInches}"`,
                Height :  `${heightFeet}'${heightInches}"`,
            })
        })
            .then(response => response.json())
            // Data retrieved.
            .then(json => {
                console.log(JSON.stringify(json));
               
            })
            // Data not retrieved.
            .catch(function (error) {
                console.log("error updating athlete info",error);
            })
        setShow(false);
        getAthleteInfo();
        window.location.reload()
    }

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    // send the requests to add, edit or delete a video link
    const updateHighlightVideos = (Id, value, action) => {

        // aciton is one of videoAction = { ADD: -1, EDIT: 0, DELTE: 1 }
        let act = action === videoAction.DELETE ? 'Delete' : 'Post';

        fetch(`/api/athleteapi/AddVideo`, {
            method: act,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Id: Id,
                UrlLink: value,
            })
        })
        .then(response => response.json())
        // Data retrieved.
        .then(json => {
            console.log(JSON.stringify(json));
        })
        // Data not retrieved.
        .catch(function (error) {
            console.log(error);
        })
        getAthleteInfo();
        window.location.reload()
    }

    const addVideo = (Id, value) => {
        updateHighlightVideos(Id, value, videoAction.ADD)
    }
    const editVideo = (Id, value) => {
        updateHighlightVideos(Id, value, videoAction.EDIT)
    }
    const deleteVideo = (Id) => {
        updateHighlightVideos(Id, "", videoAction.DELETE);
    }

    return (
        <div className="profile-background">
            <Container>
                <Row className="d-flex align-items-center mb-2">
                    <Col xs={12} md={8}>
                        <h3 className="heading-lg">
                            {athlete.FirstName} {athlete.LastName}
                        </h3>
                    </Col>
                    {/* ----- Upload btn on hover effect ----- */}
                    <Col xs={12} md={4} onMouseLeave={() => setShowUpload(false)} style={{minHeight: 200}}>
                        <Image src={!!img ? img : "https://www.searchpng.com/wp-content/uploads/2019/02/Profile-PNG-Icon.png"} thumbnail width={250} onMouseEnter={() => setShowUpload(true)} id="profile-img"/>
                        {
                            showUpload && <Button href="/Athlete/Upload" id="upload-img">Upload</Button> 
                        }
                    </Col>
                </Row>

                {/* -----  Edit Profile button ----- */}
               

                <Row className="mb-3">
                    <Col>
                        <Button className="profile-info-edit-btn" onClick={handleShow}>
                            Edit Info
                        </Button>
                    </Col>
                </Row>


                {/* -----  Modal to edit the athlete data ----- */}
            

                <Row>
                    <Modal show={show} onHide={handleClose} className="p-5">
                        <Modal.Header closeButton>
                            <Modal.Title>Edit Profile</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            {/*First Name*/}
                            <Form.Group controlId="firstname"className="edit-profile-form-group">
                                <Row className="d-flex align-items-center mb-2">
                                    <Col md={4}>
                                        <Form.Label>First Name</Form.Label>
                                    </Col>
                                    <Col md={8}>
                                        <Form.Control type="input" placeholder="Enter first name" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
                                    </Col>
                                </Row>
                            </Form.Group>
                            {/*Last Name*/}
                            <Form.Group controlId="lastname" className="edit-profile-form-group">
                                <Row className="d-flex align-items-center mb-2">
                                    <Col md={4}>
                                        <Form.Label>Last Name</Form.Label>
                                    </Col>
                                    <Col md={8}>
                                        <Form.Control type="input" placeholder="Enter last name" value={lastName} onChange={(e) => setLastName(e.target.value)} />
                                    </Col>
                                </Row>
                            </Form.Group>
                            {/*Date of Birth*/}
                            <Form.Group controlId="dateofbirth" className="edit-profile-form-group">
                                <Row className="d-flex align-items-center mb-2">
                                    <Col md={4}>
                                        <Form.Label>Date of Birth</Form.Label>
                                    </Col>
                                    <Col md={8}>
                                        <Form.Control
                                            type="date"
                                            placeholder="Enter date of birth"
                                            value={dateOfBirth}
                                            onChange={(e) => setDateOfBirth(e.target.value)}
                                        />
                                    </Col>
                                </Row>
                            </Form.Group>
                            {/*Gender*/}
                            <Form.Group controlId="gender" className="edit-profile-form-group">
                                <Row className="d-flex align-items-center mb-2">
                                    <Col md={4}>
                                        <Form.Label>Gender</Form.Label>
                                    </Col>
                                    <Col md={8}>
                                        <Form.Control as="select" value={gender} onChange={(e) => setGender(e.target.value)}>
                                            <option value="" disabled selected>Select Gender</option>
                                            <option value="Male">Male</option>
                                            <option value="Female">Female</option>
                                        </Form.Control>
                                    </Col>
                                </Row>
                            </Form.Group>
                            {/*Position*/}
                            <Form.Group controlId="position" className="edit-profile-form-group">
                                <Row className="d-flex align-items-center mb-2">
                                    <Col md={4}>
                                        <Form.Label>Position</Form.Label>
                                    </Col>
                                    <Col md={8}>
                                        <Form.Control as="select" value={position} onChange={(e) => setPosition(e.target.value)}>
                                            <option disabled selected>Select Position</option>
                                            <option>Middle</option>
                                            <option>Libero</option>
                                            <option>Setter</option>
                                            <option>Left Side</option>
                                            <option>Opposite</option>
                                        </Form.Control>
                                    </Col>
                                </Row>
                            </Form.Group>

                            <Form.Group controlId="height" className="edit-profile-form-group">
                                <Row className="d-flex align-items-center mb-2">
                                    <Col md={4}>
                                        <Form.Label>Height</Form.Label>
                                    </Col>
                                    <Col md={4}>
                                        <Form.Control as="select" value={heightFeet} onChange={(e) => setHeightFeet(e.target.value)}>
                                            <option disabled selected value="">Feet</option>
                                            {feetInches.map(i => {
                                                if (i > 3 && i < 9) {
                                                    return <option key={i}>{i}</option>
                                                }
                                            })}
                                        </Form.Control>
                                    </Col>
                                    <Col md={4}>
                                        <Form.Control as="select" value={heightInches} onChange={(e) => setHeightInches(e.target.value)}>
                                            <option disabled selected value="">Inches</option>
                                            {feetInches.map(i => <option key={i}>{i}</option>)}
                                        </Form.Control>
                                    </Col>
                                </Row>
                            </Form.Group>

                            <Form.Group controlId="spiketouch" className="edit-profile-form-group">
                                <Row className="d-flex align-items-center mb-2">
                                    <Col md={4}>
                                        <Form.Label>Spike Touch</Form.Label>
                                    </Col>
                                    <Col md={4}>
                                        <Form.Control as="select" value={spikeTouchFeet} onChange={(e) => setSpikeTouchFeet(e.target.value)} >
                                            <option disabled selected value="">Feet</option>
                                            {feetInches.map(i => {
                                                if (i > 4) {
                                                    return <option key={i}>{i}</option>
                                                }
                                            })}
                                        </Form.Control>
                                    </Col>
                                    <Col md={4}>
                                        <Form.Control as="select" value={spikeTouchInches} onChange={(e) => setSpikeTouchInches(e.target.value)}>
                                            <option disabled selected value="">Inches</option>
                                            {feetInches.map(i => <option key={i}>{i}</option>)}
                                        </Form.Control>
                                    </Col>
                                </Row>
                            </Form.Group>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={handleClose}>
                                Close
                            </Button>
                            <Button variant="danger" onClick={updateAthleteInfo}>
                                Save Changes
                            </Button>
                        </Modal.Footer>
                    </Modal>
                </Row>

                {/*Modal end */}




                {/* --------------- Cards to show athlete info  ----------------------- */}
                <Row>
                    <Col>
                        <Card>
                            <Card.Body>
                                <Card.Title className="heading-md">Age</Card.Title>
                                <Card.Text>{athlete.Age === 0 ? "" :  parseInt(Date().substr(10, 5)) - athlete.Age}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col>
                        <Card>
                            <Card.Body>
                                <Card.Title className="heading-md">Position</Card.Title>
                                <Card.Text>{athlete.Position}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col>
                        <Card>
                            <Card.Body>
                                <Card.Title className="heading-md">Gender</Card.Title>
                                <Card.Text>{athlete.Gender}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col>
                        <Card>
                            <Card.Body>
                                <Card.Title className="heading-md">Height</Card.Title>
                                <Card.Text>{athlete.Height}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col>
                        <Card>
                            <Card.Body>
                                <Card.Title className="heading-md">Spike Touch</Card.Title>
                                <Card.Text>{athlete.SpikeTouch}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>


                {/***************************** Athlete Videos*******************************/}
                <Row className="my-3">
                    <Col>
                        <Card>
                            <Card.Body className="text-center">
                                <VideoCarousels videos={videos} addVideo={addVideo} editVideo={editVideo} deleteVideo={deleteVideo} />
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
                <br/>
            </Container>
        </div>
    );
};

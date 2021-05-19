import React, { useState } from 'react'
import { Col, Modal, Button, Row,Form } from 'react-bootstrap';

// 
// Function: converts any youtube video links below to the embed link such as https://www.youtube.com/embed/tlTdbc5byAs.  
//           if the input is invalid then return an empty string
//  
//  https://www.youtube.com/watch?v=tlTdbc5byAs&t=73s
//  https://youtu.be/tlTdbc5byAs
//  https://www.youtube.com/embed/tlTdbc5byAs
//
const converShareYoutubeToEmbed = (video) => {

    let ret = "";

    if (video && video.UrlLink) {
        const url = video.UrlLink;
        const regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|&v=)([^#&?]*).*/;
        const match = url.match(regExp);

        ret = (match && match[2].length === 11)
            ? 'https://www.youtube.com/embed/' + match[2]
            : video.UrlLink;
    }
    return ret;
}

// show individual vides
export default function VideoCard({ video, addVideo, editVideo, deleteVideo }) {
    const actions = {
        INIT: "Init",
        ADD: "Add",
        EDIT: "Edit",
        DELETE: "Delete"
    };
    const [action, setAction] = useState(actions.INIT);
    const [value, setValue] = useState(video?.UrlLink);
    const [show, setShow] = useState(false);

    const handleClose = () => { setShow(false); }
    const handleShow = (action) => {
        setAction(action);
        // do not show vaule if adding a new video but just placehold string
        if (action === actions.ADD) {
            setValue('');
        }
        setShow(true);
    }

    // clicking on 'Add Link' or 'Edit Link' goes here 
    const handleSave = () => {
        switch (action) {
            case actions.EDIT:
                editVideo(video.Id, value)
                break;
            case actions.ADD:
                addVideo(video.Id, value)
                break;
            default:
        }

        handleClose();
    }
    
    return (
        <Col style={{ margin: 0, padding: 0 }} >
            <div class="card">
                <iframe src={converShareYoutubeToEmbed(video)} height="300" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowFullScreen="true" webkitallowfullscreen="true" mozallowfullscreen="true"></iframe>
                <div class="card-body" style={{ backgroundColor: '#333333' }}>
                    {
                        video?.Id != -1 ?
                            <div className="text-center">
                                <Button variant="danger" style={{ margin: '2px 5px' }} onClick={() => handleShow(actions.EDIT)}> Edit Link</Button>
                                <Button variant="danger" style={{ margin: '2px 5px' }} onClick={() => deleteVideo(video.Id)}> Delete Link</Button>
                            </div>
                            :
                            <div className="text-center">
                                <Button variant="danger" style={{ margin: '0 5px' }} onClick={() => handleShow(actions.ADD)}> Add Youtube Link</Button>
                            </div>
                    }
                </div>
            </div>

            {/*Show the 'Add Link' or 'Edit Link' Model dialog after clicking on the 'Add Link' or 'Edit Link' button*/}
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{action} Highlight Video Link</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group controlId="video">
                        <Row className="d-flex align-items-center">
                            <Col md={2}>
                                <Form.Label className="d-flex align-items-center" >Link:</Form.Label>
                            </Col>
                            <Col md={10}>
                                <Form.Control
                                    type="input"
                                    placeholder="Enter Youtube Embed Link"
                                    defaultValue={video?.UrlLink}
                                    onChange={(e) => setValue(e.target.value)}
                                />
                            </Col>
                        </Row>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="danger" onClick={handleSave}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </Col>
    )
}

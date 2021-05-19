import React, { useState } from 'react'
import { Col, Row } from 'react-bootstrap';
import VideoCard from './VideoCard';

// 
// In: vidoes - an array of video in the format {id, UrlLink},  here id is the video ID (integer) and UrlLink is a video link
// Out: displayVideos -  an array of two videos [{id, UrlLink}, {id, UrlLink}] so that every time clicking on the carousel's prev or next button, 
//                       will render two videos at a time.
// Addition:  1)  { Id: -1, UrlLink: "" } - a placeholder for adding an empty video
// 
const createDisplayVideos = (videos, displayVideos) => {

    let videosLen = videos.length;
    for (let i = 0; i < videosLen; i += 2) {
        // new video array
        let elements = []
        for (let v = i; (v < i + 2) && (v < videosLen); ++v) {
            elements.push(videos[v])
        }

        if (elements.length % 2 != 0) {
            // add an empty video at the last so the last frame shows two videos instead of 1 video
            elements.push({ Id: -1, UrlLink: "" });
        }

        displayVideos.push(elements);
    }
}

// show two videos in a row
export default function VideoCarousels({ videos, addVideo, editVideo, deleteVideo }) {
    const [currImg, setCurrImg] = useState(0);

    let displayVideos = [];
    let index = 0;

    // convert a list of video links to a list of pair of video links in order to render 2 videos each time
    createDisplayVideos(videos, displayVideos);

    return (
        <Row d-flex className="video-carousel" align-items-center>

            {/*left arrow*/}
            <Col
                className="left"
                onClick={() => {
                    currImg > 0 && setCurrImg(currImg - 1);
                }}
            >
                <i class="fas fa-chevron-circle-left" style={{ fontSize: 30 }}></i>
            </Col>

            {/*two vidoes in center*/}
            <Col className="center">
                <Row className="d-flex align-items-center">
                    {
                        displayVideos && displayVideos[currImg]?.map(video => <VideoCard video={video} addVideo={addVideo} editVideo={editVideo} deleteVideo={deleteVideo} />)
                    }
                </Row>
            </Col>

            {/*right arrow*/}
            <Col
                className="right"
                onClick={() => {
                    currImg < displayVideos.length - 1 && setCurrImg(currImg + 1);
                }}
            >
                <i class="fas fa-chevron-circle-right" style={{ fontSize: 30 }}></i>
            </Col>

        </Row>
    );
}
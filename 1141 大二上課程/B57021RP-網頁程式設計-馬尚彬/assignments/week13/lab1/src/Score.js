import React from "react";

export default function Score({ color, score, onClick }){
    const boardStyle = {
        fontSize: "50px",
        color: "white",
        backgroundColor: color,
        width: "150px",
        height: "150px",
        border: "none",
        margin: "15px",
    };
    
    return (
        <button style={boardStyle} onClick={onClick}>{score}</button>
    )
}
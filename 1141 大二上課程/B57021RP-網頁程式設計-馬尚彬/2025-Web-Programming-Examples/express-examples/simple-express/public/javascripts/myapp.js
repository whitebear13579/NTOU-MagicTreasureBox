function getTest() {
    fetch("hello")
        .then(response => response.json())
        .then(res => {
            document.getElementById("display").innerHTML = res.message;
        })
        .catch(err => {
            console.error("GET error:", err);
        });
}

function postTest() {
    fetch("/", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ name: "Curry" }) // 假裝有要發布的資料
    })
        .then(response => response.json())
        .then(res => {
            document.getElementById("display").innerHTML = res.status;
        })
        .catch(err => {
            console.error("POST error:", err);
        });
}

function putTest() {
    fetch("hello", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ name: "Curry" }) // 假裝有更新的資料
    })
        .then(response => response.json())
        .then(res => {
            document.getElementById("display").innerHTML = res.message;
        })
        .catch(err => {
            console.error("PUT error:", err);
        });
}

function deleteTest() {
    fetch("hello", {
        method: "DELETE"
    })
        .then(response => response.json())
        .then(res => {
            document.getElementById("display").innerHTML = res.status;
        })
        .catch(err => {
            console.error("DELETE error:", err);
        });
}
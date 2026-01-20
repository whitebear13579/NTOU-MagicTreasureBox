// 引入套件
const path = require('path');
const cookieParser = require('cookie-parser');
const logger = require('morgan');

const express = require('express');
const app = express();

app.use(cookieParser());
app.use(logger('dev'));

app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(express.static(path.join(__dirname, 'public')));

// 引入Router (routes目錄底下的todo.js)
const todoRouter = require("./routes/todo");
// 此處的/todo代表連線到該Router的基本路徑為 http://localhost:3000/todo
app.use("/todo", todoRouter);

module.exports = app;
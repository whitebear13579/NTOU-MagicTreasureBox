// 引入套件
const express = require("express");
const mongoose = require('mongoose');
const router = express.Router();

// 連接到 MongoDB 資料庫，第一個是本地端，第二個是雲端
// mongoose.connect('mongodb://localhost:27017/tododb');
mongoose.connect('mongodb+srv://admin:admin6631@cluster0.tfxu0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0'); // 連結雲端Atlas
const db = mongoose.connection;

// 與資料庫連線發生錯誤時
db.on('error', console.error.bind(console, 'Connection fails!'));

// 與資料庫連線成功連線時
db.once('open', function () {
    console.log('Connected to database...');
});

// 該collection的格式設定
const todoSchema = new mongoose.Schema({
    thing: { //事項名稱
        type: String, //設定該欄位的格式
        required: true //設定該欄位是否必填
    },
    isDone: { //是否已完成
        type: Boolean,
        required: true,
        default: false //設定預設值
    },
    createdDate: { //新增的時間
        type: Date,
        default: Date.now,
        required: true
    },
})

const Todo = mongoose.model('Todo', todoSchema);

// 取得全部資料
// 使用非同步，才能夠等待資料庫回應
router.get("/", async (req, res) => {
    // 使用try catch方便Debug的報錯訊息
    try {
        // 找出Todo資料資料表中的全部資料
        const todo = await Todo.find();
        // 將回傳的資訊轉成Json格式後回傳
        res.json(todo);
    } catch (err) {
        // 如果資料庫出現錯誤時回報 status:500 並回傳錯誤訊息 
        res.status(500).json({ message: err.message })
    }
});

// 新增待辦事項
// 將Method改為Post
router.post("/", async (req, res) => {
    // 從req.body中取出資料
    const todo = new Todo({
        thing: req.body.thing,
        isDone: req.body.isDone,
    });
    try {
        // 使用.save()將資料存進資料庫
        const newTodo = await todo.save();
        // 回傳status:201代表新增成功 並回傳新增的資料
        res.status(201).json(newTodo);
    } catch (err) {
        // 錯誤訊息發生回傳400 代表使用者傳入錯誤的資訊
        res.status(400).json({ message: err.message })
    }
});

// 在網址中傳入id用以查詢
router.get("/:id", async (req, res) => {
    try {
        const todo = await Todo.findById(req.params.id);
        if (todo == undefined) {
            return res.status(404).json({ message: "Can't find todo" })
        } else {
            return res.status(200).json(todo);
        }
    } catch (err) {
        return res.status(500).json({ message: err.message });
    }
});

// 更新代辦事項
router.put("/:id", async (req, res) => {
    try {
        // 將取出的代辦事項更新
        const newTodo = await Todo.findByIdAndUpdate(req.params.id, req.body, { new: true });
        res.json(newTodo);
    } catch (err) {
        // 資料庫操作錯誤將回傳500及錯誤訊息
        res.status(500).json({ message: "update todo failed!" });
    }
});

// 刪除代辦事項
router.delete("/:id", async (req, res) => {
    try {
        // 將取出的代辦事項刪除      
        await Todo.findByIdAndDelete(req.params.id);
        // 回傳訊息
        res.json({ message: "Delete todo successfully!" });
    } catch (err) {
        // 資料庫操作錯誤將回傳500及錯誤訊息
        console.log(err);
        res.status(500).json({ message: "remove todo failed!" });
    }
});

// Export 該Router
module.exports = router;
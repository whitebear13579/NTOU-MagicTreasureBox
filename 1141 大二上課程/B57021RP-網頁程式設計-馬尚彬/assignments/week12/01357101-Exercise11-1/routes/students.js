const express = require("express");
const mongoose = require('mongoose');
const router = express.Router();
require('dotenv').config();

//mongoose.connect('mongodb://localhost:27017/week12_hw');
mongoose.connect(process.env.MONGODB_URI);
const db = mongoose.connection;

db.on('error', console.error.bind(console, 'Connection fails!'));

db.once('open', function () {
    console.log('Connected to database...');
});

const studentsSchema = new mongoose.Schema({
    name: String,
    age: Number,
    grade: String,
});

const stu = mongoose.model('Student', studentsSchema);

router.get("/", async(req, res) => {
    try{
        const list = await stu.find();
        res.json(list);
    } catch (err){
        res.status(500).json({ message: err.message });
    }
});

router.post("/", async (req, res) => {
    const newStu = new stu({
        name: req.body.name,
        age: req.body.age,
        grade: req.body.grade
    })
    try{
        const addStu = await newStu.save();
        res.status(201).json(addStu);
    }catch(err){
        res.status(400).json({ message: err.message });
    }
});

router.delete("/:id",  async (req, res) => {
    try{
        await stu.findByIdAndDelete(req.params.id);
        res.json({ message: "Delete record successfully!" });
    }catch(err){
        console.log(err);
        res.status(500).json({ message: "Remove record failed!" });
    }
});

router.put("/:id",  async (req, res) => {
    try{
        const updStu = await stu.findByIdAndUpdate(req.params.id, req.body, { new: true });
        res.json(updStu);
    }catch(err){
        res.status(500).json({ message: "Update record failed!"});
    }
});

module.exports = router;
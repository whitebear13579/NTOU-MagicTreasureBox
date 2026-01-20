const express = require('express');
const router = express.Router();

// GET home page
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});

// GET service
router.get('/hello', function (req, res) {
  res.send({message: 'Hello World!'});
});

// POST service
router.post('/', function (req, res) {
  res.send({status: 'OK!'});
});

// PUT service
router.put('/hello', function (req, res) {
  res.send({message: 'Hello New World!'});
});

// DELETE service
router.delete('/hello', function (req, res) {
  res.send({status: 'Done!'});
});

module.exports = router;
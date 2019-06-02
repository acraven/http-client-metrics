const router = require('express').Router();

var throttle = require("express-throttle");

var options = {
  "rate": "1/s",
  "on_throttled": function(req, res, next, bucket) {
    res.status(503).send();
  }
};

router.post('/events/:name', throttle(options), (req, res) => {
  const name = req.params.name.toLowerCase();

  setTimeout(
    () => {
      res.status(201).send({name});
    },
    100 + Math.random() * 100);
});

module.exports = router;
const router = require('express').Router();

router.post('/events/:name', (req, res) => {
  const name = req.params.name.toLowerCase();

  setTimeout(
    () => {
      res.status(201).send({name});
    },
    50 + Math.random() * 50);
});

module.exports = router;
const router = require('express').Router();

router.post('/events/:name', (req, res) => {
  const name = req.params.name.toLowerCase();

  setTimeout(
    () => {
      res.status(201).send({name});
    },
    100 + Math.random() * 100);
});

module.exports = router;
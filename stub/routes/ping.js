const router = require('express').Router();

router.get('/.ping', (req, res) => {
  res.send('PONG');
});

module.exports = router;
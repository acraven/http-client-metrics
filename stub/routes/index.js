const router = require('express').Router();
const ping = require('./ping');
const events = require('./events');

router.use(ping);
router.use(events);

router.get('/*', (req, res) => {
  res.status(404).send(JSON.stringify({error: { reason: 'NOTFOUND', path: req.path }}));
});

module.exports = router;
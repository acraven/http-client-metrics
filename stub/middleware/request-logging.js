const log = require('../modules/log');

// Log requests and responses
const requestLogging = (req, res, next) => {
  if (req.originalUrl.startsWith('/.')) {
    next();
    return;
  }

  log.info(`${req.method} ${req.originalUrl}`);

  res.on('finish', () => {
    log.info(`${req.method} ${req.originalUrl} => ${res.statusCode}`);
  });

  next();
};

module.exports = requestLogging;
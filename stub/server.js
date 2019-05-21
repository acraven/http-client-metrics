const express = require('express');

const requestLogging = require('./middleware/request-logging');
const routes = require('./routes');
const log = require('./modules/log');

const port = 8080;

process.on('SIGINT', function() { log.info('Caught SIGINT. Exiting...'); process.exit(1); });
process.on('SIGTERM', function() { log.info('Caught SIGTERM. Exiting...'); process.exit(1); });

const app = express();

app.use(requestLogging);
app.use(routes);

app.listen(port, () => {
   log.info(`Listening on port ${port}!`);
});
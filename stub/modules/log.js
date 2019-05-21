exports.debug = function(message) {
   if (process.env.LOG_LEVEL && process.env.LOG_LEVEL.toUpperCase() === 'DEBUG') {
     console.log(message);
   }
 };
 
 exports.info = function(message) {
   if (
     !process.env.LOG_LEVEL ||
     process.env.LOG_LEVEL.toUpperCase() === 'INFO' ||
     process.env.LOG_LEVEL.toUpperCase() === 'DEBUG'
   ) {
     console.log(message);
   }
 };
 
 exports.error = function(...args) {
   console.error(...args);
 }; 
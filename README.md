choco install superbenchmarker

sb -u "http://localhost:8090/decorated" -c 1 -n 100 -m POST -B

Other Ideas
-----------
* Importance of using context.RequestAborted (CancellationToken)
* Using polly retry means using timeout policy instead of httpclient timeout (TimeoutRejectedException in retry and circuit breaker policies)
* Polly rate limiting

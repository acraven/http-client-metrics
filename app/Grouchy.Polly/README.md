ConcurrencyLimitPolicy - max n concurrent requests - no limit to rate in any period
    No of concurrent actions inside the policy execute

RateLimitPolicy - start n in period t, with burst b - no limit to concurrent requests
    No of actions through the policy execute in a time period

IConcurrencyLimitStrategy
    Task WaitAsync() - wait (for maxwait) or (and) throw ConcurrencyLimitRejectionException
    void Release()

IRateLimitStrategy
    Task WaitAsync() - wait (for maxwait) or (and) throw RateLimitRejectionException

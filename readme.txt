# Pokemon API

## What I would do differently in production:
1. Host on Azure (would need build.yaml file)
1. Use Azure to add DNS.
1. Upgrade to standard plan so can use horizontal scaling if needed.(Load balancer)
1. Add rate limiting, to prevent DDOS attacks, keep availability high.
1. Cache the stuff I read in a database, so that I don't abuse the APIs I call.
1. Add some End2End tests, that run after each deployment.
1. Add logging.
1. Add monitoring for SRE.
1. If you had some paying customers, then SSO to identify them. (they might want to pay for more API use.).

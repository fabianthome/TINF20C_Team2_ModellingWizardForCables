The CableWizardBackend directory contains an executable to run the backend. The backend is currently configured to listen to port 5000 for HTTP and port 5001 for HTTPS.

The CableWizardFrontend directory contains browser bundles that can be served by a webserver, however the backend's CORS policy currently only allows requests from http://localhost:4200.

A running demo version of this application can be found on https://cable.sowiho.de/.
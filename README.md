# Open Fiber Azure Function

> A simple Azure Function to monitor the Open Fiber Site.

<br>

OpenFiberAzureFunction as the title says, it's an Azure Function running every work day at 6AM, that checks the Open Fiber page for the fiber connection coverage of a specified address, and then send an email with the result.

Open Fiber it's an Italian company working on bringing fiber connection to a lot of small villages, mine included, tired of checking the coverage page every morning, i decided to develop this function that checks the page for me, and sends me an email with the coverage status, that can be FTTH, FWA or no coverage at all.

<br>

## How has been made? 

This Function has been made using C# and net core 3.1 with VS Code, Azure Function has been used as the infrastructure, and Angle Sharp and SendGrid for parsing the page and sending the final email.

<br>

### Developed by Alex Pagnotta

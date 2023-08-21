# In-Webo Web Services API ASP.NET Development Kit


## Description
This package, provided by inWebo Technologies, includes a set of ASP.NET (C#) sample code to integrate inWebo Authentication and Provisioning APIs in any PHP-based Web site.

## Getting Started
Before you start writing code, you need to have an InWebo administrator account. You can get one at: https://www.myinwebo.com/signup 
When logged in to inWebo Administration Console, go to tab "Secure Sites". From this screen, you will be able to get:

* your inWebo Service ID
* WEB SERVICES API ACCESS certificate file

These 2 items are mandatory. Once your have them:

- Copy the certificate file on your computer (in a convenient place, as you see fit)
- Go the "src" directory of the package:
    * Rename file "Web.config-sample" in "Web.config"
    * Edit this file and modify parameters accordingly:
        - path to the log file
        - path to the certificate file
        - certificate passphrase
        - inWebo Service ID
        - You may also want to add an inWebo user login name to your configuration file, to test some additional functions. Use your own login or create a new one as you see fit
- Run the Visual Studio solution "websitedemo.sln"

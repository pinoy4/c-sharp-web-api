#INTRO

This project is meant to test and combine the most common features of a web API.
It is made up of a web app in `C#` and a database running `postgresql`.

#SETUP

To start the database run `docker-compose up` in the terminal from the root
folder of the project. This will start two containers:

- postgres: runs the database;  
    username=`mwtest`, password=`mwtest`, port=`5432`, database=`mwtest`
- pgadmin: eases access to the database. It is reachable at `localhost:5050`;  
    username=`pgadmin4@pgadmin.org`, password=`admin`

#FEATURES:

- [x] database connection
- [ ] database migrations
- [ ] automatic swagger documentation
- [ ] authentication
- [ ] authorization

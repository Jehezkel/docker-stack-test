# docker-stack-test
## docker-compose.yml

```
podman compose -f docker-compose.yml up -d
```
### Nginx
For static files serving and backend proxy
Config in nginx folder
Static files from angular-dist volume
### angular-build
Build process that outputs file onto angular-dist volume
### database
Postgres with volume for persistance


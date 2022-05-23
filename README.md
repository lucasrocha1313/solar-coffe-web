# solar-coffe-web
Project to practice vue and dotnet

## Backend
```
dotnet run
```
## Frontend
```
npm run server
```

## Database for development and tests
Inside the **db** folder run:
```
docker-compose up
```

The database will be available on _localhost:5432_. The credentials are visible on file *dbcredentials.env*

*PS: This docker-compose will create a volume called dbvol.*

### Creating the database schema
It was created a Makefile with the necessary command to create the database. Inside the backend folder just run:
```
make db
```
# AdminByRequestPoc

Opgaveløsningen er baseret på en .NET Core ramme med ASP.NET Core 8.

---------------------------------------------------------------------

Der findes p.t. 3 projekter hhv.:
- Gateway.Api
- OrderProcessingService
- ProductCatalogService

---------------------------------------------------------------------

Funktioner:
- Gateway.Api er tænkt at skulle forestå routing up- og downstreaming af requests og responses fra og til klienter og videre til de relevante services.
- ProductCatalog servicen håndterer produkter og typer af produkter i form af produktoprettelse, listning, opdatering, sletning etc.
- OrderProcessing servicen håndterer ordreoprettelse, opdatering, statustracking, listning, sletning etc.

---------------------------------------------------------------------

DataStorage:
- ProductCatalog servicen er p.t. baseret på en SQLExpress database.
- OrderProcessing servicen baseres ligeledes på SQL-Server.

Ved valg af datastorage har det været overvejet at benytte en NoSql database i form af MongoDb for ProductCatalog servicen.
Det kunne være hensigtsmæssigt for at løsne de bånd, som en relationel database ligger på en datamodel for produkter.
Det konkrete valg er foretaget af tidsmæssige årsager, men der foregår en migrering fra SQL-Server til MongoDb.
Ligeledes kunne det være relevant at vurdere om Azure eller tilsvarende cloud storage modeller kunne bidrage til en optimal
storageløsning mht. skalering, sharding osv. 

---------------------------------------------------------------------

Arkitektur:
Arkitekturen for hver af de indgående microservices er forsøgt holdt i en "Clean Architecture for Microservices" form med brug af
et Repository pattern. Andre modeller har været overvejet, fx. en arkitektur baseret på Hexagonal arkitektur.

- Core: Indeholder domæne, forretningslogik og interfaces hertil
	- Business:
		- Interfaces: 
	- Domain:
		- Entities: De enkelte modelklasser fx. Product og Category
- Infrastructure: Indeholder diverse omkring infrastructure, EF 
	- Data: Contextbeskrivelse til EF
	- Migrations: Migrationsscripts til EF
	- Repositories:
- Presentation: Indeholder controllerklasser, client setup osv.
	- Controllers: Controller klasse for ProductCatalog

Tilgang til begge services er baseret på RESTful api'er for CRUD operationer.
Der er til api'erne implementeret API versionering.
Interaktion imellem services kunne for simpelheds skyld være baseret på synkron http REST kald. Det er ikke optimalt i produktionsøjemed.
I stedet kunne overvejes brug af asynkron interaktion evt. i form af en event/messagebus.

Logning:
Logning er p.t. ikke implementeret, men ELK kunne være en mulighed.

---------------------------------------------------------------------

Performance overvejelser:
Der er p.t. ikke implementeret cashing. Flere cashing modeller kunne overvejes, herunder Output caching, Responsing cashing og eller 
in-memory cashing.
I videst mulige omfang bør det tilstræbes at benytte asynkron modelarkitektur ved produktionsløsningen.

---------------------------------------------------------------------

Sikkerhed:
JWT-authentication: Er p.t. ikke implementeret
Rate-limiting: Rate-limiting er delvist implementeret på ProductCatalog services (Get) for at forhindre bl.a. DDOS angreb.

---------------------------------------------------------------------

Skalering:
Der er foretaget indledende opsætning til Docker containerisering og foretaget overvelser omkring cashing og database sharding
uden at disse har resulteret i konkrete implementeringer.
Det vurderes at containerisering og cashing vil være de umiddelbart nemmeste at implmentere.

---------------------------------------------------------------------

Test:
Der er p.t. ikke implementeret nogen form for tests. Unittesting af de enkelte services vil være oplagt.
De enkelte services kan testes vha. Swagger eller Postman.

---------------------------------------------------------------------

Kør:
Applicationen kan p.t. afvikles i fx. Visual Studio 2022 ved at clone applikationen fra GitHub. 
Der skal være installeret en SQLExpress database, som applikationen kan få adgang til. 
Tilret evt. ConnectionString i AppSettings.json filen for hver service.

Bemærk: 
- OrderProcessing service er ikke implementeret færdig.
- Gateway.Api er ikke færdiggjort.
- Exception handling er kun delvist udført.
- Der forefindes andre fejl og mangler i applikationen.

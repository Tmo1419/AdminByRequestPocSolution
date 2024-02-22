# AdminByRequestPoc

Opgavel�sningen er baseret p� en .NET Core ramme med ASP.NET Core 8.

---------------------------------------------------------------------

Der findes p.t. 3 projekter hhv.:
- Gateway.Api
- OrderProcessingService
- ProductCatalogService

---------------------------------------------------------------------

Funktioner:
- Gateway.Api er t�nkt at skulle forest� routing up- og downstreaming af requests og responses fra og til klienter og videre til de relevante services.
- ProductCatalog servicen h�ndterer produkter og typer af produkter i form af produktoprettelse, listning, opdatering, sletning etc.
- OrderProcessing servicen h�ndterer ordreoprettelse, opdatering, statustracking, listning, sletning etc.

---------------------------------------------------------------------

DataStorage:
- ProductCatalog servicen er p.t. baseret p� en SQLExpress database.
- OrderProcessing servicen baseres ligeledes p� SQL-Server.

Ved valg af datastorage har det v�ret overvejet at benytte en NoSql database i form af MongoDb for ProductCatalog servicen.
Det kunne v�re hensigtsm�ssigt for at l�sne de b�nd, som en relationel database ligger p� en datamodel for produkter.
Det konkrete valg er foretaget af tidsm�ssige �rsager, men der foreg�r en migrering fra SQL-Server til MongoDb.
Ligeledes kunne det v�re relevant at vurdere om Azure eller tilsvarende cloud storage modeller kunne bidrage til en optimal
storagel�sning mht. skalering, sharding osv. 

---------------------------------------------------------------------

Arkitektur:
Arkitekturen for hver af de indg�ende microservices er fors�gt holdt i en "Clean Architecture for Microservices" form med brug af
et Repository pattern. Andre modeller har v�ret overvejet, fx. en arkitektur baseret p� Hexagonal arkitektur.

- Core: Indeholder dom�ne, forretningslogik og interfaces hertil
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

Tilgang til begge services er baseret p� RESTful api'er for CRUD operationer.
Der er til api'erne implementeret API versionering.
Interaktion imellem services kunne for simpelheds skyld v�re baseret p� synkron http REST kald. Det er ikke optimalt i produktions�jemed.
I stedet kunne overvejes brug af asynkron interaktion evt. i form af en event/messagebus.

Logning:
Logning er p.t. ikke implementeret, men ELK kunne v�re en mulighed.

---------------------------------------------------------------------

Performance overvejelser:
Der er p.t. ikke implementeret cashing. Flere cashing modeller kunne overvejes, herunder Output caching, Responsing cashing og eller 
in-memory cashing.
I videst mulige omfang b�r det tilstr�bes at benytte asynkron modelarkitektur ved produktionsl�sningen.

---------------------------------------------------------------------

Sikkerhed:
JWT-authentication: Er p.t. ikke implementeret
Rate-limiting: Rate-limiting er delvist implementeret p� ProductCatalog services (Get) for at forhindre bl.a. DDOS angreb.

---------------------------------------------------------------------

Skalering:
Der er foretaget indledende ops�tning til Docker containerisering og foretaget overvelser omkring cashing og database sharding
uden at disse har resulteret i konkrete implementeringer.
Det vurderes at containerisering og cashing vil v�re de umiddelbart nemmeste at implmentere.

---------------------------------------------------------------------

Test:
Der er p.t. ikke implementeret nogen form for tests. Unittesting af de enkelte services vil v�re oplagt.
De enkelte services kan testes vha. Swagger eller Postman.

---------------------------------------------------------------------

K�r:
Applicationen kan p.t. afvikles i fx. Visual Studio 2022 ved at clone applikationen fra GitHub. 
Der skal v�re installeret en SQLExpress database, som applikationen kan f� adgang til. 
Tilret evt. ConnectionString i AppSettings.json filen for hver service.

Bem�rk: 
- OrderProcessing service er ikke implementeret f�rdig.
- Gateway.Api er ikke f�rdiggjort.
- Exception handling er kun delvist udf�rt.
- Der forefindes andre fejl og mangler i applikationen.

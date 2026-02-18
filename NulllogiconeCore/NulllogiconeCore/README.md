# Null Logic One

The semantic API behind OLI-it, https://www.oli-it.com  
All Entitites with endpoints for html, json, and RDF

> This repository is a fork from an old OLI-it project in Azure DevOps.  


## Goal

The goal is to have a RDF API again for OLI-it deployed to https://nulllogicone.net/ 


## Visualize

https://issemantic.net/rdf-visualizer

## Approach

 create a new dotnet Core Web + Api project for html, json and rdf output.

## ToDo

[x] Move legacy projects to archive folder, delete it soon.  
[x] Deploy to real Azure App Service site, configure DNS.  
[x] Create GitHub Actions for automated deployment  
[x] Add SPARQL endpoint (deployed as Ontop container in Azure)  
[ ] Add more OLI-it entities (SAPCT-NKBZ).  
[ ] Improve RDF output with more ontologies.  

## Available Resources

### Static Files
- `/schema.rdfs` - RDF Schema ontology definition
- `/example-queries.sparql` - Example SPARQL queries for common use cases

### SPARQL Endpoint
The SPARQL endpoint is available internally at `http://10.0.3.4:8080/sparql` (private Azure Container Instance)

Example query:
```sparql
PREFIX nlo: <http://nulllogicone.net/schema.rdfs#>

SELECT ?stamm ?name ?email WHERE {
  ?stamm a nlo:Stamm ;
         nlo:name ?name ;
         nlo:email ?email .
} LIMIT 10
```

See `/example-queries.sparql` for more query examples including Wurzeln (junction table) queries.



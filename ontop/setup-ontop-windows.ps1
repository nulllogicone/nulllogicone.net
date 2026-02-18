# Run Ontop Native on Windows (for LocalDB)

# This script runs Ontop directly on Windows to support LocalDB named pipe connections

Write-Host "Setting up Ontop for Windows..." -ForegroundColor Cyan

# Download Ontop CLI
$ontopVersion = "5.5.0"
$ontopUrl = "https://github.com/ontop/ontop/releases/download/ontop-$ontopVersion/ontop-cli-$ontopVersion.zip"
$ontopZip = "ontop-cli.zip"
$ontopDir = "ontop-cli"

if (!(Test-Path $ontopDir)) {
    Write-Host "Downloading Ontop CLI..." -ForegroundColor Yellow
    Invoke-WebRequest -Uri $ontopUrl -OutFile $ontopZip -UseBasicParsing
    
    Write-Host "Extracting..." -ForegroundColor Yellow
    Expand-Archive -Path $ontopZip -DestinationPath $ontopDir -Force
    Remove-Item $ontopZip
    Write-Host "Ontop CLI installed!" -ForegroundColor Green
}

# Update ontop.properties for Windows LocalDB
$propsContent = @"
# Ontop Configuration for LocalDB (Windows Named Pipes)

jdbc.url=jdbc:sqlserver://(localdb)\\MSSQLLocalDB;databaseName=null;integratedSecurity=false;encrypt=false;trustServerCertificate=true
jdbc.driver=com.microsoft.sqlserver.jdbc.SQLServerDriver
jdbc.user=oli
jdbc.password=Ko-0L1-oK

# Ontology and mapping files
ontop.ontologyFile=nulllogicone.ttl
ontop.mappingFile=nulllogicone.obda.ttl

# SPARQL endpoint configuration
ontop.endpoint.port=8080
ontop.endpoint.cors-allowed-origins=*
ontop.log.level=INFO
"@

Set-Content -Path "ontop-localdb.properties" -Value $propsContent
Write-Host "Created ontop-localdb.properties for LocalDB" -ForegroundColor Green

# Copy JDBC driver
Write-Host "Copying JDBC driver..." -ForegroundColor Yellow
Copy-Item -Path "jdbc\*.jar" -Destination "$ontopDir\jdbc\" -Force

Write-Host "`n✅ Setup complete!" -ForegroundColor Green
Write-Host "`nTo start the SPARQL endpoint:" -ForegroundColor Cyan
Write-Host "  .\ontop-cli\ontop endpoint -p ontop-localdb.properties" -ForegroundColor Yellow
Write-Host "`nThen open: http://localhost:8080/" -ForegroundColor Yellow

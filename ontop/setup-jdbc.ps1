# Download SQL Server JDBC Driver for Ontop

Write-Host "Downloading Microsoft SQL Server JDBC Driver..." -ForegroundColor Cyan

# Create jdbc directory if it doesn't exist
$jdbcDir = Join-Path $PSScriptRoot "jdbc"
if (-not (Test-Path $jdbcDir)) {
    New-Item -Path $jdbcDir -ItemType Directory | Out-Null
    Write-Host "Created directory: $jdbcDir" -ForegroundColor Green
}

# Download from Maven Central (more reliable)
$version = "12.6.2.jre11"
$downloadUrl = "https://repo1.maven.org/maven2/com/microsoft/sqlserver/mssql-jdbc/12.6.2.jre11/mssql-jdbc-$version.jar"
$jarFile = Join-Path $jdbcDir "mssql-jdbc-$version.jar"

try {
    # Download the driver directly
    Write-Host "Downloading from Maven Central..." -ForegroundColor Yellow
    $ProgressPreference = 'SilentlyContinue'  # Speed up download
    Invoke-WebRequest -Uri $downloadUrl -OutFile $jarFile -UseBasicParsing
    
    # Verify download
    if ((Get-Item $jarFile).Length -lt 100000) {
        throw "Download failed or file is too small"
    }
    
    Write-Host "Downloaded successfully! $('{0:N2}' -f ((Get-Item $jarFile).Length / 1MB)) MB" -ForegroundColor Green
    Write-Host "Saved to: $jarFile" -ForegroundColor Green

    Write-Host "`nSetup complete! JDBC driver is ready in: $jdbcDir" -ForegroundColor Cyan
    Write-Host "Driver file: $(Split-Path $jarFile -Leaf)" -ForegroundColor Green
    Write-Host "`nYou can now start Ontop with: docker-compose up -d" -ForegroundColor Yellow

} catch {
    Write-Host "Error: $_" -ForegroundColor Red
    if (Test-Path $jarFile) {
        Write-Host "Cleaning up failed download..." -ForegroundColor Yellow
        Remove-Item $jarFile -Force -ErrorAction SilentlyContinue
    }
    exit 1
}

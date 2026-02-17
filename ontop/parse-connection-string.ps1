# Parse SQL Connection String from Key Vault and create separate secrets
# This helps if you have a full connection string but need individual components

param(
    [Parameter(Mandatory=$true)]
    [string]$KeyVaultName,
    
    [Parameter(Mandatory=$true)]
    [string]$ConnectionStringSecretName,
    
    [switch]$DryRun
)

Write-Host "Fetching connection string from Key Vault: $KeyVaultName" -ForegroundColor Cyan

# Get the connection string from Key Vault
$connectionString = az keyvault secret show --vault-name $KeyVaultName --name $ConnectionStringSecretName --query value -o tsv

if (-not $connectionString) {
    Write-Error "Failed to retrieve connection string from Key Vault"
    exit 1
}

Write-Host "Connection string retrieved successfully" -ForegroundColor Green
Write-Host "Parsing connection string..." -ForegroundColor Cyan

# Parse connection string components
$parts = $connectionString -split ';'
$server = ""
$database = ""
$user = ""
$password = ""

foreach ($part in $parts) {
    if ($part -match '(?i)^(Server|Data Source)=(.+)') {
        $server = $matches[2]
    }
    elseif ($part -match '(?i)^(Database|Initial Catalog)=(.+)') {
        $database = $matches[2]
    }
    elseif ($part -match '(?i)^(User Id|User|UserId|UID)=(.+)') {
        $user = $matches[2]
    }
    elseif ($part -match '(?i)^(Password|Pwd)=(.+)') {
        $password = $matches[2]
    }
}

# Display parsed values
Write-Host "`nParsed components:" -ForegroundColor Yellow
Write-Host "  Server: $server"
Write-Host "  Database: $database"
Write-Host "  User: $user"
Write-Host "  Password: $('*' * $password.Length)"

if (-not $server -or -not $user -or -not $password) {
    Write-Error "Failed to parse all required components from connection string"
    exit 1
}

if ($DryRun) {
    Write-Host "`n[DRY RUN] Would create these secrets:" -ForegroundColor Yellow
    Write-Host "  - sql-server"
    Write-Host "  - sql-database"
    Write-Host "  - sql-user"
    Write-Host "  - sql-password"
    Write-Host "`nRun without -DryRun to actually create secrets" -ForegroundColor Cyan
    exit 0
}

# Create individual secrets in Key Vault
Write-Host "`nCreating individual secrets in Key Vault..." -ForegroundColor Cyan

az keyvault secret set --vault-name $KeyVaultName --name "sql-server" --value $server
Write-Host "  ✓ Created sql-server" -ForegroundColor Green

az keyvault secret set --vault-name $KeyVaultName --name "sql-database" --value $database
Write-Host "  ✓ Created sql-database" -ForegroundColor Green

az keyvault secret set --vault-name $KeyVaultName --name "sql-user" --value $user
Write-Host "  ✓ Created sql-user" -ForegroundColor Green

az keyvault secret set --vault-name $KeyVaultName --name "sql-password" --value $password
Write-Host "  ✓ Created sql-password" -ForegroundColor Green

Write-Host "`n✓ All secrets created successfully!" -ForegroundColor Green
Write-Host "`nYou can now reference these in Azure Container Instance:" -ForegroundColor Cyan
Write-Host "  SQL_SERVER: @Microsoft.KeyVault(SecretUri=https://$KeyVaultName.vault.azure.net/secrets/sql-server/)"
Write-Host "  SQL_USER: @Microsoft.KeyVault(SecretUri=https://$KeyVaultName.vault.azure.net/secrets/sql-user/)"
Write-Host "  SQL_PASSWORD: @Microsoft.KeyVault(SecretUri=https://$KeyVaultName.vault.azure.net/secrets/sql-password/)"

# Secure ACI Deployment Script
# Fetches secrets from Key Vault and deploys with secure parameters

param(
    [string]$ResourceGroup = "Default-Web-WestEurope",
    [string]$KeyVaultName = "oli-it-kv-test",
    [string]$ContainerName = "nullsparql"
)

# Get current public IP
Write-Host "Getting current public IP address..." -ForegroundColor Cyan
$myIp = (Invoke-RestMethod -Uri "https://api.ipify.org?format=text" -ErrorAction Stop).Trim()
Write-Host "Current IP: $myIp" -ForegroundColor Yellow

# Add IP to Key Vault firewall
Write-Host "Adding IP to Key Vault firewall..." -ForegroundColor Cyan
$addRule = az keyvault network-rule add --name $KeyVaultName --ip-address $myIp 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Warning "Failed to add firewall rule (may already exist)"
}

# Wait for firewall rule to propagate
Write-Host "Waiting for firewall rule to propagate..." -ForegroundColor Cyan
Start-Sleep -Seconds 10

try {
    Write-Host "Fetching secrets from Key Vault: $KeyVaultName" -ForegroundColor Cyan

    # Fetch secrets from Key Vault
    $sqlServer = az keyvault secret show --vault-name $KeyVaultName --name sql-server --query value -o tsv
    $sqlUser = az keyvault secret show --vault-name $KeyVaultName --name sql-user --query value -o tsv
    $sqlPassword = az keyvault secret show --vault-name $KeyVaultName --name sql-password --query value -o tsv
    $logAnalyticsWorkspaceId = az keyvault secret show --vault-name $KeyVaultName --name log-analytics-workspace-id --query value -o tsv
    $logAnalyticsWorkspaceKey = az keyvault secret show --vault-name $KeyVaultName --name log-analytics-workspace-key --query value -o tsv

    if (-not $sqlServer -or -not $sqlUser -or -not $sqlPassword -or -not $logAnalyticsWorkspaceId -or -not $logAnalyticsWorkspaceKey) {
        Write-Error "Failed to fetch secrets from Key Vault"
        Write-Warning "Make sure you ran setup-keyvault-secrets.ps1 first to store all secrets"
        exit 1
    }

Write-Host "Deploying container with secure environment variables..." -ForegroundColor Cyan

    # Delete existing container to ensure consistent IP assignment
    Write-Host "Checking for existing container..." -ForegroundColor Gray
    $existingContainer = az container show --name $ContainerName --resource-group $ResourceGroup 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Deleting existing container to preserve IP..." -ForegroundColor Yellow
        az container delete --name $ContainerName --resource-group $ResourceGroup --yes --no-wait 2>&1 | Out-Null
        Write-Host "Waiting for cleanup..." -ForegroundColor Gray
        Start-Sleep -Seconds 15
    }

    # Deploy using Azure CLI with secure environment variables
    az container create `
        --resource-group $ResourceGroup `
        --name $ContainerName `
        --image oliitacr.azurecr.io/nulllogicone-ontop:latest `
        --cpu 0.5 `
        --memory 1.0 `
        --ports 8080 `
        --protocol TCP `
        --os-type Linux `
        --restart-policy Always `
        --ip-address Private `
        --vnet "/subscriptions/33dd8226-abb3-4f36-b1f0-059e18b9570a/resourceGroups/Default-Network-WestEurope/providers/Microsoft.Network/virtualNetworks/OLI-it-VNet" `
        --subnet "container" `
        --assign-identity "/subscriptions/33dd8226-abb3-4f36-b1f0-059e18b9570a/resourcegroups/Default-Web-WestEurope/providers/Microsoft.ManagedIdentity/userAssignedIdentities/nullsparql-identity" `
        --acr-identity "/subscriptions/33dd8226-abb3-4f36-b1f0-059e18b9570a/resourcegroups/Default-Web-WestEurope/providers/Microsoft.ManagedIdentity/userAssignedIdentities/nullsparql-identity" `
        --log-analytics-workspace $logAnalyticsWorkspaceId `
        --log-analytics-workspace-key $logAnalyticsWorkspaceKey `
        --secure-environment-variables `
            SQL_SERVER=$sqlServer `
            SQL_USER=$sqlUser `
            SQL_PASSWORD=$sqlPassword

    if ($LASTEXITCODE -eq 0) {
        Write-Host "`nDeployment successful!" -ForegroundColor Green
        Write-Host "Secrets are now stored securely (not visible in Portal)" -ForegroundColor Green
        
        # Get and display the assigned private IP
        $containerIp = az container show --name $ContainerName --resource-group $ResourceGroup --query "properties.ipAddress.ip" -o tsv
        if ($containerIp) {
            Write-Host "`nContainer Private IP: $containerIp" -ForegroundColor Cyan
            Write-Host "Make sure your .NET app uses: http://${containerIp}:8080/sparql" -ForegroundColor Yellow
        }
    } else {
        Write-Error "Deployment failed"
        exit 1
    }

    # Clear sensitive variables from memory
    $sqlServer = $null
    $sqlUser = $null
    $sqlPassword = $null
    $logAnalyticsWorkspaceId = $null
    $logAnalyticsWorkspaceKey = $null
}
finally {
    # Always remove IP from Key Vault firewall
    Write-Host "`nRemoving IP from Key Vault firewall..." -ForegroundColor Cyan
    $removeRule = az keyvault network-rule remove --name $KeyVaultName --ip-address $myIp 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "IP removed successfully" -ForegroundColor Green
    } else {
        Write-Warning "Failed to remove IP (you may need to remove it manually)"
    }
}

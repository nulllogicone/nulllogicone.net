# Start NulllogiconeCore with automatic port cleanup
# This script ensures clean startup by stopping any conflicting processes first

Write-Host "Starting NulllogiconeCore with port cleanup..." -ForegroundColor Green

# Run cleanup first
& "$PSScriptRoot\cleanup-ports.ps1"

# Wait a moment for processes to fully terminate
Start-Sleep -Seconds 2

# Navigate to API project and start
Set-Location "$PSScriptRoot\NulllogiconeCore\NulllogiconeCore"

Write-Host "Starting API server..." -ForegroundColor Green
dotnet run

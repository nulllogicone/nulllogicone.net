# Cleanup script to stop all processes using ports 5131 and 7045
# Run this before starting your API if you get "address already in use" errors

Write-Host "Checking for processes using ports 5131 and 7045..." -ForegroundColor Yellow

# Check port 5131 (HTTP)
$port5131 = netstat -ano | Select-String ":5131.*LISTENING"
if ($port5131) {
    $processIds = $port5131 | ForEach-Object { ($_ -split '\s+')[-1] } | Sort-Object -Unique
    foreach ($processId in $processIds) {
        try {
            $process = Get-Process -Id $processId -ErrorAction Stop
            Write-Host "Stopping process: $($process.ProcessName) (PID: $processId) on port 5131" -ForegroundColor Red
            Stop-Process -Id $processId -Force
        }
        catch {
            Write-Host "Process $processId already terminated" -ForegroundColor Gray
        }
    }
}

# Check port 7045 (HTTPS)
$port7045 = netstat -ano | Select-String ":7045.*LISTENING"
if ($port7045) {
    $processIds = $port7045 | ForEach-Object { ($_ -split '\s+')[-1] } | Sort-Object -Unique
    foreach ($processId in $processIds) {
        try {
            $process = Get-Process -Id $processId -ErrorAction Stop
            Write-Host "Stopping process: $($process.ProcessName) (PID: $processId) on port 7045" -ForegroundColor Red
            Stop-Process -Id $processId -Force
        }
        catch {
            Write-Host "Process $processId already terminated" -ForegroundColor Gray
        }
    }
}

# Stop any remaining dotnet processes
$dotnetProcesses = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue
if ($dotnetProcesses) {
    Write-Host "Stopping remaining dotnet processes..." -ForegroundColor Red
    $dotnetProcesses | Stop-Process -Force
}

Write-Host "Port cleanup completed!" -ForegroundColor Green
Write-Host "Ports 5131 and 7045 should now be available." -ForegroundColor Green

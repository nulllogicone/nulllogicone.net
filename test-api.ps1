# Nulllogicone API Test Script
# Test the API endpoints to verify they're working correctly

Write-Host "Testing Nulllogicone API..." -ForegroundColor Green
$baseUrl = "http://localhost:5131"

# Test API info endpoint
Write-Host "`nTesting /about endpoint..." -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$baseUrl/about" -Method Get
    Write-Host "✓ API Info:" -ForegroundColor Green
    $response | ConvertTo-Json -Depth 3
} catch {
    Write-Host "✗ Failed to get API info: $($_.Exception.Message)" -ForegroundColor Red
}

# Test Stamm endpoints
Write-Host "`nTesting /stamm endpoints..." -ForegroundColor Yellow
try {
    $stamms = Invoke-RestMethod -Uri "$baseUrl/stamm" -Method Get
    Write-Host "✓ GET /stamm - Found $($stamms.Count) Stamm entries:" -ForegroundColor Green
    $stamms | ForEach-Object { Write-Host "  - [$($_.Id)] $($_.Name): $($_.Description)" -ForegroundColor Cyan }
    
    if ($stamms.Count -gt 0) {
        $firstStamm = Invoke-RestMethod -Uri "$baseUrl/stamm/$($stamms[0].Id)" -Method Get
        Write-Host "✓ GET /stamm/$($stamms[0].Id) - Details:" -ForegroundColor Green
        Write-Host "  Name: $($firstStamm.Name)" -ForegroundColor Cyan
        Write-Host "  Description: $($firstStamm.Description)" -ForegroundColor Cyan
    }
} catch {
    Write-Host "✗ Failed to test Stamm endpoints: $($_.Exception.Message)" -ForegroundColor Red
}

# Test PostIt endpoints
Write-Host "`nTesting /postit endpoints..." -ForegroundColor Yellow
try {
    $postits = Invoke-RestMethod -Uri "$baseUrl/postit" -Method Get
    Write-Host "✓ GET /postit - Found $($postits.Count) PostIt entries:" -ForegroundColor Green
    $postits | ForEach-Object { 
        $status = if ($_.IsCompleted) { "✓" } else { "○" }
        Write-Host "  - [$($_.Id)] $status $($_.Title): $($_.Content)" -ForegroundColor Cyan 
    }
} catch {
    Write-Host "✗ Failed to test PostIt endpoints: $($_.Exception.Message)" -ForegroundColor Red
}

# Test TopLab endpoints
Write-Host "`nTesting /toplab endpoints..." -ForegroundColor Yellow
try {
    $toplabs = Invoke-RestMethod -Uri "$baseUrl/toplab" -Method Get
    Write-Host "✓ GET /toplab - Found $($toplabs.Count) TopLab entries:" -ForegroundColor Green
    $toplabs | ForEach-Object { 
        Write-Host "  - [$($_.Id)] Priority $($_.Priority): $($_.Name) [$($_.Category)]" -ForegroundColor Cyan 
    }
    
    $categories = Invoke-RestMethod -Uri "$baseUrl/toplab/categories" -Method Get
    Write-Host "✓ GET /toplab/categories - Found categories:" -ForegroundColor Green
    $categories | ForEach-Object { Write-Host "  - $_" -ForegroundColor Cyan }
} catch {
    Write-Host "✗ Failed to test TopLab endpoints: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`nAPI Testing Complete!" -ForegroundColor Green
Write-Host "You can also view the OpenAPI documentation at: $baseUrl/openapi/v1.json" -ForegroundColor Magenta
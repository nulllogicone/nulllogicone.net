# RDF Content Negotiation Test Script
# Test the Stamm endpoints with different Accept headers to verify RDF support

Write-Host "Testing Nulllogicone API RDF Content Negotiation..." -ForegroundColor Green
$baseUrl = "http://localhost:5131"

# Test if API is running
Write-Host "`nTesting if API is running..." -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "$baseUrl/about" -Method Get
    Write-Host "✓ API is running: $($response.Name) v$($response.Version)" -ForegroundColor Green
} catch {
    Write-Host "✗ API is not running. Please start the API first." -ForegroundColor Red
    exit 1
}

# Test regular JSON response
Write-Host "`nTesting regular JSON response..." -ForegroundColor Yellow
try {
    $headers = @{ "Accept" = "application/json" }
    $stamms = Invoke-RestMethod -Uri "$baseUrl/stamm" -Method Get -Headers $headers
    Write-Host "✓ JSON Response - Found $($stamms.Count) Stamm entries" -ForegroundColor Green
    if ($stamms.Count -gt 0) {
        Write-Host "  First entry: [$($stamms[0].Id)] $($stamms[0].Name)" -ForegroundColor Cyan
    }
} catch {
    Write-Host "✗ Failed to get JSON response: $($_.Exception.Message)" -ForegroundColor Red
}

# Test RDF/XML response
Write-Host "`nTesting RDF/XML response..." -ForegroundColor Yellow
try {
    $headers = @{ "Accept" = "application/rdf+xml" }
    $response = Invoke-WebRequest -Uri "$baseUrl/stamm" -Method Get -Headers $headers
    Write-Host "✓ RDF/XML Response (Content-Type: $($response.Headers['Content-Type'])):" -ForegroundColor Green
    Write-Host $response.Content -ForegroundColor Cyan
} catch {
    Write-Host "✗ Failed to get RDF/XML response: $($_.Exception.Message)" -ForegroundColor Red
}

# Test Turtle response
Write-Host "`nTesting Turtle response..." -ForegroundColor Yellow
try {
    $headers = @{ "Accept" = "text/turtle" }
    $response = Invoke-WebRequest -Uri "$baseUrl/stamm" -Method Get -Headers $headers
    Write-Host "✓ Turtle Response (Content-Type: $($response.Headers['Content-Type'])):" -ForegroundColor Green
    Write-Host $response.Content -ForegroundColor Cyan
} catch {
    Write-Host "✗ Failed to get Turtle response: $($_.Exception.Message)" -ForegroundColor Red
}

# Test JSON-LD response
Write-Host "`nTesting JSON-LD response..." -ForegroundColor Yellow
try {
    $headers = @{ "Accept" = "application/ld+json" }
    $response = Invoke-WebRequest -Uri "$baseUrl/stamm" -Method Get -Headers $headers
    Write-Host "✓ JSON-LD Response (Content-Type: $($response.Headers['Content-Type'])):" -ForegroundColor Green
    Write-Host $response.Content -ForegroundColor Cyan
} catch {
    Write-Host "✗ Failed to get JSON-LD response: $($_.Exception.Message)" -ForegroundColor Red
}

# Test N-Triples response
Write-Host "`nTesting N-Triples response..." -ForegroundColor Yellow
try {
    $headers = @{ "Accept" = "application/n-triples" }
    $response = Invoke-WebRequest -Uri "$baseUrl/stamm" -Method Get -Headers $headers
    Write-Host "✓ N-Triples Response (Content-Type: $($response.Headers['Content-Type'])):" -ForegroundColor Green
    Write-Host $response.Content -ForegroundColor Cyan
} catch {
    Write-Host "✗ Failed to get N-Triples response: $($_.Exception.Message)" -ForegroundColor Red
}

# Test single Stamm entry with RDF
Write-Host "`nTesting single Stamm entry with RDF/XML..." -ForegroundColor Yellow
try {
    # First get a JSON response to find an ID
    $stamms = Invoke-RestMethod -Uri "$baseUrl/stamm" -Method Get
    if ($stamms.Count -gt 0) {
        $firstId = $stamms[0].Id
        $headers = @{ "Accept" = "application/rdf+xml" }
        $response = Invoke-WebRequest -Uri "$baseUrl/stamm/$firstId" -Method Get -Headers $headers
        Write-Host "✓ Single Stamm RDF/XML Response for ID $firstId" -ForegroundColor Green
        Write-Host $response.Content -ForegroundColor Cyan
    } else {
        Write-Host "⚠ No Stamm entries available to test single entry endpoint" -ForegroundColor Yellow
    }
} catch {
    Write-Host "✗ Failed to get single Stamm RDF response: $($_.Exception.Message)" -ForegroundColor Red
}

# Test content type preference order
Write-Host "`nTesting content type preference (multiple Accept types)..." -ForegroundColor Yellow
try {
    $headers = @{ "Accept" = "application/rdf+xml, text/turtle, application/json" }
    $response = Invoke-WebRequest -Uri "$baseUrl/stamm" -Method Get -Headers $headers
    Write-Host "✓ Multiple Accept Types Response (should prefer RDF/XML):" -ForegroundColor Green
    Write-Host "Content-Type: $($response.Headers['Content-Type'])" -ForegroundColor Magenta
    Write-Host "First 200 chars: $($response.Content.Substring(0, [Math]::Min(200, $response.Content.Length)))..." -ForegroundColor Cyan
} catch {
    Write-Host "✗ Failed to test multiple accept types: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`nRDF Content Negotiation Testing Complete!" -ForegroundColor Green
Write-Host "Summary of supported RDF formats:" -ForegroundColor Magenta
Write-Host "  - application/rdf+xml (RDF/XML)" -ForegroundColor Cyan
Write-Host "  - text/turtle (Turtle)" -ForegroundColor Cyan
Write-Host "  - application/ld+json (JSON-LD)" -ForegroundColor Cyan
Write-Host "  - application/n-triples (N-Triples)" -ForegroundColor Cyan
Write-Host "  - application/json (Default JSON)" -ForegroundColor Cyan

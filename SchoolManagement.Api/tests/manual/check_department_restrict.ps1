# Non-destructive test for Department delete Restrict behavior
# Usage: PowerShell -ExecutionPolicy Bypass -File .\check_department_restrict.ps1 -BaseUrl http://localhost:5048
param(
    [string]$BaseUrl = "http://localhost:5048"
)

Write-Host "BaseUrl: $BaseUrl"

# create a department
$deptName = "Dept-Restrict-Test-$(Get-Date -Format yyyyMMddHHmmss)"
$deptPayload = @{ name = $deptName } | ConvertTo-Json
Write-Host "Creating department: $deptName"
try {
    $resp = Invoke-RestMethod -Uri "$BaseUrl/api/departments" -Method Post -Body $deptPayload -ContentType "application/json"
    $deptId = $resp.data.id
    Write-Host "Created Department Id: $deptId"
} catch {
    Write-Error "Failed to create department: $_"
    exit 1
}

# create a student referencing that department
$studentPayload = @{ name = "Test Student $deptId"; email = "test+$deptId@example.com"; departmentId = $deptId } | ConvertTo-Json
Write-Host "Creating student referencing department $deptId"
try {
    $sresp = Invoke-RestMethod -Uri "$BaseUrl/api/students" -Method Post -Body $studentPayload -ContentType "application/json"
    $studentId = $sresp.data.id
    Write-Host "Created Student Id: $studentId"
} catch {
    Write-Error "Failed to create student: $_"
    exit 1
}

# attempt to delete department
Write-Host "Attempting to delete department $deptId (should fail with 409)"
try {
    $del = Invoke-WebRequest -Uri "$BaseUrl/api/departments/$deptId" -Method Delete -ContentType "application/json" -ErrorAction Stop
    Write-Host "Unexpected success deleting department (status $($del.StatusCode))"
} catch {
    # Extract response body
    if ($_.Exception.Response -ne $null) {
        $res = $_.Exception.Response
        $reader = New-Object System.IO.StreamReader($res.GetResponseStream())
        $body = $reader.ReadToEnd() | ConvertFrom-Json -ErrorAction SilentlyContinue
        Write-Host "Expected failure. Status: $($res.StatusCode)"
        Write-Host "Response body:`n$($body | ConvertTo-Json -Depth 5)"
    } else {
        Write-Error "Delete request failed: $_"
    }
}

# Verify department still exists
Write-Host "Verifying department still exists"
try {
    $g = Invoke-RestMethod -Uri "$BaseUrl/api/departments/$deptId" -Method Get
    Write-Host "Department exists: $($g.data.name) (id $($g.data.id))"
} catch {
    Write-Error "Department not found after attempted delete - unexpected: $_"
}

# Verify student still references the department
Write-Host "Verifying student still references department"
try {
    $sg = Invoke-RestMethod -Uri "$BaseUrl/api/students/$studentId" -Method Get
    Write-Host "Student departmentId: $($sg.data.departmentId)"
} catch {
    Write-Error "Failed to GET student: $_"
}

Write-Host "Test complete. The script did not perform any destructive cleanup."
# Extract icon from Alice.exe and save as PNG

$exePath = "C:\Program Files\Alice\Alice.exe"
$outputDir = Join-Path $PSScriptRoot "Images"

if (-not (Test-Path $exePath)) {
    Write-Host "Error: Alice.exe not found at $exePath" -ForegroundColor Red
    exit 1
}

# Create Images directory if it doesn't exist
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
}

Write-Host "Extracting icon from Alice.exe..." -ForegroundColor Cyan

# Load System.Drawing assembly
Add-Type -AssemblyName System.Drawing

try {
    # Extract icon from executable
    $icon = [System.Drawing.Icon]::ExtractAssociatedIcon($exePath)
    
    if ($icon -eq $null) {
        Write-Host "Error: Could not extract icon from Alice.exe" -ForegroundColor Red
        exit 1
    }
    
    # Convert icon to bitmap
    $bitmap = $icon.ToBitmap()
    
    # Save as PNG (light version)
    $lightPath = Join-Path $outputDir "HeyAlice.light.png"
    $bitmap.Save($lightPath, [System.Drawing.Imaging.ImageFormat]::Png)
    
    # Create a slightly darker version for dark mode
    # We'll create a copy and adjust brightness
    $darkBitmap = New-Object System.Drawing.Bitmap($bitmap)
    
    # Apply a slight darkening effect for dark mode
    for ($x = 0; $x -lt $darkBitmap.Width; $x++) {
        for ($y = 0; $y -lt $darkBitmap.Height; $y++) {
            $pixel = $darkBitmap.GetPixel($x, $y)
            if ($pixel.A -gt 0) {  # Only process non-transparent pixels
                # Slightly darken the pixel (multiply by 0.85)
                $r = [Math]::Min(255, [int]($pixel.R * 0.85))
                $g = [Math]::Min(255, [int]($pixel.G * 0.85))
                $b = [Math]::Min(255, [int]($pixel.B * 0.85))
                $darkPixel = [System.Drawing.Color]::FromArgb($pixel.A, $r, $g, $b)
                $darkBitmap.SetPixel($x, $y, $darkPixel)
            }
        }
    }
    
    $darkPath = Join-Path $outputDir "HeyAlice.dark.png"
    $darkBitmap.Save($darkPath, [System.Drawing.Imaging.ImageFormat]::Png)
    
    # Clean up dark bitmap
    $darkBitmap.Dispose()
    
    Write-Host "Icon extracted successfully!" -ForegroundColor Green
    Write-Host "  Light icon: $lightPath" -ForegroundColor Gray
    Write-Host "  Dark icon: $darkPath" -ForegroundColor Gray
    
    # Clean up
    $icon.Dispose()
    $bitmap.Dispose()
}
catch {
    Write-Host "Error extracting icon: $_" -ForegroundColor Red
    exit 1
}

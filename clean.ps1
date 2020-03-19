#!/usr/bin/env pwsh

# Generate image names using the data in the "component.json" file
$component = Get-Content -Path "component.json" | ConvertFrom-Json
$buildImage="$($component.registry)/$($component.name):$($component.version)-build"
$testImage="$($component.registry)/$($component.name):$($component.version)-test"

# Clean up build directories
if (Test-Path "obj") {
    Remove-Item -Recurse -Force -Path "obj"
}
if (Test-Path "dist") {
    Remove-Item -Recurse -Force -Path "dist"
}

Get-ChildItem -Path "." -Include "obj" -Recurse | foreach($_) { Remove-Item -Force -Recurse $_.FullName }
Get-ChildItem -Path "." -Include "bin" -Recurse | foreach($_) { Remove-Item -Force -Recurse $_.FullName }
Remove-Item -Force -Recurse -Path "*.nupkg"

# Remove docker images
docker rmi $buildImage --force
docker rmi $testImage --force
docker image prune --force

# Remove existed containers
docker ps -a | Select-String -Pattern "Exit" | foreach($_) { docker rm $_.ToString().Split(" ")[0] }

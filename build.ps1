#!/usr/bin/env pwsh

Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

# Generate names for the image and container using the data in the "component.json" file
$component = Get-Content -Path "component.json" | ConvertFrom-Json
$image="$($component.registry)/$($component.name):$($component.version)-build"
$container=$component.name

# Remove build files
if (Test-Path "obj") {
    Remove-Item -Recurse -Force -Path "obj"
}

# Build docker image
docker build -f docker/Dockerfile.build -t $image .

# Create and copy compiled files, then destroy the container
docker create --name $container $image
docker cp "$($container):/obj" ./obj
docker cp "$($container):/dist" ./dist
docker rm $container
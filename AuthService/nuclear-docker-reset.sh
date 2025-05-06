#!/bin/bash

echo "💣 Stopping and removing containers, volumes, and orphans..."
docker compose down -v --remove-orphans

echo "🧹 Pruning Docker volumes..."
docker volume prune -f

echo "🗑️ Removing SQL Server image to force fresh pull..."
docker rmi mcr.microsoft.com/azure-sql-edge:latest || echo "⚠️ SQL Server image not found or already removed."

echo "🧨 Deleting EF Core migrations folder..."
rm -rf AuthService/Migrations

echo "🛠️ Regenerating fresh EF Core migration..."
dotnet ef migrations add InitialCreate --project AuthService --startup-project AuthService

echo "🔨 Rebuilding Docker images from scratch..."
docker compose build --no-cache

echo "🚀 Starting up containers with forced volume recreation..."
docker compose up -d --force-recreate --renew-anon-volumes

echo "✅ Done! Your containers are rebuilt from a clean slate."

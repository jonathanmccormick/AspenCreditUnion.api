#!/bin/bash
set -e

# Add .NET tools to PATH
export PATH="$PATH:/root/.dotnet/tools"

# Wait a bit to ensure everything is ready
echo "Running database migrations..."

# We need to use a connection string directly since we're running from published files
cd /app
dotnet api.dll --apply-migrations

# Start the application
echo "Starting the application..."
exec dotnet api.dll
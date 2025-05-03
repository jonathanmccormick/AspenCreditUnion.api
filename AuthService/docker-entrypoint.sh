#!/bin/bash
set -e

# If no arguments are provided, run the web application
if [ $# -eq 0 ]; then
    echo "Starting web application..."
    exec dotnet AuthService.dll
fi

# Apply migrations if the command includes --apply-migrations
if [[ "$*" == *--apply-migrations* ]]; then
    echo "Applying database migrations..."
    dotnet "AuthService.dll" --apply-migrations
    # Don't exit here if you want to run the app after migrations
    dotnet "AuthService.dll"
fi

# If command starts with dotnet or other specific command, execute it
exec "$@"
#!/bin/bash
set -e

# If the command starts with dotnet, proceed with the default command
if [ "${1#dotnet}" != "$1" ]; then
    exec "$@"
fi

# Apply migrations if the command includes --apply-migrations
if [[ "$*" == *--apply-migrations* ]]; then
    echo "Applying database migrations..."
    dotnet "AuthService.dll" --apply-migrations
    exit 0
fi

# Otherwise, run the command
exec "$@"
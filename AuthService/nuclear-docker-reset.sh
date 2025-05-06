docker compose down -v
docker volume prune -f
docker compose build --no-cache
docker compose up --force-recreate

build:
	dotnet build
clean:
	dotnet clean
restore:
	dotnet restore
watch:
	dotnet watch --project ./src/RabbitMQ_Lab/RabbitMQ_Lab.csproj
run:
	dotnet run --project ./src/RabbitMQ_Lab/RabbitMQ_Lab.csproj
test:
	dotnet test
run-docker:
	docker compose up --build
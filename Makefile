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

## docker run -d --name rabbitmq-service -p 15672:15672 -p 5672:5672 rabbitmq:3-management
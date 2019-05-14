# PartsUnlimited

An example of an IoT data aggreagor and a set of historians with distributed service APIs.

Running the Temperature Historian in a Container

    The existing tools for debugging ASP.NET Core applications in Docker containers with Visual Studio Core does not yet support .NET Core 1.1. We will debug the code standalone, and build the container as a separate step.

    Open the Terminal pane on Visual Studio Code and publish the code

     dotnet restore && dotnet publish -o ./publish

    Create the Dockerfile as:

     FROM mcr.microsoft.com/dotnet/core/sdk:2.2
     WORKDIR /app
     EXPOSE 5000
     COPY ./publish .
     ENTRYPOINT ["dotnet", "Historian.dll"]

    Build the Docker image “docker build -t lab2/temperaturehistorian:1.0 .”

    Create a new container from the image and run it “docker run --rm -it -p 8181:5000 lab2/temperaturehistorian:1.0”

    Navigate to http://localhost:8181/swagger/ and verify the container works

    Terminate the container with Ctrl-C


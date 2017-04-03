
FROM microsoft/dotnet
LABEL Name=pc.core.auth Version=0.0.1 
WORKDIR /app
COPY ./PC.Core.Auth.WebApi .
#ENV ASPNETCORE_URLS http://+:80
EXPOSE 80
RUN dotnet restore
ENTRYPOINT ["dotnet", "run"]
